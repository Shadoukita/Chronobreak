using System;
using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Enums;
using GameServerCore.NetInfo;
using Chronobreak.GameServer.Scripting.CSharp;
using LeaguePackets.Game.Events;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Chronobreak.GameServer.Inventory;
using Chronobreak.GameServer.Logging;
using log4net;
using GameServerLib.GameObjects;
using MoonSharp.Interpreter;
using Chronobreak.GameServer.Scripting.Lua;
using GameServerLib.Managers;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;
using static GameServerCore.Content.HashFunctions;
using System.Linq;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

public class Champion : ObjAIBase, IExperienceOwner
{

    private static ILog _logger = LoggerProvider.GetLogger();
    /// <summary>
    /// Player number ordered by the config file.
    /// </summary>
    internal ClientInfo ClientInfo { get; private set; }
    internal int ClientId => ClientInfo.ClientId;
    public RuneInventory RuneList { get; }
    public TalentInventory TalentInventory { get; set; }
    public ChampionStats ChampionStats { get; protected set; }
    public ChampionStatistics ChampionStatistics { get; private set; }
    public Experience Experience { get; init; }
    public override bool SpawnShouldBeHidden => false;
    public override bool HasSkins => true;

    public List<EventHistoryEntry> EventHistory { get; } = [];

    public Table BBAvatarVars = LuaScriptEngine.NewTable();

    BehaviourTree BehaviourTree = null;

    private DeathData _lastDeathData;

    public bool InPool { get; internal set; } // Controlled by a SpawnPoint.
    public bool ShopForceEnabled;
    public bool ShopEnabled => InPool || ShopForceEnabled;

    public Champion(string model,
                    ClientInfo clientInfo,
                    uint netId = 0,
                    TeamId team = TeamId.TEAM_ORDER,
                    Stats? stats = null,
                    ChampionStats? championStats = null)
        : base(model, clientInfo.Name, new Vector2(), 1200, clientInfo.SkinNo, netId, team, stats)
    {
        //TODO: Champion.ClientInfo?
        ClientInfo = clientInfo;

        RuneList = clientInfo.Runes;
        TalentInventory = clientInfo.Talents;
        ChampionStats = championStats is not null ? championStats : new();

        ChampionStatistics = new()
        {
            TeamId = (int)Team
        };

        Experience = new(this);
        float amount = GlobalData.ChampionVariables.AmbientGoldAmount;
        float interval = GlobalData.ChampionVariables.AmbientGoldInterval;
        Stats.GoldPerSecond.BaseValue = amount / interval;
        //TODO: automaticaly rise spell levels with CharData.SpellLevelsUp

        Spells[(int)SpellSlotType.SummonerSpellSlots] = new Spell(this, clientInfo.SummonerSkills[0], (int)SpellSlotType.SummonerSpellSlots);
        Spells[(int)SpellSlotType.SummonerSpellSlots].LevelUp();
        Spells[(int)SpellSlotType.SummonerSpellSlots + 1] = new Spell(this, clientInfo.SummonerSkills[1], (int)SpellSlotType.SummonerSpellSlots + 1);
        Spells[(int)SpellSlotType.SummonerSpellSlots + 1].LevelUp();

        Replication = new ReplicationHero(this);

        if (clientInfo.PlayerId == -1)
        {
            IsBot = true;
            LoadBehaviourTree();
        }
    }

    internal void LoadBehaviourTree()
    {
        BehaviourTree = Game.ScriptEngine.CreateObject<BehaviourTree>($"BehaviourTrees.Map{Game.Map.Id}", Model) ?? new BehaviourTree();
        BehaviourTree.Owner = this;
    }

