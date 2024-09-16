using System;
using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.StatsNS;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;

public class ObjBuilding : AttackableUnit
{
    //Riot::FrameTimer DeathTimer
    protected float Armor;
    protected float BaseStaticHPRegen;
    //objProp_Place propPlace;
    protected bool LoadedFromSerializer;
    internal bool HealthRegenEnabled
    {
        get => Stats.RegenEnabled;
        set => Stats.RegenEnabled = value;
    }
    List<AttackableUnit> Parents = [];
    public override bool IsAffectedByFoW => false;

    public ObjBuilding(
        string name,
        string model,
        int collisionRadius = 40,
        Vector2 position = new(),
        int visionRadius = 0,
        uint netId = 0,
        TeamId team = TeamId.TEAM_ORDER,
        Stats stats = null) :
        base(name, model, collisionRadius, position, visionRadius, netId, team, stats)
    {
        CollisionType = CollisionType.Unk2;
        Stats.HealthPoints.BaseValue = 100000000;
        HealthRegenEnabled = true;
        Stats.CurrentHealth = Stats.HealthPoints.Total;
        if (!IsLifestealImmune)
        {
            IsLifestealImmune = true;
        }
    }

    //TODO: Deduplicate
    public override float GetPostMitigationDamage(float damage, DamageType type, AttackableUnit attacker)
    {
        if (damage <= 0f)
        {
            return 0.0f;
        }

        float stat;
        switch (type)
        {
            case DamageType.DAMAGE_TYPE_PHYSICAL:
                stat = Stats.Armor.Total;
                break;
            case DamageType.DAMAGE_TYPE_MAGICAL:
                stat = Stats.MagicResist.Total;
                break;
            case DamageType.DAMAGE_TYPE_TRUE:
                return damage;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        float mitigationPercent;
        if (stat >= 0)
        {
            mitigationPercent = 100 / (100 + stat);
        }
        else
        {
            mitigationPercent = 2 - (100 / (100 - stat));
        }

        return damage * mitigationPercent;
    }

    internal virtual Lane GetLane()
    {
        return Lane.LANE_Unknown;
    }

    internal override float GetAttackRatioWhenAttackingTurret()
    {
        //Yes, this is right
        return GlobalData.DamageRatios.BuildingToHero;
    }
    internal override float GetAttackRatioWhenAttackingMinion()
    {
        return GlobalData.DamageRatios.BuildingToUnit;
    }
    internal override float GetAttackRatioWhenAttackingChampion()
    {
        return GlobalData.DamageRatios.BuildingToHero;
    }
    internal override float GetAttackRatioWhenAttackingBuilding()
    {
        return GlobalData.DamageRatios.BuildingToBuilding;
    }
    internal override float GetAttackRatio(AttackableUnit attackingUnit)
    {
        return attackingUnit.GetAttackRatioWhenAttackingBuilding();
    }
}
