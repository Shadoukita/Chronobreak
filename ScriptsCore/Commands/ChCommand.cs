namespace Commands;

public class ChCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "ch";
    public override string Syntax => $"{Command} championName";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
            return;
        }

        var c = new Champion
        (
            split[1],
            Game.PlayerManager.GetClientInfoByChampion(champion),
            champion.NetId,
            Game.PlayerManager.GetPeerInfo(clientId).Champion.Team
        );
        c.SetPosition(
            champion.Position
        );

        c.ChangeModel(split[1]); // trigger the "modelUpdate" proc
        c.SetTeam(champion.Team);
        ApiHandlers.RemoveGameObject(champion);
        ApiHandlers.AddGameObject(c);
        Game.PlayerManager.GetPeerInfo(clientId).Champion = c;
    }
}
