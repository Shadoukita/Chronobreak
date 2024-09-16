namespace Buffs
{
    public class RenektonTargetDiced : BuffScript
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