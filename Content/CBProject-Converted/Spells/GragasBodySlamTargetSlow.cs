namespace Buffs
{
    public class GragasBodySlamTargetSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", "", "", },
            BuffName = "GragasBodySlamTargetSlow",
            BuffTextureName = "GragasBodySlam.dds",
        };
        float slowAmount;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            slowAmount = -0.35f;
            ApplyAssistMarker(attacker, attacker, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, slowAmount);
        }
    }
}