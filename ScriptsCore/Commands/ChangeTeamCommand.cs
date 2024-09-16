namespace Commands;

public class ChangeTeamCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "changeteam";
    public override string Syntax => $"{Command} teamNumber";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
            return;
        }

        if (!int.TryParse(split[1], out var t))
        {
            return;
        }

        var team = t.ToTeamId();
        champion.SetTeam(team);
    }
}
