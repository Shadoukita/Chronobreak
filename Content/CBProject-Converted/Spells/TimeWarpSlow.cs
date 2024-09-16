namespace Buffs
{
    public class TimeWarpSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ChronoClockSlow_tar.troy", "global_slow.troy", },
            BuffName = "Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float speedMod;
        float attackSpeedMod;
        float moveSpeedMod;
        public TimeWarpSlow(float speedMod = default, float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.speedMod = speedMod;
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
            //RequireVar(this.speedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, speedMod);
        }
    }
}