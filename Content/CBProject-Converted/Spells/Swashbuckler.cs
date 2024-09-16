namespace Buffs
{
    public class Swashbuckler : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Swashbuckler",
            BuffTextureName = "MasterYi_DoubleStrike.dds",
            NonDispellable = true,
        };
        /*
        //TODO: Uncomment and fix
        public override void OnHitUnit(AttackableUnit target, float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase && default is not BaseTurret)
            {
                float targetHealth = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if(targetHealth <= 0.3f)
                {
                    damageAmount *= 1.3f;
                }
            }
        }
        */
    }
}