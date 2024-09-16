namespace Buffs
{
    public class SejuaniArcticAssaultBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "sejuani_arctic_assault_buf.troy", },
            BuffName = "SejuaniArcticAssaultBuff",
            BuffTextureName = "Annie_GhastlyShield.dds",
        };
        float defenses;
        public SejuaniArcticAssaultBuff(float defenses = default)
        {
            this.defenses = defenses;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            //RequireVar(this.defenses);
            IncFlatSpellBlockMod(owner, defenses);
            IncFlatArmorMod(owner, defenses);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, defenses);
            IncFlatArmorMod(owner, defenses);
        }
    }
}