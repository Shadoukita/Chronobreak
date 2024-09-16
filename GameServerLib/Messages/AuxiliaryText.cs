namespace Chronobreak.GameServer.Messages;

/// <summary>
/// Display an Auxiliary Text box
/// The packet definition has the read/write size too small so its difficult to use this till LeaguePackets is updated
/// </summary>
public class AuxiliaryText
{
    // Auxiliary
    public static void ShowAuxiliaryText(int clientId, string message)
    {
        Game.PacketNotifier.NotifyS2C_ShowAuxiliaryText(clientId, message);
    }

    public static void RefreshAuxiliaryText(int clientId, string message)
    {
        Game.PacketNotifier.NotifyS2C_ShowAuxiliaryText(clientId, message);
    }

    public static void HideAuxiliaryText(int clientId)
    {
        Game.PacketNotifier.NotifyS2C_HideAuxiliaryText(clientId);
    }
}