namespace Buffs
{
    public class RumbleGrenadeSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", "", "", },
            BuffName = "RumbleGrenadeSlow",
            BuffTextureName = "Rumble_Electro Harpoon.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float slowAmount;
        public RumbleGrenadeSlow(float slowAmount = default)
        {
            this.slowAmount = slowAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowAmount);
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            ApplyAssistMarker(attacker, attacker, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, slowAmount);
        }
    }
}