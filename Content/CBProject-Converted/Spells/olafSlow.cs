namespace Buffs
{
    public class OlafSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "root", },
            AutoBuffActivateEffect = new[] { "olaf_waterLog_Slow.troy", "olaf_waterLog_debuf.troy", "", },
            BuffName = "Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        float attackSpeedMod;
        float moveSpeedMod;
        public OlafSlow(float movementSpeedMod = default, float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
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
            //RequireVar(this.movementSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, movementSpeedMod);
        }
    }
}