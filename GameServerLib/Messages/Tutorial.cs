namespace Chronobreak.GameServer.Messages;

public abstract class Tutorial
{
    public static void TutorialPopup(int clientId, string message, bool localized = false)
    {
        if (localized)
        {
            Game.PacketNotifier.NotifyS2C_DisplayLocalizedTutorialChatText(clientId, message);
        }
        else
        {
            Game.PacketNotifier.NotifyS2C_OpenTutorialPopup(clientId, message);
        }
    }

    public static void OnTutorialAudioFinished()
    {
        //TODO: Implement event
    }

    public static void OnPopupClosed()
    {
        //TODO: Implement event
    }
}
