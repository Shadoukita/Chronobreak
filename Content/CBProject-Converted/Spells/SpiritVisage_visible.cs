namespace Buffs
{
    public class SpiritVisage_visible : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spirit Visage",
            BuffTextureName = "3065_Spirit_Visage.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if (healthPercent <= 0.4f)
                {
                    float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                    float healthToInc = maxHealth * 0.01f;
                    IncHealth(owner, healthToInc, owner);
                }
            }
        }
    }
}