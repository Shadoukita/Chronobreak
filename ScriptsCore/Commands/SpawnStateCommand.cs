namespace Commands;

public class SpawnStateCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "spawnstate";
    public override string Syntax => $"{Command} 0 (disable) / 1 (enable)";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');

        if (split.Length < 2 || !byte.TryParse(split[1], out var input) || input > 1)
        {
            SyntaxError();
            ShowSyntax();
        }
        else
        {
            ApiHandlers.MapGameMode.MapScriptMetadata.MinionSpawnEnabled = input != 0;
            Game.Config.SetGameFeatures(FeatureFlags.EnableLaneMinions, input != 0);
        }
    }
}
