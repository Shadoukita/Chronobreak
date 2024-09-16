namespace Commands;

public class CooldownReductionCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "cdr";
    public override string Syntax => $"{Command} bonusCdr";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (float.TryParse(split[1], out var cdr))
        {
            champion.Stats.CooldownReduction.IncPercentBonusPerm(cdr / 100f);
        }
    }
}
