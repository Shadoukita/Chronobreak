namespace Buffs
{
    public class CannibalismMaxHPGained : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CannibalismMaxHPGained",
            BuffTextureName = "Sion_SpiritFeast.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float cannibalismMaxHPMod;
        public CannibalismMaxHPGained(float cannibalismMaxHPMod = default)
        {
            this.cannibalismMaxHPMod = cannibalismMaxHPMod;
        }
        public override void OnActivate()
        {
            charVars.CannibalismMaxHPMod += cannibalismMaxHPMod;
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, charVars.CannibalismMaxHPMod);
        }
    }
}