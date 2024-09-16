namespace Commands;

public class AdCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "ad";
    public override string Syntax => $"{Command} bonusAd";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length >= 2)
        {
            if (float.TryParse(split[1], out var ad))
            {
                champion.Stats.AttackDamage.FlatBonus += ad;
                return;
            }
        }

        SyntaxError();
        ShowSyntax();
    }
}
