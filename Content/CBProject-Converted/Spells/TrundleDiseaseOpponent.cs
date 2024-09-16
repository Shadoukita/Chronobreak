namespace Buffs
{
    public class TrundleDiseaseOpponent : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleDiseaseOpponent",
            BuffTextureName = "Twitch_DeadlyVenom_temp.dds",
        };
        float debuffAmount;
        public TrundleDiseaseOpponent(float debuffAmount = default)
        {
            this.debuffAmount = debuffAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.debuffAmount);
            IncFlatArmorMod(owner, debuffAmount);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, debuffAmount);
        }
    }
}