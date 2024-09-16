namespace Buffs
{
    public class UrgotCorrosiveDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "UrgotCorrosiveDamage",
            BuffTextureName = "UrgotCorrosiveCharge.dds",
            IsDeathRecapSource = true,
        };
        EffectEmitter particle1;
        float tickDamage;
        float lastTimeExecuted;
        public UrgotCorrosiveDebuff(float tickDamage = default)
        {
            this.tickDamage = tickDamage;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out particle1, out _, "UrgotCorrosiveDebuff_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.tickDamage);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, tickDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
        }
    }
}