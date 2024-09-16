namespace ItemPassives
{
    public class ItemID_3082 : ItemScript
    {
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RanduinsOmen)) == 0 && RandomChance() < 0.2f && attacker is not BaseTurret)
            {
                float nextBuffVars_MoveSpeedMod = -0.35f;
                AddBuff(owner, attacker, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
                float nextBuffVars_AttackSpeedMod = -0.35f;
                AddBuff(owner, attacker, new Buffs.Cripple(nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.COMBAT_DEHANCER, 0, true, false);
            }
        }
    }
}