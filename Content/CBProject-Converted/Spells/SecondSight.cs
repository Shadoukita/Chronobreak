namespace Buffs
{
    public class SecondSight : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Second Sight",
            BuffTextureName = "Cardmaster_SealFate.dds",
        };
        float attackSpeedMod;
        float moveSpeedMod;
        public SecondSight(float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void UpdateBuffs()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
        public override void OnKill(AttackableUnit target)
        {
            IncGold(owner, 2);
        }
    }
}