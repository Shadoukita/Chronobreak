namespace Spells
{
    public class PantheonBasicAttack : SpellScript
    {
        float[] effect0 = { 0.15f, 0.15f, 0.15f, 0.15f, 0.15f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_CertainDeath)) > 0 && target is ObjAIBase && target is not BaseTurret)
            {
                float tarHP = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float hpThreshold = effect0[level - 1];
                if (tarHP <= hpThreshold)
                {
                    hitResult = HitResult.HIT_Critical;
                }
            }
            float baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1, 1, false, false, attacker);
        }
    }
}