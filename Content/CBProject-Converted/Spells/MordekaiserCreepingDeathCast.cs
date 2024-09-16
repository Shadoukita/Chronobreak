namespace Spells
{
    public class MordekaiserCreepingDeathCast : SpellScript
    {
        int[] effect0 = { 26, 32, 38, 44, 50 };
        int[] effect1 = { 24, 38, 52, 66, 80 };
        int[] effect2 = { 10, 15, 20, 25, 30 };
        public override void SelfExecute()
        {
            float healthCost = effect0[level - 1];
            float temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, target, new Buffs.MordekaiserCreepingDeathCheck(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (target == owner)
            {
                int nextBuffVars_DamagePerTick = effect1[level - 1];
                float nextBuffVars_DefenseStats = effect2[level - 1];
                AddBuff(owner, target, new Buffs.MordekaiserCreepingDeath(nextBuffVars_DamagePerTick, nextBuffVars_DefenseStats), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
                SpellCast(owner, target, default, default, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
            }
        }
    }
}