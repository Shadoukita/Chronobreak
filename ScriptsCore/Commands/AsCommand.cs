namespace Commands;

public class AsCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "as";
    public override string Syntax => $"{Command} bonusAs (percent)";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (float.TryParse(split[1], out var attackSpeed))
        {
            champion.Stats.AttackSpeedMultiplier.IncPercentBasePerm(attackSpeed);
        }
    }
}
