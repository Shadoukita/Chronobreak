namespace Buffs
{
    public class MaokaiSapMagicChaos : BuffScript
    {
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                ObjAIBase attacker = GetBuffCasterUnit();
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MaokaiSapMagicMelee)) == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.MaokaiSapMagicHot(), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
                }
            }
        }
    }
}