
using GameServerLib.Packets;
using LeaguePackets.Game;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleMoveConfirm : IPacketHandler<Waypoint_Acc>
    {
        public bool HandlePacket(int userId, Waypoint_Acc req)
        {
            // TODO: check movement cheat
            return true;
        }
    }
}
