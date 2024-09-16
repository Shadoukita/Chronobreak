
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleScoreboard : IPacketHandler<C2S_StatsUpdateReq>
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public bool HandlePacket(int userId, C2S_StatsUpdateReq req)
        {
            _logger.Debug($"Player {Game.PlayerManager.GetPeerInfo(userId).Name} has looked at the scoreboard.");
            // Send to that player stats packet
            //var champion = _playerManager.GetPeerInfo(userId).Champion;

            //Game.PacketNotifier.NotifyS2C_HeroStats(champion);
            return true;
        }
    }
}
