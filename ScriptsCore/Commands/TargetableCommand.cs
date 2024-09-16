namespace Commands;

public class TargettableCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "targetable";
    public override string Syntax => $"{Command} false (untargetable) / true (targetable)";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');

        if (split.Length != 2 || !bool.TryParse(split[1], out var userInput))
        {
            SyntaxError();
            ShowSyntax();
        }
        else
        {
            champion.SetStatus(StatusFlags.Targetable, userInput);
        }
    }
}