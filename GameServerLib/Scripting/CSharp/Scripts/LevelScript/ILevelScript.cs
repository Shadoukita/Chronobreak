using System.Numerics;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.Barracks;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace MapScripts;

public interface ILevelScript
{
    float InitialSpawnTime { get; }
    void OnPostLevelLoad() { }
    void OnLevelInit() { }
    void OnLevelInitServer() { }
    void BarrackReactiveEvent(TeamId team, Lane lane) { }
    void HandleDestroyedObject(AttackableUnit destroyed) { }
    void DisableSuperMinions(TeamId team, Lane lane) { }
    InitMinionSpawnInfo? GetInitSpawnInfo(Lane lane, TeamId team) => null;
    MinionSpawnInfo? GetMinionSpawnInfo(Lane lane, int waveCount, TeamId teamID) => null;
    void NeutralMinionDeath(string minionName, ObjAIBase killer, Vector3 position) { }
}