    internal override void OnAdded()
    {
        Game.ObjectManager.AddChampion(this);
        base.OnAdded();
        TalentInventory.Activate(this);

        ItemData? bluePill = ContentManager.GetItemData(Game.Map.GameMode.MapScriptMetadata.RecallSpellItemId);
        if (bluePill is not null)
        {
            if (!string.IsNullOrEmpty(bluePill.SpellName))
            {
                Spells[(int)SpellSlotType.BluePillSlot] = new Spell(this, bluePill.SpellName, (int)SpellSlotType.BluePillSlot);
                Stats.SetSpellEnabled((byte)SpellSlotType.BluePillSlot, true);
            }
            ItemInventory.SetExtraItem(7, bluePill);
        }

        // Runes
        byte runeItemSlot = 14;
        foreach (var rune in RuneList.Runes)
        {
            var runeItem = ContentManager.GetItemData((int)rune.Value);
            var newRune = ItemInventory.SetExtraItem(runeItemSlot, runeItem);
            AddStatModifier(runeItem);
            runeItemSlot++;
        }
        Stats.SetSummonerSpellEnabled(0, true);
        Stats.SetSummonerSpellEnabled(1, true);

        if (Game.Map.GameMode.MapScriptMetadata.InitialLevel > 1)
        {
            Experience.LevelUp((byte)(Game.Map.GameMode.MapScriptMetadata.InitialLevel - 1));
        }
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        var peerInfo = Game.PlayerManager.GetClientInfoByChampion(this);
        Game.PacketNotifier.NotifyS2C_CreateHero(peerInfo, userId, team, doVision);
        Game.PacketNotifier.NotifyAvatarInfo(peerInfo, userId);

        bool ownChamp = peerInfo.ClientId == userId;
        if (ownChamp)
        {
            // Buy blue pill
            int relativeBluePillSlot = SpellSlotType.BluePillSlot - SpellSlotType.InventorySlots;
            var itemInstance = ItemInventory.GetItem((byte)relativeBluePillSlot);
            Game.PacketNotifier.NotifyBuyItem(this, itemInstance);

            // Set spell levels
            foreach (var spell in Spells)
            {
                if (spell == null)
                {
                    continue;
                }

                if (spell.Level > 0)
                {
                    // NotifyNPC_UpgradeSpellAns has no effect here
                    Game.PacketNotifier.NotifyS2C_SetSpellLevel(userId, NetId, spell.Slot, spell.Level);

                    float currentCD = spell.CurrentCooldown;
                    float totalCD = spell.Cooldown;
                    if (currentCD > 0)
                    {
                        Game.PacketNotifier.NotifyCHAR_SetCooldown(this, spell.Slot, currentCD, totalCD, userId);
                    }
                }
            }
        }
    }

    internal override void OnRemoved()
    {
        base.OnRemoved();
        Game.ObjectManager.RemoveChampion(this);
    }

    public Vector2 GetSpawnPosition()
    {
        return Game.Map.SpawnPoints.TryGetValue(Team, out SpawnPoint? spawnPoint) ? spawnPoint.Position : Game.Map.NavigationGrid.MiddleOfMap;
    }

    internal override Spell LevelUpSpell(byte slot)
    {
        if (Experience.SpellTrainingPoints.TrainingPoints == 0)
        {
            return null;
        }

        var spell = base.LevelUpSpell(slot);
        if (spell != null)
        {
            Experience.SpellTrainingPoints.SpendTrainingPoint();

            Game.PacketNotifier.NotifyNPC_UpgradeSpellAns(ClientId, NetId, slot, spell.Level, (byte)Experience.SpellTrainingPoints.TrainingPoints);

            if (spell.Level == 1)
            {
                Spells[slot].Sealed = false;
            }
        }

        return spell;
    }

