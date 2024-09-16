
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleSyncSimTime : IPacketHandler<SynchSimTimeC2S>
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public bool HandlePacket(int userId, SynchSimTimeC2S req)
        {
            //Check this
            var diff = req.TimeLastServer - req.TimeLastClient;
            if (req.TimeLastClient > req.TimeLastServer)
            {
                var peerInfo = Game.PlayerManager.GetPeerInfo(userId);
                var msg = $"Client {peerInfo.ClientId} sent an invalid heartbeat - Timestamp error (diff: {diff})";
                _logger.Warn(msg);
            }

            return true;
        }
    }
}
