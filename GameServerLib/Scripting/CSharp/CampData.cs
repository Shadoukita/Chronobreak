using System;
using System.Numerics;
using GameServerCore.Enums;
using System.Collections.Generic;

namespace Chronobreak.GameServer.Scripting.CSharp;
public class CampData
{
    public int GroupNumber { get; init; }
    public string TimerType { get; init; } = "";
    public AudioVOComponentEvent RevealEvent { get; init; }
    public float SpawnDuration { get; init; }
    public float RespawnTime { get; init; }
    public List<Vector3> Positions { get; init; } = [];
    public string MinimapIcon { get; init; } = "";
    public TeamId GroupBuffSide { get; init; }
    public float GroupDelaySpawnTime { get; set; }
    public Action? Timer { get; set; }
    public List<List<KeyValuePair<string, string>>> Groups { get; init; } = [];
    public List<List<string>> UniqueNames { get; init; } = [];
    public List<Dictionary<string, bool>> AliveTracker { get; init; } = [];
    public List<Vector3> FacePositions { get; init; } = [];
    public string AIScript = "";
}
