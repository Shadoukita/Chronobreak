using Buffs;

namespace ScriptsCore.Commands
{
    public class DisableCooldownCommand(CommandManager commandManager) : CommandBase(commandManager)
    {
        public override string Command => "debugcd";
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
                    SpellBuffRemove(champion, nameof(DebugCooldownBuff));
                    break;
                case 1:
                    AddBuff(champion, champion, new DebugCooldownBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL);
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
    public class DebugCooldownBuff : CBuffScript
    {
        public override void OnUpdateStats()
        {
            if (target is ObjAIBase t)
            {
                //SummonerSpell Cooldowns Resets dont work?
                //SetSlotSpellCooldownTime(t, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SummonerSpellSlots, 0);
                //SetSlotSpellCooldownTime(t, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SummonerSpellSlots, 0);

                SetSlotSpellCooldownTime(t, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SetSlotSpellCooldownTime(t, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SetSlotSpellCooldownTime(t, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                SetSlotSpellCooldownTime(t, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);

                SetSlotSpellCooldownTime(t, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots, 0);
                SetSlotSpellCooldownTime(t, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots, 0);
                SetSlotSpellCooldownTime(t, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots, 0);
                SetSlotSpellCooldownTime(t, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots, 0);
                SetSlotSpellCooldownTime(t, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots, 0);
                SetSlotSpellCooldownTime(t, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots, 0);
                SetSlotSpellCooldownTime(t, 6, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots, 0);
            }
        }
    }
}