
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Attributes;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    [DisabledHandler]
    public class HandleClick : IPacketHandler<SendSelectedObjID>
    {
        private static ILog _logger = LoggerProvider.GetLogger();
        public bool HandlePacket(int userId, SendSelectedObjID req)
        {
            //Annoying ahh message
            //var msg = $"Object {Game.PlayerManager.GetPeerInfo(userId).Champion.NetId} clicked on {req.SelectedNetID}";
            //_logger.Debug(msg);

            return true;
        }
    }
}
