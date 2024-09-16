using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore.Enums;
using GameServerCore.NetInfo;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer;

public class PlayerManager
{
    private static ILog _logger = LoggerProvider.GetLogger();

    private readonly List<ClientInfo> _players = [];
    public Dictionary<TeamId, int> TeamPlayerCount { get; set; } = [];

    public void AddPlayers(List<PlayerConfig> players)
    {
        foreach (PlayerConfig p in players)
        {
            _logger.Info($"Player: {p.Name} added as {p.Champion}");
            AddPlayer(p);
        }
    }

    public void AddPlayer(PlayerConfig config)
    {
        if (!TeamPlayerCount.TryAdd(config.Team, 1))
        {
            TeamPlayerCount[config.Team]++;
        }

        string[] summonerSkills =
        [
            config.Summoner1,
            config.Summoner2
        ];

        TeamId teamId = config.Team;
        ClientInfo info = new(
            config.Rank,
            teamId,
            config.Ribbon,
            config.Icon,
            config.Skin,
            config.Name,
            summonerSkills,
            config.PlayerID,
            new(config.Runes),
            new(config.Talents)
        );

        Champion c = new(
            config.Champion,
            info,
            0,
            teamId
        );

        info.Champion = c;
        Vector2 pos = GetHeroInitialSpawnPosition(info);
        c.SetPosition(pos, false);
        c.StopMovement();
        c.UpdateMoveOrder(OrderType.Stop);
        _players.Add(info);

        Game.ObjectManager.AddObject(c);
    }


    public void AddPlayer(ClientInfo info)
    {
        _players.Add(info);
    }

    // GetPlayerFromPeer
    public ClientInfo? GetPeerInfo(int clientId)
    {
        return _players.Find(x => x.ClientId == clientId);
    }

    public ClientInfo? GetClientInfoByPlayerId(long playerId)
    {
        return _players.Find(c => c.PlayerId == playerId);
    }

    public ClientInfo? GetClientInfoByChampion(Champion champ)
    {
        return _players.Find(c => c.Champion == champ);
    }

    public List<ClientInfo> GetPlayers(bool includeBots = true)
    {
        if (!includeBots)
        {
            return _players.FindAll(c => !c.Champion.IsBot);
        }

        return _players;
    }

    private Vector2 GetHeroInitialSpawnPosition(ClientInfo player)
    {
        int playerCount = TeamPlayerCount[player.Team];
        int playerIndex = player.InitialSpawnIndex;

        Vector2 pos = Game.Map.NavigationGrid.MiddleOfMap;
        if (Game.Map.SpawnPoints.TryGetValue(player.Team, out var spawn))
        {
            pos = spawn.Position;
        }

        if (!ContentManager.HeroSpawnOffset.TryGetValue(player.Team, out Dictionary<int, List<SpawnOffsetInfo>>? teamList))
        {
            return pos;
        }

        List<SpawnOffsetInfo> playerCountOffsetList = teamList.GetValueOrDefault(playerCount, teamList[teamList.Keys.Max()]);
        SpawnOffsetInfo offsetInfo = playerCountOffsetList.Find(x => x.Index == playerIndex) ?? new SpawnOffsetInfo();
        return pos + offsetInfo.PositionOffset;
    }

    public bool CheckIfAllPlayersLeft()
    {
        var players = GetPlayers(false);
        // The number of those who are disconnected and not even loads.
        var count = players.Count(p => !p.IsStartedClient && p.IsDisconnected);
        _logger.Info($"The number of disconnected players {count}/{players.Count}");
        if (count == players.Count)
        {
            _logger.Info("All players have left the server. It's lonely here :(");
            Game.StateHandler.SetGameState(GameState.EXIT);
            return true;
        }
        return false;
    }
}
