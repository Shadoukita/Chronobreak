using System.Collections.Generic;
using GameServerCore.Enums;
using Newtonsoft.Json.Linq;

namespace Chronobreak.GameServer;

public class PlayerConfig(JToken playerData)
{
    public long PlayerID { get; private set; } = playerData.Value<long>("playerId");
    public string Rank { get; private set; } = playerData.Value<string>("rank") ?? "DIAMOND";
    public string Name { get; private set; } = playerData.Value<string>("name") ?? "Test";
    public string Champion { get; private set; } = playerData.Value<string>("champion") ?? "";
    public TeamId Team { get; private set; } = GetTeamFromString(playerData.Value<string>("team"));
    public short Skin { get; private set; } = playerData.Value<short>("skin");
    public string Summoner1 { get; private set; } = playerData.Value<string>("summoner1") ?? "SummonerFlash";
    public string Summoner2 { get; private set; } = playerData.Value<string>("summoner2") ?? "SummonerHeal";
    public short Ribbon { get; private set; } = playerData.Value<short>("ribbon");
    public int Icon { get; private set; } = playerData.Value<int>("icon");
    public string BlowfishKey { get; private set; } = playerData.Value<string>("blowfishKey") ?? "";
    public Dictionary<int, int>? Runes { get; private set; } = playerData.SelectToken("runes")?.ToObject<Dictionary<int, int>>() ?? [];
    public Dictionary<int, int>? Talents { get; private set; } = playerData.SelectToken("talents")?.ToObject<Dictionary<int, int>>() ?? [];

    private static TeamId GetTeamFromString(string? team)
    {
        return team?.ToUpperInvariant() switch
        {
            "BLUE" or "ORDER" => TeamId.TEAM_ORDER,
            "RED" or "PURPLE" or "CHAOS" => TeamId.TEAM_CHAOS,
            "NEUTRAL" => TeamId.TEAM_NEUTRAL,
            _ => TeamId.TEAM_UNKNOWN
        };
    }
}