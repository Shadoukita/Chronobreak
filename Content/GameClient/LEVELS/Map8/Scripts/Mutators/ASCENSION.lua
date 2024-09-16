local a, b
OnInitClient = function()
    local c, d
    OnInit()
    PreloadCharacter("Leona")
    PreloadCharacter("Lux")
    PreloadCharacter("Xerath")
end
OnInitServer = function()
    local c, d, e
    OnInit()
    ApplyPersistentBuffToAllChampions("AscRespawn", false)
end
OnInit = function()
    local c, d
    PreloadCharacter("AscRelic")
    PreloadCharacter("AscWarpIcon")
    PreloadCharacter("AscXerath")
    PreloadSpell("AscRespawn")
end
