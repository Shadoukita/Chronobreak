namespace Buffs
{
    public class HextechRevolver : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "WillOfTheAncientsFriendly",
            BuffTextureName = "2008_Tome_of_Combat_Mastery.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentSpellVampMod(owner, 0.15f);
        }
    }
}