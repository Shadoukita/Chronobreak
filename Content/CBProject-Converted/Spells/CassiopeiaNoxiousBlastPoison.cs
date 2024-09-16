namespace Buffs
{
    public class CassiopeiaNoxiousBlastPoison : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Global_Poison.troy", },
            BuffName = "CassiopeiaNoxiousBlast",
            BuffTextureName = "Cassiopeia_NoxiousBlast.dds",
        };
        float damagePerTick;
        float lastTimeExecuted;
        public CassiopeiaNoxiousBlastPoison(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                ApplyDamage(attacker, target, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
            }
        }
    }
}