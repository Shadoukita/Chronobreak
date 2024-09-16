using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using GameServerCore.Enums;
using GameServerLib.Packets;
using GameServerLib.Services;
using LeaguePackets.Game;
using LeaguePackets.LoadScreen;
using Chronobreak.GameServer.Chatbox;
using Chronobreak.GameServer.Commands;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.Core;
using Chronobreak.GameServer.Handlers;
using Chronobreak.GameServer.Logging;
using Chronobreak.GameServer.Packets.PacketHandlers;
using Chronobreak.GameServer.Scripting.CSharp;
using log4net;
using PacketDefinitions420;

namespace Chronobreak.GameServer
{
    /// <summary>
    /// Class that contains and manages all qualities of the game such as managers for networking and game mechanics, as well as the starting, pausing, and stopping of the game.
    /// </summary>
    public partial class Game
    {
        // Crucial Game Vars

        private static List<GameScriptTimer> _gameScriptTimers = [];
        /// <summary>
        /// Class containing all information about the game's configuration such as game content location, map spawn points, whether cheat commands are enabled, etc.
        /// </summary>
        public static Config Config { get; protected set; }

        // Function Vars

        private static ILog _logger = LoggerProvider.GetLogger();

        // Server Vars

        /// <summary>
        /// Time until the game unpauses (if paused).
        /// </summary>
        public static long PauseTimeLeft { get; private set; }

        /// <summary>
        /// Time since the game has started. Mostly used for networking to sync up players with the server.
        /// </summary>

        // Networking Vars

        internal static PacketServer PacketServer { get; set; }
        /// <summary>
        /// Handler for request packets sent by game clients.
        /// </summary>
        internal static PacketRouter RequestHandler { get; set; }
        /// <summary>
        /// Interface containing all function related packets (except handshake) which are sent by the server to game clients.
        /// </summary>
        internal static PacketNotifier PacketNotifier { get; private set; }

        // Managers & Handlers

        /// <summary>
        /// Interface containing all (public) functions used by ObjectManager. ObjectManager manages GameObjects, their properties, and their interactions such as being added, removed, colliding with other objects or terrain, vision, teams, etc.
        /// </summary>
        internal static ObjectManager ObjectManager { get; private set; } = new();
        /// <summary>
        /// Class which manages all chat based commands.
        /// </summary>
        internal static CommandManager CommandManager { get; private set; }
        /// <summary>
        /// Interface of functions used to identify players or their properties (such as their champion).
        /// </summary>
        public static PlayerManager PlayerManager { get; private set; } = new();
        /// <summary>
        /// Class that handles the loading and passing of GameClient Info
        /// </summary>
        internal static ContentManager ContentManager { get; private set; }
        /// <summary>
        /// Contains all map related game settings such as collision handler, navigation grid, announcer events, and map properties. Doubles as a Handler/Manager for all MapScripts.
        /// </summary>
        internal static MapHandler Map { get; private set; }
        internal static StateHandler StateHandler { get; private set; }

        // Scripting Vars

        /// <summary>
        /// Class that compiles and loads all scripts which will be used for the game (ex: spells, items, AI, maps, etc).
        /// </summary>
        internal static CSharpScriptEngine ScriptEngine { get; private set; }

        /// <summary>
        /// Instantiates all game managers and handlers.
        /// </summary>
        public Game(Config config, ushort port)
        {
            _logger.Info("Creating GameServer Instance...");
            Config = config;
            AssemblyService.TryLoadAssemblies(config.AssemblyNames);
            ScriptEngine = new CSharpScriptEngine();
            StateHandler = new(30, Config.ForcedStart);
            CommandManager = new(Config.ChatCheatsEnabled);

            _logger.Info("Loading Server Content...");
            ContentManager = new(Config);
            Map = new(Config.GameConfig.Map);

            Time.SetTicksPerSecond(config.TickRate);
            SetupNetworking(port, Config.Players);

            _logger.Info("GameServer Ready!");
        }

        public void Start()
        {
            _logger.Info("Starting GameServer...");

            Map.LoadMapObjects();
            Map.OnLevelLoad();
            PlayerManager.AddPlayers(Config.Players);

            //TODO: Add setting a preferred IP rather listening on all possible connections
            _logger.Info($"GameServer ready for clients to connect on Port: {PacketServer.GetPort()}");
            GameLoop();
        }

        /// <summary>
        /// Function which initiates ticking of the game's logic.
        /// </summary>
        private void GameLoop()
        {
            double timeout = 0;

            Stopwatch lastMapDurationWatch = new();

            //bool wasNotPaused = true;

            while (StateHandler.State is not GameState.EXIT)
            {
                double lastSleepDuration = lastMapDurationWatch.Elapsed.TotalMilliseconds;
                lastMapDurationWatch.Restart();

                Time.Update((float)lastSleepDuration);

                switch (StateHandler.State)
                {
                    case GameState.PREGAME:
                        StateHandler.UpdatePreGame();
                        break;
                    case GameState.GAMELOOP:
                        Update();
                        break;
                    case GameState.PAUSE:
                        StateHandler.UpdatePause();
                        break;
                    case GameState.SPAWN:
                    case GameState.ENDGAME:
                    case GameState.PRE_EXIT:
                    case GameState.EXIT:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Invalid GameState: {StateHandler.State}");
                }

                double lastUpdateDuration = lastMapDurationWatch.Elapsed.TotalMilliseconds;
                double oversleep = lastSleepDuration - timeout;
                timeout = Math.Max(0, Time.TickRate - lastUpdateDuration - oversleep);

                PacketServer.NetLoop((uint)timeout);
            }
        }

