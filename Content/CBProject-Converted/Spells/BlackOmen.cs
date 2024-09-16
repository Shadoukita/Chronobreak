namespace Buffs
{
    public class BlackOmen : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BlackOmen",
            BuffTextureName = "3143_Randuins_Omen.dds",
        };
        float attackSpeedMod;
        public BlackOmen(float attackSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, attackSpeedMod);
        }
    }
}