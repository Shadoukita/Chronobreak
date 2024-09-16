namespace Buffs
{
    public class OrianaQuickSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.troy", },
            BuffName = "Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float attackSpeedMod;
        float moveSpeedMod;
        int level;
        float startTime;
        float[] effect0 = { -0.3f, -0.4f, -0.5f };
        public OrianaQuickSlow(float attackSpeedMod = default, float moveSpeedMod = default, int level = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
            this.level = level;
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
            startTime = GetGameTime();
        }
        public override void OnUpdateStats()
        {
            int level = this.level;
            float speedMod = effect0[level - 1];
            float currentTime = GetGameTime();
            float elapsedTime = currentTime - startTime;
            elapsedTime = 2 - elapsedTime;
            float elapsedRatio = elapsedTime / 2;
            elapsedRatio = Math.Max(elapsedRatio, 0);
            float totalSpeed = speedMod * elapsedRatio;
            IncPercentMultiplicativeMovementSpeedMod(owner, totalSpeed);
        }
    }
}