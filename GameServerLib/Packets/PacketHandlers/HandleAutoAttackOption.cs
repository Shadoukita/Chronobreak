
using GameServerLib.Packets;
using LeaguePackets.Game;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleAutoAttackOption : IPacketHandler<C2S_UpdateGameOptions>
    {
        public bool HandlePacket(int userId, C2S_UpdateGameOptions req)
        {
            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            champion.AutoAttackAutoAcquireTarget = req.AutoAttackEnabled;
            return true;
        }
    }
}
