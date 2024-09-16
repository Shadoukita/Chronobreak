using System;
using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Enums;
using GameServerCore.Packets.Enums;
using LeaguePackets.Game.Common;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Chronobreak.GameServer.Handlers;
using Chronobreak.GameServer.Logging;
using log4net;
using MapScripts;

namespace Chronobreak.GameServer.API
{
    public static class ApiMapFunctionManager
    {
        // Required variables.
        private static ILog _logger = LoggerProvider.GetLogger();

        public static Shop CreateShop(string name, Vector2 position, TeamId team)
        {
            return new Shop(name, position, team);
        }

        /// <summary>
        /// Creates and returns a nexus
        /// </summary>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <param name="position"></param>
        /// <param name="team"></param>
        /// <param name="nexusRadius"></param>
        /// <param name="sightRange"></param>
        /// <returns></returns>
        public static Nexus CreateNexus(string name, string model, Vector2 position, TeamId team, int nexusRadius, int sightRange, Stats stats = null)
        {
            Nexus obj = new(name, team, nexusRadius, position, sightRange, stats);
            AddObject(obj);
            return obj;
        }

        /// <summary>
        /// Creates and returns an inhibitor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <param name="position"></param>
        /// <param name="team"></param>
        /// <param name="lane"></param>
        /// <param name="inhibRadius"></param>
        /// <param name="sightRange"></param>
        /// <returns></returns>
        public static Inhibitor CreateInhibitor(string name, Vector2 position, TeamId team, int inhibRadius, int sightRange, Stats stats = null)
        {
            Inhibitor obj = new(name, team, inhibRadius, position, sightRange, stats);
            return obj;
        }

        public static Barrack CreateBarrack(string name, Vector2 position, TeamId team)
        {
            return new Barrack(name, team, position);
        }

        /// <summary>
        /// Creates a tower
        /// </summary>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <param name="position"></param>
        /// <param name="team"></param>
        /// <param name="turretType"></param>
        /// <param name="turretItems"></param>
        /// <param name="lane"></param>
        /// <param name="aiScript"></param>
        /// <param name="mapObject"></param>
        /// <param name="netId"></param>
        /// <returns></returns>
        public static LaneTurret CreateLaneTurret(string name, string model, Vector2 position, TeamId team, int turretPosition, Lane lane = Lane.LANE_Unknown, string aiScript = "", MapObject mapObject = default, uint netId = 0)
        {
            return new(name, model, position, turretPosition, team, lane, mapObject, null, aiScript, netId);
        }

        /// <summary>
        /// Gets the turret item list from MapScripts
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int[] GetTurretItems(Dictionary<int, int[]> turretItemList, int type)
        {
            if (!turretItemList.ContainsKey(type))
            {
                return Array.Empty<int>();
            }

            return turretItemList[type];
        }

        /// <summary>
        /// Creates and returns a minion
        /// </summary>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <param name="position"></param>
        /// <param name="netId"></param>
        /// <param name="team"></param>
        /// <param name="skinId"></param>
        /// <param name="ignoreCollision"></param>
        /// <param name="isTargetable"></param>
        /// <param name="aiScript"></param>
        /// <param name="damageBonus"></param>
        /// <param name="healthBonus"></param>
        /// <param name="initialLevel"></param>
        /// <returns></returns>
        public static Minion CreateMinion(
            string name, string model, Vector2 position, ObjAIBase owner = null, uint netId = 0,
            TeamId team = TeamId.TEAM_NEUTRAL, int skinId = 0, bool ignoreCollision = false,
            bool isTargetable = false, bool isWard = false, string aiScript = "", int initialLevel = 1)
        {
            var m = new Minion(owner, position, model, name, team, skinId, ignoreCollision, isTargetable, isWard, null, null, aiScript, initialLevel);
            Game.ObjectManager.AddObject(m);
            return m;
        }

        public static Minion CreateMinionTemplete(
            string name, string model, Vector2 position,
            TeamId team = TeamId.TEAM_NEUTRAL, int skinId = 0, bool ignoreCollision = false,
            bool isTargetable = false, bool isWard = false, string aiScript = "", int initialLevel = 1)
        {
            return new Minion(null, position, model, name, team, skinId, ignoreCollision, isTargetable, isWard, null, null, aiScript, initialLevel);
        }

        /// <summary>
        /// Checks if minion spawn is enabled
        /// </summary>
        /// <returns></returns>
        public static bool IsMinionSpawnEnabled()
        {
            return Game.Config.GameFeatures.HasFlag(FeatureFlags.EnableLaneMinions);
        }

        /// <summary>
        /// Creates and returns a jungle camp
        /// </summary>
        /// <param name="position"></param>
        /// <param name="groupNumber"></param>
        /// <param name="teamSideOfTheMap"></param>
        /// <param name="campTypeIcon"></param>
        /// <param name="respawnTimer"></param>
        /// <param name="doPlayVO"></param>
        /// <param name="revealEvent"></param>
        /// <param name="spawnDuration"></param>
        /// <returns></returns>
        /*public static NeutralMinionCamp CreateJungleCamp(Vector3 position, byte groupNumber, TeamId teamSideOfTheMap, string campTypeIcon, float respawnTimer, bool doPlayVO = false, byte revealEvent = 74, float spawnDuration = 0.0f)
        {
            var camp = new NeutralMinionCamp(position, groupNumber, teamSideOfTheMap, campTypeIcon, respawnTimer, doPlayVO, revealEvent, spawnDuration);
            Game.ObjectManager.AddObject(camp);
            return camp;
        }*/

