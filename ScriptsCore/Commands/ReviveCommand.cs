namespace Commands;

public class ReviveCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "revive";
    public override string Syntax => $"{Command}";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        if (!champion.Stats.IsDead)
        {
            ChatManager.Send("Your champion is already alive.");
            return;
        }

        ChatManager.Send("Your champion has revived!");
        champion.Respawn();
    }
}
