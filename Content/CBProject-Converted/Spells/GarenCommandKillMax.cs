namespace Buffs
{
    public class GarenCommandKillMax : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CannibalismMaxHPGained",
            BuffTextureName = "Sion_SpiritFeast.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float hPGain;
        public GarenCommandKillMax(float hPGain = default)
        {
            this.hPGain = hPGain;
        }
        public override void OnActivate()
        {
            charVars.HPGain += hPGain;
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, charVars.HPGain);
        }
    }
}