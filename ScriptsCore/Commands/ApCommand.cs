namespace Commands;

public class ApCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "ap";
    public override string Syntax => $"{Command} bonusAp";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (float.TryParse(split[1], out var ap))
        {
            champion.Stats.AbilityPower.IncFlatBonusPerm(ap);
        }
    }
}
