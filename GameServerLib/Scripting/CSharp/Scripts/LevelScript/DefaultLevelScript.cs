using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.Barracks;
using Chronobreak.GameServer.GameObjects.AttackableUnits;

namespace MapScripts;

public class DefaultLevelScript : ILevelScript
{
    public float InitialSpawnTime { get; private set; } = 0.0f;
    public virtual void OnPostLevelLoad() { }
    public virtual void OnLevelInit() { }
    public virtual void OnLevelInitServer() { }
    public virtual void BarrackReactiveEvent(TeamId team, Lane lane) { }
    public virtual void HandleDestroyedObject(AttackableUnit destroyed) { }
    public virtual void DisableSuperMinions(TeamId team, Lane lane) { }
    public virtual InitMinionSpawnInfo? GetInitSpawnInfo(Lane lane, TeamId team) => null;
    public virtual MinionSpawnInfo? GetMinionSpawnInfo(Lane lane, int waveCount, TeamId teamID) => null;
    public virtual void NeutralMinionDeath(string minionName, ObjAIBase killer, Vector3 position) { }
}