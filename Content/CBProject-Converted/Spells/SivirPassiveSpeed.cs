namespace Buffs
{
    public class SivirPassiveSpeed : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "spine", },
            AutoBuffActivateEffect = new[] { "Sivir_PassiveHaste.troy", },
            BuffName = "SivirPassiveSpeed",
            BuffTextureName = "Sivir_Sprint.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            IncFlatMovementSpeedMod(owner, 50);
        }
        public override void OnUpdateStats()
        {
            IncFlatMovementSpeedMod(owner, 50);
        }
    }
}