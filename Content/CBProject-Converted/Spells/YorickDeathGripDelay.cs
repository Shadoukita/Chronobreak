namespace Buffs
{
    public class YorickDeathGripDelay : BuffScript
    {
        int damageToDeal;
        Vector3 pos;
        int[] effect0 = { 5, 7, 9 };
        public YorickDeathGripDelay(int damageToDeal = default, Vector3 pos = default)
        {
            this.damageToDeal = damageToDeal;
            this.pos = pos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageToDeal);
            //RequireVar(this.pos);
        }
        public override void OnDeactivate(bool expired)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_DamageToDeal = damageToDeal;
            Vector3 nextBuffVars_Pos = pos;
            AddBuff(attacker, owner, new Buffs.YorickDeathGrip(nextBuffVars_DamageToDeal, nextBuffVars_Pos), 50, 1, effect0[level - 1], BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0.1f, true, false, false);
        }
    }
}