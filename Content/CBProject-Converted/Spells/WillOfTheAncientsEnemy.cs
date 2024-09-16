namespace Buffs
{
    public class WillOfTheAncientsEnemy : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WillOfTheAncientsEnemy",
            BuffTextureName = "2008_Tome_of_Combat_Mastery.dds",
        };
        float aP_Debuff;
        public WillOfTheAncientsEnemy(float aP_Debuff = default)
        {
            this.aP_Debuff = aP_Debuff;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aP_Debuff);
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, aP_Debuff);
        }
    }
}