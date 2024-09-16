namespace Buffs
{
    public class UrgotSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", "", "", },
            BuffName = "Terror Capacitor Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
        };
        float slowAmount;
        float[] effect0 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            slowAmount = effect0[level - 1];
            ApplyAssistMarker(attacker, attacker, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, slowAmount);
        }
    }
}