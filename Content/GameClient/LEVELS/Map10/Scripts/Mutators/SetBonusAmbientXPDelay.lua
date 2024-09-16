local a, b
TEAM_ORDER = 100
TEAM_CHAOS = 200
DELAY = 0
OnInitServer = function(c)
    local d, e, f
    DELAY = c.GetFloat(c)
    LuaForEachChampion(TEAM_ORDER, "InitBonusAmbientXPDelay")
    LuaForEachChampion(TEAM_CHAOS, "InitBonusAmbientXPDelay")
end
InitBonusAmbientXPDelay = function(c)
    local d, e, f, g, h, i
    ApplyPersistentBuff(c, "BonusAmbientXPDelay", false, 1, 1)
    AddBuffCounter(c, "BonusAmbientXPDelay", DELAY, 120)
end
