namespace Buffs
{
    public class EmblemOfValor : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Emblem of Valour",
            BuffTextureName = "3052_Reverb_Coil.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatHPRegenMod(owner, 2);
        }
    }
}