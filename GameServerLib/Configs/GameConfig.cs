using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Chronobreak.GameServer;

internal class GameConfig
{
    internal int Map { get; init; }
    internal string GameMode { get; init; }
    internal string?[] Mutators { get; init; }

    internal GameConfig(JToken? gameData)
    {
        Map = gameData?.SelectToken("map")?.Value<int>() ?? 1;
        GameMode = gameData?.SelectToken("gameMode")?.Value<string>()?.ToUpper().Replace(" ", string.Empty) ?? "CLASSIC";

        string?[] mutators = gameData?.SelectToken("mutators")?.Values<string>()?.ToArray() ?? new string?[8];
        if (mutators.Length is not 8)
        {
            Array.Resize(ref mutators, 8);
        }
        Mutators = mutators;
    }
}