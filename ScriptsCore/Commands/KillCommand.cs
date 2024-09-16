namespace Commands;

public class KillCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "kill";
    public override string Syntax => $"{Command} minions";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');

        if (split.Length < 2)
        {
            SyntaxError();
            ShowSyntax();
        }
        else if (split[1] is "minions")
        {
            foreach (var o in ApiHandlers.GetGameObjects())
            {
                if (o.Value is Minion minion)
                {
                    ApplyDamage(minion, minion, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW);
                }
            }
        }
        else
        {
            SyntaxError();
            ShowSyntax();
        }
    }
}
