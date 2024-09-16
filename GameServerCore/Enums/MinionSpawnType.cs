namespace GameServerCore.Enums
{
    public enum MinionSpawnType : byte
    {
        MINION_TYPE_MELEE = 0x00,
        MINION_TYPE_SUPER = 0x01,
        MINION_TYPE_CANNON = 0x02,
        MINION_TYPE_CASTER = 0x03,

        SRU_MINION_TYPE_SUPER = 0x00,
        SRU_MINION_TYPE_MELEE = 0x01,
        SRU_MINION_TYPE_CASTER = 0x02,
        SRU_MINION_TYPE_CANNON = 0x04,
    }
}