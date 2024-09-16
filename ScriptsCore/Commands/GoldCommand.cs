namespace Commands;

public class GoldCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "gold";
    public override string Syntax => $"{Command} goldAmount";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (float.TryParse(split[1], out var gold))
        {
            IncGold(champion, gold);
        }
    }
}
