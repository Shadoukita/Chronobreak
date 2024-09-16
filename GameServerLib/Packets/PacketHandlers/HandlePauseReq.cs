
using GameServerLib.Packets;
using LeaguePackets.Game;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandlePauseReq : IPacketHandler<PausePacket>
    {
        public bool HandlePacket(int userId, PausePacket req)
        {
            var pauser = Game.PlayerManager.GetPeerInfo(userId);
            Game.StateHandler.Pause(pauser);
            return true;
        }
    }
}