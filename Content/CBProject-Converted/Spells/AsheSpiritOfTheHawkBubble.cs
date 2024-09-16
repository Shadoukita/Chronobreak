namespace Buffs
{
    public class AsheSpiritOfTheHawkBubble : BuffScript
    {
        Vector3 targetPos;
        Region bubbleID;
        public AsheSpiritOfTheHawkBubble(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            Vector3 targetPos = this.targetPos;
            TeamId teamID = GetTeamID_CS(owner);
            bubbleID = AddPosPerceptionBubble(teamID, 1000, targetPos, 8, default, false);
            SpellEffectCreate(out _, out _, "bowmaster_frostHawk_terminate.troy", default, teamID, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true);
            SpellEffectCreate(out _, out _, "bowmaster_frostHawk_terminate_02.troy", default, teamID, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
    }
}