    private float _EXPTimer;
    private float _goldTimer;
    internal override void Update()
    {
        base.Update();

        BehaviourTree?.Update();

        if (ChampionStats.IsGeneratingGold)
        {
            _goldTimer -= Game.Time.DeltaTime;
            if (_goldTimer <= 0)
            {
                float interval = GlobalData.ChampionVariables.AmbientGoldInterval;
                GoldOwner.AddGold(Stats.GoldPerSecond.Total * interval, false);
                _goldTimer = interval;
            }
        }
        else if (Game.Time.GameTime >= GlobalData.ObjAIBaseVariables.AmbientGoldDelay)
        {
            ChampionStats.IsGeneratingGold = true;
        }

        if (GlobalData.ChampionVariables.AmbientXPAmount > 0)
        {
            _EXPTimer -= Game.Time.DeltaTime;
            if (_EXPTimer <= 0)
            {
                Experience.AddEXP(GlobalData.ChampionVariables.AmbientXPAmount, false);
                _EXPTimer = GlobalData.ChampionVariables.AmbientXPInterval;
            }
        }

        if (Stats.IsDead && Stats.RespawnTimer >= 0)
        {
            Stats.RespawnTimer -= Game.Time.DeltaTime;
            if (Stats.RespawnTimer <= 0)
            {
                Respawn();
            }
        }

        // TODO: Find out the best way to bulk send these for all champions (tool tip handler?).
        // League sends a single packet detailing every champion's tool tip changes.
        //TODO: What about buffs on non-champions that can also have tooltips?
        List<ToolTipData> _tipsChanged = [];
        foreach (var spell in Spells)
        {
            if (spell?.ToolTipData.Changed == true)
            {
                _tipsChanged.Add(spell.ToolTipData);
                spell.ToolTipData.MarkAsUnchanged();
            }
        }
        //TODO: Either spawn scripts instead of buffs on stack, or have ToolTipData in the slot
        foreach (var buff in Buffs.All())
        {
            if (buff.ToolTipData.Changed)
            {
                _tipsChanged.Add(buff.ToolTipData);
                buff.ToolTipData.MarkAsUnchanged();
            }
        }
        if (_tipsChanged.Count > 0)
        {
            Game.PacketNotifier.NotifyS2C_ToolTipVars(_tipsChanged);
        }
    }

    public void Respawn()
    {
        var spawnPos = GetSpawnPosition();
        SetPosition(spawnPos);

        float parToRestore = 0;
        // TODO: Find a better way to do this, perhaps through scripts. Otherwise, make sure all types are accounted for.
        if (Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.MANA || Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.Energy || Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.Wind)
        {
            parToRestore = Stats.ManaPoints.Total;
        }
        Stats.CurrentMana = parToRestore;
        Stats.CurrentHealth = Stats.HealthPoints.Total;
        Stats.IsDead = false;
        Stats.IsZombie = false;
        PopAllCharacterData();
        SetStatus(
            StatusFlags.CanAttack | StatusFlags.CanCast |
            StatusFlags.CanMove | StatusFlags.CanMoveEver |
            StatusFlags.Targetable, true
        );
        Stats.RespawnTimer = -1;

        SetDashingState(false, MoveStopReason.HeroReincarnate);
        ResetWaypoints();

        Game.PacketNotifier.NotifyHeroReincarnateAlive(this, parToRestore);
        ApiEventManager.OnResurrect.Publish(this);
    }

    internal bool OnDisconnect()
    {
        ItemInventory.ClearUndoHistory();
        ApiEventManager.OnDisconnect.Publish(this);
        StopMovement();
        SetWaypoints(Game.Map.PathingHandler.GetPath(Position, GetSpawnPosition(), PathfindingRadius));
        UpdateMoveOrder(OrderType.MoveTo, true);
        return true;
    }

    internal void OnKill(DeathData deathData)
    {
        ApiEventManager.OnKill.Publish(deathData.Killer, deathData);

        if (deathData.Unit is Minion)
        {
            ChampionStatistics.MinionsKilled++;
            if (deathData.Unit.Team == TeamId.TEAM_NEUTRAL)
            {
                ChampionStatistics.NeutralMinionsKilled++;
            }

            var gold = deathData.Unit.Stats.GoldGivenOnDeath.Total;
            if (gold <= 0)
            {
                return;
            }

            GoldOwner.AddGold(gold, true, deathData.Unit);

            if (ChampionStats.DeathSpree > 0)
            {
                ChampionStats.GoldFromMinions += gold;
            }

            if (ChampionStats.GoldFromMinions >= 1000)
            {
                ChampionStats.GoldFromMinions -= 1000;
                ChampionStats.DeathSpree--;
            }
        }
    }

    internal float GetRespawnTime()
    {
        return Game.Map.MapData.DeathTimes.ElementAtOrDefault(Experience.Level);
    }