        /// <summary>
        /// Function called every tick of the game.
        /// </summary>
        private static void Update()
        {
            // This section dictates the priority of updates.
            Time.GameTime += Time.DeltaTime;
            Map.Update();
            // Objects
            ObjectManager.Update();
            CommandManager.Update();

            //An error occurs here every end of game, cause is unknown
            try
            {
                foreach (GameScriptTimer timer in _gameScriptTimers.ToList())
                {
                    timer.Update();
                    if (timer.ToRemove)
                    {
                        _gameScriptTimers.Remove(timer);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        // Networking Methods

        /// <summary>
        /// Sets up the server's needed networking classes and variables
        /// </summary>
        /// <param name="port"></param>
        /// <param name="players"></param>
        private void SetupNetworking(ushort port, IReadOnlyList<PlayerConfig> players)
        {
            var blowfishKeys = new string[players.Count];
            for (var i = 0; i < players.Count; i++)
            {
                blowfishKeys[i] = players[i].BlowfishKey;
            }

            //TODO: Determine if ResponseHandler should still be used in the projects current/future state
            //ResponseHandler = new NetworkHandler<ICoreRequest>();
            RequestHandler = new PacketRouter();
#if !DEBUG
            try
            {
#endif
            PacketServer = new PacketServer(port, blowfishKeys, Config.LogOutPackets, Config.LogInPackets);
            InitializePacketHandlers();
#if !DEBUG
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
#endif
            // TODO: switch the notifier with ResponseHandler
            PacketNotifier = new PacketNotifier(PacketServer.PacketHandlerManager);
        }

        /// <summary>
        /// Registers Request Handlers for each request packet.
        /// </summary>
        private void InitializePacketHandlers()
        {
            // maybe use reflection, the problem is that Register is generic and so it needs to know its type at
            // compile time, maybe just use interface and in runetime figure out the type - and again there is
            // a problem with passing generic delegate to non-generic function, if we try to only constraint the
            // argument to interface ICoreRequest we will get an error cause our generic handlers use generic type
            // even with where statement that doesn't work

            ChatManager chatManager = new();
            RequestHandler.Register<Chat>(chatManager);
            RequestHandler.Register<QuickChat>(chatManager);

            CameraService cameraService = new();
            RequestHandler.Register<World_LockCamera_Server>(cameraService);
            RequestHandler.Register<World_SendCamera_Server>(cameraService);

            HandleSpells handleSpells = new();
            RequestHandler.Register<NPC_CastSpellReq>(handleSpells);
            RequestHandler.Register<C2S_SpellChargeUpdateReq>(handleSpells);

            HandleItem handleItem = new();
            RequestHandler.Register<BuyItemReq>(handleItem);
            RequestHandler.Register<C2S_UndoItemReq>(handleItem);
            RequestHandler.Register<RemoveItemReq>(handleItem);
            RequestHandler.Register<SwapItemReq>(handleItem);

            ClientConnectionService clientConnectionService = new();
            RequestHandler.Register<C2S_Exit>(clientConnectionService);
            RequestHandler.Register<C2S_SoftReconnect>(clientConnectionService);

            RequestHandler.Register(new HandleAttentionPing());
            RequestHandler.Register(new HandleAutoAttackOption());
            RequestHandler.Register(new HandleBlueTipClicked());
            RequestHandler.Register(new HandleClick());
            RequestHandler.Register(new HandleEmotion());
            RequestHandler.Register(new HandleSyncSimTime());
            RequestHandler.Register(new HandleLoadPing());
            RequestHandler.Register(new HandleJoinTeam());
            RequestHandler.Register(new HandleMove());
            RequestHandler.Register(new HandleMoveConfirm());
            RequestHandler.Register(new HandlePauseReq());
            RequestHandler.Register(new HandleQueryStatus());
            RequestHandler.Register(new HandleQuestClicked());
            RequestHandler.Register(new HandleScoreboard());
            RequestHandler.Register(new HandleUpgradeSpellReq());
            RequestHandler.Register(new HandleSpawn());
            RequestHandler.Register(new HandleStartGame());
            RequestHandler.Register(new HandleStatsConfirm());
            RequestHandler.Register(new HandleSurrender());
            RequestHandler.Register(new HandleSync());
            RequestHandler.Register(new HandleUnpauseReq());
            RequestHandler.Register(new HandleUseObject());
        }

        /// <summary>
        /// Adds a timer to the list of timers so that it ticks with the game.
        /// </summary>
        /// <param name="timer">Timer instance.</param>
        public static void AddGameScriptTimer(GameScriptTimer timer)
        {
            _gameScriptTimers.Add(timer);
        }

        //TODO: Remove?
        /// <summary>
        /// Unused function meant to get the instances of a specific type who rely on Game as a parameter.
        /// </summary>
        /// <returns>List of instances of type T.</returns>
        private static List<T?> GetInstances<T>(Game g)
        {
            return Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(t => t.BaseType == typeof(T))
                .Select(t => (T?)Activator.CreateInstance(t, g)).ToList() ?? [];
        }
    }
}
