using Buffs;

namespace Commands
{
    public class DebugManaCommand(CommandManager commandManager) : CommandBase(commandManager)
    {
        public override string Command => "debugmana";
        public override string Syntax => $"{Command} (0|1)";

        public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
        {
            if (!int.TryParse(arguments.Split(" ").LastOrDefault(), out int option))
            {
                SyntaxError();
                ShowSyntax();
                return;
            }

            switch (option)
            {
                case 0:
                    SpellBuffRemove(champion, nameof(DebugManaBuff));
                    break;
                case 1:
                    AddBuff(champion, champion, new DebugManaBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL);
                    break;
                default:
                    SyntaxError();
                    ShowSyntax();
                    break;
            }
        }
    }
}

namespace Buffs
{
    public class DebugManaBuff : CBuffScript
    {
        public override void OnUpdateStats()
        {
            if (target.Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.MANA)
            {
                IncPAR(target, 1000, PrimaryAbilityResourceType.MANA);
            }
            else if (target.Stats.PrimaryAbilityResourceType == PrimaryAbilityResourceType.Energy)
            {
                IncPAR(target, 1000, PrimaryAbilityResourceType.Energy);
            }
        }
    }
}