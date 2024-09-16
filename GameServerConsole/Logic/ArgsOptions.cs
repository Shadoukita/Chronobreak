using CommandLine;

namespace Chronobreak.GameServerConsole;

/// <summary>
/// Class housing launch arguments and their parsing used for the GameServerConsole.
/// </summary>
public class ArgsOptions
{
    [Option("config", Default = "Settings/GameInfo.json")]
    public string GameInfoJsonPath { get; set; }

    [Option("config-gameserver", Default = "Settings/GameServerSettings.json")]
    public string GameServerSettingsJsonPath { get; set; }

    [Option("config-json", Default = "")]
    public string GameInfoJson { get; set; }

    [Option("config-gameserver-json", Default = "")]
    public string GameServerSettingsJson { get; set; }

    [Option("port", Default = (ushort)5119)]
    public ushort ServerPort { get; set; }

    public static ArgsOptions Parse(string[] args)
    {
        ArgsOptions options = null;
        Parser.Default.ParseArguments<ArgsOptions>(args).WithParsed(argOptions => options = argOptions);
        return options;
    }
}