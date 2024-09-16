
namespace Commands
{
    public class TpCommand(CommandManager commandManager) : CommandBase(commandManager)
    {
        public override string Command => "tp";
        public override string Syntax => $"{Command} [target NetID (0 or none for self)] X Y";

        public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
        {
            var split = arguments.ToLower().Split(' ');

            if (split.Length < 3 || split.Length > 4)
            {
                SyntaxError();
                ShowSyntax();
                return;
            }

            if (split.Length > 3 && uint.TryParse(split[1], out uint targetNetID) && float.TryParse(split[2], out float x) && float.TryParse(split[3], out float y))
            {
                var obj = ApiHandlers.GetGameObjectByNetId(targetNetID);
                if (obj is AttackableUnit unit)
                {
                    unit.TeleportTo(new Vector2(x, y));
                }
                else
                {
                    SyntaxMessage("An object with the netID: " + targetNetID + " was not found.");
                    ShowSyntax();
                }
            }
            else if (float.TryParse(split[1], out x) && float.TryParse(split[2], out y))
            {
                champion.TeleportTo(new Vector2(x, y), true);
            }
        }
    }
}