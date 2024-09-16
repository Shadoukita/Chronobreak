namespace Buffs
{
    public class RumbleGrenadeCD : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RumbleGrenadeAmmo",
            BuffTextureName = "Heimerdinger+HextechMicroRockets.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
    }
}