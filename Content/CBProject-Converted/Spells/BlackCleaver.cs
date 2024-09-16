namespace Buffs
{
    public class BlackCleaver : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Black Cleaver",
            BuffTextureName = "3071_The_Black_Cleaver.dds",
        };
        float armorReduction;
        public BlackCleaver(float armorReduction = default)
        {
            this.armorReduction = armorReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorReduction);
            SpellEffectCreate(out _, out _, "BlackCleave_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorReduction);
        }
    }
}