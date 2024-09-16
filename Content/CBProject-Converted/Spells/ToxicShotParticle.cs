namespace Buffs
{
    public class ToxicShotParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Global_poison.troy", },
            BuffName = "Toxic Shot",
            BuffTextureName = "Teemo_PoisonedDart.dds",
            IsDeathRecapSource = true,
        };
        float damagePerTick;
        float damagePerTickFirst;
        float lastTimeExecuted;
        public ToxicShotParticle(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            damagePerTickFirst = damagePerTick * 1.5f;
            ApplyDamage(attacker, owner, damagePerTickFirst, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0.14f, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0.14f, 1, false, false, attacker);
            }
        }
    }
}