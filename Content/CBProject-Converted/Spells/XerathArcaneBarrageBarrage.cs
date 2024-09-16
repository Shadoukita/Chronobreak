namespace Buffs
{
    public class XerathArcaneBarrageBarrage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XerathBarrage",
            BuffTextureName = "Xerath_ArcaneBarrage.dds",
        };
        int[] effect0 = { 80, 70, 60, 40, 40 };
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.XerathArcaneBarrageBarrage)) == 0)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float spellCooldown = effect0[level - 1];
                float cooldownStat = GetPercentCooldownMod(owner);
                float multiplier = 1 + cooldownStat;
                float newCooldown = multiplier * spellCooldown;
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            }
        }
    }
}