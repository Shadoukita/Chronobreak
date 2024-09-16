using GameServerCore.Enums;
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleSpawn : IPacketHandler<C2S_CharSelected>
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public bool HandlePacket(int userId, C2S_CharSelected req)
        {
            _logger.Debug("Spawning map");

            var userInfo = Game.PlayerManager.GetPeerInfo(userId);
            var players = Game.PlayerManager.GetPlayers(true);
            Game.PacketNotifier.NotifyS2C_StartSpawn(userId, userInfo.Team, players);

            if (Game.StateHandler.State is GameState.GAMELOOP)
            {
                Game.ObjectManager.OnReconnect(userId, userInfo.Team, true);
            }
            else
            {
                Game.ObjectManager.SpawnObjects(userInfo);
            }

            Game.PacketNotifier.NotifySpawnEnd(userId);
            return true;
        }
    }
}