namespace Commands;

public class ParticleCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "particle";
    public override string Syntax => $"{Command} <particle_name.troy>";

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
            //TODO:
            //ApiFunctionManager.AddParticleTarget(champion, split[1], champion);
        }
    }
}
