namespace Buffs
{
    public class AuraofDespairDrainLife : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "Despair_tar.troy", },
            BuffName = "AuraofDespairDamage",
            BuffTextureName = "SadMummy_AuraofDespair.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lifeLossPercent;
        float lastTimeExecuted;
        public AuraofDespairDrainLife(float lifeLossPercent = default)
        {
            this.lifeLossPercent = lifeLossPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifeLossPercent);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float temp1 = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float percentDamage = temp1 * lifeLossPercent;
                ApplyDamage(attacker, owner, percentDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, default, false, false);
            }
        }
    }
}