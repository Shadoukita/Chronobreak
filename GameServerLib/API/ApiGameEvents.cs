using LeaguePackets.Game.Events;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.Logging;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

internal class TimedEvent
{
    internal float TimeOfExecution;
    internal Action Event;
}

namespace Chronobreak.GameServer.API
{
    public static class GameAnnouncementManager
    {
        private static ILog _logger = LoggerProvider.GetLogger();

        static List<TimedEvent> Events = [];
        public static void Update()
        {
            while (Game.Time.GameTime >= Events.FirstOrDefault()?.TimeOfExecution)
            {
                TimedEvent timedEvent = Events.First();
                timedEvent.Event();
                Events.Remove(timedEvent);
            }
        }

        public static void CreateTimedEvent(Action timedEvent, float timeOfExecution)
        {
            if (timedEvent is not null)
            {
                Events.Add(new() { Event = timedEvent, TimeOfExecution = timeOfExecution * 1000 });
                Events = Events.OrderBy(x => x.TimeOfExecution).ToList();
            }
        }

        public static void AnnounceCaptureAltar(Minion altar, byte index)
        {
            Game.PacketNotifier.NotifyS2C_OnEventWorld(new OnCaptureAltar() { CapturePoint = index, OtherNetID = altar.NetId });
        }

        public static void AnnounceCapturePointCaptured(LaneTurret turret, char point, Champion? captor = null)
        {
            IEvent captured;
            switch (char.ToUpper(point))
            {
                case 'A':
                    captured = new OnCapturePointCaptured_A();
                    break;
                case 'B':
                    captured = new OnCapturePointCaptured_B();
                    break;
                case 'C':
                    captured = new OnCapturePointCaptured_C();
                    break;
                case 'D':
                    captured = new OnCapturePointCaptured_D();
                    break;
                case 'E':
                    captured = new OnCapturePointCaptured_E();
                    break;
                default:
                    _logger.Warn($"Announcement with Id {point} doesn't exist! Please use numbers between A and E");
                    return;
            }
            captured.OtherNetID = captor?.NetId ?? 0;

            Game.PacketNotifier.NotifyOnEvent(captured, turret);
        }

        public static void AnnounceCapturePointNeutralized(LaneTurret turret, char point)
        {
            IEvent neutralized;
            switch (char.ToUpper(point))
            {
                case 'A':
                    neutralized = new OnCapturePointNeutralized_A();
                    break;
                case 'B':
                    neutralized = new OnCapturePointNeutralized_B();
                    break;
                case 'C':
                    neutralized = new OnCapturePointNeutralized_C();
                    break;
                case 'D':
                    neutralized = new OnCapturePointNeutralized_D();
                    break;
                case 'E':
                    neutralized = new OnCapturePointNeutralized_E();
                    break;
                default:
                    _logger.Warn($"Announcement with Id {point} doesn't exist! Please use numbers between 1 and 5");
                    return;
            }

            Game.PacketNotifier.NotifyOnEvent(neutralized, turret);
        }

        public static void AnnounceChampionAscended(Champion champion)
        {
            Game.PacketNotifier.NotifyS2C_OnEventWorld(new OnChampionAscended() { OtherNetID = champion.NetId }, champion);
        }

        public static void AnnounceClearAscended()
        {
            Game.PacketNotifier.NotifyS2C_OnEventWorld(new OnClearAscended());
            ApiMapFunctionManager.NotifyAscendant();
        }

        public static void AnnounceKillDragon(DeathData data)
        {
            var killDragon = new OnKillDragon()
            {
                //TODO: Figure out all the parameters, their values look random(?).
                //All Map11 replays have the same values in this event besides OtherNetId.
                OtherNetID = data.Unit.NetId
            };
            Game.PacketNotifier.NotifyS2C_OnEventWorld(killDragon, data.Killer);
        }

        public static void AnnounceKillWorm(DeathData data)
        {
            var killDragon = new OnKillWorm()
            {
                //TODO: Figure out all the parameters, their values look random(?).
                OtherNetID = data.Unit.NetId
            };
            Game.PacketNotifier.NotifyS2C_OnEventWorld(killDragon, data.Killer);
        }

        public static void AnnounceKillSpiderBoss(DeathData data)
        {
            var killDragon = new OnKillSpiderBoss()
            {
                //Couldn't find a replay with this event, but i assume it should follow the same logic as the other 2.
                OtherNetID = data.Unit.NetId
            };
            Game.PacketNotifier.NotifyS2C_OnEventWorld(killDragon, data.Killer);
        }

        public static void AnnounceMinionAscended(Minion minion)
        {
            Game.PacketNotifier.NotifyS2C_OnEventWorld(new OnMinionAscended() { OtherNetID = minion.NetId }, minion);
        }

        public static void AnnounceMinionsSpawn()
        {
            Game.PacketNotifier.NotifyS2C_OnEventWorld(new OnMinionsSpawn());
        }

        public static void AnnouceNexusCrystalStart()
        {
            Game.PacketNotifier.NotifyS2C_OnEventWorld(new OnNexusCrystalStart());
        }

        public static void AnnounceStartGameMessage(int message, int map = 0)
        {
            IEvent annoucement;
            switch (message)
            {
                case 1:
                    annoucement = new OnStartGameMessage1();
                    break;
                case 2:
                    annoucement = new OnStartGameMessage2();
                    break;
                case 3:
                    annoucement = new OnStartGameMessage3();
                    break;
                case 4:
                    annoucement = new OnStartGameMessage4();
                    break;
                case 5:
                    annoucement = new OnStartGameMessage5();
                    break;
                default:
                    _logger.Warn($"Announcement with Id {message} doesn't exist! Please use numbers between 1 and 5");
                    return;
            }
            (annoucement as ArgsGlobalMessageGeneric)!.MapNumber = map;

            Game.PacketNotifier.NotifyS2C_OnEventWorld(annoucement);
        }

        public static void AnnounceVictoryPointThreshold(LaneTurret turret, int index)
        {
            IEvent pointThreshHold;
            switch (index)
            {
                case 1:
                    pointThreshHold = new OnVictoryPointThreshold1();
                    break;
                case 2:
                    pointThreshHold = new OnVictoryPointThreshold2();
                    break;
                case 3:
                    pointThreshHold = new OnVictoryPointThreshold3();
                    break;
                case 4:
                    pointThreshHold = new OnVictoryPointThreshold4();
                    break;
                default:
                    _logger.Warn($"Announcement with Id {index} doesn't exist! Please use numbers between 1 and 4");
                    return;
            }

            Game.PacketNotifier.NotifyOnEvent(pointThreshHold, turret);
        }
    }
}
