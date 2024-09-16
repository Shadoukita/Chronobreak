using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameServerCore.Enums;
using Chronobreak.GameServer.Logging;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chronobreak.GameServer
{
    /// <summary>
    /// Class that contains basic game information which is used to decide how the game will function after starting, such as players, their spawns,
    /// the packages which control the functionality of their champions/abilities, and lastly whether basic game mechanics such as 
    /// cooldowns/mana costs/minion spawns should be enabled/disabled.
    /// </summary>
    public class Config
    {
        internal const string VERSION_STRING = "Version 4.20.0.315 [PUBLIC]";

        private static ILog _logger = LoggerProvider.GetLogger();
        internal static readonly Version VERSION = new(4, 20, 0, 315);

        public List<PlayerConfig> Players { get; private set; }
        internal string HttpPostAddress { get; private set; } = string.Empty;
        internal bool SupressScriptNotFound { get; private set; }

        internal GameConfig GameConfig { get; private set; } = null!;
        internal FeatureFlags GameFeatures { get; private set; }
        internal float ForcedStart { get; private set; }
        internal int TickRate { get; private set; }
        internal long GameId { get; private set; }
        internal bool ChatCheatsEnabled { get; private set; }
        internal bool IsDamageTextGlobal { get; private set; }
        internal string ContentPath { get; private set; } = string.Empty;
        internal string[] AssemblyNames { get; private set; } = [];
        internal bool LogInPackets { get; private set; }
        internal bool LogOutPackets { get; private set; }

        public bool EnableContentLogs { get; set; }
        public bool UseCacheFile { get; set; }

        /// <summary>
        /// Loads config from json text or a json file.
        /// </summary>
        /// <param name="json">Config Text or path to Config File</param>
        /// <exception cref="ArgumentNullException">Invalid/Null/Empty Config provided</exception>
        public Config(string json)
        {
            Players = [];

            //Check Json File
            if (File.Exists(json))
            {
                var file = File.ReadAllText(json);
                if (string.IsNullOrEmpty(file))
                {
                    throw new ArgumentNullException(json, "The provided json file is empty, please provide a valid config.");
                }
                LoadConfig(file);
                return;
            }

            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentNullException(json, "The provided json text is null or empty, please provide a valid config.");
            }

            // Load raw json text
            LoadConfig(json);
        }

        private void LoadConfig(string json)
        {
            _logger.Info("PARSING CONFIG");
            JObject data;
            try
            {
                data = JObject.Parse(json);
            }
            catch (JsonReaderException)
            {
                _logger.Error("Error Parsing GameInfo config file!");
                throw;
            }
            GameId = data.Value<long>("gameId");

            // DATA
            GameConfig = new GameConfig(data?.Value<JToken?>("game"));

            JArray playerConfigurations = data?.Value<JArray>("players")!;
            foreach (var player in playerConfigurations)
            {
                var playerConfig = new PlayerConfig(player);
                Players.Add(playerConfig);
            }

            // GAME-INFO
            JToken? gameInfo = data?.Value<JToken?>("gameInfo");

            TickRate = gameInfo?.Value<int?>("TICK_RATE") ?? 30;
            //Time to the game get forced to start, even if not all players are connected
            ForcedStart = gameInfo?.Value<float?>("FORCE_START_TIMER") * 1000 ?? 60_000.0f;

            UseCacheFile = gameInfo?.Value<bool?>("USE_CACHE") ?? false;
            IsDamageTextGlobal = gameInfo?.Value<bool?>("IS_DAMAGE_TEXT_GLOBAL") ?? false;
            EnableContentLogs = gameInfo?.Value<bool?>("ENABLE_CONTENT_LOADING_LOGS") ?? false;
            SupressScriptNotFound = gameInfo?.Value<bool?>("SUPRESS_SCRIPT_NOT_FOUND_LOGS") ?? false;
            ChatCheatsEnabled = gameInfo?.Value<bool?>("CHEATS_ENABLED") ?? false;
            LogInPackets = gameInfo?.Value<bool?>("LOG_IN_PACKETS") ?? false;
            LogOutPackets = gameInfo?.Value<bool?>("LOG_OUT_PACKETS") ?? false;
            SetGameFeatures(FeatureFlags.EnableManaCosts, gameInfo?.Value<bool?>("MANACOSTS_ENABLED") ?? true);
            SetGameFeatures(FeatureFlags.EnableCooldowns, gameInfo?.Value<bool?>("COOLDOWNS_ENABLED") ?? true);
            SetGameFeatures(FeatureFlags.EnableLaneMinions, gameInfo?.Value<bool?>("MINION_SPAWNS_ENABLED") ?? true);

            ContentPath = gameInfo?.Value<string?>("CONTENT_PATH") ?? string.Empty;
            //Address used to make an end-game API call to deliver all game stats. (Used for end-game screen on laucnher-clients mostly)
            HttpPostAddress = gameInfo?.Value<string?>("ENDGAME_HTTP_POST_ADDRESS") ?? string.Empty;
            AssemblyNames = gameInfo?.SelectToken("scriptAssemblies")?.Values<string>().ToArray() as string[] ?? [];

            //Evaluate if content path is correct, if not try to path traversal to find it
            if (!Directory.Exists(ContentPath))
            {
                throw new DirectoryNotFoundException($"Content directory \"{ContentPath}\" does not exist!");
            }
        }

        public void SetGameFeatures(FeatureFlags flag, bool enabled)
        {
            // Toggle the flag on.
            if (enabled)
            {
                GameFeatures |= flag;
            }
            // Toggle off.
            else
            {
                GameFeatures &= ~flag;
            }
        }
    }
}