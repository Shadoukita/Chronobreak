local a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q
TEAM_UNKNOWN = 0
TEAM_ORDER = 100
TEAM_CHAOS = 200
TEAM_NEUTRAL = 300
TEAM_MAX = 400
HOSTILE = 1
INACTIVE = 0
a = {}
a[1] = "Dragon"
a[2] = "GiantWolf"
a[3] = "RabidWolf"
a[4] = "wolf"
a[5] = "Golem"
a[6] = "Ghast"
a[7] = "LesserWraith"
a[8] = "YoungLizard"
a[9] = "Lizard"
NeutralMinionNames = a
HERO_EXP_RADIUS = 1250
a = {}
b = {}
b.GoldGiven = 0
b.ExpGiven = 30
b.SoulGiven = 0
a.Lizard = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 120
b.SoulGiven = 0
a.Dragon = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 60
b.SoulGiven = 0
a.Golem = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 70
b.SoulGiven = 0
a.AncientGolem = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 15
b.SoulGiven = 0
a.LesserWraith = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 70
b.SoulGiven = 0
a.LizardElder = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 30
b.SoulGiven = 0
a.GiantWolf = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 10
b.SoulGiven = 0
a.YoungLizard = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 20
b.SoulGiven = 0
a.wolf = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 40
b.SoulGiven = 0
a.Wraith = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 50
b.SoulGiven = 0
a.RabidWolf = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 50
b.SoulGiven = 0
a.Ghast = b
b = {}
b.GoldGiven = 0
b.ExpGiven = 50
b.SoulGiven = 0
a.Urf = b
DefaultNeutralMinionValues = a
NeutralTimers = {}
a = {}
b = {}
e, f, g, h, i, j, k, l, m, n, o, p, q = Make3DPoint(4976.4, 12.8093, 7277.27)
b[1] = Make3DPoint(4859, 12.8092, 6974)
b[2] = Make3DPoint(5263.88, 12.8092, 6979.76)
b[3] = e
b[4] = f
b[5] = g
b[6] = h
b[7] = i
b[8] = j
b[9] = k
b[10] = l
b[11] = m
b[12] = n
b[13] = o
b[14] = p
b[15] = q
c = {}
f, g, h, i, j, k, l, m, n, o, p, q = Make3DPoint(9875.22, 12.8093, 6991.7)
c[1] = Make3DPoint(9647.71, 12.8091, 6788.22)
c[2] = Make3DPoint(10115, 12.8092, 6804.03)
c[3] = f
c[4] = g
c[5] = h
c[6] = i
c[7] = j
c[8] = k
c[9] = l
c[10] = m
c[11] = n
c[12] = o
c[13] = p
c[14] = q
d = {}
e, f, g, h, i, j, k, l, m, n, o, p, q = Make3DPoint(7424.55, 12.8093, 8238.64)
d[1] = e
d[2] = f
d[3] = g
d[4] = h
d[5] = i
d[6] = j
d[7] = k
d[8] = l
d[9] = m
d[10] = n
d[11] = o
d[12] = p
d[13] = q
e = {}
g, h, i, j, k, l, m, n, o, p, q = Make3DPoint(9874.73, 36.1971, 9974.98)
e[1] = Make3DPoint(9407.59, 36.413, 9949.78)
e[2] = g
e[3] = h
e[4] = i
e[5] = j
e[6] = k
e[7] = l
e[8] = m
e[9] = n
e[10] = o
e[11] = p
e[12] = q
f = {}
h, i, j, k, l, m, n, o, p, q = Make3DPoint(5478, 12.8091, 5189.35)
f[1] = Make3DPoint(5249.46, 12.8091, 5415.25)
f[2] = h
f[3] = i
f[4] = j
f[5] = k
f[6] = l
f[7] = m
f[8] = n
f[9] = o
f[10] = p
f[11] = q
g = {}
l, m, n, o, p, q = Make3DPoint(7466.14, 12.8093, 6164.43)
g[1] = Make3DPoint(7342.52, 12.8092, 6505)
g[2] = Make3DPoint(7480.53, 12.8093, 6362.67)
g[3] = Make3DPoint(7159.11, 12.8093, 6374.56)
g[4] = Make3DPoint(7166.2, 12.8094, 6164.43)
g[5] = l
g[6] = m
g[7] = n
g[8] = o
g[9] = p
g[10] = q
h = {}
k, l, m, n, o, p, q = Make3DPoint(2029.36, 12.8092, 1194.98)
h[1] = Make3DPoint(1684.99, 12.8091, 807.362)
h[2] = Make3DPoint(1994.8, 12.8092, 816.687)
h[3] = k
h[4] = l
h[5] = m
h[6] = n
h[7] = o
h[8] = p
h[9] = q
i = {}
l, m, n, o, p, q = Make3DPoint(12911.8, 12.8091, 12592.8)
i[1] = Make3DPoint(12527.7, 12.8092, 12955)
i[2] = Make3DPoint(12910.6, 12.8092, 12962.7)
i[3] = l
i[4] = m
i[5] = n
i[6] = o
i[7] = p
i[8] = q
j = {}
m, n, o, p, q = Make3DPoint(2048.87, 12.8092, 12621.3)
j[1] = Make3DPoint(2048.28, 12.8092, 12935.1)
j[2] = Make3DPoint(2323.71, 12.8091, 12919)
j[3] = m
j[4] = n
j[5] = o
j[6] = p
j[7] = q
k = {}
n, o, p, q = Make3DPoint(13186.4, 12.8093, 874.649)
k[1] = Make3DPoint(12916.1, 12.8092, 901.078)
k[2] = Make3DPoint(12896.1, 12.8093, 1221.74)
k[3] = n
k[4] = o
k[5] = p
k[6] = q
a[1] = b
a[2] = c
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
a[8] = i
a[9] = j
a[10] = k
CampSpawnPoints = a
a = {}
b = {}
e, f, g, h, i, j, k, l, m, n, o, p, q = Make3DPoint(4976.4, 12.8093, 7277.27)
b[1] = Make3DPoint(4859, 12.8092, 6974)
b[2] = Make3DPoint(5263.88, 12.8092, 6979.76)
b[3] = e
b[4] = f
b[5] = g
b[6] = h
b[7] = i
b[8] = j
b[9] = k
b[10] = l
b[11] = m
b[12] = n
b[13] = o
b[14] = p
b[15] = q
c = {}
f, g, h, i, j, k, l, m, n, o, p, q = Make3DPoint(9875.22, 12.8093, 6991.7)
c[1] = Make3DPoint(9647.71, 12.8091, 6788.22)
c[2] = Make3DPoint(10115, 12.8092, 6804.03)
c[3] = f
c[4] = g
c[5] = h
c[6] = i
c[7] = j
c[8] = k
c[9] = l
c[10] = m
c[11] = n
c[12] = o
c[13] = p
c[14] = q
d = {}
e, f, g, h, i, j, k, l, m, n, o, p, q = Make3DPoint(7424.55, 12.8093, 8238.64)
d[1] = e
d[2] = f
d[3] = g
d[4] = h
d[5] = i
d[6] = j
d[7] = k
d[8] = l
d[9] = m
d[10] = n
d[11] = o
d[12] = p
d[13] = q
e = {}
g, h, i, j, k, l, m, n, o, p, q = Make3DPoint(9874.73, 36.1971, 9974.98)
e[1] = Make3DPoint(9407.59, 36.413, 9949.78)
e[2] = g
e[3] = h
e[4] = i
e[5] = j
e[6] = k
e[7] = l
e[8] = m
e[9] = n
e[10] = o
e[11] = p
e[12] = q
f = {}
h, i, j, k, l, m, n, o, p, q = Make3DPoint(5478, 12.8091, 5189.35)
f[1] = Make3DPoint(5249.46, 12.8091, 5415.25)
f[2] = h
f[3] = i
f[4] = j
f[5] = k
f[6] = l
f[7] = m
f[8] = n
f[9] = o
f[10] = p
f[11] = q
g = {}
l, m, n, o, p, q = Make3DPoint(7466.14, 12.8093, 6164.43)
g[1] = Make3DPoint(7342.52, 12.8092, 6505)
g[2] = Make3DPoint(7480.53, 12.8093, 6362.67)
g[3] = Make3DPoint(7159.11, 12.8093, 6374.56)
g[4] = Make3DPoint(7166.2, 12.8094, 6164.43)
g[5] = l
g[6] = m
g[7] = n
g[8] = o
g[9] = p
g[10] = q
h = {}
k, l, m, n, o, p, q = Make3DPoint(2029.36, 12.8092, 1194.98)
h[1] = Make3DPoint(1684.99, 12.8091, 807.362)
h[2] = Make3DPoint(1994.8, 12.8092, 816.687)
h[3] = k
h[4] = l
h[5] = m
h[6] = n
h[7] = o
h[8] = p
h[9] = q
i = {}
l, m, n, o, p, q = Make3DPoint(12911.8, 12.8091, 12592.8)
i[1] = Make3DPoint(12527.7, 12.8092, 12955)
i[2] = Make3DPoint(12910.6, 12.8092, 12962.7)
i[3] = l
i[4] = m
i[5] = n
i[6] = o
i[7] = p
i[8] = q
j = {}
m, n, o, p, q = Make3DPoint(2048.87, 12.8092, 12621.3)
j[1] = Make3DPoint(2048.28, 12.8092, 12935.1)
j[2] = Make3DPoint(2323.71, 12.8091, 12919)
j[3] = m
j[4] = n
j[5] = o
j[6] = p
j[7] = q
k = {}
n, o, p, q = Make3DPoint(13186.4, 12.8093, 874.649)
k[1] = Make3DPoint(12916.1, 12.8092, 901.078)
k[2] = Make3DPoint(12896.1, 12.8093, 1221.74)
k[3] = n
k[4] = o
k[5] = p
k[6] = q
a[1] = b
a[2] = c
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
a[8] = i
a[9] = j
a[10] = k
CampFacePoints = a
a = {}
b = {}
b[1] = "GiantWolf"
b[2] = "GiantWolf"
b[3] = "RabidWolf"
a[1] = b
b = {}
b[1] = "GiantWolf"
b[2] = "GiantWolf"
b[3] = "RabidWolf"
a[2] = b
b = {}
b[1] = "Dragon"
a[3] = b
b = {}
b[1] = "Golem"
b[2] = "Golem"
a[4] = b
b = {}
b[1] = "Golem"
b[2] = "Golem"
a[5] = b
b = {}
b[1] = "Ghast"
b[2] = "LesserWraith"
b[3] = "LesserWraith"
b[4] = "LesserWraith"
b[5] = "LesserWraith"
a[6] = b
b = {}
b[1] = "YoungLizard"
b[2] = "Lizard"
b[3] = "Lizard"
a[7] = b
b = {}
b[1] = "wolf"
b[2] = "wolf"
b[3] = "GiantWolf"
a[8] = b
b = {}
b[1] = "wolf"
b[2] = "wolf"
b[3] = "GiantWolf"
a[9] = b
b = {}
b[1] = "YoungLizard"
b[2] = "Lizard"
b[3] = "Lizard"
a[10] = b
PredefinedCamps = a
a = {}
a[1] = {}
a[2] = {}
a[3] = {}
a[4] = {}
a[5] = {}
a[6] = {}
a[7] = {}
a[8] = {}
a[9] = {}
a[10] = {}
NeutralMinionCamps = a
NUMBER_OF_CAMPS = #NeutralMinionCamps
NeutralMinionInit = function()
    local r, s, t, u, v, w, x
    r, s, t = pairs(NeutralMinionNames)
    for u, v in r, s, t do
        PreloadCharacter(v)
    end
    for u = 1, NUMBER_OF_CAMPS, 1 do
        NeutralMinionCamps[u].Positions = CampSpawnPoints[u]
        NeutralMinionCamps[u].FacePositions = CampFacePoints[u]
        NeutralMinionCamps[u].UniqueNames = {}
        NeutralMinionCamps[u].AliveTracker = {}
        NeutralMinionCamps[u].Team = TEAM_NEUTRAL
        NeutralMinionCamps[u].DamageBonus = 0
        NeutralMinionCamps[u].HealthBonus = 0
        NeutralMinionCamps[u].AIState = INACTIVE
        NeutralMinionCamps[u].GroupsRespawnTime = 120
        NeutralMinionCamps[u].CampLevel = 1
        NeutralMinionCamps[u].TimerType = ""
        NeutralMinionCamps[u].MinimapIcon = ""
    end
    s = {}
    s[1] = PredefinedCamps[1]
    NeutralMinionCamps[1].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[1].GroupsChance = s
    NeutralMinionCamps[1].GroupsRespawnTime = 80
    NeutralMinionCamps[1].GroupDelaySpawnTime = 0
    s = {}
    s[1] = PredefinedCamps[2]
    NeutralMinionCamps[2].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[2].GroupsChance = s
    NeutralMinionCamps[2].GroupsRespawnTime = 80
    NeutralMinionCamps[2].GroupDelaySpawnTime = 0
    s = {}
    s[1] = PredefinedCamps[3]
    NeutralMinionCamps[3].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[3].GroupsChance = s
    NeutralMinionCamps[3].GroupsRespawnTime = 210
    NeutralMinionCamps[3].GroupDelaySpawnTime = 15
    s = {}
    s[1] = PredefinedCamps[4]
    NeutralMinionCamps[4].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[4].GroupsChance = s
    NeutralMinionCamps[4].GroupsRespawnTime = 80
    NeutralMinionCamps[4].GroupDelaySpawnTime = 0
    s = {}
    s[1] = PredefinedCamps[5]
    NeutralMinionCamps[5].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[5].GroupsChance = s
    NeutralMinionCamps[5].GroupsRespawnTime = 80
    NeutralMinionCamps[5].GroupDelaySpawnTime = 0
    s = {}
    s[1] = PredefinedCamps[6]
    NeutralMinionCamps[6].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[6].GroupsChance = s
    NeutralMinionCamps[6].GroupsRespawnTime = 150
    NeutralMinionCamps[6].GroupDelaySpawnTime = 0
    s = {}
    s[1] = PredefinedCamps[7]
    NeutralMinionCamps[7].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[7].GroupsChance = s
    NeutralMinionCamps[7].GroupsRespawnTime = 80
    NeutralMinionCamps[7].GroupDelaySpawnTime = 0
    s = {}
    s[1] = PredefinedCamps[8]
    NeutralMinionCamps[8].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[8].GroupsChance = s
    NeutralMinionCamps[8].GroupsRespawnTime = 80
    NeutralMinionCamps[8].GroupDelaySpawnTime = 0
    s = {}
    s[1] = PredefinedCamps[9]
    NeutralMinionCamps[9].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[9].GroupsChance = s
    NeutralMinionCamps[9].GroupsRespawnTime = 80
    NeutralMinionCamps[9].GroupDelaySpawnTime = 0
    s = {}
    s[1] = PredefinedCamps[10]
    NeutralMinionCamps[10].Groups = s
    s = {}
    s[1] = 100
    NeutralMinionCamps[10].GroupsChance = s
    NeutralMinionCamps[10].GroupsRespawnTime = 80
    NeutralMinionCamps[10].GroupDelaySpawnTime = 0
