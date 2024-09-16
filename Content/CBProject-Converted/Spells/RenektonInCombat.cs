namespace Buffs
{
    public class RenektonInCombat : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "RenekthonPredator",
            BuffTextureName = "Corki_RapidReload.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}