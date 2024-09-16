using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer;
using Chronobreak.GameServer.Logging;
using log4net;

namespace GameServerLib.Services;

//TODO: Properly implement Client Tracking

public class ClientConnectionService : IPacketHandler<C2S_Exit>, IPacketHandler<C2S_SoftReconnect>
{
    private static ILog _logger = LoggerProvider.GetLogger();

    public bool HandlePacket(int userId, C2S_Exit req)
    {
        var player = Game.PlayerManager.GetClientInfoByPlayerId(userId);
        if (player is not null)
        {
            _logger.Info($"{player.Name} has exited the game.");
        }
        return Game.PacketServer.PacketHandlerManager.HandleDisconnect(userId);
    }

    public bool HandlePacket(int userId, C2S_SoftReconnect req)
    {
        var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
        peerInfo.IsStartedClient = true;
        peerInfo.IsDisconnected = false;
        Game.ObjectManager.OnReconnect(userId, peerInfo.Team, false);
        return true;
    }
}