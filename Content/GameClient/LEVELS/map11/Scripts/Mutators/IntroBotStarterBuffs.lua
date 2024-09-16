local a, b
IntroBotStarterBuffsOnInit = function()
    local c, d, e, f, g, h, i, j, k, l, m
    for f = 0, 4, 1 do
        g = GetPlayerByClientID(f)
        if g ~= nil then
            ApplyPersistentBuff(g, "IntroBotDamageIndicatorController", false, 1, 1)
            ApplyPersistentBuff(g, "IntroBotPlayerDamageReduction", false, 1, 1)
            ApplyPersistentBuff(g, "IntroBotPlayerExp", false, 1, 1)
        end
    end
    ApplyPersistentBuffToAllChampions("IntroBotReducedAttackRange", false)
end
