using Chronobreak.GameServer.Chatbox;
using Chronobreak.GameServer.Commands;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Commands;

public class TemplateCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "template";
    public override string Syntax => $"{Command} [example1, example2, example3]";

    private string Example1 => HTMLFHelperUtils.Font(20, HTMLFHelperUtils.Colors.RED, "Executed Example-1");
    private string Example2 => HTMLFHelperUtils.Font(20, HTMLFHelperUtils.Colors.YELLOW, "Executed Example-2");
    private string Example3 => HTMLFHelperUtils.Font(20, HTMLFHelperUtils.Colors.GREEN, "Executed Example-3");

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        if (!hasReceivedArguments)
        {
            ChatManager.Send($"Not enough args provided for command {Command}", clientId);
        }
        var args = arguments.ToLower().Split(' ').ToList();

        switch (args[1])
        {
            case "example1":
                ChatManager.Send($"TemplateCommands: {Example1}", clientId);
                break;
            case "example2":
                ChatManager.Send($"TemplateCommands: {Example2}", clientId);
                break;
            case "example3":
                ChatManager.Send($"TemplateCommands: {Example3}", clientId);
                break;
            default:
                ShowSyntax();
                break;
        }
    }
}