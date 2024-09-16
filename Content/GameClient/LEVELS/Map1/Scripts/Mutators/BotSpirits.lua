local a, b
OnInitClient = function()
    local c, d
end
OnInitServer = function()
    local c, d, e
    PreloadSpell("NightmareBotSpiritManager")
    ApplyPersistentBuffToAllChampions("NightmareBotSpiritManager", false)
    ApplyPersistentBuffToAllChampions("NightmareBotSpiritPreloader", false)
end
