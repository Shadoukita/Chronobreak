
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Chatbox;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleBlueTipClicked : IPacketHandler<C2S_OnTipEvent>
    {
        public bool HandlePacket(int userId, C2S_OnTipEvent req)
        {
            // TODO: can we use player net id from request?
            var playerNetId = Game.PlayerManager.GetPeerInfo(userId).Champion.NetId;
            Game.PacketNotifier.NotifyS2C_HandleTipUpdate(userId, "", "", "", 0, playerNetId, req.TipID);

            var msg = $"Clicked blue tip with netid: {req.TipID}";
            ChatManager.Send(msg);
            return true;
        }
    }
}
