namespace Buffs
{
    public class Fear : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Fear",
            BuffTextureName = "Fiddlesticks_Terrify.dds",
            PopupMessage = new[] { "game_floatingtext_Feared", },
        };
        public override BuffScriptMetaData BuffMetaData { get; } = new BuffScriptMetaData
        {
            BuffAddType = BuffAddType.STACKS_AND_OVERLAPS,
            BuffType = BuffType.FEAR
        };

        EffectEmitter a;
        EffectEmitter confetti;
        public override void OnActivate()
        {
            SetFeared(owner, true);
            SetCanCast(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.Fear)) > 0)
            {
                TeamId teamID = GetTeamID_CS(attacker); // UNUSED
                int fiddlesticksSkinID = GetSkinID(attacker);
                if (fiddlesticksSkinID == 6)
                {
                    SpellEffectCreate(out a, out _, "GlobalFear_Surprise.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                    SpellEffectCreate(out confetti, out _, "Party_HornConfetti_Instant.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "BUFFBONE_CSTM_HORN", default, attacker, default, default, false, false, false, false, true);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "Global_Fear.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                }
            }
            else
            {
                SpellEffectCreate(out a, out _, "Global_Fear.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetFeared(owner, false);
            SetCanCast(owner, true);
            SpellEffectRemove(a);
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            int fiddlesticksSkinID = GetSkinID(attacker);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(confetti);
            }
        }
        public override void OnUpdateStats()
        {
            SetCanCast(owner, false);
            IncPercentMultiplicativeMovementSpeedMod(owner, -0.4f);
        }
    }
}