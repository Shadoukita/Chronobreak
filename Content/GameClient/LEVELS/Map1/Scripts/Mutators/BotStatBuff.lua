local a, b
OnInitClient = function()
    local c, d
end
OnInitServer = function()
    local c, d, e
    PreloadSpell("NightmareBotStatBuffManager")
    ApplyPersistentBuffToAllChampions("NightmareBotStatBuffManager", false)
end
