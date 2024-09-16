namespace Buffs
{
    public class NashorsToothSpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NashorsTooth",
            BuffTextureName = "MasterYi_LeapStrike.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, 35);
        }
    }
}