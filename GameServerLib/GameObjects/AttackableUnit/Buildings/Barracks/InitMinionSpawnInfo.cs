using System.Collections.Generic;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.Barracks;

public class InitMinionSpawnInfo
{
    public int WaveSpawnInterval { get; set; }
    public int MinionSpawnInterval { get; set; }
    public bool IsDestroyed { get; set; }
    public Dictionary<string, MinionData> InitialMinionData { get; set; } = [];
}

