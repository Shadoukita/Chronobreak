using System;
using System.Numerics;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.Scripting.CSharp;
using GameServerCore;
using GameServerCore.Content;
using GameServerCore.Enums;
using E = GameServerCore.Extensions;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.Logging;
using log4net;
using FCS = Chronobreak.GameServer.Scripting.CSharp.Converted.Functions_CS;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;
using GameServerLib.Managers;
using GameServerCore.NetInfo;
using GameServerLib.Scripting.Lua.Scripts;
using GameServerLib.Content;
using GameServerLib.GameObjects;
using static GameServerCore.Content.HashFunctions;

namespace Chronobreak.GameServer.Scripting.Lua;

public static partial class Functions
{
    private static readonly ILog _logger = LoggerProvider.GetLogger();
    private static readonly Random random = new();
    private const float DEFAULT_CAMP_DELAY_SPAWN_TIME = 100.0f; //Unhardcode/Figure out where this comes from?

    //In League this is a static array of size 64;
    internal static readonly List<NeutralTimer> NeutralTimers = new(64);
    //In League this is a static array of size 64;
    internal static readonly List<AiTimer> LevelTimers = new(64);

    private static AttackableUnit? GetUnitOrNull(AttackableUnit? unit = null) =>
        unit ?? BBScript.CurrentlyExecuted?.Args.ScriptOwner;
    private static AttackableUnit GetUnit(AttackableUnit? unit = null)
    {
        unit = GetUnitOrNull(unit);
        if (unit == null)
        {
            throw new Exception();
        }
        return unit;
    }
    private static Buff? GetBuffOrNull(Buff? buff = null)
    {
        return buff ?? (BBScript.CurrentlyExecuted?.WrapperScript as BBBuffScript)?.Buff;
    }
    private static Buff GetBuff(Buff? buff = null)
    {
        buff = GetBuffOrNull(buff);
        if (buff == null)
        {
            throw new Exception();
        }
        return buff;
    }
    private static Spell? GetSpellOrNull(Spell? spell = null)
    {
        return spell ?? (BBScript.CurrentlyExecuted?.WrapperScript as BBSpellScript)?.Spell;
    }
    private static Spell GetSpell(Spell? spell = null)
    {
        spell = GetSpellOrNull(spell);
        if (spell == null)
        {
            throw new Exception();
        }
        return spell;
    }

    [BBFunc(Dest = "Position")]
    public static Vector3 GetPointByUnitFacingOffset(AttackableUnit unit, float distance, float offsetAngle)
    {
        var unitPos = unit.Position3D;
        var dir = unit.Direction.ToVector2().Normalized();

        offsetAngle %= 360;
        if (offsetAngle > 180)
            offsetAngle -= 360;
        //if(offsetAngle < 0)
        //    offsetAngle += 360;

        return unitPos + (dir.Rotated(offsetAngle) * distance).ToVector3(0);
    }

    [BBFunc]
    public static float DistanceBetweenPoints(Vector3 point1, Vector3 point2)
    {
        return Vector3.Distance(point1, point2);
    }

    public static float GetTime()
    {
        return Game.Time.GameTime / 1000f;
    }

    [BBFunc(Dest = "Seconds")]
    public static float GetGameTime()
    {
        return Game.Time.GameTime / 1000f;
    }

    [BBFunc]
    public static Minion SpawnMinion(
        string name,
        string skin,
        string? aiScript,
        Vector3 pos,
        TeamId team,
        bool stunned,
        bool rooted,
        bool silenced,
        bool invulnerable,
        bool magicImmune,
        bool ignoreCollision,
        float visibilitySize,
        bool isWard = false,
        bool placemarker = false,
        Champion? goldRedirectTarget = null //TODO:
    )
    {
        ObjAIBase? owner = goldRedirectTarget; //GetUnit(owner) as ObjAIBase;

        //HACK: In case someone wants to attach particles to a ServerOnly object
        if (
            skin
            is "TestCube"
            or "TestCubeRender10Vision"
            or "TestCubeRenderBasicAttack"
            or "TestCubeRender10VisionBasicAttack"
            or "SpellBook1"
        )
        {
            skin = "TestCubeRender";
        }

        var m = new Minion(
            owner, pos.ToVector2(),
            model: skin, name, team, skinId: 0,
            ignoreCollision,
            isWard: isWard,
            AIScript: aiScript ?? "",
            initialLevel: 1,
            visionRadius: (int)Math.Max(1, visibilitySize)
        );
        if (stunned) m.SetStatus(StatusFlags.Stunned, true);
        if (rooted) m.SetStatus(StatusFlags.Rooted, true);
        if (silenced) m.SetStatus(StatusFlags.Silenced, true);
        if (invulnerable) m.SetStatus(StatusFlags.Invulnerable, true);
        if (magicImmune) m.SetStatus(StatusFlags.MagicImmune, true);
        if (placemarker)
        {
            //TODO: Ghosted, Rooted, etc...
            m.SetStatus(StatusFlags.Targetable, false);
            m.SetStatus(
                  StatusFlags.NoRender
                | StatusFlags.ForceRenderParticles
                | StatusFlags.Ghosted
                | StatusFlags.SuppressCallForHelp
                | StatusFlags.IgnoreCallForHelp
                | StatusFlags.CallForHelpSuppressor
                | StatusFlags.Invulnerable
                , true
            );
        }
        Game.ObjectManager.AddObject(m);
        return m;
    }

    internal static BBBuffScriptCtrArgs? NextBBBuffScriptCtrArgs;
    internal class BBBuffScriptCtrArgs : BuffScriptMetaData
    {
        public Table? BuffVarsTable;
    }

    [BBFunc] //TODO: BB-only and Lua-only versions?
    public static void SpellBuffAdd(
        // Beginning of positional parameters
        ObjAIBase? attacker,
        AttackableUnit target,
        [BBBuffName] string buffName = "",
        int maxStack = 1,
        int numberOfStacks = 1,
        float duration = 25000,

        [BBParam("", null, null, null)]
        Table? buffVarsTable = null,
        // Ending of positional parameters

        BuffAddType buffAddType = BuffAddType.REPLACE_EXISTING,
        BuffType buffType = BuffType.INTERNAL,
        float tickRate = 0, //TODO:
        bool stacksExclusive = false, //TODO:
        bool canMitigateDuration = false, //TODO:
        bool isHiddenOnClient = false
    )
    {
        Spell? spell = GetSpellOrNull(/*originSpell*/);
        var name = (buffName == "") ?
            BBScript.CurrentlyExecuted?.Args.Name : //TODO: Wrap in function?
            buffName;
        if (name == null)
        {
            throw new ArgumentException();
        }

        /*
        var buff = target.GetBuffWithName(buffName);
        var metadata = buff?.BuffScript.BuffMetaData;
        if (metadata != null)
        {
            if (metadata.BuffAddType != buffAddType)
            {
                _logger.Error($"{name}: BuffAddType {metadata.BuffAddType} != {buffAddType}");
            }
            if (metadata.BuffType != buffType)
            {
                _logger.Error($"{name}: BuffType {metadata.BuffType} != {buffType}");
            }
            if (metadata.MaxStacks != maxStack)
            {
                _logger.Error($"{name}: MaxStacks {metadata.MaxStacks} != {maxStack}");
            }
            if (metadata.TickRate != tickRate)
            {
                _logger.Error($"{name}: TickRate {metadata.TickRate} != {tickRate}");
            }
            if (metadata.StacksExclusive != stacksExclusive)
            {
                _logger.Error($"{name}: StacksExclusive {metadata.StacksExclusive} != {stacksExclusive}");
            }
            if (metadata.CanMitigateDuration != canMitigateDuration)
            {
                _logger.Error($"{name}: CanMitigateDuration {metadata.CanMitigateDuration} != {canMitigateDuration}");
            }
            if (metadata.IsHidden != isHiddenOnClient)
            {
                _logger.Error($"{name}: IsHidden {metadata.IsHidden} != {isHiddenOnClient}");
            }
        }
        */

        var args = new BBBuffScriptCtrArgs()
        {
            BuffAddType = buffAddType,
            BuffType = buffType,
            MaxStacks = maxStack,
            TickRate = tickRate,
            StacksExclusive = stacksExclusive,
            CanMitigateDuration = canMitigateDuration,
            IsHidden = isHiddenOnClient,

            BuffVarsTable = buffVarsTable,
        };

        var prev = NextBBBuffScriptCtrArgs;
        NextBBBuffScriptCtrArgs = args;
        target.Buffs.Add(
            name, duration, numberOfStacks,
            spell,
            target, attacker
        );
        NextBBBuffScriptCtrArgs = prev;
    }

    [BBFunc]
    public static void SpellBuffRemoveCurrent(AttackableUnit target)
    {
        Buff? currentBuff = GetBuff();
        if (currentBuff != null)
        {
            target.Buffs.RemoveStack(currentBuff.Name);
        }
    }

