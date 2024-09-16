namespace Buffs
{
    public class Silence : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Silence.troy", },
            BuffName = "Silence",
            BuffTextureName = "Voidwalker_Spellseal.dds",
            PopupMessage = new[] { "game_floatingtext_Silenced", },
        };

        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.SILENCE,
            BuffAddType = BuffAddType.STACKS_AND_OVERLAPS,
            CanMitigateDuration = true
        };
        public override void OnActivate()
        {
            SetSilenced(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSilenced(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetSilenced(owner, true);
        }
    }
}