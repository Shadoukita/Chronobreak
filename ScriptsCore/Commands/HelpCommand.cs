namespace Commands;

public class HelpCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    private const string COMMAND_PREFIX = "<font color=\"#E175FF\"><b>";
    private const string COMMAND_SUFFIX = "</b></font>, ";
    private readonly int MESSAGE_MAX_SIZE = 512;

    public override string Command => "help";
    public override string Syntax => $"{Command}";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        if (!ApiHandlers.CheatsEnabled)
        {
            var msg = "[CB] Chat commands are disabled in this game.";
            ChatManager.Send(msg);
            return;
        }

        var commands = CommandManager.GetCommandsStrings();
        var commandsString = "";
        var lastCommandString = "";
        var isNewMessage = false;

        ChatManager.Send("List of available commands: ");

        foreach (var command in commands)
        {
            if (isNewMessage)
            {
                commandsString = new string(lastCommandString);
                isNewMessage = false;
            }

            lastCommandString = $"{COMMAND_PREFIX}" +
            $"{CommandManager.CommandStarterCharacter}{command}" +
            $"{COMMAND_SUFFIX}";

            if (commandsString.Length + lastCommandString.Length >= MESSAGE_MAX_SIZE)
            {
                ChatManager.Send(commandsString);
                commandsString = "";
                isNewMessage = true;
            }
            else
            {
                commandsString = $"{commandsString}{lastCommandString}";
            }
        }

        if (commandsString.Length != 0)
        {
            ChatManager.Send(commandsString);
        }

        ChatManager.Send("There are " + commands.Count + " commands");
    }
}
