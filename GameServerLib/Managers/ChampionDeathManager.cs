using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.Handlers;
using System.Collections.Generic;
using LeaguePackets.Game.Events;
using Chronobreak.GameServer;
using System.Linq;
using System;

namespace GameServerLib.Managers
{

    internal static class ChampionDeathManager
    {
        /// <summary>
        /// I want to keep the definition on LeaguePackets untouched, so I made this constructor here
        /// </summary>
        /// <param name="deathData"></param>
        /// <param name="marker"></param>
        /// <returns></returns>
        internal static OnDeathAssist OnDeathAssistConstructor(DeathData deathData, AssistMarker marker)
        {
            return new()
            {
                AtTime = marker.StartTime,
                PhysicalDamage = marker.PhysicalDamage,
                MagicalDamage = marker.MagicalDamage,
                TrueDamage = marker.TrueDamage,
                OrginalGoldReward = deathData.GoldReward,
                KillerNetID = deathData.Killer.NetId,
                OtherNetID = deathData.Unit.NetId
            };
        }

        internal static void NotifyAssistEvent(Dictionary<ObjAIBase, AssistMarker> assists, DeathData data)
        {
            float assistPercent = 1.0f / assists.Count;
            foreach (var champion in assists.Keys)
            {
                OnDeathAssist onDeathAssist = OnDeathAssistConstructor(data, assists[champion]);
                onDeathAssist.PercentageOfAssist = assistPercent;
                Game.PacketNotifier.NotifyOnEvent(onDeathAssist, champion);
            }
        }

        internal static void NotifyDeathEvent(DeathData data, ObjAIBase[] assists = null)
        {
            OnChampionDie championDie = new()
            {
                OtherNetID = data.Killer.NetId,
                GoldGiven = data.GoldReward,
                AssistCount = assists.Length,
            };

            if (assists != null)
            {
                for (int i = 0; i < assists.Length && i < 12; i++)
                {
                    championDie.Assists[i] = assists[i].NetId;
                }
            }

            Game.PacketNotifier.NotifyOnEvent(championDie, data.Unit);
        }

        internal static void ProcessKill(DeathData data)
        {
            MapHandler map = Game.Map;

            // TODO: Find out if we can unhardcode some of the fractions used here.
            Champion killed = data.Unit as Champion;
            Champion killer = data.Killer as Champion;
            data.GoldReward = map.GameMode.MapScriptMetadata.ChampionBaseGoldValue;
            if (killed.ChampionStatistics.CurrentKillingSpree > 1)
            {
                data.GoldReward = Math.Min(data.GoldReward * MathF.Pow(7f / 6f, killed.ChampionStatistics.CurrentKillingSpree - 1), map.GameMode.MapScriptMetadata.ChampionMaxGoldValue);
            }
            else if (killed.ChampionStatistics.CurrentKillingSpree == 0 && killed.ChampionStats.DeathSpree >= 1)
            {
                data.GoldReward *= 11f / 12f;
                if (killed.ChampionStats.DeathSpree > 1)
                {
                    data.GoldReward = Math.Max(data.GoldReward * MathF.Pow(0.8f, killed.ChampionStats.DeathSpree / 2), map.GameMode.MapScriptMetadata.ChampionMinGoldValue);
                }
                killed.ChampionStats.DeathSpree++;
            }

            if (!map.HasFirstBloodHappened)
            {
                data.GoldReward += map.GameMode.MapScriptMetadata.FirstBloodExtraGold;
                map.HasFirstBloodHappened = true;
            }

            Dictionary<ObjAIBase, AssistMarker> assists = [];
            foreach (var assistMarker in killed.EnemyAssistMarkers)
            {
                if (!assists.ContainsKey(assistMarker.Source) && assistMarker.Source is Champion c)
                {
                    assists.Add(c, assistMarker);
                    RecursiveGetAlliedAssists(assists, c, data);
                }
            }
            assists.Remove(killer);
            assists = assists.OrderBy(x => x.Value.StartTime).ToDictionary(x => x.Key, x => x.Value);
            ObjAIBase[] assistObjArray = assists.Keys.ToArray();

            NotifyAssistEvent(assists, data);
            NotifyDeathEvent(data, assistObjArray);
            NotifyChampionKillEvent(data);
            ProcessKillRewards(killed, killer, assistObjArray, data.GoldReward);
            UpdateKillerStats(killer);
            killed.ChampionStatistics.CurrentKillingSpree = 0;
            killed.ChampionStats.DeathSpree++;
        }

        internal static void NotifyChampionKillEvent(DeathData data)
        {
            Game.PacketNotifier.NotifyOnEvent(new OnChampionKill() { OtherNetID = data.Unit.NetId }, data.Killer);
        }

        internal static void UpdateKillerStats(Champion c)
        {
            //Check if GoldFromMinions should be reset
            c.ChampionStats.GoldFromMinions = 0;
            c.ChampionStatistics.Kills++;
            c.ChampionStats.DeathSpree = 0;
            c.ChampionStatistics.CurrentKillingSpree++;
            if (c.ChampionStatistics.CurrentKillingSpree > c.ChampionStatistics.LargestKillingSpree)
            {
                c.ChampionStatistics.LargestKillingSpree = c.ChampionStatistics.CurrentKillingSpree;
            }
        }

        internal static void RecursiveGetAlliedAssists(Dictionary<ObjAIBase, AssistMarker> assistMarkers, Champion champ, DeathData deathData)
        {
            foreach (var assist in champ.AlliedAssistMarkers)
            {
                if (assist.Source is not Champion c)
                {
                    continue;
                }

                if (!assistMarkers.ContainsKey(assist.Source))
                {
                    assistMarkers.Add(assist.Source, assist);
                    RecursiveGetAlliedAssists(assistMarkers, c, deathData);
                }
                else
                {
                    assistMarkers[c].StartTime = assistMarkers[c].StartTime < assist.StartTime ? assist.StartTime : assistMarkers[c].StartTime;
                }
            }
        }

        internal static void ProcessKillRewards(Champion killed, Champion killer, ObjAIBase[] assists, float gold)
        {
            float xpShareFactor = assists.Length + 1;

            killer.Experience.AddEXP(killer.Experience.GetEXPGrantedFromChampion(killed) / xpShareFactor);
            foreach (ObjAIBase obj in assists)
            {
                if (obj is Champion c)
                {
                    c.Experience.AddEXP(c.Experience.GetEXPGrantedFromChampion(killed) / xpShareFactor);
                }
            }

            killer.GoldOwner.AddGold(gold);
            foreach (ObjAIBase obj in assists)
            {
                if (obj is not Champion c)
                {
                    continue;
                }

                float assistGold = gold / 2 * (1.0f / assists.Length);
                int killDiff = c.ChampionStatistics.Assists - c.ChampionStatistics.Kills;
                if (killDiff > 0)
                {
                    assistGold += 15 + 15 * MathF.Min(killDiff, 3);
                }
                //TODO: Check if the Assist + Bonus can't exeed the original value gained by the killer or just the Bonus alone.
                assistGold = MathF.Min(gold, assistGold);

                c.GoldOwner.AddGold(assistGold);
                c.ChampionStatistics.Assists++;
            }
        }
    }
}
