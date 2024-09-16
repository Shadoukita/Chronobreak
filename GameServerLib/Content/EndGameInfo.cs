using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Newtonsoft.Json;

namespace Chronobreak.GameServer.Content
{
    public class EndGameInfo
    {
        [JsonProperty]
        internal long GameId;
        [JsonProperty]
        internal float Time;
        [JsonProperty]
        internal int WinningTeam;
        [JsonProperty]
        internal Dictionary<TeamId, EndGameTeamStats> EndGameTeams = new()
        {
            {TeamId.TEAM_ORDER, new EndGameTeamStats()},
            {TeamId.TEAM_CHAOS, new EndGameTeamStats()}
        };

        internal EndGameInfo(int winningTeam)
        {
            GameId = Game.Config.GameId;
            Time = Game.Time.GameTime;
            WinningTeam = winningTeam;
            foreach (var player in Game.PlayerManager.GetPlayers())
            {
                EndGameTeams[player.Team].Players.Add(new EndGameChampionStats(player.Champion));
            }
        }

        internal void Post(string address)
        {
            HttpClient client = new();
            string serializedEndGameInfo = JsonConvert.SerializeObject(this, Formatting.Indented);
            StringContent payload = new(serializedEndGameInfo, Encoding.UTF8, "application/json");
            client.PostAsync(address, payload);
        }
    }
}

internal class EndGameTeamStats
{
    [JsonProperty]
    internal int TeamKillScore => Players.Sum(x => x.ChampionStatistics.Kills);
    [JsonProperty]
    internal float TeamPointScore => Players.Sum(x => x.Score);
    [JsonProperty]
    internal List<EndGameChampionStats> Players = [];
}

internal class EndGameChampionStats
{
    [JsonProperty]
    internal string Name;
    [JsonProperty]
    internal string Champion;
    [JsonProperty]
    internal int SkinId;
    [JsonProperty]
    internal int Level;
    [JsonProperty]
    internal float Score;
    [JsonProperty]
    internal ChampionStatistics ChampionStatistics;
    [JsonProperty]
    internal List<int> Items = [];

    public EndGameChampionStats(Champion ch)
    {
        Name = ch.Name;
        Champion = ch.Model;
        SkinId = ch.SkinID;
        Level = ch.Experience.Level;
        Score = ch.ChampionStats.Score;
        ChampionStatistics = ch.ChampionStatistics;

        foreach (var item in ch.ItemInventory.GetItems())
        {
            Items.Add(item.ItemData.Id);
        }
    }
}