namespace Buffs
{
    public class IceBlastSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Freeze.troy", },
            BuffName = "Iceblast Slow",
            BuffTextureName = "3022_Frozen_Heart.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        float attackSpeedMod;
        public IceBlastSlow(float movementSpeedMod = default, float attackSpeedMod = default)
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