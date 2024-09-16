namespace Buffs
{
    public class MordekaiserSyphonDmg : BuffScript
    {
        int count;
        int[] effect0 = { 70, 115, 160, 205, 250 };
        float[] effect1 = { 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f };
        float[] effect2 = { 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f };
        public override void OnActivate()
        {
            //RequireVar(this.baseDamage);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            count = 0;
            ApplyDamage((ObjAIBase)owner, attacker, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (count == 0)
            {
                float percentLeech;
                int level = GetLevel(owner);
                if (target is Champion)
                {
                    percentLeech = effect1[level - 1];
                }
                else
                {
                    percentLeech = effect2[level - 1];
                }
                float shieldAmount = percentLeech * damageAmount;
                IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                count = 1;
            }
        }
    }
}