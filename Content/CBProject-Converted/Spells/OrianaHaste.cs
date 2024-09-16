namespace Buffs
{
    public class OrianaHaste : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "GLOBAL_HASTE.troy", },
            BuffName = "OrianaDissonanceAlly",
            BuffTextureName = "OriannaCommandDissonance.dds",
        };
        int level;
        float attackSpeedMod;
        float moveSpeedMod;
        float ticksLeft;
        float[] effect0 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        public OrianaHaste(int level = default, float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.level = level;
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void UpdateBuffs()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
            ApplyAssistMarker(attacker, target, 10);
            ticksLeft = 8;
        }
        public override void OnUpdateStats()
        {
            int level = this.level;
            float speedMod = effect0[level - 1];
            float elapsedRatio = this.ticksLeft / 8;
            float totalSpeed = speedMod * elapsedRatio;
            IncPercentMovementSpeedMod(owner, totalSpeed);
            float ticksLeft = this.ticksLeft - 1;
            this.ticksLeft = ticksLeft;
        }
    }
}