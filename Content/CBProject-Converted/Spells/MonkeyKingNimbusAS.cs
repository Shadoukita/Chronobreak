namespace Buffs
{
    public class MonkeyKingNimbusAS : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MonkeyKingNimbusAS",
            BuffTextureName = "MonkeyKingNimbusStrike.dds",
        };
        float attackSpeedVar;
        public MonkeyKingNimbusAS(float attackSpeedVar = default)
        {
            this.attackSpeedVar = attackSpeedVar;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedVar);
            IncPercentAttackSpeedMod(owner, attackSpeedVar);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedVar);
        }
    }
}