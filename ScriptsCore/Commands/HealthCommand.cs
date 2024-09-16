namespace Commands;

public class HealthCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "health";
    public override string Syntax => $"{Command} maxHealth";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (float.TryParse(split[1], out var hp))
        {
            IncPermanentFlatHPPoolMod(champion, hp);
        }
    }
}
