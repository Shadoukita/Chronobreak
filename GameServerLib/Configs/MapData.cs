using System.Collections.Generic;
using Chronobreak.GameServer.Content;

namespace Chronobreak.GameServer;

public class MapData
{
    public int Id { get; private set; }
    public Dictionary<string, float> MapConstants { get; private set; } = [];
    /// <summary>
    /// Collection of MapObjects present within a map's room file, with the key being the name present in the room file. Refer to <see cref="MapObject"/>.
    /// </summary>
    public List<MapObject> MapObjects { get; internal set; } = [];
    /// <summary>
    /// Experience required to level, ordered from 2 and up.
    /// </summary>
    public List<float> ExpCurve { get; private set; } = [];
    public float BaseExpMultiple { get; set; }
    public float LevelDifferenceExpMultiple { get; set; }
    public float MinimumExpMultiple { get; set; }
    /// <summary>
    /// Amount of time death should last depending on level.
    /// </summary>
    public List<float> DeathTimes { get; private set; } = [];
    /// <summary>
    /// Potential progression of stats per-level of jungle monsters.
    /// </summary>
    /// TODO: Figure out what this is and how to implement it.
    public List<float> StatsProgression { get; private set; } = [];

    public List<int> UnpurchasableItemList = [];
    public List<int> ItemInclusionList = [];

    public MapData(int mapId)
    {
        Id = mapId;
    }
}