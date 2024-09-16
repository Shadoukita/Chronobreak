namespace Buffs
{
    public class KarmaSBStealthBreak : BuffScript
    {
        float cooldownToRestore;
        public KarmaSBStealthBreak(float cooldownToRestore = default)
        {
            this.cooldownToRestore = cooldownToRestore;
        }
        public override void OnActivate()
        {
            //RequireVar(this.cooldownToRestore);
            SetSlotSpellCooldownTimeVer2(cooldownToRestore, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}