namespace Spells
{
    public class PantheonQ : Pantheon_Throw { }
    public class Pantheon_Throw : SpellScript
    {
        int[] effect0 = { 65, 105, 145, 185, 225 };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield2)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield)) == 0)
            {
                AddBuff(owner, owner, new Buffs.Pantheon_Aegis_Counter(), 5, 1, 25000, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, false, false, false);
                int count = GetBuffCountFromAll(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                if (count >= 4)
                {
                    AddBuff(owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                }
            }
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float atkDmg = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float throwDmg = effect0[level - 1];
            float bonusDamage = atkDmg - baseDamage;
            bonusDamage *= 1.4f;
            float finalDamage = bonusDamage + throwDmg;
            float maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float currentHP = GetHealth(target, PrimaryAbilityResourceType.MANA);
            float critHealth = maxHP * 0.15f;
            if (currentHP <= critHealth && GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_CertainDeath)) > 0)
            {
                finalDamage *= 1.5f;
            }
            ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
        }
    }
}