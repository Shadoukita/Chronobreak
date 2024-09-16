using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.API;

/// <summary>
/// Class that contains functions that should only be used for debugging purposes.
/// </summary>
public static class DebugUtils
{
    private static ILog _logger = LoggerProvider.GetLogger();

    /// <summary>
    /// Logs the given string and its arguments to the server console as info.
    /// Instanced classes in the arguments will be a string representation of the object's namespace.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    public static void LogInfo(string format, params object[] args)
    {
        _logger.Info(string.Format(format, args));
    }

    /// <summary>
    /// Logs the given string to the server console as debug info.
    /// Only works in Debug mode.
    /// Instanced classes in the arguments will be a string representation of the object's namespace.
    /// </summary>
    /// <param name="format">String to debug print.</param>
    public static void LogDebug(string format, params object[] args)
    {
        _logger.Debug(string.Format(format, args));
    }

    /// <summary>
    /// Logs the given string to the server console as info.
    /// </summary>
    /// <param name="format">String to print.</param>
    public static void LogInfo(string format)
    {
        _logger.Info(format);
    }

    /// <summary>
    /// Logs the given string to the server console as debug info.
    /// Only works in Debug mode.
    /// </summary>
    /// <param name="format">String to debug print.</param>
    public static void LogDebug(string format)
    {
        _logger.Debug(format);
    }
}