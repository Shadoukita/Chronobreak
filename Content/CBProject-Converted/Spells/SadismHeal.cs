namespace Buffs
{
    public class SadismHeal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        float lastTimeExecuted;
        float[] effect0 = { 0.01667f, 0.02292f, 0.02917f };
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float factor = effect0[level - 1];
            float heal = factor * maxHealth;
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                IncHealth(owner, heal, owner);
            }
        }
    }
}