namespace Commands;

public class ModelCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "model";
    public override string Syntax => $"{Command} modelName";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.Split(' ');
        if (split.Length >= 2)
        {
            champion.ChangeModel(split[1]);
        }
        else
        {
            SyntaxError();
            ShowSyntax();
        }
    }
}
