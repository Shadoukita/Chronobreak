namespace Commands;

public class RainbowCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    private Champion _me;
    private bool _run;
    private float _a = 0.5f;
    private float _speed = 0.25f;
    private int _delay = 250;

    public override string Command => "rainbow";
    public override string Syntax => $"{Command} alpha speed";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        var split = arguments.ToLower().Split(' ');

        _me = champion;

        if (split.Length > 1)
        {
            _ = float.TryParse(split[1], out _a);
        }

        if (split.Length > 2)
        {
            _ = float.TryParse(split[2], out _speed);
            _delay = (int)(_speed * 1000);
        }

        _run = !_run;
        if (_run)
        {
            Task.Run(() => TaskRainbow());
        }
    }

    public void TaskRainbow()
    {
        while (_run)
        {
            var rainbow = new byte[4];
            new Random().NextBytes(rainbow);
            Thread.Sleep(_delay);
            BroadcastTint(_me.Team, false, 0.0f, 0, 0, 0, 1f);
            BroadcastTint(_me.Team, true, _speed, rainbow[1], rainbow[2], rainbow[3], _a);
        }

        Thread.Sleep(_delay);
        BroadcastTint(_me.Team, false, 0.0f, 0, 0, 0, 1f);
    }

    public void BroadcastTint(TeamId team, bool enable, float speed, byte r, byte g, byte b, float a)
    {
        Color color = new()
        {
            R = r,
            G = g,
            B = b,
            A = (byte)(uint)(a * 255.0f)
        };
        ApiHandlers.PacketNotifier.NotifyTint(team, enable, speed, 1, color);
    }
}
