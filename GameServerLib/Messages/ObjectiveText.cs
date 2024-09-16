namespace Chronobreak.GameServer.Messages;

public static class ObjectiveText
{
    public static void Show(int clientId, string message)
    {
        Game.PacketNotifier.NotifyS2C_ShowObjectiveText(clientId, message);
    }

    public static void Replace(int clientId, string message)
    {
        Game.PacketNotifier.NotifyS2C_ReplaceObjectiveText(clientId, message);
    }

    public static void Hide(int clientId)
    {
        Game.PacketNotifier.NotifyS2C_HideObjectiveText(clientId);
    }
}