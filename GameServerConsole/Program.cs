using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using GameServerConsole.Properties;
using Chronobreak.GameServer;
using Chronobreak.GameServer.Logging;
using Chronobreak.GameServerConsole.Logic;
using Chronobreak.GameServerConsole.Utility;
using log4net;

namespace Chronobreak.GameServerConsole
{
    /// <summary>
    /// Class representing the program piece, or commandline piece of the server; where everything starts (GameServerConsole -> GameServer, etc).
    /// </summary>
    internal class Program
    {
        private static readonly ILog _logger = LoggerProvider.GetLogger();
        private static ArgsOptions _parsedArgs;
        private static Config _config;
        private static Game _game;
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        private static void Main(string[] args)
        {
            HouseKeeping(args);
            Banner();
            Launch();
        }

        private static void HouseKeeping(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && GetConsoleWindow() != IntPtr.Zero)
            {
                Console.SetWindowSize((int)(Console.LargestWindowWidth * 0.75f), Console.WindowHeight);
            }

            var eEventHandler = new UnhandledExceptionEventHandler((sender, eventArgs) =>
            {
                _logger.Fatal(null, (Exception)eventArgs.ExceptionObject);
            });
            AppDomain.CurrentDomain.UnhandledException += eEventHandler;

            // Culture can cause interference in reading numbers and dates
            var culture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // If the command line interface was ran with additional parameters (perhaps via a shortcut or just via another command line)
            // Refer to ArgsOptions for all possible launch parameters
            _parsedArgs = ArgsOptions.Parse(args);
            _parsedArgs.GameInfoJson = LoadConfig(_parsedArgs.GameInfoJsonPath, _parsedArgs.GameInfoJson, Encoding.UTF8.GetString(Resources.GameInfo));
        }

        private static void Banner()
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            ConsoleColor[] colors = new[]
            {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Blue,
            };
            string build = $"Chronobreak Build: {BuildInfo.ServerVersion} {BuildInfo.ServerBuildTime}";
            string[] result = """
                         ,- _~. ,,                            ,,                      ,,
                        (' /|   ||                            ||                  _   ||
                       ((  ||   ||/\\ ,._-_  /'\\ \\/\\  /'\\ ||/|, ,._-_  _-_   < \, ||/\
                       ((  ||   || ||  ||   || || || || || || || ||  ||   || \\  /-|| ||_<
                        ( / |   || ||  ||   || || || || || || || |'  ||   ||/   (( || || |
                         -____- \\ |/  \\,  \\,/  \\ \\ \\,/  \\/    \\,  \\,/   \/\\ \\,\
                                  _/
                       """.Split("\n");

            Console.Title = build;
            for (var i = 0; i < result.Length; i++)
            {
                Console.ForegroundColor = colors[i % colors.Length];
                Console.WriteLine(result[i]);
            }
            Console.ForegroundColor = currentColor;
            _logger.Debug(build);
        }

        private static void Launch()
        {
            try
            {
                _config = new Config(_parsedArgs.GameInfoJson);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.GetType()}: {ex.Message}");
                Console.ReadKey();
                return;
            }
            //TODO: Unhardcode these assembly names and move them to the Game's config, so users can easily load their own custom dlls
            _game = new Game(_config, _parsedArgs.ServerPort);
            _logger.Info($"Game started on port: {_parsedArgs.ServerPort}");

#if DEBUG
            // When debugging, optionally the game client can be launched automatically given the path (placed in GameServerSettings.json) to the folder containing the League executable.
            var configGameServerSettings = new GameServerConfig();
            configGameServerSettings.Load(_parsedArgs.GameServerSettingsJson, _parsedArgs.GameServerSettingsJsonPath);

            if (configGameServerSettings.AutoStartClient)
            {
                LaunchClient(_parsedArgs, configGameServerSettings.ClientLocation);
            }
            else
            {
                _logger.Info("Server is ready, clients can now connect.");
            }
#endif
            // This is where the actual GameServer starts.
#if !DEBUG
            try
            {
#endif
            _game.Start();
#if !DEBUG
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
#endif
        }

        /// <summary>
        /// Used to parse any of the configuration files used for the GameServer, ex: GameInfo.json or GameServerSettings.json. 
        /// </summary>
        /// <param name="filePath">Full path to the configuration file.</param>
        /// <param name="currentJsonString">String representing the content of the configuration file. Usually empty.</param>
        /// <param name="defaultJsonString">String representing the default content of the configuration file. Usually what is already defined in the respective configuration file.</param>
        /// <returns>The string defined in the configuration file or defined via launch arguments.</returns>
        private static string LoadConfig(string filePath, string currentJsonString, string defaultJsonString)
        {
            if (!string.IsNullOrEmpty(currentJsonString))
            {
                return currentJsonString;
            }

            try
            {
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }

                var settingsDirectory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(settingsDirectory))
                {
                    throw new Exception($"Creating Config File failed. Invalid Path: {filePath}");
                }
                Directory.CreateDirectory(settingsDirectory);
                File.WriteAllText(filePath, defaultJsonString);
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }

            return defaultJsonString;
        }

        private static void LaunchClient(ArgsOptions args, string clientLocation)
        {
            //List<Process> processes = new(); ??
            var leaguePath = clientLocation;
            if (Directory.Exists(leaguePath))
            {
                leaguePath = Path.Combine(leaguePath, "League of Legends.exe");
            }
            if (!File.Exists(leaguePath) || string.IsNullOrEmpty(leaguePath))
            {
                _logger.Warn("Unable to find League of Legends.exe. Check the GameServerSettings.json settings and your League location.");
                return;
            }

            string arguments = $"\"8394\" \"LoLLauncher.exe\" \"\" \"127.0.0.1 {args.ServerPort} {_config.Players[0].BlowfishKey} {_config.Players[0].PlayerID} NA\"";
            string workingDirectory = Path.GetDirectoryName(leaguePath);
            ProcessStartInfo startInfo = new(leaguePath)
            {
                Arguments = arguments,
                WorkingDirectory = workingDirectory
            };

            Process leagueProcess = new()
            {
                StartInfo = startInfo,
            };

            //processes.Add(leagueProcess); ??

            _logger.Info("Launching League of Legends. You can disable this in GameServerSettings.json.");

            leagueProcess.Start();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WindowsConsoleCloseDetection.SetCloseHandler((_) =>
                {
                    if (!leagueProcess.HasExited)
                    {
                        leagueProcess.Kill();
                    }
                    return true;
                });
            }
        }
    }
}
