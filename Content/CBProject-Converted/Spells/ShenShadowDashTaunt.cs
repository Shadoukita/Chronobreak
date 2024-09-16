namespace Buffs
{
    public class ShenShadowDashTaunt : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Shen Shadow Dash Taunt",
            BuffTextureName = "GSB_taunt.dds",
            PopupMessage = new[] { "game_floatingtext_Taunted", },
        };
        EffectEmitter asdf1;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out asdf1, out _, "Global_Taunt_multi_unit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_HEAD_LOC", default, owner, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.Taunt), attacker);
            SpellEffectRemove(asdf1);
        }
    }
}