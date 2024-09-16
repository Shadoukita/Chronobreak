namespace Commands;

public class XpCommand(CommandManager commandManager) : CommandBase(commandManager)
{

    public override string Command => "xp";
    public override string Syntax => $"{Command} xp";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
            return;
        }

        if (float.TryParse(split[1], out float xp))
        {
            if (xp < 0)
            {
                return;
            }
            else if (xp > ApiHandlers.MapData.ExpCurve.LastOrDefault())
            {
                xp = ApiHandlers.MapData.ExpCurve.LastOrDefault();
            }
            IncExp(champion, xp);
        }
    }
}
