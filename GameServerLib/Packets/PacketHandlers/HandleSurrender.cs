
using GameServerLib.Packets;
using LeaguePackets.Game;
using static Chronobreak.GameServer.API.ApiMapFunctionManager;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleSurrender : IPacketHandler<C2S_TeamSurrenderVote>
    {
        public bool HandlePacket(int userId, C2S_TeamSurrenderVote req)
        {
            var c = Game.PlayerManager.GetPeerInfo(userId).Champion;
            HandleSurrender(userId, c, req.VotedYes);
            return true;
        }
    }
}
