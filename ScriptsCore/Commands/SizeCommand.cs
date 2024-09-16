namespace Commands;

public class SizeCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "size";
    public override string Syntax => $"{Command} size";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (float.TryParse(split[1], out float size))
        {
            champion.Stats.Size.IncPercentBonusPerm(size);
        }
    }
}
