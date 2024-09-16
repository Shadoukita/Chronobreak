using GameServerCore.Packets.Enums;
using GameServerLib.Packets;
using LeaguePackets.Game;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleAttentionPing : IPacketHandler<C2S_MapPing>
    {
        public bool HandlePacket(int userId, C2S_MapPing req)
        {
            var client = Game.PlayerManager.GetPeerInfo(userId);
            Game.PacketNotifier.NotifyS2C_MapPing(req.Position, (Pings)req.PingCategory, req.TargetNetID, client);
            return true;
        }
    }
}
