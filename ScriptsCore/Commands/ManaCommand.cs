namespace Commands;

public class ManaCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "mana";
    public override string Syntax => $"{Command} maxMana";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (float.TryParse(split[1], out var mp))
        {
            IncPermanentFlatPARPoolMod(champion, mp);
            IncPAR(champion, mp, champion.Stats.PrimaryAbilityResourceType);
        }
    }
}
