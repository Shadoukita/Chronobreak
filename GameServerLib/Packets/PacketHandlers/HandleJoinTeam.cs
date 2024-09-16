
using GameServerLib.Packets;
using LeaguePackets.LoadScreen;

namespace Chronobreak.GameServer.Packets.PacketHandlers
{
    public class HandleJoinTeam : IPacketHandler<RequestJoinTeam>
    {
        public bool HandlePacket(int userId, RequestJoinTeam req)
        {
            var team = Game.PlayerManager.GetPeerInfo(userId).Team;
            var humanPlayers = Game.PlayerManager.GetPlayers(false);
            uint version = uint.Parse(Config.VERSION.ToString().Replace(".", string.Empty));

            Game.PacketNotifier.NotifyLoadScreenInfo(userId, team, humanPlayers);

            // Distributes each players info by UserId
            foreach (var player in humanPlayers)
            {
                // Load everyone's player name.
                Game.PacketNotifier.NotifyRequestRename(userId, player);
                // Load everyone's champion.
                Game.PacketNotifier.NotifyRequestReskin(userId, player);

                //TODO: Take out of the loop.
                if (player.ClientId == userId)
                {
                    Game.PacketNotifier.NotifyKeyCheck(player.ClientId, player.PlayerId, version, broadcast: true);
                }
            }

            return true;
        }
    }
}