namespace Buffs
{
    public class MegaAdhesiveTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "global_slow.troy", },
            BuffName = "Mega Adhesive",
            BuffTextureName = "ChemicalMan_LaunchGoo.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float slowPercent;
        public MegaAdhesiveTarget(float slowPercent = default)
        {
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowPercent);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, slowPercent);
        }
    }
}