namespace Buffs
{
    public class OrianaDoT : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShadowWalk",
            BuffTextureName = "Blitzcrank_StaticField.dds",
        };
        float tickDamage;
        float lastTimeExecuted;
        public OrianaDoT(float tickDamage = default)
        {
            this.tickDamage = tickDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.tickDamage);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, tickDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, 0, false, false, attacker);
                AddBuff(attacker, attacker, new Buffs.OrianaPowerDagger(), 3, 1, 6, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}