    public void ForceDead()
    {
        Stats.IsZombie = false;
        _lastDeathData.BecomeZombie = false;
        DieInternal();
        Game.PacketNotifier.NotifyNPC_ForceDead(_lastDeathData);
    }

    public override void Die(DeathData data)
    {
        if (Stats.IsZombie)
        {
            ForceDead();
            return;
        }

        ApiEventManager.OnDeath.Publish(data.Unit, data);
        Stats.RespawnTimer = (data.DeathDuration = GetRespawnTime()) * 1000.0f;
        ChampionStatistics.Deaths++;
        Stats.IsZombie = data.BecomeZombie;

        if (data.BecomeZombie)
        {
            ApiEventManager.OnZombie.Publish(this, data);
        }

        //TODO: Check this
        if (data.Killer is not Champion)
        {
            Champion? cKiller = EnemyAssistMarkers.Find(x => x.Source is Champion)?.Source as Champion;
            if (cKiller is not null)
            {
                data.Killer = cKiller;
            }
        }

        if (data.Killer is Champion c)
        {
            ChampionDeathManager.ProcessKill(data);
            c.OnKill(data);
            //Publish OnChampionKill (?)
        }

        Game.PacketNotifier.NotifyNPC_Hero_Die(data);
        DieInternal();
        _lastDeathData = data;
    }

    private void DieInternal()
    {
        Stats.IsDead = true;
        Game.ObjectManager.StopTargeting(this);
        SetDashingState(false, MoveStopReason.Death);
        Buffs.RemoveNotLastingThroughDeath();
        EventHistory.Clear();
    }

    private T CreateEventForHistory<T>(AttackableUnit source, IEventSource sourceScript) where T : ArgsForClient, new()
    {
        if (source is null || sourceScript is null)
        {
            return null;
        }

        T e = new()
        {
            ParentCasterNetID = source.NetId,
            OtherNetID = NetId,
            ScriptNameHash = 1,
            ParentScriptNameHash = sourceScript.ScriptNameHash,
            EventSource = 0, // ?
            Unknown = 0, // ?
            SourceObjectNetID = 0,
            Bitfield = 0 // ?
        };

        if (sourceScript.ParentScript != null)
        {
            e.ScriptNameHash = sourceScript.ScriptNameHash;
            e.ParentScriptNameHash = sourceScript.ParentScript.ScriptNameHash;
        }
        else if (sourceScript is Buff b && b.OriginSpell != null)
        {
            e.ScriptNameHash = sourceScript.ScriptNameHash;
            e.ParentScriptNameHash = HashString(b.OriginSpell.Name);
        }

        EventHistoryEntry entry = new()
        {
            Timestamp = Game.Time.GameTime / 1000f, // ?
            Count = 1, //TODO: stack?
            Source = source.NetId,
            Event = (IEvent)e
        };

        EventHistory.Add(entry);
        return e;
    }

    /*
    //TODO:
    public override Buff AddBuff(
        string buffName, float duration, int stacks,
        Spell originspell,
        AttackableUnit onto, ObjAIBase from,
        IEventSource parent = null, IBuffScript script = null
    )
    {
        var buff = base.AddBuff(buffName, duration, stacks, originspell, onto, from, parent, script);
        if (buff != null)
        {
            CreateEventForHistory<OnBuff>(buff.SourceUnit, buff);
            return buff;
        }
        return buff;
    }
    */

    public void TintScreen(bool enable, float speed, GameServerCore.Content.Color color)
    {
        Game.PacketNotifier.NotifyTintPlayer(this, enable, speed, color);
    }

    internal override void TakeHeal(HealData healData)
    {
        base.TakeHeal(healData);

        var e = CreateEventForHistory<OnCastHeal>(healData.SourceUnit, healData.SourceScript);
        if (e != null)
        {
            e.HealAmmount = healData.HealAmount;
        }

        if (healData.SourceUnit is Champion ch)
        {
            ch.ChampionStatistics.TotalHeal += (int)healData.HealAmount;
        }
    }

