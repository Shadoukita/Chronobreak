namespace Commands;

public class LevelCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "level";
    public override string Syntax => $"{Command} level";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        var maxLevel = ApiHandlers.MapGameMode.MapScriptMetadata.MaxLevel;

        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (byte.TryParse(split[1], out var lvl))
        {
            if (lvl <= champion.Experience.Level || lvl > maxLevel)
            {
                ChatManager.Send($"The level must be higher than current and smaller or equal to what the gamemode allows({maxLevel})!");
                return;
            }
            champion.Experience.LevelUp((byte)(lvl - champion.Experience.Level));
        }
    }
}
