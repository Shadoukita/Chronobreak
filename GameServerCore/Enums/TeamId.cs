namespace GameServerCore.Enums
{
    public enum TeamId : int
    {
        //Check: is supposed to be 0xFFFFFF9C
        TEAM_ALL = -0xFFFFF9C,
        TEAM_UNKNOWN = 0x0,
        TEAM_ORDER = 0x64,
        TEAM_CHAOS = 0xC8,
        TEAM_NEUTRAL = 0x12C,
        TEAM_MAX = 0x190
    }
}