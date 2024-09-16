using GameServerCore;
using GameServerCore.Enums;
using GameServerLib.Packets;
using LeaguePackets.LoadScreen;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Chatbox;

public class ChatManager : IPacketHandler<Chat>, IPacketHandler<QuickChat>
{
    private static ILog _logger = LoggerProvider.GetLogger();

    private static string TEXT_COLOR = HTMLFHelperUtils.Colors.YELLOW;
    private const string _teamChatColor = "<font color=\"#00FF00\">";
    private const string _enemyChatColor = "<font color=\"#FF0000\">";

    public static void Send(string message, uint sourceNetId = 0)
    {
        Game.PacketNotifier.NotifyS2C_SystemMessage(message, sourceNetId);
    }

    public static void Send(string message, int clientId, uint sourceNetId = 0)
    {
        Game.PacketNotifier.NotifyS2C_SystemMessage(clientId, message, sourceNetId);
    }

    public static void Send(string message, TeamId team, uint sourceNetId = 0)
    {
        Game.PacketNotifier.NotifyS2C_SystemMessage(team, message, sourceNetId);
    }

    internal static void OnReceive(int clientId, uint netId, bool localized, string msgParams, string message, ChatType type)
    {
        if (Game.Config.ChatCheatsEnabled && message.StartsWith(Game.CommandManager.CommandStarterCharacter))
        {
            Game.CommandManager.ExecuteCommand(clientId, message.Remove(0, 1));
            return;
        }

        var client = Game.PlayerManager.GetPeerInfo(clientId);

        var formattedMessage = $"{client.Name}({client.Champion.Model}): </font>" + $"<font color=\"{HTMLFHelperUtils.Colors.WHITE}\">{message}";

        var allyTeam = client.Team;
        var enemyTeam = allyTeam.GetEnemyTeam();

        switch (type)
        {
            case ChatType.All:
                var allyMsg = _teamChatColor + "[All] " + formattedMessage;
                var enemyMsg = _enemyChatColor + "[All] " + formattedMessage;

                Send(allyMsg, allyTeam, netId);
                Send(enemyMsg, enemyTeam, netId);
                return;
            case ChatType.Team:
                var teamMsg = _teamChatColor + formattedMessage;

                Send(teamMsg, allyTeam, netId);
                return;
            case ChatType.Private:
                //TODO: Implement private messages
                return;
            default:
                _logger.Error("Unknown ChatMessageType:" + type);
                Send("Unable to send message.", client.ClientId, 0);
                return;
        }
    }

    public bool HandlePacket(int userId, Chat req)
    {
        OnReceive(req.ClientID, req.NetID, req.Localized, req.Params, req.Message, (ChatType)req.ChatType);
        return true;
    }

    public bool HandlePacket(int userId, QuickChat req)
    {
        _logger.Debug("Received QuickChat");
        return true;
    }
}