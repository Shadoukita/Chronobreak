namespace Commands;

public class ClearSlowCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "clearslows";
    public override string Syntax => $"{Command}";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        champion.Stats.MoveSpeed.ClearSlows();
        ChatManager.Send("Your slows have been cleared!");
    }
}
