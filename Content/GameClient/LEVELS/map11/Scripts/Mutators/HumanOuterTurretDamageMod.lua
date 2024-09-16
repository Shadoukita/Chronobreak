local a, b
TEAM_ORDER = 100
TEAM_CHAOS = 200
FRONT_TOWER = 3
RIGHT_LANE = 0
CENTER_LANE = 1
LEFT_LANE = 2
HumanOuterTurretDamageModOnInit = function(c)
    local d, e, f
    modifier = math.floor(c.GetFloat(c) * 100)
    SetBuffOnTeamTurrets(TEAM_ORDER, modifier)
    SetBuffOnTeamTurrets(TEAM_CHAOS, modifier)
end
SetBuffOnTeamTurrets = function(c, g)
    local e, f, h, i, j, k, l, m, n, o
    for i = RIGHT_LANE, LEFT_LANE, 1 do
        innerTurret = GetTurret(c, i, FRONT_TOWER)
        ApplyPersistentBuff(innerTurret, "ModTurretDamageToHumans", false, 1, 1)
        AddBuffCounter(innerTurret, "ModTurretDamageToHumans", g, 100)
    end
end
