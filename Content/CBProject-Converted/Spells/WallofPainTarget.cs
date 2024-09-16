namespace Buffs
{
    public class WallofPainTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", },
            BuffName = "Wall of Pain Slow",
            BuffTextureName = "Lich_WallOfPain.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        float numTicks;
        float lastTimeExecuted;
        public WallofPainTarget(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
            numTicks = 20;
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                numTicks--;
            }
            float curSlowPercent = numTicks / 20;
            float speedMod = curSlowPercent * moveSpeedMod;
            IncPercentMultiplicativeMovementSpeedMod(owner, speedMod);
        }
    }
}