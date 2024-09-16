local a, b, c, d, e, f, g, h, i
TEAM_UNKNOWN = 0
TEAM_ORDER = 100
TEAM_CHAOS = 200
TEAM_NEUTRAL = 300
TEAM_MAX = 400
HOSTILE = 1
INACTIVE = 0
a = {}
a[1] = "HA_AP_HealthRelic"
a[2] = "AramSpeedShrine"
a[3] = "HA_FB_HealthRelic"
NeutralMinionNames = a
HERO_EXP_RADIUS = 1000
a = {}
b = {}
b.GoldGiven = 0
b.ExpGiven = 0
b.SoulGiven = 0
a.HA_AP_HealthRelic = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 0
b.SoulGiven = 0
a.AramSpeedShrine = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 0
b.SoulGiven = 0
a.HA_FB_HealthRelic = b
DefaultNeutralMinionValues = a
NeutralTimers = {}
a = {}
b = {}
c, d, e, f, g, h, i = Make3DPoint(7582.1, -193.8, 6785.5)
b[1] = c
b[2] = d
b[3] = e
b[4] = f
b[5] = g
b[6] = h
b[7] = i
c = {}
d, e, f, g, h, i = Make3DPoint(5929.7, -194, 5190.9)
c[1] = d
c[2] = e
c[3] = f
c[4] = g
c[5] = h
c[6] = i
d = {}
e, f, g, h, i = Make3DPoint(8893.9, -187.7, 7889)
d[1] = e
d[2] = f
d[3] = g
d[4] = h
d[5] = i
e = {}
f, g, h, i = Make3DPoint(4790.2, -188.5, 3934.3)
e[1] = f
e[2] = g
e[3] = h
e[4] = i
a[1] = b
a[2] = c
a[3] = d
a[4] = e
CampSpawnPoints = a
a = {}
b = {}
c, d, e, f, g, h, i = Make3DPoint(7582.1, -193.8, 6785.5)
b[1] = c
b[2] = d
b[3] = e
b[4] = f
b[5] = g
b[6] = h
b[7] = i
c = {}
d, e, f, g, h, i = Make3DPoint(5929.7, -194, 5190.9)
c[1] = d
c[2] = e
c[3] = f
c[4] = g
c[5] = h
c[6] = i
d = {}
e, f, g, h, i = Make3DPoint(8893.9, -187.7, 7889)
d[1] = e
d[2] = f
d[3] = g
d[4] = h
d[5] = i
e = {}
f, g, h, i = Make3DPoint(4790.2, -188.5, 3934.3)
e[1] = f
e[2] = g
e[3] = h
e[4] = i
a[1] = b
a[2] = c
a[3] = d
a[4] = e
CampFacePoints = a
a = {}
b = {}
b[1] = "HA_AP_HealthRelic"
a[1] = b
b = {}
b[1] = "HA_AP_HealthRelic"
a[2] = b
b = {}
b[1] = "HA_AP_HealthRelic"
a[3] = b
b = {}
b[1] = "HA_AP_HealthRelic"
a[4] = b
PredefinedCamps = a
a = {}
b = {}
b[1] = "HA_FB_HealthRelic"
a[1] = b
b = {}
b[1] = "HA_FB_HealthRelic"
a[2] = b
PredefinedCampsFB = a
a = {}
a[1] = {}
a[2] = {}
a[3] = {}
a[4] = {}
NeutralMinionCamps = a
NUMBER_OF_CAMPS = #NeutralMinionCamps
NeutralMinionInit = function()
    local j, k, l, m, n, o, p, q
    j = GetGameMode()
    k, l, m = pairs(NeutralMinionNames)
    for n, o in k, l, m do
        PreloadCharacter(o)
    end
    if j == "FIRSTBLOOD" then
        k = {}
        k[1] = {}
        k[2] = {}
        NeutralMinionCamps = k
        NUMBER_OF_CAMPS = #NeutralMinionCamps
    end
    for n = 1, NUMBER_OF_CAMPS, 1 do
        NeutralMinionCamps[n].Positions = CampSpawnPoints[n]
        NeutralMinionCamps[n].FacePositions = CampFacePoints[n]
        NeutralMinionCamps[n].UniqueNames = {}
        NeutralMinionCamps[n].AliveTracker = {}
        NeutralMinionCamps[n].Team = TEAM_NEUTRAL
        NeutralMinionCamps[n].DamageBonus = 0
        NeutralMinionCamps[n].HealthBonus = 0
        NeutralMinionCamps[n].AIState = INACTIVE
        NeutralMinionCamps[n].GroupsRespawnTime = 60
        NeutralMinionCamps[n].CampLevel = 1
        NeutralMinionCamps[n].TimerType = ""
        NeutralMinionCamps[n].MinimapIcon = ""
    end
    if j == "ARAM" or j == "KINGPORO" then
        l = {}
        l[1] = PredefinedCamps[1]
        NeutralMinionCamps[1].Groups = l
        l = {}
        l[1] = 100
        NeutralMinionCamps[1].GroupsChance = l
        NeutralMinionCamps[1].GroupsRespawnTime = 40
        NeutralMinionCamps[1].GroupDelaySpawnTime = 120
        NeutralMinionCamps[1].CampLevel = 3
        l = {}
        l[1] = PredefinedCamps[2]
        NeutralMinionCamps[2].Groups = l
        l = {}
        l[1] = 100
        NeutralMinionCamps[2].GroupsChance = l
        NeutralMinionCamps[2].GroupsRespawnTime = 40
        NeutralMinionCamps[2].GroupDelaySpawnTime = 120
        NeutralMinionCamps[2].CampLevel = 3
        l = {}
        l[1] = PredefinedCamps[3]
        NeutralMinionCamps[3].Groups = l
        l = {}
        l[1] = 100
        NeutralMinionCamps[3].GroupsChance = l
        NeutralMinionCamps[3].GroupsRespawnTime = 40
        NeutralMinionCamps[3].GroupDelaySpawnTime = 120
        NeutralMinionCamps[3].CampLevel = 3
        l = {}
        l[1] = PredefinedCamps[4]
        NeutralMinionCamps[4].Groups = l
        l = {}
        l[1] = 100
        NeutralMinionCamps[4].GroupsChance = l
        NeutralMinionCamps[4].GroupsRespawnTime = 40
        NeutralMinionCamps[4].GroupDelaySpawnTime = 120
        NeutralMinionCamps[4].CampLevel = 3
    elseif j == "FIRSTBLOOD" then
        l = {}
        l[1] = PredefinedCampsFB[1]
        NeutralMinionCamps[1].Groups = l
        l = {}
        l[1] = 100
        NeutralMinionCamps[1].GroupsChance = l
        NeutralMinionCamps[1].GroupsRespawnTime = 75
        NeutralMinionCamps[1].GroupDelaySpawnTime = 90
        NeutralMinionCamps[1].CampLevel = 3
        l = {}
        l[1] = PredefinedCampsFB[2]
        NeutralMinionCamps[2].Groups = l
        l = {}
        l[1] = 100
        NeutralMinionCamps[2].GroupsChance = l
        NeutralMinionCamps[2].GroupsRespawnTime = 75
        NeutralMinionCamps[2].GroupDelaySpawnTime = 90
        NeutralMinionCamps[2].CampLevel = 3
    end
