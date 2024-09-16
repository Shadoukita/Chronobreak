
using GameServerLib.Packets;
using LeaguePackets.Game;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleStatsConfirm : IPacketHandler<OnReplication_Acc>
    {
        public bool HandlePacket(int userId, OnReplication_Acc req)
        {
            return true;
        }
    }
}
