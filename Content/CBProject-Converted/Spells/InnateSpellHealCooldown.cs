namespace Buffs
{
    public class InnateSpellHealCooldown : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Meditate",
            BuffTextureName = "MasterYi_Vanish.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}