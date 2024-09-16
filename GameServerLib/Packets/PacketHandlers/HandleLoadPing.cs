
using GameServerLib.Packets;
using LeaguePackets.Game;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleLoadPing : IPacketHandler<C2S_Ping_Load_Info>
    {
        public bool HandlePacket(int userId, C2S_Ping_Load_Info req)
        {
            var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
            if (peerInfo == null)
            {
                return false;
            }

            Game.PacketNotifier.NotifyPingLoadInfo(peerInfo, req);
            return true;
        }
    }
}
