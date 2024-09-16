local a, b
TEAM_ORDER = 100
TEAM_CHAOS = 200
AMBIENT_XP = 1
OnInitServer = function(c)
    local d, e, f
    AMBIENT_XP = c.GetFloat(c)
    LuaForEachChampion(TEAM_ORDER, "InitBonusAmbientXP")
    LuaForEachChampion(TEAM_CHAOS, "InitBonusAmbientXP")
end
InitBonusAmbientXP = function(c)
    local d, e, f, g, h, i
    ApplyPersistentBuff(c, "BonusAmbientXP", false, 1, 1)
    AddBuffCounter(c, "BonusAmbientXP", AMBIENT_XP, 100)
end
