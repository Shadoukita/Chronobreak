namespace Commands;

public class PacketCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    public override string Command => "packet";
    public override string Syntax => $"{Command} XX XX XX...";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        try
        {
            var s = arguments.Split(' ');
            if (s.Length < 2)
            {
                SyntaxError();
                ShowSyntax();
                return;
            }
            List<byte> _bytes = [Convert.ToByte(s[1], 16)];

            for (var i = 2; i < s.Length; i++)
            {
                if (s[i].Equals("netid"))
                {
                    _bytes.Add(Convert.ToByte(champion.NetId));
                }
                else
                {
                    _bytes.Add(Convert.ToByte(s[i], 16));
                }
            }

            ApiHandlers.PacketNotifier.NotifyDebugPacket(clientId, [.. _bytes]);
        }
        catch
        {
            // ignored
        }
    }
}
