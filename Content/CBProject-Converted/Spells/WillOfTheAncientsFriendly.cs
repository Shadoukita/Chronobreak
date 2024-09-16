namespace Buffs
{
    public class WillOfTheAncientsFriendly : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "WillOfTheAncientsFriendly",
            BuffTextureName = "2008_Tome_of_Combat_Mastery.dds",
        };
        float aP_Buff;
        float spellVamp_Buff;
        public WillOfTheAncientsFriendly(float aP_Buff = default, float spellVamp_Buff = default)
        {
            this.aP_Buff = aP_Buff;
            this.spellVamp_Buff = spellVamp_Buff;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aP_Buff);
            //RequireVar(this.spellVamp_Buff);
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, aP_Buff);
            IncPercentSpellVampMod(owner, spellVamp_Buff);
        }
    }
}