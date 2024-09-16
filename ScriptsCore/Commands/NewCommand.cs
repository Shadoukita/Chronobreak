namespace Commands;

public class NewCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "newcommand";
    public override string Syntax => $"{Command}";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var msg = $"The new command added by {CommandManager.CommandStarterCharacter} help has been executed";
        ChatManager.Send(msg);
        CommandManager.RemoveCommand(Command);
    }
}
