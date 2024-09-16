namespace Buffs
{
    public class TalonShadowAssaultMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TalonShadowAssaultMarker",
        };
        EffectEmitter particleZ;
        EffectEmitter particleY;
        public override void OnActivate()
        {
            TeamId attackerTeam = GetTeamID_CS(attacker);
            int attackerSkinID = GetSkinID(attacker);
            if (attackerSkinID == 3)
            {
                SpellEffectCreate(out particleZ, out particleY, "talon_ult_blade_hold_dragon.troy", "talon_ult_blade_hold_team_ID_red_dragon.troy", attackerTeam, 1, 0, TeamId.TEAM_UNKNOWN, attackerTeam, owner, false, owner, "root", default, attacker, default, default, false, false, false, false, true);
            }
            else
            {
                SpellEffectCreate(out particleZ, out particleY, "talon_ult_blade_hold.troy", "talon_ult_blade_hold_team_ID_red.troy", attackerTeam, 1, 0, TeamId.TEAM_UNKNOWN, attackerTeam, owner, false, owner, "root", default, attacker, default, default, false, false, false, false, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleZ);
            SpellEffectRemove(particleY);
        }
    }
}