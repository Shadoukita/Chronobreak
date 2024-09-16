namespace Buffs
{
    public class MonkeyKingCloneFull : BuffScript
    {
        EffectEmitter particle; // UNUSED
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out _, "MonkeyKingClone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, teamID, default, default, true, owner, "root", default, target, "root", default, false);
        }
    }
}