namespace Buffs
{
    public class YorickGhoulCounter2 : BuffScript
    {
        public override void OnActivate()
        {
            int yorickLevel = GetLevel(owner);
            float yorickAP = GetFlatMagicDamageMod(owner);
            float healthFromAP = yorickAP * 1;
            float aDFromAP = yorickAP * 0.2f;
            float healthFromLevel = 50 * yorickLevel;
            float aDFromLevel = 2 * yorickLevel;
            float totalAD = aDFromLevel + aDFromAP;
            float totalHealth = healthFromLevel + healthFromAP;
            IncPermanentFlatHPPoolMod(attacker, totalHealth);
            IncPermanentFlatPhysicalDamageMod(attacker, totalAD);
            IncPermanentFlatArmorMod(attacker, totalAD);
            IncPermanentFlatSpellBlockMod(attacker, totalAD);
        }
        public override void OnDeactivate(bool expired)
        {
            if (!IsDead(attacker))
            {
                ApplyDamage(attacker, attacker, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
            }
        }
    }
}