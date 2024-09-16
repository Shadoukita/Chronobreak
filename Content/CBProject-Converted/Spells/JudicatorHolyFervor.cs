namespace Buffs
{
    public class JudicatorHolyFervor : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "JudicatorHolyFervor",
            BuffTextureName = "Judicator_DivineBlessing.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}