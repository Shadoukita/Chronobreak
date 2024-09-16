namespace Buffs
{
    public class ViktorAugmentW : BuffScript
    {
        public override void OnActivate()
        {
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ViktorGravitonFieldAugment));
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.1f);
        }
    }
}