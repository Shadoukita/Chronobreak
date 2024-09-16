namespace Buffs
{
    public class MordekaiserCreepingDeathDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserCreepingDeathDebuff",
            BuffTextureName = "FallenAngel_TormentedSoil.dds",
        };
        float damagePerTick;
        int count;
        float[] effect0 = { 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f };
        float[] effect1 = { 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f };
        public MordekaiserCreepingDeathDebuff(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            count = 0;
            ApplyDamage((ObjAIBase)owner, attacker, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (count == 0)
            {
                float percentLeech;
                int level = GetLevel(owner);
                if (target is Champion)
                {
                    percentLeech = effect0[level - 1];
                }
                else
                {
                    percentLeech = effect1[level - 1];
                }
                float shieldAmount = percentLeech * damageAmount;
                IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                count = 1;
            }
        }
    }
}