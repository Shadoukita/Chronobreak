
using GameServerLib.Packets;
using LeaguePackets.Game;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleQueryStatus : IPacketHandler<C2S_QueryStatusReq>
    {
        public bool HandlePacket(int userId, C2S_QueryStatusReq req)
        {
            Game.PacketNotifier.NotifyS2C_QueryStatusAns(userId);
            return true;
        }
    }
}
