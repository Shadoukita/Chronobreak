namespace Commands;

public class StopAnimationCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "stopanim";
    public override string Syntax => $"{Command} animationName";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');
        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else
        {
            champion.StopAnimation(split[1]);
        }
    }
}
