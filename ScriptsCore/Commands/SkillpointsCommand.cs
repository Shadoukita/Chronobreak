namespace Commands;

public class SkillpointsCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "skillpoints";
    public override string Syntax => $"{Command}";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        champion.Experience.SpellTrainingPoints.AddTrainingPoints(17);
        ApiHandlers.PacketNotifier.NotifyNPC_UpgradeSpellAns(clientId, champion.NetId, 0, 0, 17);
    }
}
