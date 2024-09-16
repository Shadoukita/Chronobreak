
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using PARType = GameServerCore.Enums.PrimaryAbilityResourceType;

namespace Chronobreak.GameServer.Scripting.Lua;

public static partial class Functions
{
    [BBFunc]
    public static float GetTotalAttackDamage(AttackableUnit target)
    {
        return target.Stats.AttackDamage.Total;
    }

    public static float GetBaseAttackDamage(AttackableUnit target)
    {
        return target.Stats.AttackDamage.TotalBase;
    }

    public static float GetFlatPhysicalDamageMod(AttackableUnit target)
    {
        return target.Stats.AttackDamage.TotalBonus;
    }

    public static void IncFlatPhysicalDamageMod(AttackableUnit target, float delta)
    {
        target.Stats.AttackDamage.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatPhysicalDamageMod(AttackableUnit target, float delta)
    {
        target.Stats.AttackDamage.IncFlatBonusPerm(delta);
    }

    public static void IncPercentPhysicalDamageMod(AttackableUnit target, float delta)
    {
        target.Stats.AttackDamage.IncPercentBonus(delta);
    }

    public static void IncPermanentPercentPhysicalDamageMod(AttackableUnit target, float delta)
    {
        target.Stats.AttackDamage.IncPercentBonusPerm(delta);
    }

    public static float GetDodge(AttackableUnit target)
    {
        return target.Stats.DodgeChance.Total;
    }

    public static void IncFlatDodgeMod(AttackableUnit target, float delta)
    {
        target.Stats.DodgeChance.IncPercentBonus(delta);
    }

    public static void IncFlatMissChanceMod(AttackableUnit target, float delta)
    {
        target.Stats.MissChance.IncPercentBonus(delta);
    }

    public static float GetFlatAttackRangeMod(AttackableUnit target)
    {
        return target.Stats.Range.Total;
    }

    public static void IncFlatAttackRangeMod(AttackableUnit target, float delta)
    {
        target.Stats.Range.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatAttackRangeMod(AttackableUnit target, float delta)
    {
        target.Stats.Range.IncFlatBonusPerm(delta);
    }

    public static void IncFlatBubbleRadiusMod(AttackableUnit target, float delta)
    {
        target.Stats.PerceptionRange.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatBubbleRadiusMod(AttackableUnit target, float delta)
    {
        target.Stats.PerceptionRange.IncFlatBonusPerm(delta);
    }

    public static void IncPercentBubbleRadiusMod(AttackableUnit target, float delta)
    {
        target.Stats.PerceptionRange.IncPercentBonus(delta);
    }

    public static void IncPermanentPercentBubbleRadiusMod(AttackableUnit target, float delta)
    {
        target.Stats.PerceptionRange.IncPercentBonusPerm(delta);
    }

    public static void IncAcquisitionRangeMod(AttackableUnit target, float delta)
    {
        target.Stats.AcquisitionRange.IncFlatBonus(delta);
    }

    public static float GetFlatCritChanceMod(AttackableUnit target)
    {
        return target.Stats.CriticalChance.Total;
    }

    public static void IncFlatCritChanceMod(AttackableUnit target, float delta)
    {
        target.Stats.CriticalChance.IncPercentBonus(delta);
    }

    public static void IncPermanentFlatCritChanceMod(AttackableUnit target, float delta)
    {
        target.Stats.CriticalChance.IncPercentBonusPerm(delta);
    }

    public static float GetFlatCritDamageMod(AttackableUnit target)
    {
        return target.Stats.CriticalDamage.Total;
    }

    public static void IncFlatCritDamageMod(AttackableUnit target, float delta)
    {
        target.Stats.CriticalDamage.IncPercentBonus(delta);
    }

    [BBFunc]
    public static void IncHealth(AttackableUnit target, float delta, AttackableUnit healer)
    {
        //target.Stats.CurrentHealth += delta;
        target.TakeHeal(new HealData(target, delta, healer));
    }

    public static float GetHealth(AttackableUnit owner, PARType PARType = 0)
    {
        return //!isCompatablePARs(owner.Stats.PrimaryAbilityResourceType, PARType) ? 0 :
            owner.Stats.CurrentHealth;
    }

    public static float GetHealthPercent(AttackableUnit owner, PARType PARType = 0)
    {
        return //!isCompatablePARs(owner.Stats.PrimaryAbilityResourceType, PARType) ? 0 :
            owner.Stats.CurrentHealth / owner.Stats.HealthPoints.Total;
    }

    [BBFunc]
    public static void IncMaxHealth(AttackableUnit target, float delta, bool incCurrentHealth)
    {
        target.Stats.HealthPoints.IncFlatBonus(delta);
        if (incCurrentHealth) target.Stats.CurrentHealth += delta;
    }

    public static float GetMaxHealth(AttackableUnit owner, PARType PARType = 0)
    {
        return //(owner.Stats.PrimaryAbilityResourceType != PARType) ? 0 :
            owner.Stats.HealthPoints.Total;
    }

    public static float GetFlatHPPoolMod(AttackableUnit target)
    {
        return target.Stats.HealthPoints.FlatBonus;
    }

    public static void IncFlatHPPoolMod(AttackableUnit target, float delta)
    {
        target.Stats.HealthPoints.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatHPPoolMod(AttackableUnit target, float delta)
    {
        var s = target.Stats;
        var prevHealth = s.HealthPoints.Total;
        target.Stats.HealthPoints.IncFlatBonusPerm(delta);
        s.CurrentHealth += s.HealthPoints.Total - prevHealth;
    }

    public static void IncPercentHPPoolMod(AttackableUnit target, float delta)
    {
        target.Stats.HealthPoints.IncPercentBonus(delta);
    }

    public static void IncFlatHPRegenMod(AttackableUnit target, float delta)
    {
        target.Stats.HealthRegeneration.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatHPRegenMod(AttackableUnit target, float delta)
    {
        target.Stats.HealthRegeneration.IncFlatBonusPerm(delta);
    }

    public static void IncPercentHPRegenMod(AttackableUnit target, float delta)
    {
        target.Stats.HealthRegeneration.IncPercentBonus(delta);
    }

    public static void IncPermanentPercentHPRegenMod(AttackableUnit target, float delta)
    {
        target.Stats.HealthRegeneration.IncPercentBonusPerm(delta);
    }

    private static bool isCompatablePARs(PARType a, PARType b)
    {
        return a == b ||
               // Presumably, the only types known to the old scripts
               (!(a is PARType.MANA or PARType.Energy or PARType.Shield) && //TODO: Verify
                b == PARType.Other);
    }

    [BBFunc]
    public static void IncPAR(AttackableUnit target, float delta, PARType PARType = 0)
    {
        if (isCompatablePARs(target.Stats.PrimaryAbilityResourceType, PARType))
            target.RestorePAR(delta);
    }

    public static float GetPAR(AttackableUnit owner, PARType PARType = 0)
    {
        return !isCompatablePARs(owner.Stats.PrimaryAbilityResourceType, PARType) ? 0 : owner.Stats.CurrentMana;
    }

    public static float GetPARPercent(AttackableUnit owner, PARType PARType = 0)
    {
        return !isCompatablePARs(owner.Stats.PrimaryAbilityResourceType, PARType)
            ? 0
            : owner.Stats.CurrentMana / owner.Stats.ManaPoints.Total;
    }

    public static float GetMaxPAR(AttackableUnit owner, PARType PARType = 0)
    {
        return !isCompatablePARs(owner.Stats.PrimaryAbilityResourceType, PARType) ? 0 : owner.Stats.ManaPoints.Total;
    }

    [BBFunc]
    public static void IncFlatPARPoolMod(AttackableUnit target, float delta, PARType PARType = 0)
    {
        if (isCompatablePARs(target.Stats.PrimaryAbilityResourceType, PARType))
            target.Stats.ManaPoints.IncFlatBonus(delta);
    }

    [BBFunc]
    public static void IncPermanentFlatPARPoolMod(AttackableUnit target, float delta, PARType PARType = 0)
    {
        //if (isCompatablePARs(target.Stats.PrimaryAbilityResourceType, PARType))
        {
            var s = target.Stats;
            var prevMana = s.ManaPoints.Total;
            target.Stats.ManaPoints.IncFlatBonusPerm(delta);
            s.CurrentMana += s.ManaPoints.Total - prevMana;
        }
    }

    [BBFunc]
    public static void IncPercentPARPoolMod(AttackableUnit target, float delta, PARType PARType = 0)
    {
        //if (isCompatablePARs(target.Stats.PrimaryAbilityResourceType, PARType))
        target.Stats.ManaPoints.IncPercentBonus(delta);
    }

    [BBFunc]
    public static void IncFlatPARRegenMod(AttackableUnit target, float delta, PARType PARType = 0)
    {
        //if (isCompatablePARs(target.Stats.PrimaryAbilityResourceType, PARType))
        target.Stats.ManaRegeneration.IncFlatBonus(delta);
    }

    [BBFunc]
    public static void IncPermanentFlatPARRegenMod(AttackableUnit target, float delta, PARType PARType = 0)
    {
        //if (isCompatablePARs(target.Stats.PrimaryAbilityResourceType, PARType))
        target.Stats.ManaRegeneration.IncFlatBonusPerm(delta);
    }

    [BBFunc]
    public static void IncPercentPARRegenMod(AttackableUnit target, float delta, PARType PARType = 0)
    {
        //if (isCompatablePARs(target.Stats.PrimaryAbilityResourceType, PARType))
        target.Stats.ManaRegeneration.IncPercentBonus(delta);
    }

    public static float GetFlatMagicDamageMod(AttackableUnit target)
    {
        return target.Stats.AbilityPower.Total;
    }

    public static void IncFlatMagicDamageMod(AttackableUnit target, float delta)
    {
        target.Stats.AbilityPower.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatMagicDamageMod(AttackableUnit target, float delta)
    {
        target.Stats.AbilityPower.IncFlatBonusPerm(delta);
    }

    public static void IncPercentMagicDamageMod(AttackableUnit target, float delta)
    {
        target.Stats.AbilityPower.IncPercentBonus(delta);
    }

    public static float GetMovementSpeed(AttackableUnit target)
    {
        return target.Stats.MoveSpeed.Total;
    }

    public static float GetFlatMovementSpeedMod(AttackableUnit target)
    {
        return target.Stats.MoveSpeed.FlatBonus;
    }

    public static void IncFlatMovementSpeedMod(AttackableUnit target, float delta)
    {
        target.Stats.MoveSpeed.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatMovementSpeedMod(AttackableUnit target, float delta)
    {
        target.Stats.MoveSpeed.IncFlatBonusPerm(delta);
    }

    public static void IncPercentMovementSpeedMod(AttackableUnit target, float delta)
    {
        target.Stats.MoveSpeed.IncBonusPercent(delta);
    }

    public static void IncPermanentPercentMovementSpeedMod(AttackableUnit target, float delta)
    {
        target.Stats.MoveSpeed.IncBonusPercentPerm(delta);
    }

    public static void IncPercentMultiplicativeMovementSpeedMod(AttackableUnit target, float delta)
    {
        target.Stats.MoveSpeed.IncBonusMultiplicativePercent(delta);
    }

    public static void IncMoveSpeedFloorMod(AttackableUnit target, float delta)
    {
        target.Stats.MoveSpeed.IncFlatBonus(delta); //TODO: Floor
    }

    public static float GetPercentAttackSpeedMod(AttackableUnit target)
    {
        return target.Stats.AttackSpeedMultiplier.Total;
    }

    public static void IncPercentAttackSpeedMod(AttackableUnit target, float delta)
    {
        target.Stats.AttackSpeedMultiplier.IncPercentBonus(delta);
    }

    public static void IncPermanentPercentAttackSpeedMod(AttackableUnit target, float delta)
    {
        target.Stats.AttackSpeedMultiplier.IncPercentBonusPerm(delta);
    }

    public static void IncPercentMultiplicativeAttackSpeedMod(AttackableUnit target, float delta)
    {
        target.Stats.AttackSpeedMultiplier.IncPercentMultiplicativeBonus(delta);
    }
    public static float GetPercentCooldownMod(AttackableUnit target)
    {
        return -target.Stats.CooldownReduction.Total;
    }

    public static void IncPercentCooldownMod(AttackableUnit target, float delta)
    {
        target.Stats.CooldownReduction.IncPercentBonus(-delta);
    }

    public static void IncPermanentPercentCooldownMod(AttackableUnit target, float delta)
    {
        target.Stats.CooldownReduction.IncPercentBonusPerm(-delta);
    }

    public static float GetPercentHardnessMod(AttackableUnit target)
    {
        return target.Stats.Tenacity.Total;
    }

    public static float GetPercentLifeStealMod(AttackableUnit target)
    {
        return target.Stats.LifeSteal.Total;
    }

    public static void IncPercentLifeStealMod(AttackableUnit target, float delta)
    {
        target.Stats.LifeSteal.IncPercentBonus(delta);
    }

    public static float GetPercentRespawnTimeMod(AttackableUnit target)
    {
        return 0; //TODO: Implement.
    }

    public static void IncPercentRespawnTimeMod(AttackableUnit target, float delta)
    {
        //TODO: Implement.
    }

    [BBFunc]
    public static float GetArmor(AttackableUnit target)
    {
        return target.Stats.Armor.Total;
    }

    public static void IncFlatArmorMod(AttackableUnit target, float delta)
    {
        target.Stats.Armor.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatArmorMod(AttackableUnit target, float delta)
    {
        target.Stats.Armor.IncFlatBonusPerm(delta);
    }

    public static void IncPercentArmorMod(AttackableUnit target, float delta)
    {
        target.Stats.Armor.IncPercentBonus(delta);
    }

    public static void IncFlatArmorPenetrationMod(AttackableUnit target, float delta)
    {
        target.Stats.ArmorPenetration.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatArmorPenetrationMod(AttackableUnit target, float delta)
    {
        target.Stats.ArmorPenetration.IncFlatBonusPerm(delta);
    }

    public static void IncPercentArmorPenetrationMod(AttackableUnit target, float delta)
    {
        target.Stats.ArmorPenetration.IncPercentBonus(delta);
    }

    public static void IncFlatPhysicalReduction(AttackableUnit target, float delta)
    {
        target.Stats.PhysicalReduction.IncFlatBonus(delta);
    }

    public static void IncPercentPhysicalReduction(AttackableUnit target, float delta)
    {
        target.Stats.PhysicalReduction.IncPercentBonus(delta);
    }

    public static void IncFlatSpellBlockMod(AttackableUnit target, float delta)
    {
        target.Stats.MagicResist.IncFlatBonus(delta);
    }

    public static void IncPermanentFlatSpellBlockMod(AttackableUnit target, float delta)
    {
        target.Stats.MagicResist.IncFlatBonusPerm(delta);
    }

    public static float GetPercentSpellBlockMod(AttackableUnit target)
    {
        //return target.Stats.MagicResist.TotalPercent;
        //TODO: Implement.
        return 0;
    }

    public static void IncPercentSpellBlockMod(AttackableUnit target, float delta)
    {
        target.Stats.MagicResist.IncPercentBonus(delta);
    }

    public static void IncFlatMagicPenetrationMod(AttackableUnit target, float delta)
    {
        target.Stats.MagicPenetration.IncFlatBonus(delta);
    }

    public static void IncPercentMagicPenetrationMod(AttackableUnit target, float delta)
    {
        target.Stats.MagicPenetration.IncPercentBonus(delta);
    }

    public static void IncFlatMagicReduction(AttackableUnit target, float delta)
    {
        target.Stats.MagicalReduction.IncFlatBonus(delta);
    }

    public static void IncPercentMagicReduction(AttackableUnit target, float delta)
    {
        target.Stats.MagicalReduction.IncPercentBonus(delta);
    }

    public static float GetPercentSpellVampMod(AttackableUnit target)
    {
        return target.Stats.SpellVamp.Total;
    }

    public static void IncPercentSpellVampMod(AttackableUnit target, float delta)
    {
        target.Stats.SpellVamp.IncPercentBonus(delta);
    }

    public static void IncFlatGoldPer10Mod(AttackableUnit target, float delta)
    {
        target.Stats.GoldPerSecond.IncFlatBonus(delta * 0.1f);
    }

    public static void IncPermanentFlatGoldPer10Mod(AttackableUnit target, float delta)
    {
        target.Stats.GoldPerSecond.IncFlatBonusPerm(delta * 0.1f);
    }

    [BBFunc]
    public static void IncPermanentGoldReward(AttackableUnit target, float delta)
    {
        target.Stats.GoldGivenOnDeath.IncFlatBonusPerm(delta);
    }

    [BBFunc]
    public static void IncPermanentExpReward(AttackableUnit target, float delta)
    {
        target.Stats.ExpGivenOnDeath.IncFlatBonusPerm(delta);
    }

    public static void IncPercentEXPBonus(AttackableUnit target, float delta)
    {
        target.Stats.ExpBonus.IncPercentBonus(delta);
    }

    public static void IncPermanentPercentEXPBonus(AttackableUnit target, float delta)
    {
        target.Stats.ExpBonus.IncPercentBonusPerm(delta);
    }

    [BBFunc]
    public static void IncScaleSkinCoef(float scale, AttackableUnit owner) //TODO: Swap params
    {
        owner.Stats.Size.IncPercentBonus(scale);
    }
}