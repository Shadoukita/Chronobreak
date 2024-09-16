namespace Buffs
{
    public class GragasPassiveHeal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GragasPassiveHeal",
            BuffTextureName = "GragasPassiveHeal.dds",
        };
        float healAmount;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            healAmount = maxHP * 0.01f;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1.9f, ref lastTimeExecuted, false))
            {
                IncHealth(owner, healAmount, owner);
            }
        }
    }
}