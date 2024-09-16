namespace Buffs
{
    public class AbsoluteZeroSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_FREEZE.TROY", },
            BuffName = "AbsoluteZeroSlow",
            BuffTextureName = "Yeti_Shatter.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        float attackSpeedMod;
        public AbsoluteZeroSlow(float movementSpeedMod = default, float attackSpeedMod = default)
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