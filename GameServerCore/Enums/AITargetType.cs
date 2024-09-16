namespace GameServerCore.Enums;

public enum AITargetType
{
    AI_TARGET_HEROES = 1 << 0,
    AI_TARGET_MINIONS = 1 << 1,
    AI_TARGET_TURRETS = 1 << 2,
    AI_TARGET_DAMPENER = 1 << 3,
    AI_TARGET_HQ = 1 << 4,

    AI_TARGET_NONE = 1 << 10
}