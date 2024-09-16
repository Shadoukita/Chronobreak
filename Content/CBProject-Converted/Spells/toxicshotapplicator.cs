namespace Buffs
{
    public class Toxicshotapplicator : BuffScript
    {
        int[] effect0 = { 6, 12, 18, 24, 30 };
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret && hitResult != HitResult.HIT_Miss && hitResult != HitResult.HIT_Dodge)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_DamagePerTick = effect0[level - 1];
                AddBuff((ObjAIBase)owner, target, new Buffs.ToxicShotParticle(nextBuffVars_DamagePerTick), 1, 1, 5.1f, BuffAddType.REPLACE_EXISTING, BuffType.POISON, 0, true, false, false);
            }
        }
    }
}