namespace Buffs
{
    public class OlafBerzerkerRage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OlafBerzerkerRage",
            BuffTextureName = "OlafBerserkerRage.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            float healthPerc = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            float aSPerc = 1 - healthPerc;
            IncPercentAttackSpeedMod(owner, aSPerc);
        }
    }
}