namespace CharScripts
{
    public class CharScriptTwitch : CharScript
    {
        float[] effect0 = { 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 5, 5, 5, 5, 5, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 10, 10, 10 };
        public override void SetVarsByLevel()
        {
            charVars.DamageAmount = effect0[level - 1];
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff(attacker, target, new Buffs.DeadlyVenom(), 6, 1, 6.1f, BuffAddType.STACKS_AND_RENEWS, BuffType.POISON, 0, true, false);
                float nextBuffVars_DamageAmount = charVars.DamageAmount;
                int nextBuffVars_LastCount = 1;
                AddBuff(attacker, target, new Buffs.DeadlyVenom_Internal(nextBuffVars_DamageAmount, nextBuffVars_LastCount), 1, 1, 6.1f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.DeadlyVenom_marker(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}