end
CreateRespawnNeutralTimer = function(y)
    local s
    return function()
        local z, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O
        A = 0
        NeutralMinionCamps[y].DamageBonus = NeutralMinionCamps[y].DamageBonus + 0
        NeutralMinionCamps[y].HealthBonus = NeutralMinionCamps[y].HealthBonus + 50
        for F = 1, #NeutralMinionCamps[y].Groups, 1 do
            if math.random(100) <= NeutralMinionCamps[y].GroupsChance[F] + A and false == false then
                NeutralMinionCamps[y].AliveTracker = {}
                for J = 1, #NeutralMinionCamps[y].Groups[F], 1 do
                    SpawnNeutralMinion(NeutralMinionCamps[y], y, F, J)
                    NeutralMinionCamps[y].AliveTracker[J] = true
                end
            end
        end
    end
end
InitializeNeutralMinionInfo = function()
    local r, s, t, u, v, w, x, P, Q, R, S, T, U, V, W, X, Y, Z, _
    for u = 1, #NeutralMinionCamps, 1 do
        for P = 1, #NeutralMinionCamps[u].Groups, 1 do
            NeutralMinionCamps[u].UniqueNames[P] = {}
            for T = 1, #NeutralMinionCamps[u].Groups[P], 1 do
                NeutralMinionCamps[u].UniqueNames[P][T] =
                    NeutralMinionCamps[u].Groups[P][T] .. u .. "." .. P .. "." .. T
            end
        end
        v = #NeutralTimers
        NeutralTimers[v + 1] = CreateRespawnNeutralTimer(u)
        NeutralMinionCamps[u].Timer = NeutralTimers[v + 1]
    end
