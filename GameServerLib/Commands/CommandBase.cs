using Chronobreak.GameServer.Chatbox;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

namespace Chronobreak.GameServer.Commands
{
    public abstract class CommandBase(CommandManager commandManager)
    {
        protected readonly CommandManager CommandManager = commandManager;

        public abstract string Command { get; }
        public abstract string Syntax { get; }
        public bool IsHidden { get; set; }
        public bool IsDisabled { get; set; }

        protected void ShowSyntax()
        {
            var msg = $"{CommandManager.CommandStarterCharacter}{Syntax}";
            SyntaxMessage(msg);
        }

        public abstract void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "");

        public virtual void Update()
        {
        }

        /// <summary>
        /// Sends a formatted message with a green [CB INFO] tag.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontSize"></param>
        public virtual void Info(string text, int fontSize = 20)
        {
            var color = HTMLFHelperUtils.Colors.GREEN;
            var tag = HTMLFHelperUtils.Bold("[CB INFO]");

            ChatManager.Send(
                HTMLFHelperUtils.Font(fontSize, color, tag) +
                HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.YELLOW, ": " + text));
        }

        /// <summary>
        /// Sends a formatted message with a blue [SYNTAX] tag.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontSize"></param>
        public virtual void SyntaxMessage(string text, int fontSize = 20)
        {
            var color = HTMLFHelperUtils.Colors.BLUE;
            var tag = HTMLFHelperUtils.Bold("[SYNTAX]");

            ChatManager.Send(HTMLFHelperUtils.Font(fontSize, color, tag) +
                             HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.YELLOW, ": " + text));
        }

        /// <summary>
        /// Sends a syntax error message with a red [ERROR] tag.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontSize"></param>
        public virtual void SyntaxError(string text = ": Incorrect command syntax", int fontSize = 20)
        {
            var color = HTMLFHelperUtils.Colors.RED;
            var tag = HTMLFHelperUtils.Bold("[ERROR]");

            ChatManager.Send(
                HTMLFHelperUtils.Font(fontSize, color, tag) +
                HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.YELLOW, ": " + text));
        }

        /// <summary>
        /// Sends a formatted message with a red [ERROR] tag.
        /// </summary>
        /// <param name="text">Text to pass</param>
        /// <param name="fontSize"></param>
        public virtual void Error(string text, int fontSize = 20)
        {
            var color = HTMLFHelperUtils.Colors.RED;
            var tag = HTMLFHelperUtils.Bold("[ERROR]");

            ChatManager.Send(
                HTMLFHelperUtils.Font(fontSize, color, tag) +
                HTMLFHelperUtils.Font(HTMLFHelperUtils.Colors.YELLOW, ": " + text));
        }
    }
}
