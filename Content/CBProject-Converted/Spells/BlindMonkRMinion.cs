namespace Buffs
{
    public class BlindMonkRMinion : BuffScript
    {
        Vector3 targetPos;
        public BlindMonkRMinion(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 ownerPos = GetUnitPosition(owner);
            Vector3 targetPos = this.targetPos;
            SpellCast((ObjAIBase)owner, attacker, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, true, ownerPos);
        }
    }
}