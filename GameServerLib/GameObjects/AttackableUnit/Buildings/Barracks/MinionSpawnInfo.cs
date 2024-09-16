using System.Collections.Generic;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.Barracks;

public class MinionSpawnInfo
{
    public bool IsDestroyed { get; set; }
    public int GoldRadius { get; set; }
    public int ExperienceRadius { get; set; }
    public Dictionary<string, MinionData> MinionData { get; set; } = [];
    public List<string> MinionSpawnOrder { get; set; } = [];
}