    [BBFunc]
    public static void SpellBuffRemove(
        AttackableUnit target,
        [BBBuffName] string buffName = "",
        ObjAIBase? attacker = null,
        float resetDuration = 0 //TODO:
    )
    {
        //TODO: Check if BB-scripts are trying to remove non-existent buffs
        //TODO: and should not throw an error in this case.
        target.Buffs.RemoveStack(buffName, attacker);
        if (resetDuration > 0)
        {
            target.Buffs.Renew(buffName, attacker, resetDuration);
        }
    }

    [BBFunc]
    public static void PreloadParticle(string name) => _ = ContentManager.GetParticleData(name);

    [BBFunc]
    public static void PreloadSpell([BBSpellName] string name) => _ = ContentManager.GetSpellData(name);

    [BBFunc]
    public static void PreloadCharacter(string name) => _ = ContentManager.GetCharData(name);


    private static int ConvertAPISlot(
        int spellSlot,
        SpellSlotType slotType = SpellSlotType.SpellSlots,
        SpellbookType spellbookType = SpellbookType.SPELLBOOK_CHAMPION
    )
    {
        if (spellbookType == SpellbookType.SPELLBOOK_SUMMONER)
        {
            slotType = SpellSlotType.SummonerSpellSlots;
        }
        int slot = ApiFunctionManager.ConvertAPISlot(spellbookType, slotType, spellSlot);
        if (slot == -1)
        {
            throw new ArgumentException();
        }
        return slot;
    }

    [BBFunc]
    public static void SetSlotSpellCooldownTime(
        ObjAIBase owner,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        int spellSlot,
        SpellbookType spellbookType,
        SpellSlotType slotType = SpellSlotType.SpellSlots,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        float src = 0
    )
    {
        int slot = ConvertAPISlot(spellSlot, slotType, spellbookType);
        owner.Spells[slot]?.SetCooldown(src);
    }

    [BBFunc(Dest = "Position")]
    public static Vector3 GetUnitPosition(AttackableUnit? unit = null)
    {
        return GetUnit(unit).Position3D;
    }

    public static int SpellBuffCount(
        AttackableUnit owner,
        string buffName,
        ObjAIBase? attacker = null
    )
    {
        return owner.Buffs.Count(buffName, attacker);
    }

    [BBFunc]
    public static TeamId GetTeamID_CS(AttackableUnit? target = null)
    {
        return GetUnitOrNull(target)?.Team ?? TeamId.TEAM_UNKNOWN;
    }

    [BBFunc]
    public static int GetTeamID(AttackableUnit? target = null)
    {
        return (int)GetTeamID_CS(target);
    }

    [BBFunc]
    public static void Move(
        AttackableUnit unit,
        Vector3 target,
        float speed,
        float gravity = 0,
        float moveBackBy = 0, //TODO:
        ForceMovementType movementType = 0, //TODO:
        ForceMovementOrdersType movementOrdersType = 0, //TODO:
        float idealDistance = 0, //TODO:
        ForceMovementOrdersFacing movementOrdersFacing = 0
    )
    {
        unit.DashToLocation(
            target.ToVector2(),
            speed, //TODO: Check if conversions are needed.
            gravity * 0.5f, //TODO: Check constant.
            movementOrdersFacing == ForceMovementOrdersFacing.KEEP_CURRENT_FACING,
            consideredCC: false
        );
    }

    [BBFunc]
    public static void ApplyAssistMarker(
        AttackableUnit source,
        AttackableUnit target,
        float duration
    )
    {
        if (target is not ObjAIBase objTarget || source is not ObjAIBase objSource)
        {
            _logger.Warn("Can't ApplyAssistMarker! (Target or Source are null or not ObjAIBase!)");
            return;
        }

        objTarget.AddAssistMarker(objSource, duration);
    }

    // ngl chief idk what riot was cooking with some of these lua variable & method names cause they are confusing as shit
    // - horns
    //TODO: Use only CInstance method for cleanup (Lua scripts)
    [BBFunc]
    public static void SpellEffectCreate(
        out EffectEmitter effectID,
        out EffectEmitter? effectID2,

        string effectName = "",
        string effectNameForOtherTeam = "", //Not present in the BBLuaConversionLibrary.lua, not sure where it came from unless

        [BBParam("OverrideVar", "OverrideVarTable", "", null)]
        TeamId FOWTeam = TeamId.TEAM_UNKNOWN, // Team that owns this FX(?)
        float FOWVisibilityRadius = 0,

        FXFlags flags = 0,

        TeamId specificTeamOnly = TeamId.TEAM_UNKNOWN,
        TeamId specificTeamOnlyOverride = TeamId.TEAM_UNKNOWN,
        AttackableUnit? specificUnitOnly = null, //TODO: Implement sending FX to one client
        bool useSpecificUnit = false,

        AttackableUnit? bindObject = null,
        string boneName = "",
        Vector3 pos = default,

        AttackableUnit? targetObject = null,
        string targetBoneName = "",
        Vector3 targetPos = default,

        bool sendIfOnScreenOrDiscard = false,//figure out which flag to use
        bool persistsThroughReconnect = false, //TODO: Requires creating Client Connection tracking or is it a FXFlag?
        bool bindFlexToOwnerPAR = false,//figure out which flag to use
        bool followsGroundTilt = false, //figure out which flag to use
        bool facesTarget = false,

        object? orientTowards = null // Vector3 or AttackableUnit
    )
    {
        ObjAIBase? caster = null; //GetUnit(caster) as ObjAIBase;

        //if (sendIfOnScreenOrDiscard) flags |= FXFlags.;
        //if (persistsThroughReconnect) flags |= FXFlags.;
        //if (bindFlexToOwnerPAR) flags |= FXFlags.;
        //if (followsGroundTilt) flags |= FXFlags.;
        if (facesTarget) flags |= FXFlags.TargetDirection;


        //TODO:
        EffectEmitter p1 = new
        (
            effectName ?? "",
            specificUnitOnly,
            targetObject,
            bindObject,
            targetBoneName ?? "",
            boneName ?? "",
            pos.ToVector2(),
            targetPos,
            //particleEnemyName: effectNameForOtherTeam ?? "",
            //followGroundTilt: followsGroundTilt,
            flags: flags,
            team: FOWTeam,
            visibilityRadius: FOWVisibilityRadius
        );
        //TODO: don't add particles in the constructor!
        //Game.ObjectManager.AddObject(p1);

        effectID = p1;
        effectID2 = null;
    }

    [BBFunc]
    public static void SpellEffectRemove(
        EffectEmitter? effectID
    )
    {
        //Game.ObjectManager.RemoveObject(effectID);
        effectID?.SetToRemove();
    }

    [BBFunc]
    public static void ApplyDamage(
        ObjAIBase attacker,
        AttackableUnit target,
        float damage,
        DamageType damageType,
        DamageSource sourceDamageType,
        float percentOfAttack = 0,
        float spellDamageRatio = 0,
        float physicalDamageRatio = 0,
        bool ignoreDamageIncreaseMods = false,
        bool ignoreDamageCrit = false,
        ObjAIBase? callForHelpAttacker = null //TODO:
    )
    {
        float totaldamage = damage;
        //TODO: Use Spell.Data.ApplyAttackDamage
        //bool applyAD = damageType is DamageType.DAMAGE_TYPE_PHYSICAL or DamageType.DAMAGE_TYPE_MIXED;
        //bool applyAP = damageType is DamageType.DAMAGE_TYPE_MAGICAL or DamageType.DAMAGE_TYPE_MIXED;
        bool applyAD = sourceDamageType == DamageSource.DAMAGE_SOURCE_ATTACK;
        bool applyAP = true;
        totaldamage += Convert.ToInt32(applyAD) * attacker.Stats.AttackDamage.TotalBonus * physicalDamageRatio;
        totaldamage += Convert.ToInt32(applyAP) * attacker.Stats.AbilityPower.Total * spellDamageRatio;
        totaldamage *= percentOfAttack;

        target.TakeDamage(
            attacker,
            totaldamage, damageType, sourceDamageType,
            ignoreDamageIncreaseMods,
            ignoreDamageCrit
        );
    }

    [BBFunc]
    public static void SetBuffToolTipVar(
        int index,
        float value
    )
    {
        GetBuff().SetToolTipVar(index - 1, value);
    }

    [BBFunc]
    public static void ForEachUnitInTargetArea(
        ObjAIBase attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        [BBBuffName] string buffNameFilter = "",
        bool inclusiveBuffFilter = false,

        [BBSubBlocksAttribute("Iterator")]
        Action<AttackableUnit>? subBlocks = null
    )
    {
        var units = FCS.GetUnitsInArea(
            attacker, center, range, flags, buffNameFilter, inclusiveBuffFilter
        );
        foreach (var unit in units)
        {
            subBlocks!(unit);
        }
    }

