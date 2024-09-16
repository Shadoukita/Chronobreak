namespace Commands;

public class ArmorCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "armor";
    public override string Syntax => $"{Command} bonusArmor";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (float.TryParse(split[1], out var armor))
        {
            champion.Stats.Armor.IncFlatBonusPerm(armor);
        }
    }
}