end
SpawnInitialNeutralMinions = function(y)
    local s, t, u, v, w, x, P, Q, R, S, T, U, V, W, X, Y
    if y <= #NeutralMinionCamps then
        t = 0
        u = false
        for P = 1, #NeutralMinionCamps[y].Groups, 1 do
            if 0 < NeutralMinionCamps[y].GroupDelaySpawnTime and u == false then
                NeutralMinionCamps[y].AliveTracker = {}
                for T = 1, #NeutralMinionCamps[y].Groups[P], 1 do
                    NeutralMinionCamps[y].AliveTracker[T] = true
                end
                InitNeutralMinionTimer(
                    NeutralMinionCamps[y].Timer,
                    NeutralMinionCamps[y].TimerType,
                    NeutralMinionCamps[y].GroupDelaySpawnTime,
                    0,
                    false
                )
            else
                if math.random(100) <= NeutralMinionCamps[y].GroupsChance[P] + t and u == false then
                    NeutralMinionCamps[y].AliveTracker = {}
                    for T = 1, #NeutralMinionCamps[y].Groups[P], 1 do
                        SpawnNeutralMinion(NeutralMinionCamps[y], y, P, T)
                        NeutralMinionCamps[y].AliveTracker[T] = true
                    end
                end
            end
        end
    end
