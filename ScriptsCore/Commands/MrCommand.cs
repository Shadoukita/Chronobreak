namespace Commands;

public class MrCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "mr";
    public override string Syntax => $"{Command} bonusMr";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (float.TryParse(split[1], out var mr))
        {
            IncPermanentFlatSpellBlockMod(champion, mr);
        }
    }
}
