using System;

namespace GameServerCore.Enums
{
    [Flags]
    public enum FeatureType
    {
        Equalize = 0x0,
        FoundryOptions = 0x1,
        OldOptions = 0x2,
        FoundryQuickChat = 0x3,
        EarlyWarningForFOWMissiles = 0x4,
        AnimatedCursors = 0x5,
        ItemUndo = 0x6,
        NewPlayerRecommendedPages = 0x7,
        HighlightLineMissileTargets = 0x8,
        ControlledChampionIndicator = 0x9,
        AlternateBountySystem = 0xA,
        NewMinionSpawnOrder = 0xB,
        TurretRangeIndicators = 0xC,
        GoldSourceInfoLogDump = 0xD,
        ParticleSkinNameTech = 0xE,
        NetworkMetrics_1 = 0xF,
        HardwareMetrics_1 = 0x10,
        TruLagMetrics = 0x11,
        DradisSDK = 0x12,
        ServerIPLogging = 0x13,
        JungleTimers = 0x14,
        TraceRouteMetrics = 0x15,
        IsLolbug19805LoggingEnabled = 0x16,
        IsLolbug19805HackyTourniquetEnabled = 0x17,
        TurretMemory = 0x18,
        TimerSyncForReplay = 0x19,
        RegisterWithLocalServiceDiscovery = 0x1A,
        MinionFarmingBounty = 0x1B,
        TeleportToDestroyedTowers = 0x1C,
        NonRefCountedCharacterStates = 0x1D,
        NumFeatures = 0x1E,
    }
}