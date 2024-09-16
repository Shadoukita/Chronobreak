namespace ItemPassives
{
    public class ItemID_3100 : ItemScript
    {
        int cooldownResevoir; // UNUSED
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts && GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
            {
                float abilityPower = GetFlatMagicDamageMod(owner);
                float baseDamage = GetBaseAttackDamage(owner);
                float nextBuffVars_BaseDamage = baseDamage; // UNUSED
                float nextBuffVars_AbilityPower = abilityPower;
                AddBuff(owner, owner, new Buffs.LichBane(nextBuffVars_AbilityPower), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            cooldownResevoir = 0;
        }
    }
}