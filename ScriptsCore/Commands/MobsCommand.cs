namespace Commands;

public class MobsCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "mobs";
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

        if (!int.TryParse(split[1], out var team))
        {
            return;
        }

        var units = ApiHandlers.GetGameObjects()
            .Where(xx => xx.Value.Team == team.ToTeamId())
            .Where(xx => xx.Value is Minion || xx.Value is NeutralMinion);

        var client = Game.PlayerManager.GetPeerInfo(clientId);
        foreach (var unit in units)
        {
            //TODO: Make PingHandler class
            ApiHandlers.PacketNotifier.NotifyS2C_MapPing(unit.Value.Position, Pings.PING_DANGER, client: client);
        }
    }
}
