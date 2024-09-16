namespace Buffs
{
    public class MissFortuneBulletSound : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MissFortuneBulletSound",
            BuffTextureName = "MissFortune_BulletTime.dds",
        };
    }
}