namespace Commands;

public class SpeedCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "speed";
    public override string Syntax => $"{Command} [flat speed] [percent speed]";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2 || split.Length > 3)
        {
            SyntaxError();
            ShowSyntax();
        }

        try
        {
            if (split.Length == 3)
            {
                IncPermanentPercentMovementSpeedMod(champion, float.Parse(split[2]) / 100);
            }
            IncPermanentFlatMovementSpeedMod(champion, float.Parse(split[1]));
        }
        catch
        {
            SyntaxError();
            ShowSyntax();
        }
    }
}
