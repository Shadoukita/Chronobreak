namespace Buffs
{
    public class AbsoluteZero_Slow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_FREEZE.TROY", },
            BuffName = "Absolute Zero Slow",
            BuffTextureName = "3022_Frozen_Heart.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        float attackSpeedMod;
        public AbsoluteZero_Slow(float movementSpeedMod = default, float attackSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeedMod);
            IncPercentMultiplicativeAttackSpeedMod(owner, attackSpeedMod);
        }
    }
}