using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    // deviates from the style guide
    //[StructLayout(LayoutKind.Explicit)]

    //TODO: Find out from where these `FieldOffset` values came from.
    //The current organization breaks a few values. Going 4 by 4 seems to fix it,
    //but i dont know if that does anything regarding the info the client receives
    public class ChampionStatistics
    {
        //[FieldOffset(0)]
        public int Assists;
        //[FieldOffset(8)]
        public int Kills;
        //[FieldOffset(16)]
        public int DoubleKills;
        //[FieldOffset(32)]
        public int UnrealKills;
        //[FieldOffset(36)]
        //public float GoldEarned;
        //[FieldOffset(40)]
        //public float GoldSpent;
        //[FieldOffset(84)]
        public int CurrentKillingSpree;
        //[FieldOffset(8)]
        public float LargestCriticalStrike;
        //[FieldOffset(92)]
        public int LargestKillingSpree;
        //[FieldOffset(96)]
        public int LargestMultiKill;
        //[FieldOffset(104)]
        public float LongestTimeSpentLiving;
        //[FieldOffset(108)]
        public float MagicDamageDealt;
        //[FieldOffset(112)]
        public float MagicDamageDealtToChampions;
        //[FieldOffset(116)]
        public float MagicDamageTaken;
        //[FieldOffset(120)]
        public int MinionsKilled;
        //[FieldOffset(126)]
        public int NeutralMinionsKilled;
        //[FieldOffset(130)]
        public int NeutralMinionsKilledInEnemyJungle;
        //[FieldOffset(134)]
        public int NeutralMinionsKilledInTeamJungle;
        //[FieldOffset(142)]
        public int Deaths;
        //[FieldOffset(146)]
        public int PentaKills;
        //[FieldOffset(150)]
        public float PhysicalDamageDealt;
        //[FieldOffset(154)]
        public float PhysicalDamageDealtToChampions;
        //[FieldOffset(158)]
        public float PhysicalDamageTaken;
        //[FieldOffset(166)]
        public int QuadraKills;
        [JsonIgnore]
        //[FieldOffset(206)]
        public int TeamId;
        //[FieldOffset(226)]
        public float TotalDamageDealt;
        //[FieldOffset(230)]
        public float TotalDamageDealtToChampions;
        //[FieldOffset(234)]
        public float TotalDamageTaken;
        //[FieldOffset(238)]
        public int TotalHeal;
        //[FieldOffset(242)]
        public float TotalTimeCrowdControlDealt;
        //[FieldOffset(246)]
        public float TotalTimeSpentDead;
        //[FieldOffset(250)]
        public int TotalUnitsHealed;
        //[FieldOffset(254)]
        public int TripleKills;
        //[FieldOffset(258)]
        public float TrueDamageDealt;
        //[FieldOffset(262)]
        public float TrueDamageDealtToChampions;
        //[FieldOffset(266)]
        public float TrueDamageTaken;
        //[FieldOffset(270)]
        public int TurretsKilled;
        //[FieldOffset(274)]
        public int BarracksKilled;
        //[FieldOffset(282)]
        public int WardsKilled;
        //[FieldOffset(286)]
        public int WardsPlaced;
        //[FieldOffset(298)]
        // sort of length (when above 0 sends malformed buffer error)
        public short Padding;

        // TODO: move to universal serializer
        // also code here is unsafe, but thats prefered than just
        // write a function that simply dumps all the variables
        public static byte[] GetBytes(ChampionStatistics stats)
        {
            int size = Marshal.SizeOf(stats);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(stats, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        public byte[] GetBytes()
        {
            return GetBytes(this);
        }
    }
}
