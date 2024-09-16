namespace Buffs
{
    public class RefreshUltimate : BuffScript
    {
        public override void OnUpdateActions()
        {
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 1);
        }
    }
}