namespace Commands;

public class CoordsCommand(CommandManager commandManager) : CommandBase(commandManager)
{
    private static ILog _logger = LoggerProvider.GetLogger();

    public override string Command => "coords";
    public override string Syntax => $"{Command}";

    public override void Execute(Champion champion, int clientId, bool hasReceivedArguments, string arguments = "")
    {
        _logger.Debug($"At {champion.Position.X}; {champion.Position.Y}");

        string dirMsg = "Not moving anywhere";
        if (!champion.IsPathEnded())
        {
            Vector2 dir = champion.Direction.ToVector2();
            // Angle measured between [1, 0] and player direction vectors (to X axis)
            double ang = Math.Acos(dir.X / dir.Length()) * (180 / Math.PI);
            dirMsg = $"dirX: {dir.X} dirY: {dir.Y} dirAngle (to X axis): {ang}";
        }

        Vector3 coords3D = champion.GetPosition3D();
        string msg = $"At Coords - X: {coords3D.X} Y: {coords3D.Z} Height: {coords3D.Y} " + dirMsg;
        ChatManager.Send(msg);
    }
}