end
NeutralMinionDeath = function(y, a0, a1)
    local u, v, w, x, P, Q, R, S, T, U, V, W, X, Y, Z, _, a2, a3, a4, a5
    v = 0
    for Q = 1, #NeutralMinionCamps, 1 do
        for U = 1, #NeutralMinionCamps[Q].UniqueNames, 1 do
            for Y = 1, #NeutralMinionCamps[Q].UniqueNames[U], 1 do
                if NeutralMinionCamps[Q].UniqueNames[U][Y] == y then
                    v = Q
                    NeutralMinionCamps[Q].AliveTracker[Y] = false
                    GiveExpToNearHeroesFromNeutral(
                        a0,
                        DefaultNeutralMinionValues[string.match(NeutralMinionCamps[Q].UniqueNames[U][Y], "%a+")].ExpGiven,
                        a1,
                        HERO_EXP_RADIUS
                    )
                    IncGold(
                        a0,
                        DefaultNeutralMinionValues[string.match(NeutralMinionCamps[Q].UniqueNames[U][Y], "%a+")].GoldGiven,
                        "GOLD_KILL_MINION"
                    )
                end
            end
        end
    end
    if 0 < v then
        for Q = 1, #NeutralMinionCamps[v].AliveTracker, 1 do
            if NeutralMinionCamps[v].AliveTracker[Q] == true then
                u = false
            end
        end
        if u == true then
            InitNeutralMinionTimer(
                NeutralMinionCamps[v].Timer,
                NeutralMinionCamps[v].TimerType,
                NeutralMinionCamps[v].GroupsRespawnTime,
                0,
                false
            )
        end
    end
end