    [BBFunc]
    public static void ForEachUnitInTargetAreaRandom(
        ObjAIBase attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        [BBBuffName] string buffNameFilter = "",
        bool inclusiveBuffFilter = false,

        [BBSubBlocksAttribute("Iterator")]
        Action<AttackableUnit>? subBlocks = null
    )
    {
        var selection = FCS.GetRandomUnitsInArea(
            attacker, center, range, flags, maximumUnitsToPick, buffNameFilter, inclusiveBuffFilter
        );
        foreach (var unit in selection)
        {
            subBlocks!(unit);
        }
    }

    [BBFunc]
    public static int GetNumberOfHeroesOnTeam(
        TeamId team,
        bool connectedFromStart, //TODO:
        bool includeBots = true
    )
    {
        int count = 0; //TODO: Use PlayerManager.PlayerTeamCount
        foreach (var player in Game.PlayerManager.GetPlayers(includeBots))
        {
            count += Convert.ToInt32(player.Team == team);
        }
        return count;
    }

    [BBFunc]
    public static void SpellBuffClear(
        AttackableUnit target,
        [BBBuffName] string buffName
    )
    {
        target.Buffs.RemoveAllStacks(buffName);
    }

    [BBFunc]
    public static void MoveAway(
        AttackableUnit unit,
        Vector3 awayFrom,
        float speed,
        float gravity,
        float distance,
        float distanceInner,
        ForceMovementType movementType,
        ForceMovementOrdersType movementOrdersType,
        float idealDistance,
        ForceMovementOrdersFacing movementOrdersFacing = 0
    )
    {
        Vector3 unitPos = unit.Position3D;
        Vector3 target = unitPos + (unitPos - awayFrom).Normalized() * (distance - distanceInner);
        Move(unit, target, speed, gravity, 0, movementType, movementOrdersType, idealDistance, movementOrdersFacing);
    }

    [BBFunc(Dest = "Result")]
    public static Vector3 GetRandomPointInAreaUnit(
        AttackableUnit target,
        float radius,
        float innerRadius
    )
    {
        return GetRandomPointInAreaPosition(target.Position3D, radius, innerRadius);
    }

    [BBFunc(Dest = "BubbleID")]
    public static Region AddPosPerceptionBubble(
        TeamId team,
        float radius,
        Vector3 pos,
        float duration,
        AttackableUnit? specificUnitsClientOnly,
        bool revealSteath
    )
    {
        return new Region(
            team, pos.ToVector2(),
            visionTarget: specificUnitsClientOnly,
            visionRadius: radius,
            revealStealth: revealSteath,
            lifetime: duration
        );
    }

    [BBFunc]
    public static void FaceDirection(
        AttackableUnit target,
        Vector3 location
    )
    {
        var targetPosition = target.Position3D;
        if (location != targetPosition)
        {
            var direction = (location - targetPosition).Normalized();
            target.FaceDirection(direction, isInstant: false);
        }
    }

    [BBFunc]
    public static void SetSpell(
        ObjAIBase target,
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType slotBook,
        [BBSpellName] string spellName
    )
    {
        var slot = ConvertAPISlot(slotNumber, slotType, slotBook);
        target.SetSpell(spellName, (byte)slot, true);
    }

    public static string GetSlotSpellName(
        ObjAIBase owner,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        int spellSlot,
        SpellbookType spellbookType = 0,
        SpellSlotType slotType = 0
    )
    {
        int slot = ConvertAPISlot(spellSlot, slotType, spellbookType);
        return owner.Spells[slot].SpellName;
    }
    public static float GetSlotSpellCooldownTime(
        ObjAIBase owner,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        int spellSlot,
        SpellbookType spellbookType = 0,
        SpellSlotType slotType = 0
    )
    {
        int slot = ConvertAPISlot(spellSlot, slotType, spellbookType);
        return owner.Spells[slot].CurrentCooldown;
    }
    public static int GetSlotSpellLevel(
        ObjAIBase owner,
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        int spellSlot,
        SpellbookType spellbookType = 0,
        SpellSlotType slotType = 0
    )
    {
        int slot = ConvertAPISlot(spellSlot, slotType, spellbookType);
        return owner.Spells[slot].Level;
    }

    [BBFunc]
    public static void SpellCast(
        ObjAIBase caster,
        AttackableUnit? target,
        Vector3? pos,
        Vector3? endPos,
        int slotNumber,
        SpellSlotType slotType,
        int overrideForceLevel = 0,
        bool overrideCoolDownCheck = false,
        bool fireWithoutCasting = false,
        bool useAutoAttackSpell = false,
        bool forceCastingOrChannelling = false,
        bool updateAutoAttackTimer = false,
        bool overrideCastPosition = false,
        Vector3 overrideCastPos = default
    )
    {
        int slot = ConvertAPISlot(slotNumber, slotType);
        caster.Spells[slot].TryCast(
            target, pos, endPos,
            overrideForceLevel,
            overrideCoolDownCheck,
            fireWithoutCasting,
            useAutoAttackSpell,
            forceCastingOrChannelling,
            updateAutoAttackTimer,
            overrideCastPosition,
            overrideCastPos
        );
    }

    [BBFunc]
    public static void SealSpellSlot(
        int spellSlot,
        SpellSlotType slotType,
        ObjAIBase target,
        bool state,
        SpellbookType spellbookType = 0 //TODO: Verify
    )
    {
        int slot = ConvertAPISlot(spellSlot, slotType, spellbookType);
        target.Spells[slot].Sealed = state;
    }

    [BBFunc]
    public static void SpellBuffRemoveType(
        AttackableUnit target,
        BuffType type
    )
    {
        target.Buffs.RemoveType(type);
    }

    [BBFunc]
    public static void SetSlotSpellCooldownTimeVer2(
        float src,
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType spellbookType,
        ObjAIBase owner,
        bool broadcastEvent = false //TODO: Verify
    )
    {
        int slot = ConvertAPISlot(slotNumber, slotType, spellbookType);
        owner.Spells[slot].SetCooldown(src);
    }

    [BBFunc]
    public static void SetSpellToolTipVar(
        float value,
        int index,
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType slotBook,
        ObjAIBase target
    )
    {
        var slot = ConvertAPISlot(slotNumber, slotType, slotBook);
        target.Spells[slot].SetToolTipVar(index - 1, value);
    }

    [BBFunc]
    public static void OverrideAnimation(
        string toOverrideAnim,
        string overrideAnim,
        AttackableUnit owner
    )
    {
        owner.SetAnimStates(toOverrideAnim, overrideAnim);
    }

    [BBFunc]
    public static int GetLevel(
        AttackableUnit target
    )
    {
        return target switch
        {
            Champion c => c.Experience.Level,
            Minion m => m.MinionLevel,
            _ => 0
        };
    }

    [BBFunc]
    public static void PlayAnimation(
        string animationName,
        float scaleTime,
        AttackableUnit target,
        bool loop,
        bool blend,
        bool Lock = false
    )
    {
        AnimationFlags flags = 0;
        //TODO: Unknown8 + Override + UniqueOverride?
        if (Lock) flags |= AnimationFlags.Lock; //TODO: Verify
        target.PlayAnimation(animationName, scaleTime, flags: flags);
    }

    [BBFunc]
    public static void UnlockAnimation(
        AttackableUnit owner,
        bool blend = false
    )
    {
        owner.StopAnimation(owner.LastAnimation, false, blend, true);
    }

    [BBFunc(Dest = "Caster")]
    public static ObjAIBase SetBuffCasterUnit()
    {
        return GetBuff().SourceUnit;
    }

    [BBFunc]
    public static void ClearOverrideAnimation(
        string toOverrideAnim,
        AttackableUnit owner
    )
    {
        owner.SetAnimStates(toOverrideAnim, "");
    }

    [BBFunc]
    public static void ForNClosestUnitsInTargetArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        [BBBuffName] string buffNameFilter = "",
        bool inclusiveBuffFilter = false,