        /// <summary>
        /// Adds a prop to the map
        /// </summary>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <param name="position"></param>
        /// <param name="height"></param>
        /// <param name="direction"></param>
        /// <param name="posOffset"></param>
        /// <param name="scale"></param>
        /// <param name="skinId"></param>
        /// <param name="skillLevel"></param>
        /// <param name="rank"></param>
        /// <param name="type"></param>
        /// <param name="netId"></param>
        /// <param name="netNodeId"></param>
        /// <returns></returns>
        public static LevelProp AddLevelProp(string name, string model, Vector3 position, Vector3 direction, Vector3 posOffset, Vector3 scale, int skinId = 0, byte skillLevel = 0, byte rank = 0, byte type = 2, uint netId = 0, byte netNodeId = 64)
        {
            return new LevelProp(netNodeId, name, model, position, direction, posOffset, scale, skinId, skillLevel, rank, type, netId);
        }

        /// <summary>
        /// Notifies prop animations (Such as the stairs at the beginning on Dominion)
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="animation"></param>
        /// <param name="animationFlag"></param>
        /// <param name="duration"></param>
        /// <param name="destroyPropAfterAnimation"></param>
        public static void NotifyPropAnimation(LevelProp prop, string animation, AnimationFlags animationFlag, float duration, bool destroyPropAfterAnimation)
        {
            var animationData = new UpdateLevelPropDataPlayAnimation
            {
                AnimationName = animation,
                AnimationFlags = (uint)animationFlag,
                Duration = duration,
                DestroyPropAfterAnimation = destroyPropAfterAnimation,
                StartMissionTime = Game.Time.GameTime,
                NetID = prop.NetId
            };
            Game.PacketNotifier.NotifyUpdateLevelPropS2C(animationData);
        }

        /// <summary>
        /// Sets up the surrender functionality
        /// </summary>
        /// <param name="time"></param>
        /// <param name="restTime"></param>
        /// <param name="length"></param>
        public static void AddSurrender(float time, float restTime, float length)
        {
            Game.Map.Surrenders.Add(TeamId.TEAM_ORDER, new SurrenderHandler(TeamId.TEAM_ORDER, time, restTime, length));
            Game.Map.Surrenders.Add(TeamId.TEAM_CHAOS, new SurrenderHandler(TeamId.TEAM_CHAOS, time, restTime, length));
        }

        public static void HandleSurrender(int userId, Champion who, bool vote)
        {
            if (Game.Map.Surrenders.TryGetValue(who.Team, out SurrenderHandler value))
            {
                value.HandleSurrender(userId, who, vote);
            }
        }

        /// <summary>
        /// Adds a fountain
        /// </summary>
        /// <param name="team"></param>
        /// <param name="position"></param>
        public static void CreateFountain(string name, TeamId team, Vector2 position)
        {
            Game.Map.SpawnPoints.Add(team, new(name, position, team));
        }

        /// <summary>
        /// Sets the features which should be enabled for this map. EX: Mana, Cooldowns, Lane Minions, etc. Refer to FeatureFlags enum.
        /// </summary>
        /// <returns></returns>
        public static void SetGameFeatures(FeatureFlags featureFlag, bool isEnabled)
        {
            Game.Config.SetGameFeatures(featureFlag, isEnabled);
        }

        /// <summary>
        /// Returns current game time.
        /// </summary>
        /// <returns></returns>
        public static float GameTime()
        {
            return Game.Time.GameTime;
        }

        public static void AddTurretItems(BaseTurret turret, int[] items)
        {
            foreach (var item in items)
            {
                turret.ItemInventory.AddItem(ContentManager.GetItemData(item));
            }
        }

        public static void AddObject(GameObject obj)
        {
            Game.ObjectManager.AddObject(obj);
        }

        /// <summary>
        /// Returns the average level of all players in the game
        /// </summary>
        /// <returns></returns>
        public static int GetPlayerAverageLevel()
        {
            float average = 0;
            var players = Game.PlayerManager.GetPlayers(true);
            foreach (var player in players)
            {
                average += player.Champion.Experience.Level / players.Count;
            }
            return (int)average;
        }

        public static void NotifyGameScore(TeamId team, float score)
        {
            Game.PacketNotifier.NotifyS2C_HandleGameScore(team, (int)score);
        }

        /// <summary>
        /// I couldn't tell the functionality for this besides Notifying the scoreboard at the start of the match
        /// </summary>
        /// <param name="capturePointIndex"></param>
        /// <param name="otherNetId"></param>
        /// <param name="PARType"></param>
        /// <param name="attackTeam"></param>
        /// <param name="capturePointUpdateCommand"></param>
        public static void NotifyHandleCapturePointUpdate(int capturePointIndex, uint otherNetId, byte PARType, byte attackTeam, CapturePointUpdateCommand capturePointUpdateCommand)
        {
            Game.PacketNotifier.NotifyS2C_HandleCapturePointUpdate(capturePointIndex, otherNetId, PARType, attackTeam, capturePointUpdateCommand);
        }

        public static void TeleportCamera(Champion target, Vector3 position)
        {
            Game.PacketNotifier.NotifyS2C_CameraBehavior(target, position);
        }

        public static void TeleportCamera(Champion target, GameObject targetObj)
        {
            Game.PacketNotifier.NotifyS2C_CameraBehavior(target, targetObj.GetPosition3D());
        }

        public static void NotifyAscendant(ObjAIBase ascendant = null)
        {
            Game.PacketNotifier.NotifyS2C_UpdateAscended(ascendant);
        }

        public static void NotifyMapPing(Vector2 position, PingCategory ping)
        {
            Game.PacketNotifier.NotifyS2C_MapPing(position, (Pings)ping);
        }

        public static ILevelScript GetLevelScript()
        {
            return Game.Map.LevelScript;
        }
    }
}
