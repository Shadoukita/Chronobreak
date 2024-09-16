namespace Buffs
{
    public class StructureDamageReduction : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "",
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is not ObjAIBase)
            {
                damageAmount *= 0.25f;
            }
        }
    }
}