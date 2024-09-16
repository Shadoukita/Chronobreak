using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    public class ReplicationAnimatedBuilding : Replication
    {
        public ReplicationAnimatedBuilding(ObjAnimatedBuilding owner) : base(owner)
        {
        }

        internal override void Update()
        {
            UpdateFloat(3, Stats.CurrentHealth, 1, 0); //mHP
            UpdateBool(Owner.IsInvulnerable, 1, 1); //IsInvulnerable
            UpdateBool(Owner.IsTargetable, 5, 0); //mIsTargetable
            UpdateUint((uint)Stats.IsTargetableToTeam, 5, 1); //mIsTargetableToTeamFlags
        }
    }
}
