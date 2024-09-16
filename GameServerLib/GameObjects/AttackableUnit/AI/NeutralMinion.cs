using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using GameServerLib.GameObjects;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Chronobreak.GameServer.Scripting.CSharp;

namespace Chronobreak.GameServer.GameObjects;

//TODO: Port this over to Minion
public class NeutralMinion : Minion
{
    public NeutralMinionCamp Camp { get; private set; }
    public string SpawnAnimation { get; private set; }

    public NeutralMinion
    (
        string name,
        string model,
        Vector3 position,
        Vector3 faceDirection,
        NeutralMinionCamp monsterCamp,
        TeamId team = TeamId.TEAM_NEUTRAL,
        string spawnAnimation = "",
        bool isTargetable = true,
        bool ignoresCollision = false,
        Stats? stats = null,
        string aiScript = ""
    ) : base
    (
        null,
        position.ToVector2(),
        model,
        name,
        team,
        0,
        ignoresCollision,
        isTargetable,
        false,
        null,
        stats,
        aiScript
    )
    {
        Direction = faceDirection;
        Camp = monsterCamp;
        Team = team;
        SpawnAnimation = spawnAnimation;
        FaceDirection(faceDirection);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        //TODO: Move this to OnAdded(right now it breaks the minion)
        Game.PacketNotifier.NotifyCreateNeutral(this, Game.Time.GameTime, userId, team, doVision);
    }

    internal override void OnAdded()
    {
        base.OnAdded();
        Game.PacketNotifier.NotifyS2C_ActivateMinionCamp(Camp);
    }

    public override void Die(DeathData data)
    {
        base.Die(data);
        NeutralCampManager.KillMinion((data.Killer as ObjAIBase)!, this);
    }


    public override void TakeDamage(DamageData damageData, IEventSource sourceScript = null)
    {
        foreach (var campMinion in Camp.Minions)
        {
            (campMinion as NeutralMinion).AIScript.OnLeashedCallForHelp((ObjAIBase)damageData.Attacker, this);
        }
        base.TakeDamage(damageData, sourceScript);
    }

    public override bool IsValidTarget(AttackableUnit target)
    {
        return target switch
        {
            BaseTurret => false,
            Minion minion => minion.Owner != null && base.IsValidTarget(target),
            _ => base.IsValidTarget(target)
        };
    }

    internal override bool CanChangeWaypoints()
    {
        //TODO: There is a small period during auto attacks where you can't set waypoints, this is a hack for jungle monsters, so they don't moonwalk
        //Ideally, it would be implemented in ObjAiBase
        return !Stats.IsDead && MovementParameters == null
               && AutoAttackSpell?.State is SpellState.READY or SpellState.COOLDOWN;
    }

    internal void SetLevel(int level, bool setFullHealth = true)
    {
        MinionLevel = level;

        if (ContentManager.MonsterDataTables.TryGetValue(CharData.MonsterDataTableID, out var dataFile))
        {
            Stats.AbilityPower.IncPercentBonusPerm(dataFile.AbilityPower.GetValueOrDefault(MinionLevel, dataFile.AbilityPower.Last().Value) - 1);
            Stats.Armor.IncPercentBonusPerm(dataFile.Armor.GetValueOrDefault(MinionLevel, dataFile.Armor.Last().Value) - 1);
            Stats.AttackDamage.IncPercentBonusPerm(dataFile.AttackDamage.GetValueOrDefault(MinionLevel, dataFile.AttackDamage.Last().Value) - 1);
            Stats.ExpGivenOnDeath.IncPercentBonusPerm(dataFile.Experience.GetValueOrDefault(MinionLevel, dataFile.Experience.Last().Value) - 1);
            Stats.GoldGivenOnDeath.IncPercentBonusPerm(dataFile.Gold.GetValueOrDefault(MinionLevel, dataFile.Gold.Last().Value) - 1);
            Stats.HealthPoints.IncPercentBonusPerm(dataFile.Health.GetValueOrDefault(MinionLevel, dataFile.Health.Last().Value) - 1);
            Stats.MagicResist.IncPercentBonusPerm(dataFile.MagicResist.GetValueOrDefault(MinionLevel, dataFile.MagicResist.Last().Value) - 1);

            if (setFullHealth)
            {
                Stats.CurrentHealth = Stats.HealthPoints.Total;
            }
        }
    }
}