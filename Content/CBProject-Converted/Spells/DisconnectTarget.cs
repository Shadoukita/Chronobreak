namespace Buffs
{
    public class DisconnectTarget : BuffScript
    {
        public override void OnActivate()
        {
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            TeleportToKeyLocation(owner, SpawnType.SPAWN_LOCATION, teamID);
            IssueOrder(attacker, OrderType.MoveTo, default, owner);
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}