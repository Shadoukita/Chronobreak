namespace Buffs
{
    public class ForceofNature : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForceofNature",
            BuffTextureName = "124_Gladiators_Pride.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if (healthPercent <= 1)
                {
                    float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                    float healthToInc = maxHealth * 0.0035f;
                    IncHealth(owner, healthToInc, owner);
                }
            }
        }
    }
}