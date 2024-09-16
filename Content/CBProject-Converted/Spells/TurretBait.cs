namespace Buffs
{
    public class TurretBait : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Reinforce",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        float bonusArmor;
        public TurretBait(float bonusArmor = default)
        {
            this.bonusArmor = bonusArmor;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusArmor);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, bonusArmor);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is BaseTurret)
            {
                ApplyTaunt(owner, target, 5);
            }
        }
    }
}