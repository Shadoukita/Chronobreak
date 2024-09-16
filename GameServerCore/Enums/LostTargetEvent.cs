namespace GameServerCore.Enums
{
    public enum LostTargetEvent : byte
    {
        DEFAULT = 0x0,
        LOST_VISIBILITY = 0x1,
        OUT_OF_RANGE = 0x2
    }
}
