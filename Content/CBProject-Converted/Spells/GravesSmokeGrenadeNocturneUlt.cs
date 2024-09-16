namespace Buffs
{
    public class GravesSmokeGrenadeNocturneUlt : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Freeze.troy", },
            BuffName = "Iceblast Slow",
            BuffTextureName = "3022_Frozen_Heart.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        public override void OnActivate()
        {
            IncPermanentFlatBubbleRadiusMod(owner, 300);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentFlatBubbleRadiusMod(owner, -300);
        }
    }
}