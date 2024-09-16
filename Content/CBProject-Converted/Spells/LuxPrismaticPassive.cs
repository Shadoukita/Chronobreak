namespace Buffs
{
    public class LuxPrismaticPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float cooldownBonus;
        float[] effect0 = { -0.02f, -0.04f, -0.06f, -0.08f, -0.1f };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldownBonus = effect0[level - 1];
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, cooldownBonus);
        }
    }
}