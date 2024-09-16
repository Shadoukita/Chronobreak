local a, b
TEAM_ORDER = 100
TEAM_CHAOS = 200
FRONT_TOWER = 3
RIGHT_LANE = 0
CENTER_LANE = 1
LEFT_LANE = 2
OnInitClient = function()
    local c, d
end
OnInitServer = function(e)
    local d, f, g
    modifier = math.floor(e.GetFloat(e) * 100)
    SetBuffOnTeamTurrets(TEAM_ORDER, modifier)
    SetBuffOnTeamTurrets(TEAM_CHAOS, modifier)
end
SetBuffOnTeamTurrets = function(e, h)
    local f, g, i, j, k, l, m, n, o, p
    for j = RIGHT_LANE, LEFT_LANE, 1 do
        innerTurret = GetTurret(e, j, FRONT_TOWER)
        ApplyPersistentBuff(innerTurret, "ModTurretDamageToHumans", false, 1, 1)
        AddBuffCounter(innerTurret, "ModTurretDamageToHumans", h, 100)
    end
end
