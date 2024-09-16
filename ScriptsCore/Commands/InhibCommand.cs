namespace Commands;

public class InhibCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "inhib";
    public override string Syntax => $"{Command}";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var min = new Minion(
            null,
            champion.Position,
            "Worm",
            "Worm",
            AIScript: "BasicJungleMonsterAI"
            );
        ApiHandlers.AddGameObject(min);
    }
}
