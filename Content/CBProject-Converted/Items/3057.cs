namespace ItemPassives
{
    public class ItemID_3057 : ItemScript
    {
        int cooldownResevoir; // UNUSED
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts && GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
            {
                float baseDamage = GetBaseAttackDamage(owner);
                float nextBuffVars_BaseDamage = baseDamage;
                bool nextBuffVars_IsSheen = true;
                AddBuff(owner, owner, new Buffs.Sheen(nextBuffVars_BaseDamage, nextBuffVars_IsSheen), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            cooldownResevoir = 0;
        }
    }
}