    public override void TakeDamage(DamageData damageData, IEventSource? sourceScript = null)
    {
        base.TakeDamage(damageData, sourceScript);

        if (damageData.Damage <= 0)
        {
            return;
        }

        Champion cAttacker = damageData.Attacker as Champion;
        var e = CreateEventForHistory<OnDamageGiven>(damageData.Attacker, sourceScript);

        switch (damageData.DamageType)
        {
            case DamageType.DAMAGE_TYPE_PHYSICAL:
                ChampionStatistics.PhysicalDamageTaken += damageData.Damage;
                break;
            case DamageType.DAMAGE_TYPE_MAGICAL:
                ChampionStatistics.MagicDamageTaken += damageData.Damage;
                break;
            case DamageType.DAMAGE_TYPE_TRUE:
                ChampionStatistics.TrueDamageTaken += damageData.Damage;
                break;
                //TODO: handle mixed damage?
        }

        if (cAttacker is not null)
        {
            switch (damageData.DamageType)
            {
                case DamageType.DAMAGE_TYPE_PHYSICAL:
                    ChampionStatistics.PhysicalDamageTaken += damageData.Damage;
                    cAttacker.ChampionStatistics.PhysicalDamageDealtToChampions += damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_MAGICAL:
                    ChampionStatistics.MagicDamageTaken += damageData.Damage;
                    cAttacker.ChampionStatistics.MagicDamageDealtToChampions += damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_TRUE:
                    ChampionStatistics.TrueDamageTaken += damageData.Damage;
                    cAttacker.ChampionStatistics.TrueDamageDealtToChampions += damageData.Damage;
                    break;
                    //TODO: handle mixed damage?
            }

            cAttacker.ChampionStatistics.TotalDamageDealtToChampions += damageData.Damage;
        }

        if (e is not null)
        {
            switch (damageData.DamageType)
            {
                case DamageType.DAMAGE_TYPE_PHYSICAL:
                    e.PhysicalDamage = damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_MAGICAL:
                    e.MagicalDamage = damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_TRUE:
                    e.TrueDamage = damageData.Damage;
                    break;
                    //TODO: handle mixed damage?
            }
        }
    }

    public void UpdateSkin(int skinNo)
    {
        SkinID = skinNo;
    }

    public void IncrementScore(float points, ScoreCategory scoreCategory, ScoreEvent scoreEvent, bool doCallOut, bool notifyText = true)
    {
        ChampionStats.Score += points;
        var scoreData = new ScoreData(this, points, scoreCategory, scoreEvent, doCallOut);
        Game.PacketNotifier.NotifyS2C_IncrementPlayerScore(scoreData);

        if (notifyText)
        {
            //TODO: Figure out what "Params" is exactly
            Game.PacketNotifier.NotifyDisplayFloatingText(new FloatingTextData(this, $"+{(int)points} Points", FloatTextType.Score, 1073741833), Team);
        }

        ApiEventManager.OnIncrementChampionScore.Publish(scoreData.Owner, scoreData);
    }

    internal override float GetAttackRatioWhenAttackingTurret()
    {
        return GlobalData.DamageRatios.HeroToBuilding;
    }
    internal override float GetAttackRatioWhenAttackingMinion()
    {
        return GlobalData.DamageRatios.HeroToUnit;
    }
    internal override float GetAttackRatioWhenAttackingChampion()
    {
        return GlobalData.DamageRatios.HeroToHero;
    }
    internal override float GetAttackRatioWhenAttackingBuilding()
    {
        return GlobalData.DamageRatios.HeroToBuilding;
    }
    internal override float GetAttackRatio(AttackableUnit attackingUnit)
    {
        return attackingUnit.GetAttackRatioWhenAttackingChampion();
    }

    public static int GetAverageChampionLevel()
    {
        float average = 0;
        var players = Game.PlayerManager.GetPlayers(true);
        foreach (var player in players)
        {
            average += player.Champion.Experience.Level / players.Count;
        }
        return (int)Math.Floor(average);
    }

    protected override void OnUpdateStats()
    {
        base.OnUpdateStats();
        foreach (var talent in TalentInventory.Talents)
        {
            talent.Value.Script.OnUpdateStats();
        }
    }
}
