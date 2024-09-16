namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    public class ChampionStats
    {
        public bool IsGeneratingGold { get; internal set; } // Used to determine if the Stats update should include generating gold. Changed in Champion.h
        public int BaronKills { get; internal set; }
        public int DragonKills { get; internal set; }
        public int DeathSpree { get; internal set; }
        public float GoldFromMinions { get; internal set; }
        public float Score { get; internal set; }
    }
}
