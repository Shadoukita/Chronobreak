namespace Buffs
{
    public class RumbleCarpetBombSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", "", "", },
            BuffName = "RumbleCarpetBombSlow",
            BuffTextureName = "GragasBodySlam.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float slowAmount;
        float[] effect0 = { -0.35f, -0.35f, -0.35f };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            slowAmount = effect0[level - 1];
            ApplyAssistMarker(attacker, attacker, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, slowAmount);
        }
    }
}