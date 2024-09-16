namespace ItemPassives
{
    public class ItemID_3071 : ItemScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Miss && target is ObjAIBase && target is not BaseTurret)
            {
                int nextBuffVars_ArmorReduction = -15;
                AddBuff(attacker, target, new Buffs.BlackCleaver(nextBuffVars_ArmorReduction), 3, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
            }
        }
        public override void OnBeingDodged(ObjAIBase target)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                float nextBuffVars_ArmorReduction = -15;
                AddBuff(owner, target, new Buffs.BlackCleaver(nextBuffVars_ArmorReduction), 3, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class _3071 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BlackCleaver",
            BuffTextureName = "3071_The_Black_Cleaver.dds",
        };
    }
}