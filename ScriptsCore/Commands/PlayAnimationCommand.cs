namespace Commands;

public class PlayAnimationCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "playanim";
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
            champion.PlayAnimation(split[1]);
        }
    }
}
