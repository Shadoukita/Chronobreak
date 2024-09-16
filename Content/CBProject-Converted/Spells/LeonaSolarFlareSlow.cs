namespace Buffs
{
    public class LeonaSolarFlareSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Slow",
            BuffTextureName = "LeonaSolarFlare.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        EffectEmitter sSSlow;
        float mSPenalty;
        public override void OnActivate()
        {
            SpellEffectCreate(out sSSlow, out _, "Global_Slow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            mSPenalty = -0.8f;
            IncPercentMultiplicativeMovementSpeedMod(owner, mSPenalty);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(sSSlow);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, mSPenalty);
        }
    }
}