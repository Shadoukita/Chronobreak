namespace Buffs
{
    public class PoppyParagonParticle : BuffScript
    {
        EffectEmitter maxParticle;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out maxParticle, out _, "PoppyDemacia_max.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger", default, owner, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(maxParticle);
        }
    }
}