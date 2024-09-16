using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chronobreak.GameServer.Chatbox;
using Chronobreak.GameServer.Core;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.Commands
{
    public class CommandManager
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        public string CommandStarterCharacter = "!";

        private SortedDictionary<string, CommandBase> _chatCommandsDictionary;

        public CommandManager(bool cheatsEnabled)
        {
            _chatCommandsDictionary = [];

            if (!cheatsEnabled)
            {
                return;
            }

            _chatCommandsDictionary = GetAllChatCommandHandlers(AssemblyService.GetAssemblies());
        }


        internal void ExecuteCommand(int clientId, string msg)
        {
            var split = msg.ToLower().Split(' ');

            var command = GetCommand(split[0]);
            if (command != null)
            {
                try
                {
                    command.Execute(Game.PlayerManager.GetPeerInfo(clientId).Champion, clientId, split.Length > 1, msg);
                }
                catch (Exception e)
                {
                    _logger.Warn($"{command} sent an exception:\n{e}");
                    ChatManager.Send("Something went wrong... Did you write the command correctly?", clientId);
                }
                return;
            }

            var errorMsg = HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.RED,
                               HTMLFHelperUtils.Bold(CommandStarterCharacter + split[0])) +
                           HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.YELLOW, " is not a valid command.");

            Game.PacketNotifier.NotifyS2C_SystemMessage(
                HTMLFHelperUtils.Font(20, HTMLFHelperUtils.Colors.RED, HTMLFHelperUtils.Bold("[ERROR]")) +
                HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.YELLOW, ": " + errorMsg));

            var infoMsg = HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.PINK,
                              HTMLFHelperUtils.Bold(CommandStarterCharacter + "help")) +
                          HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.PINK, " for a list of available commands.");

            Game.PacketNotifier.NotifyS2C_SystemMessage(
                HTMLFHelperUtils.Font(20, HTMLFHelperUtils.Colors.GREEN, HTMLFHelperUtils.Bold("[CB INFO]") +
                                                                         HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.YELLOW, ": " + infoMsg)));
        }

        internal SortedDictionary<string, CommandBase> GetAllChatCommandHandlers(Assembly[] loadFromArray)
        {
            var commands = new List<CommandBase>();
            var args = new object[] { this };
            foreach (var loadFrom in loadFromArray)
            {
                commands.AddRange(loadFrom.GetTypes()
                    .Where(t => t.BaseType == typeof(CommandBase) && t.Namespace == "Commands")
                    .Select(t => (CommandBase)Activator.CreateInstance(t, args)));
            }
            var commandsOutput = new SortedDictionary<string, CommandBase>();

            foreach (var converter in commands)
            {
                commandsOutput.Add(converter.Command, converter);
            }

            return commandsOutput;
        }

        public bool AddCommand(CommandBase command)
        {
            if (_chatCommandsDictionary.ContainsKey(command.Command))
            {
                return false;
            }

            _chatCommandsDictionary.Add(command.Command, command);
            return true;
        }

        public bool RemoveCommand(CommandBase command)
        {
            if (!_chatCommandsDictionary.ContainsValue(command))
            {
                return false;
            }

            _chatCommandsDictionary.Remove(command.Command);
            return true;
        }

        public bool RemoveCommand(string commandString)
        {
            if (!_chatCommandsDictionary.ContainsKey(commandString))
            {
                return false;
            }

            _chatCommandsDictionary.Remove(commandString);
            return true;
        }

        public List<CommandBase> GetCommands()
        {
            return _chatCommandsDictionary.Values.ToList();
        }

        public List<string> GetCommandsStrings()
        {
            return _chatCommandsDictionary.Keys.ToList();
        }

        public CommandBase GetCommand(string commandString)
        {
            if (_chatCommandsDictionary.ContainsKey(commandString))
            {
                return _chatCommandsDictionary[commandString];
            }

            return null;
        }

        internal void Update()
        {
            foreach (var command in _chatCommandsDictionary.Values)
            {
                command.Update();
            }
        }
    }
}
