namespace Chronobreak.GameServer;

/// <summary>
/// Class that handles generating Network related Ids
/// </summary>
public static class NetworkId
{
    private static uint _dwStart = 0x40000000; //new netid
    private static readonly object _lock = new();

    private static int _clientIndex = -1;
    private static readonly object _clientIdLock = new();

    /// <summary>
    /// Generates a new unique net identifier for a GameObject.
    /// </summary>
    public static uint GetNetId()
    {
        lock (_lock)
        {
            _dwStart++;
            return _dwStart;
        }
    }

    /// <summary>
    /// Generates a new unique Client/Peer ID
    /// </summary>
    internal static int GetClientId()
    {
        lock (_clientIdLock)
        {
            _clientIndex++;
            return _clientIndex;
        }
    }
}