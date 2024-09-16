using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer;

namespace GameServerLib.Services;

public class CameraService : IPacketHandler<World_LockCamera_Server>, IPacketHandler<World_SendCamera_Server>
{
    public bool HandlePacket(int userId, World_LockCamera_Server req)
    {
        return true;
    }

    public bool HandlePacket(int userId, World_SendCamera_Server req)
    {
        Game.PacketNotifier.NotifyWorld_SendCamera_Server_Acknologment(Game.PlayerManager.GetPeerInfo(userId), req);
        return true;
    }
}