end
CreateRespawnNeutralTimer = function(r)
    local k
    return function()
        local s, t, u, v, w, x, y, z, A, B, C, D, E, F, G, H
        t = 0
        NeutralMinionCamps[r].DamageBonus = NeutralMinionCamps[r].DamageBonus + 0
        NeutralMinionCamps[r].HealthBonus = NeutralMinionCamps[r].HealthBonus + 0
        for y = 1, #NeutralMinionCamps[r].Groups, 1 do
            if math.random(100) <= NeutralMinionCamps[r].GroupsChance[y] + t and false == false then
                NeutralMinionCamps[r].AliveTracker = {}
                for C = 1, #NeutralMinionCamps[r].Groups[y], 1 do
                    SpawnNeutralMinion(NeutralMinionCamps[r], r, y, C)
                    NeutralMinionCamps[r].AliveTracker[C] = true
                end
            end
        end
    end
end
InitializeNeutralMinionInfo = function()
    local j, k, l, m, n, o, p, q, I, J, K, L, M, N, O, P, Q, R, S
    for m = 1, #NeutralMinionCamps, 1 do
        for q = 1, #NeutralMinionCamps[m].Groups, 1 do
            NeutralMinionCamps[m].UniqueNames[q] = {}
            for L = 1, #NeutralMinionCamps[m].Groups[q], 1 do
                NeutralMinionCamps[m].UniqueNames[q][L] =
                    NeutralMinionCamps[m].Groups[q][L] .. m .. "." .. q .. "." .. L
            end
        end
        n = #NeutralTimers
        NeutralTimers[n + 1] = CreateRespawnNeutralTimer(m)
        NeutralMinionCamps[m].Timer = NeutralTimers[n + 1]
        CreateNeutralCamp(NeutralMinionCamps[m], m)
    end
end
SpawnInitialNeutralMinions = function(r)
    local k, l, m, n, o, p, q, I, J, K, L, M
    if r <= #NeutralMinionCamps then
        l = 0
        m = false
        for q = 1, #NeutralMinionCamps[r].Groups, 1 do
            if 0 < NeutralMinionCamps[r].GroupDelaySpawnTime and m == false then
                NeutralMinionCamps[r].AliveTracker = {}
                for L = 1, #NeutralMinionCamps[r].Groups[q], 1 do
                    NeutralMinionCamps[r].AliveTracker[L] = true
                end
            else
                if math.random(100) <= NeutralMinionCamps[r].GroupsChance[q] + l and m == false then
                    NeutralMinionCamps[r].AliveTracker = {}
                    for L = 1, #NeutralMinionCamps[r].Groups[q], 1 do
                        NeutralMinionCamps[r].AliveTracker[L] = true
                    end
                end
            end
        end
    end
end
NeutralMinionDeath = function(r, T, U)
    local m, n, o, p, q, I, J, K, L, M, N, O, P, Q, R
    n = 0
    for I = 1, #NeutralMinionCamps, 1 do
        for M = 1, #NeutralMinionCamps[I].UniqueNames, 1 do
            for Q = 1, #NeutralMinionCamps[I].UniqueNames[M], 1 do
                if NeutralMinionCamps[I].UniqueNames[M][Q] == r then
                    n = I
                    NeutralMinionCamps[I].AliveTracker[Q] = false
                end
            end
        end
    end
    if 0 < n then
        for I = 1, #NeutralMinionCamps[n].AliveTracker, 1 do
            if NeutralMinionCamps[n].AliveTracker[I] == true then
                m = false
            end
        end
        if m != false then
            InitNeutralMinionTimer(
                NeutralMinionCamps[n].Timer,
                NeutralMinionCamps[n].TimerType,
                NeutralMinionCamps[n].GroupsRespawnTime,
                0,
                false
            )
        end
    end
end
