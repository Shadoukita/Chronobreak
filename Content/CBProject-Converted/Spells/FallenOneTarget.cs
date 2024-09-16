namespace Buffs
{
    public class FallenOneTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "FallenOne_tar.troy", },
            BuffName = "FallenOne",
            BuffTextureName = "Lich_DeathRay.dds",
            NonDispellable = true,
        };
    }
}