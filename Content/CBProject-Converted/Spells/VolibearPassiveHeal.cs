namespace Buffs
{
    public class VolibearPassiveHeal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "volibear_passive_heal.troy", },
            BuffName = "VolibearPassiveHeal",
            BuffTextureName = "VolibearPassive.dds",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float factor = charVars.RegenPercent * 0.08333f;
            float heal = factor * maxHealth;
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                IncHealth(owner, heal, owner);
            }
        }
    }
}