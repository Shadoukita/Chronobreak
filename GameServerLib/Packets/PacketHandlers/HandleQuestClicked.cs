
using GameServerLib.Packets;
using LeaguePackets.Game;
using Chronobreak.GameServer.Chatbox;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleQuestClicked : IPacketHandler<C2S_OnQuestEvent>
    {
        public bool HandlePacket(int userId, C2S_OnQuestEvent req)
        {
            var msg = $"Clicked quest with netid: {req.QuestID}";
            ChatManager.Send(msg);
            return true;
        }
    }
}
