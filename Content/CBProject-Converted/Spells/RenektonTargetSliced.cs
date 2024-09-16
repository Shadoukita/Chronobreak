namespace Buffs
{
    public class RenektonTargetSliced : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "RenekthonStackHolder",
            BuffTextureName = "Cardmaster_RapidToss.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}