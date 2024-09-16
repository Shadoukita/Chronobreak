namespace Buffs
{
    public class Feast_internal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float[] effect0 = { 0.07f, 0.11f, 0.15f };
        int[] effect2 = { 90, 120, 150 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float sizeByLevel = effect0[level - 1]; // UNUSED
            IncScaleSkinCoef(0.5f, owner);
        }
        public override void OnUpdateStats()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
            if (count == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float sizeByLevel = effect0[level - 1];
                float bonus = count * sizeByLevel;
                IncScaleSkinCoef(bonus, owner);
                float healthPerStack = effect2[level - 1];
                float bonusHealth = healthPerStack * count;
                IncFlatHPPoolMod(owner, bonusHealth);
            }
        }
    }
}