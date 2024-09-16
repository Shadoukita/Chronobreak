
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleSync : IPacketHandler<SynchVersionC2S>
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public bool HandlePacket(int userId, SynchVersionC2S req)
        {
            //Logging->writeLine("Client version: %s", version->version);

            var mapId = Game.Config.GameConfig.Map;
            _logger.Debug("Current map: " + mapId);

            var info = Game.PlayerManager.GetPeerInfo(userId);
            var versionMatch = req.Version == Config.VERSION_STRING;
            info.IsMatchingVersion = versionMatch;

            // Version might be an invalid value, currently it trusts the client
            if (!versionMatch)
            {
                _logger.Warn($"Client's version ({req.Version}) does not match server's {Config.VERSION}");
            }
            else
            {
                _logger.Debug("Accepted client version (" + req.Version + ") from client = " + req.ClientID + " & PlayerID = " + info.PlayerId);
            }

            Game.PacketNotifier.NotifySynchVersion(
                userId, info.Team, Game.PlayerManager.GetPlayers(), Config.VERSION_STRING,
                Game.Config.GameConfig.GameMode,
                ContentManager.GameFeatures,
                mapId,
                Game.Map.MutatorNames
            );

            return true;
        }
    }
}
