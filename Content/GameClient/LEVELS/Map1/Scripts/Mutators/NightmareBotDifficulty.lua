local a, b
TEAM_ORDER = 100
TEAM_CHAOS = 200
DIFFICULTY_INDEX = 1
OnInitClient = function()
    local c, d
end
OnInitServer = function(e)
    local d, f, g
    DIFFICULTY_INDEX = e.GetFloat(e)
    LuaForEachChampion(TEAM_ORDER, "SetNightmareBotDifficulty")
    LuaForEachChampion(TEAM_CHAOS, "SetNightmareBotDifficulty")
end
SetNightmareBotDifficulty = function(e)
    local d, f, g, h, i, j
    ApplyPersistentBuff(e, "NightmareBotDifficulty", false, DIFFICULTY_INDEX, 3)
end
