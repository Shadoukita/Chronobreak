namespace Buffs
{
    public class Malice_marker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}