        [BBSubBlocksAttribute("Iterator")]
        Action<AttackableUnit>? subBlocks = null
    )
    {
    }

    [BBFunc(Dest = "Result")]
    public static bool CanSeeTarget(
        AttackableUnit viewer,
        AttackableUnit target
    )
    {
        return true;
    }

    [BBFunc(Dest = "SkinID")]
    public static int GetSkinID(AttackableUnit unit)
    {
        return (unit as ObjAIBase)?.SkinID ?? 0;
    }

    [BBFunc]
    public static void DestroyMissile(
        SpellMissile? missileID
    )
    {
        missileID?.SetToRemove(); //TODO: or SetToRemoveBlock?
    }

    [BBFunc(Dest = "BubbleID")]
    public static Region AddUnitPerceptionBubble(
        TeamId team,
        float radius,
        AttackableUnit target,
        float duration,
        AttackableUnit? specificUnitsClientOnly = null,
        AttackableUnit? revealSpecificUnitOnly = null,
        bool revealSteath = false
    )
    {
        return new Region(
            team, target.Position,
            collisionUnit: target, //HACK: to force the region to follow the unit
            visionTarget: revealSpecificUnitOnly ?? specificUnitsClientOnly,
            visionRadius: radius,
            revealStealth: revealSteath,
            lifetime: duration
        );
    }

    [BBFunc]
    public static void RemovePerceptionBubble(
        Region bubbleID
    )
    {
        //Game.ObjectManager.RemoveObject(bubbleID);
        bubbleID.SetToRemove();
    }

    #region GetCastInfo
    public static Spell? CastedSpell;
    [BBSpellName] public static string GetSpellName() => FCS.GetSpellName(CastedSpell!);
    public static float GetPARCost() => FCS.GetPARCost(CastedSpell!);
    public static int GetSpellSlot() => FCS.GetSpellSlot(CastedSpell!); //TODO: Verify
    public static int GetCastSpellLevelPlusOne() => FCS.GetSpellLevelPlusOne(CastedSpell!);
    public static bool GetIsAttackOverride() => FCS.GetIsAttackOverride(CastedSpell!);
    public static int GetCastSpellTargetsHitPlusOne() => FCS.GetSpellTargetsHitPlusOne(CastedSpell!);
    #endregion

    [BBFunc]
    public static Vector3 GetCastSpellTargetPos() => FCS.GetSpellTargetPos(GetSpell(CastedSpell));
    public static Vector3 GetCastSpellTargetPos(Spell spell) => FCS.GetSpellTargetPos(spell);
    [BBFunc]
    public static Vector3 GetCastSpellDragEndPos() => FCS.GetSpellDragEndPos(GetSpell(CastedSpell));

    [BBFunc]
    public static void SetDodgePiercing(
        AttackableUnit target,
        bool value
    )
    {
        target.SetDodgePiercing(value);
    }

    [BBFunc]
    public static void Say(
        AttackableUnit owner,
        string toSay,
        object? src = null
    )
    {
        Console.WriteLine($"{owner.Model} {nameof(Say)} {toSay} {src}");
    }

    [BBFunc(Dest = "ID")]
    public static Fade? PushCharacterFade(
        AttackableUnit target,
        float fadeAmount,
        float fadeTime,
        Fade? ID = null //HACK:
    )
    {
        if (ID != null && fadeAmount == 0)
        {
            target.FadeIn(ID, fadeTime);
            return null;
        }
        return target.FadeOut(fadeAmount, fadeTime);
    }

    [BBFunc]
    public static void OverrideAutoAttack(
        int spellSlot,
        SpellSlotType slotType,
        AttackableUnit owner,
        int autoAttackSpellLevel, //TODO:
        bool cancelAttack
    )
    {
        var slot = ConvertAPISlot(spellSlot, slotType);
        (owner as ObjAIBase)!.SetAutoAttackSpell(slot, cancelAttack);
    }

    [BBFunc]
    public static void RemoveOverrideAutoAttack(
        AttackableUnit owner,
        bool cancelAttack
    )
    {
        (owner as ObjAIBase/*TODO:*/)!.ResetAutoAttackSpell(cancelAttack);
    }


    [BBFunc]
    public static void SetPARCostInc(
        ObjAIBase spellSlotOwner,
        int spellSlot,
        SpellSlotType slotType,
        float cost,
        PrimaryAbilityResourceType PARType
    )
    {
        var slot = ConvertAPISlot(spellSlot, slotType);
        if (spellSlotOwner.Stats.PrimaryAbilityResourceType == PARType)
        {
            spellSlotOwner.Spells[slot].SetIncreaseManaCost(cost);
        }
    }


    [BBFunc]
    public static void CancelAutoAttack(
        AttackableUnit target,
        bool reset
    )
    {
        (target as ObjAIBase).CancelAutoAttack(reset);
    }

    [BBFunc]
    public static float GetBuffRemainingDuration(
        AttackableUnit target,
        [BBBuffName] string buffName
    )
    {
        return target.Buffs.GetRemainingDuration(buffName);
    }

    [BBFunc]
    public static Champion? GetChampionBySkinName(
        string skin,
        TeamId team
    )
    {
        var champions = Game.ObjectManager.GetAllChampions();
        foreach (var champion in champions)
        {
            if (champion.Model == skin && champion.Team.HasFlag(team))
            {
                return champion;
            }
        }
        return null;
    }

    [BBFunc]
    public static void IssueOrder(
        AttackableUnit whomToOrder,
        OrderType order,
        Vector3? targetOfOrderPosition = null,
        AttackableUnit? targetOfOrder = null
    )
    {
        if (targetOfOrder != null)
        {
            targetOfOrderPosition = targetOfOrder.Position3D;
        }

        if (whomToOrder is ObjAIBase objAiBase)
        {
            objAiBase.IssueOrDelayOrder(order, targetOfOrder, targetOfOrderPosition?.ToVector2() ?? Vector2.Zero);
        }
    }

    [BBFunc]
    public static void LinkVisibility(
        AttackableUnit unit1,
        AttackableUnit unit2
    )
    {
    }

    [BBFunc]
    public static void ApplyTaunt(
        AttackableUnit attacker,
        AttackableUnit target,
        float duration
    )
    {
        target.Buffs.Add("Taunt", duration, 1, null, target, (ObjAIBase)attacker);
    }

    [BBFunc]
    public static void StopChanneling(
        ObjAIBase caster,
        ChannelingStopCondition stopCondition,
        ChannelingStopSource stopSource
    )
    {
        caster.StopChanneling(stopCondition, stopSource);
    }

    [BBFunc]
    public static void ForEachChampion(
        TeamId team,
        [BBBuffName] string buffNameFilter = "",
        bool inclusiveBuffFilter = false,

        [BBSubBlocksAttribute("Iterator")]
        Action<Champion>? subBlocks = null
    )
    {
        var champions = FCS.GetChampions(
            team, buffNameFilter, inclusiveBuffFilter
        );
        foreach (var champion in champions)
        {
            subBlocks!(champion);
        }
    }

    [BBFunc]
    public static void DebugSay(
        AttackableUnit owner,
        string toSay,
        object? src = null
    )
    {
        Console.WriteLine($"{owner.Model} {nameof(DebugSay)} {toSay} {src}");
    }

    [BBFunc]
    public static void TeleportToPosition(
        AttackableUnit owner,
        [BBParam("Name", null, "", null)]
        Vector3 castPosition
    )
    {
        ApiFunctionManager.TeleportTo(owner, castPosition.ToVector2());
    }

    [BBFunc(Dest = "ID")]
    public static int PushCharacterData(
        string skinName,
        AttackableUnit target,
        bool overrideSpells
    )
    {
        return (target as ObjAIBase)?.PushCharacterData(skinName, overrideSpells) ?? 0;
    }

    [BBFunc]
    public static void PopCharacterData(
        AttackableUnit target,
        int ID
    )
    {
        (target as ObjAIBase)?.PopCharacterData(ID);
    }


    [BBFunc]
    public static float DistanceBetweenObjectAndPoint(
        AttackableUnit Object,
        Vector3 point
    )
    {
        return Vector3.Distance(Object.Position3D, point);
    }

    [BBFunc]
    public static string GetUnitSkinName(
        AttackableUnit target
    )
    {
        return target.Model;
    }

    [BBFunc]
    public static ObjAIBase GetPetOwner(
        Pet pet
    )
    {
        return pet.Owner;
    }

    [BBFunc]
    public static void StartTrackingCollisions(
        AttackableUnit target,
        bool value
    )
    {
    }

    [BBFunc]
    public static void IncGold(
        AttackableUnit target,
        float delta
    )
    {
        target.GoldOwner.AddGold(delta);
    }

    [BBFunc]
    public static void SetTargetingType(
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType? slotBook,
        TargetingType targetType,
        AttackableUnit target
    )
    {
    }

    [BBFunc]
    public static void ForNClosestVisibleUnitsInTargetArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        [BBBuffName] string buffNameFilter = "",
        bool inclusiveBuffFilter = false,

        [BBSubBlocksAttribute("Iterator")]
        Action<AttackableUnit>? subBlocks = null
    )
    {
    }

    [BBFunc]
    public static void ForEachUnitInTargetRectangle(
        AttackableUnit attacker,
        Vector3 center,
        float halfWidth,
        float halfLength,
        SpellDataFlags flags,
        [BBBuffName] string buffNameFilter = "",
        bool inclusiveBuffFilter = false,

        [BBSubBlocksAttribute("Iterator")]
        Action<AttackableUnit>? subBlocks = null
    )
    {
    }

    [BBFunc]
    public static void DestroyMissileForTarget(
        AttackableUnit target
    )
    {
        //TODO: Optimize object retrieval
        var objects = Game.ObjectManager.GetObjects();
        foreach (var go in objects.Values)
        {
            if (go is SpellMissile m && m.TargetUnit == target)
            {
                m.SetToRemove(); //TODO: or SetToRemoveBlock?
            }
        }
    }

    [BBFunc]
    public static void TeleportToKeyLocation(
        AttackableUnit owner,
        SpawnType location,
        TeamId team
    )
    {
        Vector2 position;
        switch (location)
        {
            case SpawnType.SPAWN_LOCATION:
                position = Game.Map.SpawnPoints[team].Position;
                break;
            default:
                return;
        }
        ApiFunctionManager.TeleportTo(owner, position);
    }

    [BBFunc]
    public static void SetPARColorOverride(
        AttackableUnit spellSlotOwner,
        int colorR,
        int colorG,
        int colorB,
        int colorA,
        int fadeR,
        int fadeG,
        int fadeB,
        int fadeA
    )
    {
    }

    [BBFunc]
    public static void SetPARMultiplicativeCostInc(
        AttackableUnit spellSlotOwner,
        int spellSlot,
        SpellSlotType slotType,
        float cost,
        PrimaryAbilityResourceType PARType
    )
    {
        var slot = ConvertAPISlot(spellSlot, slotType);
        if (isCompatablePARs(spellSlotOwner.Stats.PrimaryAbilityResourceType, PARType))
        {
            (spellSlotOwner as ObjAIBase)?.Spells[slot].IncreaseMultiplicativeManaCost(cost);
        }
    }

    [BBFunc]
    public static void PopAllCharacterData(
        AttackableUnit target
    )
    {
        (target as ObjAIBase)?.PopAllCharacterData();
    }

    [BBFunc]
    public static void SetSpellOffsetTarget(
        int slotNumber,
        SpellSlotType slotType,
        [BBSpellName] string spellName,
        SpellbookType spellbookType,
        AttackableUnit caster,
        AttackableUnit offsetTarget
    )
    {
        //oriana time 
        if (caster is ObjAIBase obj)
        {
            int slot = obj.GetSpell(spellName)?.Slot ?? ConvertAPISlot(slotNumber, slotType, spellbookType);
            obj?.Spells[slot].ChangeOffsetTarget(offsetTarget, false, true);
        }
    }

    [BBFunc]
    public static void AddSpellOffsetTarget(
        int slotNumber,
        SpellSlotType slotType,
        [BBSpellName] string spellName,
        SpellbookType spellbookType,
        AttackableUnit caster,
        AttackableUnit offsetTarget
    )
    {
        if (caster is ObjAIBase obj)
        {
            int slot = obj.GetSpell(spellName)?.Slot ?? ConvertAPISlot(slotNumber, slotType, spellbookType);
            obj?.Spells[slot].ChangeOffsetTarget(offsetTarget);
        }
    }

    [BBFunc]
    public static void RemoveSpellOffsetTarget(
        int slotNumber,
        SpellSlotType slotType,
        [BBSpellName] string spellName,
        SpellbookType spellbookType,
        AttackableUnit caster,
        AttackableUnit offsetTarget,
        bool removeall = false
    )
    {
        if (caster is ObjAIBase obj)
        {
            int slot = obj.GetSpell(spellName)?.Slot ?? ConvertAPISlot(slotNumber, slotType, spellbookType);
            obj?.Spells[slot].ChangeOffsetTarget(offsetTarget, false, removeall);
        }
    }

    public static List<AttackableUnit> GetSpellOffsetTargetList
    (
        int slotNumber,
        SpellSlotType slotType,
        [BBSpellName] string spellName,
        SpellbookType spellbookType,
        AttackableUnit caster
    )
    {
        if (caster is ObjAIBase obj)
        {
            int slot = obj.GetSpell(spellName)?.Slot ?? ConvertAPISlot(slotNumber, slotType, spellbookType);
            return obj.Spells[slot].OffsetTargets;
        }
        return [];
    }

    [BBFunc]
    public static void PauseAnimation(
        AttackableUnit unit,
        bool pause
    )
    {
        unit.PauseAnimation(pause);
    }

    [BBFunc]
    public static void SkipNextAutoAttack(
        ObjAIBase target
    )
    {
        target.SkipNextAutoAttack();
    }

    [BBFunc]
    public static void SetCameraPosition(
        [BBParam("", "Table", null, null)]
        Champion owner,
        Vector3 position
    )
    {
    }

    [BBFunc]
    public static void StopMove(
        AttackableUnit target
    )
    {
        target.StopMovement();
    }

    [BBFunc]
    public static void SetUseSlotSpellCooldownTime(
        float src,
        AttackableUnit owner,
        bool broadcastEvent
    )
    {
    }

    [BBFunc]
    public static void PopCharacterFade(
        AttackableUnit target,
        Fade ID
    )
    {
        target.FadeIn(ID);
    }

    [BBFunc(Dest = "OutputAngle")]
    public static float GetOffsetAngle(
        AttackableUnit unit,
        Vector3 offsetPoint
    )
    {
        return 0;
    }

    [BBFunc]
    public static void SetTriggerUnit(
        AttackableUnit trigger
    )
    {
    }

    [BBFunc]
    public static AttackableUnit SetUnit(
        AttackableUnit src
    )
    {
        return src;
    }

    [BBFunc]
    public static void SetSlotSpellIcon(
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType spellbookType,
        ObjAIBase owner,
        int iconIndex
    )
    {
        int slot = ConvertAPISlot(slotNumber, slotType, spellbookType);
        owner.Spells[slot].IconIndex = (byte)iconIndex;
    }

    [BBFunc(Dest = "Result")]
    public static bool IsPathable(
        Vector3 destPos
    )
    {
        //TODO: Perhaps they meant the possibility of building a path,
        //TODO: and not the passability of a particular point
        return Game.Map.PathingHandler.IsWalkable(destPos.ToVector2());
    }

    [BBFunc]
    public static void SetAutoAcquireTargets(
        AttackableUnit target,
        bool value
    )
    {
    }

    [BBFunc]
    public static void RedirectGold(
        AttackableUnit redirectSource,
        AttackableUnit? redirectTarget
    )
    {

    }

    [BBFunc(Dest = "GroundPos")]
    public static Vector3 GetGroundHeight(
        Vector3 queryPos
    )
    {
        var pos = queryPos.ToVector2();
        return pos.ToVector3(Game.Map.NavigationGrid.GetHeightAtLocation(pos));
    }

    [BBFunc]
    public static void ShowHealthBar(
        AttackableUnit unit,
        bool show
    )
    {
        Game.PacketNotifier.NotifyS2C_ShowHealthBar(unit, show);
    }

    [BBFunc(Dest = "Result")]
    public static Vector3 GetRandomPointInAreaPosition(
        Vector3 pos,
        float radius,
        float innerRadius
    )
    {
        if (innerRadius > radius)
        {
            (radius, innerRadius) = (innerRadius, radius);
        }
        return pos + (
            E.FromAngle(random.NextSingle() * 360f - 180f) *
            (random.NextSingle() * (radius - innerRadius) + innerRadius)
        ).ToVector3(0);
    }

    [BBFunc]
    public static void FadeInColorFadeEffect(
        int colorRed,
        int colorGreen,
        int colorBlue,
        float fadeTime,
        float maxWeight,
        TeamId specificToTeam
    )
    {
        var color = new Color
        {
            R = (byte)colorRed,
            G = (byte)colorGreen,
            B = (byte)colorBlue,
            A = 255,
        };
        Game.PacketNotifier.NotifyTint(specificToTeam, true, fadeTime, maxWeight, color);
    }

    [BBFunc]
    public static float GetSpellBlock(
        AttackableUnit target
    )
    {
        return target.Stats.MagicResist.Total;
    }

    [BBFunc]
    public static float GetBuffStartTime(
        AttackableUnit target,
        [BBBuffName] string buffName
    )
    {
        return target.Buffs.GetStartTime(buffName);
    }

    [BBFunc]
    public static void IncExp(
        AttackableUnit target,
        float delta
    )
    {
        (target as Champion)?.Experience.AddEXP(delta);
    }

    [BBFunc]
    public static void StopCurrentOverrideAnimation(
        string animationName,
        AttackableUnit target,
        bool blend
    )
    {
        target.StopAnimation(animationName); //TODO:
    }

    [BBFunc(Dest = "Result")]
    public static Vector3 GetMissilePosFromID(
        SpellMissile targetID
    )
    {
        return targetID.Position3D;
    }

    [BBFunc(Dest = "Result")]
    public static bool GetIsZombie(
        AttackableUnit unit
    )
    {
        return unit.Stats.IsZombie;
    }

    [BBFunc]
    public static void ForEachPointOnLine(
        Vector3 center,
        Vector3 faceTowardsPos,
        float size,
        float pushForward,
        int iterations,

        [BBSubBlocks("Iterator")]
        Action<Vector3> subBlocks
    )
    {
    }

    public static void ModifyPosition(ref Vector3 position, float x, float y, float z)
    {
        position += new Vector3(x, y, z);
    }

    [BBFunc(Dest = "Result")]
    public static bool IsInBrush(AttackableUnit unit)
    {
        return Game.Map.NavigationGrid.IsBush(unit.Position);
    }

    [BBFunc]
    public static void MoveToUnit(
        AttackableUnit unit,
        AttackableUnit target,
        float speed,
        float gravity,
        ForceMovementOrdersType movementOrdersType, //TODO:
        float moveBackBy,
        float maxTrackDistance,
        float idealDistance, //TODO:
        float timeOverride
    )
    {
        unit.DashToTarget(target, speed, gravity, false, maxTrackDistance, moveBackBy, timeOverride);
    }

    [BBFunc]
    public static void ForEachUnitInTargetAreaAddBuff(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        ObjAIBase buffAttacker,
        [BBBuffName] string buffName = "",
        BuffAddType buffAddType = 0,
        BuffType buffType = 0,
        int buffMaxStack = 1,
        int buffNumberOfStacks = 1,
        float buffDuration = 25000,

        [BBParam("", null, null, null)]
        Table? buffVarsTable = null,

        float tickRate = 0, //TODO:
        bool isHiddenOnClient = false,
        bool inclusiveBuffFilter = false
    )
    {
        var units = FCS.GetUnitsInArea(
            attacker, center, range, flags, "", inclusiveBuffFilter
        );
        foreach (var unit in units)
        {
            SpellBuffAdd(
                buffAttacker, unit, buffName,
                buffMaxStack, buffNumberOfStacks, buffDuration,
                buffVarsTable, buffAddType, buffType,
                tickRate, false, false, isHiddenOnClient
            );
        }
    }

    [BBFunc]
    public static void ForceDead(
        AttackableUnit owner
    )
    {
        if (owner is Champion champion)
        {
            champion.ForceDead();
        }
    }

    [BBFunc]
    public static void FadeOutColorFadeEffect(
        float fadeTime,
        TeamId specificToTeam
    )
    {
        Game.PacketNotifier.NotifyTint(specificToTeam, false, fadeTime, 0, new Color());
    }

    [BBFunc]
    public static void ApplyNearSight(
        AttackableUnit attacker,
        AttackableUnit target,
        float duration
    )
    {
        SpellBuffAdd((ObjAIBase)attacker, target, "NearSight", 1, 1, duration, buffAddType: BuffAddType.RENEW_EXISTING, buffType: BuffType.NEAR_SIGHT);
    }

    [BBFunc(Dest = "NewPosition")]
    public static Vector3 GetNearestPassablePosition(
        [BBParam("", "VarTable", null, null)] //TODO: Validate "VarTable"
        AttackableUnit owner,
        Vector3 position
    )
    {
        return position;
    }

    [BBFunc]
    public static void StopMoveBlock(
        AttackableUnit target
    )
    {
        target.SetDashingState(false, MoveStopReason.UnitCollision);
    }

    [BBFunc]
    public static void ReincarnateHero(
        AttackableUnit target
    )
    {
        //ReincarnateNonDeadHero
    }

    [BBFunc]
    public static void Alert(
        string toAlert,
        object? src = null
    )
    {
        Console.WriteLine($"{nameof(Alert)} {toAlert} {src}");
    }

    [BBFunc]
    [BBBuffName]
    public static string GetDamagingBuffName()
    {
        return "";
    }

    [BBFunc]
    public static void InvalidateUnit(
        AttackableUnit target
    )
    {
    }

    [BBFunc]
    public static void ForEachVisibleUnitInTargetAreaRandom(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        [BBBuffName] string buffNameFilter,
        bool inclusiveBuffFilter,

        [BBSubBlocks("Iterator")]
        Action<AttackableUnit> subBlocks
    )
    {
    }

    [BBFunc]
    public static void IncSpellLevel(
        ObjAIBase spellSlotOwner,
        int spellSlot,
        SpellSlotType slotType
    )
    {
        int slot = ConvertAPISlot(spellSlot, slotType);
        spellSlotOwner.Spells[slot].LevelUp();
    }

    [BBFunc]
    public static void SetNotTargetableToTeam(
        AttackableUnit target,
        bool toAlly,
        bool toEnemy
    )
    {
    }

    [BBFunc]
    public static void SetScaleSkinCoef(
        float scale,
        AttackableUnit owner
    )
    {
    }

    [BBFunc]
    public static void ClearPARColorOverride(
        AttackableUnit spellSlotOwner
    )
    {
    }

    [BBFunc(Dest = "IncCost")]
    public static float GetPARMultiplicativeCostInc(
        AttackableUnit spellSlotOwner,
        int spellSlot,
        SpellSlotType slotType,
        PrimaryAbilityResourceType PARType
    )
    {
        return 0;
    }

    [BBFunc]
    public static void ForEachPointAroundCircle(
        Vector3 center,
        float radius,
        int iterations,

        [BBSubBlocks("Iterator")]
        Action<Vector3> subBlocks
    )
    {
    }

    [BBFunc]
    public static void ForceRefreshPath(
        AttackableUnit unit
    )
    {
    }

    [BBFunc]
    public static void RemoveLinkVisibility(
        AttackableUnit unit1,
        AttackableUnit unit2
    )
    {
    }

    [BBFunc]
    public static void UpdateCanCast(
        AttackableUnit target
    )
    {
    }

    [BBFunc]
    public static void SetSpellCastRange(
        float newRange
    )
    {
        //TODO: GetSpell().CastRange = newRange;
    }

    [BBFunc]
    public static void DispellNegativeBuffs(
        AttackableUnit attacker
    )
    {
    }

    [BBFunc]
    public static void SetVoiceOverride(
        string overrideSuffix,
        AttackableUnit target
    )
    {
    }

    [BBFunc]
    public static void ResetVoiceOverride(
        AttackableUnit target
    )
    {
    }

    [BBFunc(Dest = "Result")]
    public static bool TestUnitAttributeFlag(
        AttackableUnit target,
        ExtraAttributeFlag attributeFlag
    )
    {
        return false;
    }

    [BBFunc(Dest = "Range")]
    public static float GetCastRange(
        ObjAIBase spellSlotOwner,
        int slotNumber,
        SpellSlotType slotType
    )
    {
        int slot = ConvertAPISlot(slotNumber, slotType);
        return spellSlotOwner.Spells[slot].CastRange;
    }

    #region Shields
    [BBFunc]
    public static void ModifyShield(
        AttackableUnit unit,
        float amount = 0,
        bool magicShield = false,
        bool physicalShield = false,
        bool noFade = false //TODO: Validate
    )
    {
        unit.SetShield(physicalShield, magicShield, amount, noFade);
    }
    [BBFunc]
    public static void IncreaseShield(
        AttackableUnit unit,
        float amount = 0,
        bool magicShield = false,
        bool physicalShield = false
    )
    {
        unit.IncShield(physicalShield, magicShield, +amount, true);
    }
    [BBFunc]
    public static void ReduceShield(
        AttackableUnit unit,
        float amount = 0,
        bool magicShield = false,
        bool physicalShield = false
    )
    {
        unit.IncShield(physicalShield, magicShield, -amount, true);
    }
    [BBFunc]
    public static void RemoveShield(
        AttackableUnit unit,
        float amount = 0,
        bool magicShield = false,
        bool physicalShield = false
    )
    {
        unit.SetShield(physicalShield, magicShield, 0, true);
    }
    #endregion

    [BBFunc]
    public static void CreateItem(
        ObjAIBase unit, //TODO: Move Inventory to AttackableUnit?
        int itemID
    )
    {
        unit.ItemInventory.AddItem(ContentManager.GetItemData(itemID));
    }

    [BBFunc]
    public static void DefUpdateAura(
        Vector3 center,
        float range,
        AttackableUnit unitScan,
        string buffName
    )
    {
    }

    internal static DamageSource SourceType;
    public static DamageSource GetSourceType()
    {
        return SourceType;
    }

    public static bool IsObjectAI(object obj)
    {
        return obj is ObjAIBase;
    }

    public static bool IsObjectHero(object obj)
    {
        return obj is Champion;
    }

    public static bool IsMelee(object obj)
    {
        return (obj is ObjAIBase ai) && ai.IsMelee;
    }

    public static bool IsTurretAI(object obj)
    {
        return obj is BaseTurret;
    }

    public static bool IsDead(object obj)
    {
        return (obj is AttackableUnit u) && u.Stats.IsDead;
    }

    // Not BB really
    public static bool BBIsTargetInFrontOfMe(AttackableUnit left, AttackableUnit right)
    {
        return false;
    }
    // Not BB really
    public static bool BBIsTargetBehindMe(AttackableUnit left, AttackableUnit right)
    {
        return false;
    }

    public static bool HasBuffOfType(AttackableUnit target, BuffType buffType)
    {
        return target.Buffs.Has(buffType);
    }

    public static bool HasPARType(AttackableUnit owner, PrimaryAbilityResourceType PARType)
    {
        return owner.Stats.PrimaryAbilityResourceType == PARType;
    }

    public static Vector3 GetPosition(AttackableUnit target)
    {
        return target.Position3D;
    }

    public static Vector3 GetNormalizedPositionDelta(AttackableUnit target, AttackableUnit attacker, bool unknown)
    {
        return (target.Position3D - attacker.Position3D).Normalized();
    }

    public static void SetPosition(AttackableUnit target, Vector3 position)
    {
        target.SetPosition(position.ToVector2(), true);
    }

    [BBFunc]
    public static void SetCanCastWhileDisabled(bool canCast)
    {
    }

    public static void _ALERT(object smth)
    {
        Console.WriteLine($"{nameof(_ALERT)} {smth}");
    }

    [BBFunc]
    public static int GetGold(
    //TODO:
    )
    {
        return 0;
    }

    [BBFunc]
    public static void SpellBuffRenew(
        AttackableUnit target,
        [BBBuffName] string buffName,
        float resetDuration = 0
    )
    {
        target.Buffs.Renew(buffName, null, resetDuration);
    }

    public static Vector3 Make3DPoint(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }

    //[BBFunc]
    public static float DistanceBetweenObjects(
        GameObject object1, //ObjectVar1
        GameObject object2  //ObjectVar2
    )
    {
        return object1.Position.Distance(object2.Position);
    }

    public static int GetObjectLaneId(AttackableUnit unit)
    {
        return unit switch
        {
            BaseTurret turret => (int)turret.Lane,
            Inhibitor inhibitor => (int)inhibitor.Lane,
            Barrack barrack => (int)barrack.Lane,
            _ => -1,
        };
    }

    public static bool IsTurretAI(AttackableUnit unit)
    {
        return unit is LaneTurret;
    }

    public static LaneTurret? GetTurret(int team, int lane, int position)
    {
        return LaneTurret.GetTurret((TeamId)team, (Lane)lane, position);
    }

    public static int GetTurretPosition(AttackableUnit unit)
    {
        if (unit is not LaneTurret turret)
        {
            return -1;
        }

        return turret.TurretIndex;
    }

    public static LaneTurret? CreateChildTurret(string turretName, string turretModel, int team, int turretIndex, int lane)
    {
        return LaneTurret.CreateChildTurret(turretName, turretModel, (TeamId)team, turretIndex, (Lane)lane);
    }

    public static LaneTurret? SpawnTurret(string turretName, string turretModel, int team, int turretIndex, int lane)
    {
        return LaneTurret.CreateChildTurret(turretName, turretModel, (TeamId)team, turretIndex, (Lane)lane);
    }

    public static bool IsDampener(AttackableUnit unit)
    {
        return unit is Inhibitor;
    }

    public static Inhibitor GetDampener(int team, int lane)
    {
        return Inhibitor.GetInhibitor((TeamId)team, (Lane)lane);
    }

    public static int GetDampenerType(AttackableUnit unit)
    {
        if (unit is not Inhibitor inhibitor)
        {
            return -1;
        }

        return (int)inhibitor.Team + (int)inhibitor.Lane;
    }

    public static void SetDampenerRespawnAnimationDuration(Inhibitor inhibitor, float respawnAnimationDuration)
    {
        inhibitor.RespawnAnimationDuration = respawnAnimationDuration;
    }

    public static Barrack GetLinkedBarrack(Inhibitor unit)
    {
        return InhibitorHelper.GetLinkedBarrack(unit);
    }

    public static int GetLane(AttackableUnit unit)
    {
        if (unit is Barrack barrack)
        {
            return (int)barrack.Lane;
        }
        if (unit is not Inhibitor inhibitor)
        {
            return -1;
        }

        Barrack LinkedBarrack = InhibitorHelper.GetLinkedBarrack(inhibitor);
        return (int)LinkedBarrack.Lane;
    }

    public static Barrack? GetBarracks(int team, int lane)
    {
        return Barrack.GetBarrack((TeamId)team, (Lane)lane);
    }

    public static void SetDampenerState(AttackableUnit unit, DampenerState state)
    {
        if (unit is Inhibitor inhibitor)
        {
            inhibitor.SetState(state);
        }
    }

    public static Nexus? GetHQ(int team)
    {
        return Nexus.GetNexus((TeamId)team);
    }

    public static int GetHQType(AttackableUnit unit)
    {
        if (unit is not Nexus nexus)
        {
            return -1;
        }

        if (nexus.Name is "HQ_T1")
        {
            return 1;
        }
        return nexus.Name is "HQ_T2" ? 2 : 0;
    }

    public static void SetNotTargetableToTeam(AttackableUnit? unit, bool notTargetable, int team)
    {
        unit?.SetIsTargetableToTeam((TeamId)team, !notTargetable);
    }

    public static void SetDisableMinionSpawn(Barrack barrack, float seconds)
    {
        barrack?.DisableInhibitor(seconds);
    }

    public static int GetTotalTeamMinionsSpawned()
    {
        return LaneMinion.Manager.Count;
    }

    public static string GetGameMode()
    {
        return Game.Config.GameConfig.GameMode;
    }

    public static void ApplyPersistentBuffToAllChampions(string buffName, bool isInternal)
    {
        foreach (ClientInfo player in Game.PlayerManager.GetPlayers())
        {
            SpellBuffAdd(null, player.Champion, buffName, buffType: isInternal ? BuffType.INTERNAL : BuffType.AURA);
        }
    }

    public static void ApplyPersistentBuff(AttackableUnit target, string buffName, bool isInternal /*Check*/, int stackCount /*Check*/, int maxStacks/*Check*/)
    {
        SpellBuffAdd(null, target, buffName, maxStacks, stackCount, buffType: isInternal ? BuffType.INTERNAL : BuffType.AURA);
    }

    public static void AddBuffCounter(AttackableUnit target, string buffName, int unk /*DELAY | AMBIENT_XP | modifier*/, int maxStacks/*Check*/)
    {
        //SpellBuffCounterAdd(null, target, buffName, maxStacks, stackCount, buffType: isInternal ? BuffType.INTERNAL : BuffType.AURA);
    }

    public static void SetBuildingHealthRegenEnabled(ObjBuilding building, bool enabled)
    {
        building.HealthRegenEnabled = enabled;
    }

    public static void SetBarracksSpawnEnabled(bool enabled)
    {
        Barrack.SetSpawn(enabled);
    }

    public static void DisableHUDForEndOfGame()
    {
        Game.PacketNotifier.NotifyS2C_DisableHUDForEndOfGame();
    }

    public static void MoveCameraFromCurrentPositionToPoint(Champion champion, Vector3? luaPosition, float travelTime, bool unk)
    {
        ClientInfo info = Game.PlayerManager.GetClientInfoByChampion(champion);

        Game.PacketNotifier.NotifyS2C_MoveCameraToPoint(info, Vector3.Zero, luaPosition ?? Vector3.Zero, travelTime, true, unk /*check*/);
    }

    public static float GetEoGPanToHQTime()
    {
        return Nexus.GetEoGPanTime();
    }

    public static float GetEoGNexusChangeSkinTime()
    {
        return Nexus.GetEoGNexusChangeSkinTime();
    }

    public static bool GetEoGUseNexusDeathAnimation()
    {
        return Nexus.GetEoGUseDeathAnimation();
    }

    public static void SetGreyscaleEnabledWhenDead(Champion champion, bool enabled)
    {
        ClientInfo info = Game.PlayerManager.GetClientInfoByChampion(champion);
        if (info is not null)
        {
            Game.PacketNotifier.NotifyS2C_SetGreyscaleEnabledWhenDead(info, enabled);
        }
    }

    public static bool FadeOutMainSFX()
    {
        //?
        return true;
    }

    public static void SetHQCurrentSkin(int team, int skinId)
    {
        Nexus nexus = Nexus.GetNexus((TeamId)team);
        //TODO
    }

    public static void SetMinionsNoRender(int team, bool enabled)
    {
        //TODO
    }

    public static void FadeMinions(int team, float fadeAmmont, float fadeTime)
    {
        //TODO
    }

    public static void HaltAllAI()
    {
        //TODO
    }

    public static void SetInputLockFlag(InputLockFlags flag, bool enabled)
    {
        //TODO
        //SetInputLockToValue((InputLockType)flag, enabled)
    }

    public static void CloseShop()
    {
        //TODO
    }

    public static void EndGame(int winningTeam)
    {
        var team = (TeamId)winningTeam;
        Game.PacketNotifier.NotifyS2C_EndGame(team);
        Game.StateHandler.SetGameState(GameState.ENDGAME);
        Game.StateHandler.SetGameToExit();

        //TODO: Maybe have a dedicated HTTP post system?
        if (!string.IsNullOrEmpty(Game.Config.HttpPostAddress))
        {
            var endGame = new EndGameInfo(winningTeam);
            endGame.Post(Game.Config.HttpPostAddress);
        }
    }

    public static void Log(string message)
    {
        _logger.Debug(message);
    }

    public static void InitNeutralMinionTimer(Closure function, string timerType, float delay, float spawnDuration, bool repeat)
    {
        NeutralTimers.Add(new()
        {
            Function = () => function.Call(),
            Elapsed = spawnDuration,
            Delay = delay,
            Repeat = repeat,
            Enabled = true,
            Name = timerType
        });
    }

    public static void InitNeutralMinionTimer_CS(Action callback, string timerType, float delay, float spawnDuration, bool repeat)
    {
        NeutralTimers.Add(new()
        {
            Function = callback,
            Elapsed = spawnDuration,
            Delay = delay,
            Repeat = repeat,
            Enabled = true,
            Name = timerType
        });
    }

    public static void CreateNeutralCamp_CS(CampData neutralCamp, int groupNumber)
    {
        NeutralCampManager.CreateMinionCamp(neutralCamp.Positions[0], neutralCamp.MinimapIcon, groupNumber, neutralCamp.GroupBuffSide, neutralCamp.RevealEvent, (int)HashString(neutralCamp.TimerType));
        NeutralMinionCamp camp = NeutralCampManager.FindCamp(groupNumber)!;
        camp.SetSpawnEndTime(Game.Time.GameTime + neutralCamp.GroupDelaySpawnTime); //maybe we could ignore the const in this case?
        camp.SpawnDuration = neutralCamp.SpawnDuration;
        camp.RespawnTime = neutralCamp.RespawnTime;
        InitNeutralMinionTimer_CS(neutralCamp.Timer, neutralCamp.TimerType, camp.SpawnEndTime, neutralCamp.SpawnDuration, false);
    }

    public static void CreateNeutralCamp(Table neutralCamp, int groupNumber)
    {
        float spawnDuration = (float?)neutralCamp.Get("SpawnDuration")?.Number ?? 0.0f;
        float respawnTime = neutralCamp.Get("GroupsRespawnTime")?.ToObject<float?>() ?? 0.0f;
        string timerTypeStr = neutralCamp.Get("TimerType")?.String ?? "";
        string revealEventStr = neutralCamp.Get("RevealEvent")?.String ?? "";

        uint timerType = HashString(timerTypeStr);
        AudioVOComponentEvent revealEvent = string.IsNullOrEmpty(revealEventStr) ? AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS : Enum.Parse<AudioVOComponentEvent>(revealEventStr, true);

        NeutralCampManager.CreateMinionCamp
        (
            (LVector3)neutralCamp.Get("Positions").Table.Get(1).UserData.Object,
            neutralCamp.Get("MinimapIcon").String,
            groupNumber,
            (TeamId)neutralCamp.Get("GroupBuffSide").Number,
            revealEvent,
            (int)timerType
        );

        NeutralMinionCamp camp = NeutralCampManager.FindCamp(groupNumber)!;
        camp.SetSpawnEndTime(Game.Time.GameTime + (neutralCamp.Get("GroupDelaySpawnTime")?.ToObject<float?>() ?? 0.0f) + DEFAULT_CAMP_DELAY_SPAWN_TIME);

        //Hack?
        camp.SpawnDuration = spawnDuration;
        camp.RespawnTime = respawnTime;
        InitNeutralMinionTimer(neutralCamp.Get("Timer").Function, timerTypeStr, camp.SpawnEndTime, spawnDuration, false);
    }

    public static NeutralMinion SpawnNeutralMinion_CS(CampData neutralCamp, int groupNumber, int groupIndex, int nameIndex)
    {
        NeutralMinion monster = new
        (
            neutralCamp.UniqueNames[groupIndex][nameIndex],
            neutralCamp.Groups[groupIndex][nameIndex].Key,
            neutralCamp.Positions[nameIndex],
            neutralCamp.FacePositions[nameIndex],
            NeutralCampManager.FindCamp(groupNumber)!,
            TeamId.TEAM_NEUTRAL,
            neutralCamp.Groups[groupIndex][nameIndex].Value,
            true,
            false,
            null,
            neutralCamp.AIScript
        );

        monster.SetLevel(Champion.GetAverageChampionLevel());
        NeutralCampManager.AddMinionToCamp(groupNumber, monster, monster.Position3D, monster.Name, neutralCamp.GroupBuffSide, neutralCamp.RevealEvent, neutralCamp.SpawnDuration);
        Game.ObjectManager.AddObject(monster);
        return monster;
    }

    public static void SpawnNeutralMinion(Table neutralCamp, int groupNumber, int groupIndex, int nameIndex)
    {
        string model;
        string spawnAnimation = "";

        DynValue value = neutralCamp.Get("Groups").Table.Get(groupIndex).Table.Get(nameIndex);
        if (value.String is not null)
        {
            model = value.String;
        }
        else
        {
            model = value.Table.Get(1).String;
            spawnAnimation = value.Table.Get(2).String;
        }

        value = neutralCamp.Get("RevealEvent");
        AudioVOComponentEvent revealEvent = value.IsNil() ? AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS : Enum.Parse<AudioVOComponentEvent>(value.String, true);

        value = neutralCamp.Get("SpawnDuration");
        float spawnDuration = value.IsNil() ? 0 : (float)value.Number;

        NeutralMinion monster = new
            (
                neutralCamp.Get("UniqueNames").Table.Get(groupIndex).Table.Get(nameIndex).String,
                model,
                (LVector3)neutralCamp.Get("Positions").Table.Get(nameIndex).UserData.Object,
                (LVector3)neutralCamp.Get("FacePositions").Table.Get(nameIndex).UserData.Object,
                NeutralCampManager.FindCamp(groupNumber)!,
                TeamId.TEAM_NEUTRAL,
                spawnAnimation,
                true,
                false,
                null,
                neutralCamp.Get("AIScript").IsNil() ? "Leashed.lua" : neutralCamp.Get("AIScript").String
            );
        //?
        monster.SetLevel(Champion.GetAverageChampionLevel());

        NeutralCampManager.AddMinionToCamp(groupNumber, monster, monster.Position3D, monster.Name, (TeamId)neutralCamp.Get("GroupBuffSide").Number, revealEvent, spawnDuration);
        //Do this somewhere else?
        Game.ObjectManager.AddObject(monster);
    }

    public static void GiveExpToNearHeroesFromNeutral(ObjAIBase obj, float exp, LVector3 position, float radius)
    {
        foreach (Champion ch in Game.ObjectManager.GetChampionsInRange(((Vector3)position).ToVector2(), radius, true))
        {
            ch.Experience.AddEXP(exp);
        }
    }

    public static void AIScriptSpellBuffAdd(AttackableUnit target, ObjAIBase caster, string buffName, int buffType,
        float duration)
    {
        SpellBuffAdd(caster, target, buffName, 1, 1, buffType: (BuffType)buffType);
    }

    public static void AIScriptSpellBuffRemove(AttackableUnit target, string buffName)
    {
        SpellBuffRemove(target, buffName);
    }

    public static bool IsHeroAI(AttackableUnit unit)
    {
        return unit is Champion;
    }

    public static void SetActorPositionFromObject(AttackableUnit unit, AttackableUnit teleportUnit)
    {
        unit.TeleportTo(teleportUnit.Position);
    }

    public static float DistanceBetweenObjectBounds(AttackableUnit unitOne, AttackableUnit unitTwo)
    {
        float centerDistance = Vector2.Distance(unitOne.Position, unitTwo.Position);
        float distanceBetweenBounds = centerDistance - (unitOne.CharData.GameplayCollisionRadius + unitTwo.CharData.GameplayCollisionRadius);
        return Math.Max(0, distanceBetweenBounds);
    }

    public static float DistanceBetweenObjectCenterAndPoint(AttackableUnit? target, Vector2 point)
    {
        if (target == null)
            return 0;

        return Vector2.Distance(target.Position, point) - target.CollisionRadius;
    }

    public static bool IsNetworkLocal()
    {
        //Is local network? Used for debugging?
        return true;
    }

    public static void Die(AttackableUnit target, DamageSource damageSource)
    {
        target.TakeDamage(target, 99999f, DamageType.DAMAGE_TYPE_TRUE, damageSource);
    }

    public static bool TargetIsMoving(AttackableUnit target)
    {
        return !target.IsPathEnded();
    }

    public static bool IsAutoAcquireTargetEnabled(AttackableUnit target)
    {
        return ((ObjAIBase)target).AutoAttackAutoAcquireTarget;
    }

    public static Vector3 GetPos(AttackableUnit target)
    {
        return target.Position3D;
    }

    public static AttackableUnit? GetOwner(AttackableUnit target)
    {
        if (target is Minion { Owner.Stats.IsDead: false } pet)
            return pet.Owner;

        return null;
    }

    //Double-check implementation
    /*
    public static void InitTimer_CS(Action function, float delay, bool repeat)
    {
        for (int i = 0; i < LevelTimers.Count; i++)
        {
            if (LevelTimers[i] is not null)
            {
                LevelTimers[i] = new()
                {
                    Name = nameof(function),
                    Delay = delay,
                    Elapsed = 0.0f,
                    Repeat = repeat,
                    Enabled = true,
                    Callback = function
                };
                break;
            }
        }
    }
    */
}
