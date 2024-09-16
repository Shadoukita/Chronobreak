using GameServerCore.Enums;
using GameServerCore.Packets.Enums;
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleEmotion : IPacketHandler<C2S_PlayEmote>
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public bool HandlePacket(int userId, C2S_PlayEmote req)
        {
            var champion = Game.PlayerManager.GetPeerInfo(userId).Champion;
            champion.StopMovement();
            champion.UpdateMoveOrder(OrderType.Taunt);
            //for later use -> tracking, etc.
            var playerName = Game.PlayerManager.GetPeerInfo(userId).Champion.Model;
            switch ((Emotions)req.EmoteID)
            {
                case Emotions.DANCE:
                    _logger.Debug("Player " + playerName + " is dancing.");
                    break;
                case Emotions.TAUNT:
                    _logger.Debug("Player " + playerName + " is taunting.");
                    break;
                case Emotions.LAUGH:
                    _logger.Debug("Player " + playerName + " is laughing.");
                    break;
                case Emotions.JOKE:
                    _logger.Debug("Player " + playerName + " is joking.");
                    break;
            }

            Game.PacketNotifier.NotifyS2C_PlayEmote((Emotions)req.EmoteID, champion.NetId);
            return true;
        }
    }
}
