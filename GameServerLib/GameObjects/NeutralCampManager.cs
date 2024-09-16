using GameServerCore.Enums;
using Chronobreak.GameServer;
using Chronobreak.GameServer.GameObjects;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Logging;
using log4net;
using System.Collections.Generic;
using System.Numerics;

namespace GameServerLib.GameObjects
{
    internal static class NeutralCampManager
    {
        private static readonly ILog _logger = LoggerProvider.GetLogger();
        private static readonly Dictionary<int, NeutralMinionCamp> Camps = [];
        //NeutralMinionTimers GetNeutralMinionTimers();
        internal static void AddMinionToCamp(int campIndex, Minion minion, Vector3 position, string minionName, TeamId teamSide, AudioVOComponentEvent revealEvent, float spawnDuration)
        {
            if (!Camps.TryGetValue(campIndex, out NeutralMinionCamp? camp))
            {
                _logger.Warn($"NeutralCampManager: Failed to add minion<{minion.NetId}> to camp<{campIndex}>. Camp does not exist.");
                return;
            }

            camp.BuffSide = teamSide;
            if (revealEvent != AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS)
            {
                camp.RevealEvent = revealEvent;
            }

            camp.SpawnEndTime = Game.Time.GameTime + spawnDuration;

            if (!camp.Minions.Contains(minion))
            {
                camp.AddMinion(minion, position);
            }
        }

        internal static void CreateMinionCamp(Vector3 position, string minimapIcon, int campIndex, TeamId teamSide, AudioVOComponentEvent revealEvent, int timerType)
        {
            //*(_DWORD *)&GameObject[2].mRenderHeroesOnly = buffSide;

            NeutralMinionCamp camp = new($"Camp{campIndex}", position, minimapIcon, minimapIcon, campIndex)
            {
                TimerType = timerType,
                RevealEvent = revealEvent
            };
            camp.SetTeam(teamSide);
            camp.Init(position, minimapIcon, campIndex, teamSide, revealEvent, timerType);
            if (!Camps.TryAdd(campIndex, camp))
            {
                _logger.Warn($"Failed to add NeutralMinionCamp with Index `{campIndex}` another camp with the same Index already exists!");
            }
            Game.ObjectManager.AddObject(camp);
        }

        internal static NeutralMinionCamp? FindCamp(int campId)
        {
            Camps.TryGetValue(campId, out NeutralMinionCamp? camp);
            return camp;
        }

        internal static void KillMinion(ObjAIBase killer, NeutralMinion minion)
        {
            minion.Camp?.KillMinion(minion, killer);
            Game.Map.LevelScript.NeutralMinionDeath(minion.Name, killer, minion.Position3D);
        }

        internal static void Reset()
        {
            Camps.Clear();
        }
    }
}
