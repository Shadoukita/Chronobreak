namespace Buffs
{
    public class Nevershade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Nevershade",
            BuffTextureName = "DrMundo_AdrenalineRush.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if (!IsDead(owner) && ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float regen = 0.003f;
                float healthInc = regen * maxHealth;
                IncHealth(owner, healthInc, owner);
            }
        }
    }
}