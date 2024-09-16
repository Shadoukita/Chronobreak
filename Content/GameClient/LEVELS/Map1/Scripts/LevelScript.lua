local a, b, c, d, e, f, g, h, i, j, k, l, m
MAX_MINIONS_EVER = 180
TEAM_ORDER = 100
TEAM_CHAOS = 200
ORDER_HQ = 1
CHAOS_HQ = 2
FRONT_TOWER = 3
MIDDLE_TOWER = 2
BACK_TOWER = 1
HQ_TOWER2 = 4
HQ_TOWER1 = 5
RIGHT_LANE = 0
CENTER_LANE = 1
LEFT_LANE = 2
INITIAL_TIME_TO_SPAWN = 90
CANNON_MINION_SPAWN_FREQUENCY = 3
INCREASE_CANNON_RATE_TIMER = 2090
MINION_HEALTH_DENIAL_PERCENT = 0
UPGRADE_MINION_TIMER = 90
EXP_GIVEN_RADIUS = 1400
GOLD_GIVEN_RADIUS = 1250
DISABLE_MINION_SPAWN_BASE_TIME = 240
DISABLE_MINION_SPAWN_MAG_TIME = 0
LAST_WAVE = -1
SPECIAL_MINION_MODE = "none"
HQTurretAttackable = false
a = {}
a.WaveSpawnRate = 30000
a.SingleMinionSpawnDelay = 800
a.ExpRadius = EXP_GIVEN_RADIUS
a.GoldRadius = GOLD_GIVEN_RADIUS
SpawnTable = a
a = {}
a.DefaultNumPerWave = 3
a.Armor = 0
a.ArmorUpgrade = 1
a.MagicResistance = 0
a.MagicResistanceUpgrade = 0.625
a.HPBonus = 0
a.HPUpgrade = 10
a.HPInhibitor = 0
a.DamageBonus = 0
a.DamageUpgrade = 0.5
a.DamageInhibitor = 0
a.ExpGiven = 64
a.ExpBonus = 0
a.ExpUpgrade = 0
a.ExpInhibitor = 0
a.GoldGiven = 18.8
a.GoldBonus = 0
a.GoldUpgrade = 0.2
a.GoldInhibitor = 0
a.GoldShared = 0
a.GoldShareUpgrade = 0
a.GoldMaximumBonus = 12
a.LocalGoldGiven = 0
a.LocalGoldBonus = 0
MeleeDefaultMinionInfo = a
a = {}
a.DefaultNumPerWave = 3
a.Armor = 0
a.ArmorUpgrade = 0.625
a.MagicResistance = 0
a.MagicResistanceUpgrade = 1
a.HPBonus = 0
a.HPUpgrade = 7.5
a.HPInhibitor = 0
a.DamageBonus = 0
a.DamageUpgrade = 1
a.DamageInhibitor = 0
a.ExpGiven = 32
a.ExpBonus = 0
a.ExpUpgrade = 0
a.ExpInhibitor = 0
a.GoldGiven = 13.8
a.GoldBonus = 0
a.GoldUpgrade = 0.2
a.GoldInhibitor = 0
a.GoldShared = 0
a.GoldShareUpgrade = 0
a.GoldMaximumBonus = 8
a.LocalGoldGiven = 0
a.LocalGoldBonus = 0
CasterDefaultMinionInfo = a
a = {}
a.DefaultNumPerWave = 0
a.Armor = 0
a.ArmorUpgrade = 1.5
a.MagicResistance = 0
a.MagicResistanceUpgrade = 1.5
a.HPBonus = 0
a.HPUpgrade = 13.5
a.HPInhibitor = 0
a.DamageBonus = 0
a.DamageUpgrade = 1.5
a.DamageInhibitor = 0
a.ExpGiven = 100
a.ExpBonus = 0
a.ExpUpgrade = 0
a.ExpInhibitor = 0
a.GoldGiven = 39.5
a.GoldBonus = 0
a.GoldUpgrade = 0.5
a.GoldInhibitor = 0
a.GoldShared = 0
a.GoldShareUpgrade = 0
a.GoldMaximumBonus = 30
a.LocalGoldGiven = 0
a.LocalGoldBonus = 0
CannonDefaultMinionInfo = a
a = {}
a.DefaultNumPerWave = 0
a.Armor = 0
a.ArmorUpgrade = 0
a.MagicResistance = 0
a.MagicResistanceUpgrade = 0
a.HPBonus = 0
a.HPUpgrade = 100
a.HPInhibitor = 0
a.DamageBonus = 0
a.DamageUpgrade = 5
a.DamageInhibitor = 0
a.ExpGiven = 100
a.ExpBonus = 0
a.ExpUpgrade = 0
a.ExpInhibitor = 0
a.GoldGiven = 39.5
a.GoldBonus = 0
a.GoldUpgrade = 0.5
a.GoldInhibitor = 0
a.GoldShared = 0
a.GoldShareUpgrade = 0
a.GoldMaximumBonus = 30
a.LocalGoldGiven = 0
a.LocalGoldBonus = 0
SuperDefaultMinionInfo = a
a = {}
a.MinionName = "Blue_Minion_Basic"
a.DefaultInfo = MeleeDefaultMinionInfo
OrderMeleeMinionInfo = a
a = {}
a.MinionName = "Red_Minion_Basic"
a.DefaultInfo = MeleeDefaultMinionInfo
ChaosMeleeMinionInfo = a
a = {}
a.MinionName = "Blue_Minion_Wizard"
a.DefaultInfo = CasterDefaultMinionInfo
OrderCasterMinionInfo = a
a = {}
a.MinionName = "Red_Minion_Wizard"
a.DefaultInfo = CasterDefaultMinionInfo
ChaosCasterMinionInfo = a
a = {}
a.MinionName = "Blue_Minion_MechCannon"
a.DefaultInfo = CannonDefaultMinionInfo
OrderCannonMinionInfo = a
a = {}
a.MinionName = "Red_Minion_MechCannon"
a.DefaultInfo = CannonDefaultMinionInfo
ChaosCannonMinionInfo = a
a = {}
a.MinionName = "Blue_Minion_MechMelee"
a.DefaultInfo = SuperDefaultMinionInfo
OrderSuperMinionInfo = a
a = {}
a.MinionName = "Red_Minion_MechMelee"
a.DefaultInfo = SuperDefaultMinionInfo
ChaosSuperMinionInfo = a
a = {}
a.IsDestroyed = false
a.NumOfSpawnDisables = 0
a.WillSpawnSuperMinion = 0
b = {}
b[1] = "Super"
b[2] = "Melee"
b[3] = "Cannon"
b[4] = "Caster"
a.SpawnOrderMinionNames = b
DefaultBarrackInfo = a
a = {}
a.Melee = OrderMeleeMinionInfo
a.Super = OrderSuperMinionInfo
a.Cannon = OrderCannonMinionInfo
a.Caster = OrderCasterMinionInfo
DefaultOrderMinionInfoTable = a
a = {}
a.Melee = ChaosMeleeMinionInfo
a.Super = ChaosSuperMinionInfo
a.Cannon = ChaosCannonMinionInfo
a.Caster = ChaosCasterMinionInfo
DefaultChaosMinionInfoTable = a
a = {}
a.DefaultInfo = DefaultBarrackInfo
a.DefaultMinionInfoTable = DefaultOrderMinionInfoTable
OrderBarrack0 = a
a = {}
a.DefaultInfo = DefaultBarrackInfo
a.DefaultMinionInfoTable = DefaultOrderMinionInfoTable
OrderBarrack1 = a
a = {}
a.DefaultInfo = DefaultBarrackInfo
a.DefaultMinionInfoTable = DefaultOrderMinionInfoTable
OrderBarrack2 = a
a = {}
a.DefaultInfo = DefaultBarrackInfo
a.DefaultMinionInfoTable = DefaultChaosMinionInfoTable
ChaosBarrack0 = a
a = {}
a.DefaultInfo = DefaultBarrackInfo
a.DefaultMinionInfoTable = DefaultChaosMinionInfoTable
ChaosBarrack1 = a
a = {}
a.DefaultInfo = DefaultBarrackInfo
a.DefaultMinionInfoTable = DefaultChaosMinionInfoTable
ChaosBarrack2 = a
AppendTable = function(n, o, p, q)
    local r, s, t, u, v, w, x, y, z, A, B, C
    if o ~= nil then
        if type(o) == "table" then
            if n == nil then
                n = {}
            end
            t, u, v = pairs(o)
            for w, x in t, u, v do
                if p == true and type(x) == "table" then
                    n[w] = AppendTable(n[w], x, q, q)
                else
                    n[w] = x
                end
            end
        end
    end
    return n
end
InitBarrackInfo = function(n)
    local D, E, F, r, s, t, u, v, w, x, y, z, A, B
    n = AppendTable(n, n.DefaultInfo, true, true)
    n.MinionInfoTable = AppendTable(n.MinionInfoTable, n.DefaultMinionInfoTable, true, false)
    if n.MinionInfoTable ~= nil then
        D = n.MinionInfoTable
        r, s, t = pairs(D)
        for u, v in r, s, t do
            w = D[u]
            if w.DefaultInfo ~= nil then
                AppendTable(w, w.DefaultInfo, true, true)
                w.NumPerWave = w.DefaultNumPerWave
                w.GoldGivenBase = w.GoldGiven
                w.ExpGivenBase = w.ExpGiven
            end
        end
    end
    if n.Overrides ~= nil then
        F, r, s = pairs(n.Overrides)
        for t, u in F, r, s do
            if type(n[t]) == "table" and type(u) == "table" then
                AppendTable(n[t], u, true, true)
            else
                if type(u) ~= "table" then
                    n[t] = u
                end
            end
        end
    end
    return n
end
a = {}
d, e, f, g, h, i, j, k, l, m = InitBarrackInfo(OrderBarrack2)
a[1] = InitBarrackInfo(OrderBarrack0)
a[2] = InitBarrackInfo(OrderBarrack1)
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
a[8] = i
a[9] = j
a[10] = k
a[11] = l
a[12] = m
OrderBarracksBonuses = a
a = {}
d, e, f, g, h, i, j, k, l, m = InitBarrackInfo(ChaosBarrack2)
a[1] = InitBarrackInfo(ChaosBarrack0)
a[2] = InitBarrackInfo(ChaosBarrack1)
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
a[8] = i
a[9] = j
a[10] = k
a[11] = l
a[12] = m
ChaosBarracksBonuses = a
a = {}
a[1] = "Dragon"
a[2] = "Golem"
a[3] = "wolf"
a[4] = "AncientGolem"
a[5] = "LesserWraith"
a[6] = "GiantWolf"
a[7] = "LizardElder"
a[8] = "YoungLizard"
a[9] = "Wraith"
a[10] = "Worm"
a[11] = "SmallGolem"
a[12] = "GreatWraith"
NeutralMinionNames = a
CreateLaneBuildingTable = function()
    local G, D
    G = {}
    G.Turret3 = true
    G.Turret2 = true
    G.Turret1 = true
    G.Barracks = true
    return G
end
a = {}
a.HQTower2 = true
a.HQTower1 = true
a.HQ = true
a[1] = CreateLaneBuildingTable()
a[2] = CreateLaneBuildingTable()
a[3] = CreateLaneBuildingTable()
OrderBuildingStatus = a
a = {}
a.HQTower2 = true
a.HQTower1 = true
a.HQ = true
a[1] = CreateLaneBuildingTable()
a[2] = CreateLaneBuildingTable()
a[3] = CreateLaneBuildingTable()
ChaosBuildingStatus = a
TotalNumberOfMinions = 0
totalNumberOfChaosBarracks = 3
totalNumberOfOrderBarracks = 3
PreloadBarrackCharacters = function(n)
    local D, E, F, r, s, t, u, v, w
    F, r, s = pairs(n.DefaultMinionInfoTable)
    for t, u in F, r, s do
        if u.PreloadedCharacter == nil then
            u.PreloadedCharacter = 1
            PreloadCharacter(u.MinionName)
        end
    end
end
OnLevelInit = function()
    local G, D, E, F, r, s, t, u, v
    E, F, r = pairs(OrderBarracksBonuses)
    for s, t in E, F, r do
        PreloadBarrackCharacters(t)
    end
    E, F, r = pairs(ChaosBarracksBonuses)
    for s, t in E, F, r do
        PreloadBarrackCharacters(t)
    end
    PreloadCharacter("OrderTurretAngel")
    PreloadCharacter("OrderTurretDragon")
    PreloadCharacter("OrderTurretNormal2")
    PreloadCharacter("OrderTurretNormal")
    PreloadCharacter("ChaosTurretWorm")
    PreloadCharacter("ChaosTurretWorm2")
    PreloadCharacter("ChaosTurretGiant")
    PreloadCharacter("ChaosTurretNormal")
    PreloadSpell("RespawnClassic")
    PreloadSpell("SpellShieldMarker")
    F, r, s, t, u, v = os.time()
    math.randomseed(F, r, s, t, u, v)
    LoadLevelScriptIntoScript("NeutralMinionSpawn.lua")
    NeutralMinionInit()
    LoadLevelScriptIntoScript("EndOfGame.lua")
    SpawnTable.WaveSpawnRate = 30000
    SpawnTable.SingleMinionSpawnDelay = 800
    SpawnTable.ExpRadius = EXP_GIVEN_RADIUS
    SpawnTable.GoldRadius = GOLD_GIVEN_RADIUS
end
OnLevelInitServer = function()
    local G, D, E, F
    InitTimer("UpgradeMinionTimer", UPGRADE_MINION_TIMER, true)
    InitTimer("IncreaseCannonMinionSpawnRate", INCREASE_CANNON_RATE_TIMER, false)
    InitTimer("AllowDamageOnBuildings", 10, false)
end
OnPostLevelLoad = function()
    local G, D
    LoadLevelScriptIntoScript("CreateLevelProps.lua")
    CreateLevelProps()
end
OppositeTeam = function(n)
    local D
    if n == TEAM_CHAOS then
        return TEAM_ORDER
    else
        return TEAM_CHAOS
    end
end
UpgradeMinionTimer = function()
    local G, D, E, F, r, s, t, u, v, w, x, y, z, A, B, C, H, I, J
    for r = 1, 2, 1 do
        if r == 1 then
            G = OrderBarracksBonuses
        else
            G = ChaosBarracksBonuses
        end
        for v = 1, 3, 1 do
            z, A, B = pairs(G[v].MinionInfoTable)
            for C, H in z, A, B do
                H.HPBonus = H.HPBonus + H.HPUpgrade
                H.DamageBonus = H.DamageBonus + H.DamageUpgrade
                H.GoldBonus = H.GoldBonus + H.GoldUpgrade
                if H.GoldBonus > H.GoldMaximumBonus then
                    H.GoldBonus = H.GoldMaximumBonus
                end
                H.LocalGoldBonus = H.LocalGoldBonus + H.GoldShareUpgrade
                H.Armor = H.Armor + H.ArmorUpgrade
                H.MagicResistance = H.MagicResistance + H.MagicResistanceUpgrade
                H.ExpBonus = H.ExpBonus + H.ExpUpgrade
                H.GoldGiven = H.GoldBonus + H.GoldGivenBase
                H.LocalGoldGiven = H.LocalGoldBonus + H.GoldShared
                H.ExpGiven = H.ExpBonus + H.ExpGivenBase
            end
        end
    end
end
AllowDamageOnBuildings = function()
    local G, D, E, F, r, s, t, u, v, w, x, y
    for F = RIGHT_LANE, LEFT_LANE, 1 do
        for u = BACK_TOWER, HQ_TOWER1, 1 do
            orderTurret = GetTurret(TEAM_ORDER, F, u)
            if orderTurret ~= nil then
                if u == FRONT_TOWER then
                    SetInvulnerable(orderTurret, false)
                    SetTargetable(orderTurret, true)
                else
                    SetInvulnerable(orderTurret, true)
                    SetNotTargetableToTeam(orderTurret, true, TEAM_CHAOS)
                end
            end
            chaosTurret = GetTurret(TEAM_CHAOS, F, u)
            if chaosTurret ~= nil then
                if u == FRONT_TOWER then
                    SetInvulnerable(chaosTurret, false)
                    SetTargetable(chaosTurret, true)
                else
                    SetInvulnerable(chaosTurret, true)
                    SetNotTargetableToTeam(chaosTurret, true, TEAM_ORDER)
                end
            end
        end
    end
end
ResetToDefaultWaveCounts = function(n)
    local D, E, F, r, s, t, u, v
    F, r, s = pairs(n)
    for t, u in F, r, s do
        u.NumPerWave = u.DefaultNumPerWave
    end
end
ClearCurrentWaveCounts = function(n)
    local D, E, F, r, s, t, u, v
    F, r, s = pairs(n)
    for t, u in F, r, s do
        u.NumPerWave = 0
    end
end
GetInitSpawnInfo = function(n, o)
    local E, F, r
    E = 0
    if o == TEAM_ORDER then
        E = OrderBarracksBonuses[n + 1]
    else
        E = ChaosBarracksBonuses[n + 1]
    end
    F = {}
    F.WaveSpawnRate = SpawnTable.WaveSpawnRate
    F.SingleMinionSpawnDelay = SpawnTable.SingleMinionSpawnDelay
    F.IsDestroyed = E.IsDestroyed
    F.MinionInfoTable = E.MinionInfoTable
    ReturnTable = F
    return ReturnTable
end
GetMinionSpawnInfo = function(n, o, p, q, K)
    local s, t, u, v, w, x, y, z
    s = 0
    if q == TEAM_ORDER then
        s = OrderBarracksBonuses[n + 1]
    else
        s = ChaosBarracksBonuses[n + 1]
    end
    t = s.MinionInfoTable.Super
    u = s.MinionInfoTable.Cannon
    ResetToDefaultWaveCounts(s.MinionInfoTable)
    if o % CANNON_MINION_SPAWN_FREQUENCY == 0 then
        u.NumPerWave = u.NumPerWave + 1
    end
    if K ~= LAST_WAVE then
        BARRACKSCOUNT = 6
        totalMinionsRemaining = MAX_MINIONS_EVER - GetTotalTeamMinionsSpawned()
        if totalMinionsRemaining <= BARRACKSCOUNT * 7 then
            if totalMinionsRemaining <= 0 then
                SPECIAL_MINION_MODE = "None"
            else
                if totalMinionsRemaining >= BARRACKSCOUNT then
                    SPECIAL_MINION_MODE = "2MeleeMinions"
                else
                    SPECIAL_MINION_MODE = "None"
                end
            end
        else
            SPECIAL_MINION_MODE = ""
        end
        LAST_WAVE = K
    end
    if s.WillSpawnSuperMinion == 1 then
        if q == TEAM_ORDER then
            if totalNumberOfChaosBarracks == 0 then
                t.NumPerWave = 2
            end
        end
        if q == TEAM_CHAOS then
            if totalNumberOfOrderBarracks == 0 then
                t.NumPerWave = 2
            end
        else
            t.NumPerWave = 1
        end
        u.NumPerWave = 0
    end
    if SPECIAL_MINION_MODE == "2MeleeMinions" then
        v = s.MinionInfoTable.Super.NumPerWave
        ClearCurrentWaveCounts(s.MinionInfoTable)
        t.NumPerWave = v
        s.MinionInfoTable.Melee.NumPerWave = math.max(2 - v, 0)
    else
        if SPECIAL_MINION_MODE == "None" then
            ClearCurrentWaveCounts(s.MinionInfoTable)
        end
    end
    v = {}
    v.NewFormat = true
    v.SpawnOrderMinionNames = s.SpawnOrderMinionNames
    v.IsDestroyed = s.IsDestroyed
    v.ExperienceRadius = SpawnTable.ExpRadius
    v.GoldRadius = SpawnTable.GoldRadius
    v.MinionInfoTable = s.MinionInfoTable
    ReturnTable = v
    return ReturnTable
end
DeactivateCorrectStructure = function(n, o, p)
    local F, r, s, t, u
    if n == TEAM_ORDER then
        F = OrderBuildingStatus
    else
        F = ChaosBuildingStatus
    end
    if p == FRONT_TOWER then
        F[o + 1].Turret3 = false
        r = GetTurret(n, o, MIDDLE_TOWER)
        SetInvulnerable(r, false)
        SetTargetable(r, true)
    else
        if p == MIDDLE_TOWER then
            F[o + 1].Turret2 = false
            r = GetTurret(n, o, BACK_TOWER)
            SetInvulnerable(r, false)
            SetTargetable(r, true)
        else
            if p == BACK_TOWER then
                F[o + 1].Turret1 = false
                r = GetDampener(n, o)
                SetInvulnerable(r, false)
                SetTargetable(r, true)
            else
                if p == HQ_TOWER2 then
                    F.HQTower2 = false
                    if F.HQTower1 == false then
                        r = GetHQ(n)
                        SetInvulnerable(r, false)
                        SetTargetable(r, true)
                    end
                else
                    if p == HQ_TOWER1 then
                        F.HQTower1 = false
                        if F.HQTower2 == false then
                            r = GetHQ(n)
                            SetInvulnerable(r, false)
                            SetTargetable(r, true)
                        end
                    end
                end
            end
        end
    end
end
GetLuaBarracks = function(n, o)
    local E, F, r
    if n == TEAM_ORDER then
        E = OrderBarracksBonuses[o + 1]
    else
        E = ChaosBarracksBonuses[o + 1]
    end
    return E
end
GetDisableMinionSpawnTime = function(n, o)
    local E, F, r
    barrack = GetLuaBarracks(o, n)
    return DISABLE_MINION_SPAWN_BASE_TIME + DISABLE_MINION_SPAWN_MAG_TIME * barrack.NumOfSpawnDisables
end
DisableBarracksSpawn = function(n, o)
    local E, F, r, s, t
    cLangBarracks = GetBarracks(o, n)
    luaBarrack = GetLuaBarracks(o, n)
    r, s, t = GetDisableMinionSpawnTime(n, o)
    SetDisableMinionSpawn(cLangBarracks, r, s, t)
    luaBarrack.NumOfSpawnDisables = luaBarrack.NumOfSpawnDisables + 1
end
BonusesCounter = 0
ApplyBarracksDestructionBonuses = function(n, o)
    local E, F, r, s, t, u, v, w, x, y, z, A, B, C, H, I
    BonusesCounter = BonusesCounter + 1
    for s = 1, 3, 1 do
        t = nil
        if n == TEAM_ORDER then
            t = OrderBarracksBonuses
            EnemyBarracks = ChaosBarracksBonuses
        else
            t = ChaosBarracksBonuses
            EnemyBarracks = OrderBarracksBonuses
        end
        y, z, A = pairs(t[s].MinionInfoTable)
        for B, C in y, z, A do
            C.HPBonus = C.HPBonus + C.HPInhibitor
            C.DamageBonus = C.DamageBonus + C.DamageInhibitor
            C.ExpGiven = C.ExpGiven - C.ExpInhibitor
            C.GoldGiven = C.GoldGiven - C.GoldInhibitor
        end
        if s == o + 1 then
            t[s].WillSpawnSuperMinion = 1
            if n == TEAM_ORDER then
                totalNumberOfChaosBarracks = totalNumberOfChaosBarracks - 1
            else
                totalNumberOfOrderBarracks = totalNumberOfOrderBarracks - 1
            end
        end
    end
end
ReductionCounter = 0
ApplyBarracksRespawnReductions = function(n, o)
    local E, F, r, s, t, u, v, w, x, y, z, A, B, C, H, I, J, L, M, N, O
    ReductionCounter = ReductionCounter + 1
    for s = 1, 3, 1 do
        t = nil
        v = nil
        if n == TEAM_ORDER then
            v = TEAM_CHAOS
            t = OrderBarracksBonuses
        else
            v = TEAM_ORDER
            t = ChaosBarracksBonuses
        end
        z, A, B = pairs(t[s].MinionInfoTable)
        for C, H in z, A, B do
            H.HPBonus = H.HPBonus - H.HPInhibitor
            H.DamageBonus = H.DamageBonus - H.DamageInhibitor
            H.ExpGiven = H.ExpGiven + H.ExpInhibitor
            H.GoldGiven = H.GoldGiven + H.GoldInhibitor
        end
        z = nil
        if s == o + 1 then
            if n == TEAM_ORDER then
                totalNumberOfChaosBarracks = totalNumberOfChaosBarracks + 1
                z = totalNumberOfChaosBarracks
            else
                totalNumberOfOrderBarracks = totalNumberOfOrderBarracks + 1
                z = totalNumberOfOrderBarracks
            end
            t[s].WillSpawnSuperMinion = 0
        end
        if z == 3 then
            HQ = GetHQ(v)
            SetInvulnerable(HQ, true)
            SetTargetable(HQ, false)
            for H = RIGHT_LANE, LEFT_LANE, 1 do
                I = GetTurret(v, H, HQ_TOWER1)
                J = GetTurret(v, H, HQ_TOWER2)
                if I ~= nil then
                    SetInvulnerable(I, true)
                    SetNotTargetableToTeam(I, true, n)
                end
                if J ~= nil then
                    SetInvulnerable(J, true)
                    SetNotTargetableToTeam(J, true, n)
                end
            end
        end
    end
end
ReactiveCounter = 0
BarrackReactiveEvent = function(n, o)
    local E, F, r, s
    ReactiveCounter = ReactiveCounter + 1
    dampener = GetDampener(n, o)
    SetInvulnerable(dampener, false)
    SetTargetable(dampener, true)
    ApplyBarracksRespawnReductions(OppositeTeam(n), o)
end
DisableSuperMinions = function(n, o)
    local E, F, r, s, t
    if n == TEAM_ORDER then
        F = ChaosBarracksBonuses[o + 1]
    else
        if n == TEAM_CHAOS then
            F = OrderBarracksBonuses[o + 1]
        end
    end
    if F then
        F.WillSpawnSuperMinion = 0
    end
end
DisactivatedCounter = 0
HandleDestroyedObject = function(n)
    local D, E, F, r, s, t, u
    HQType = GetHQType(n)
    if HQType == ORDER_HQ or HQType == CHAOS_HQ then
        if HQType == CHAOS_HQ then
            EndOfGameCeremony(TEAM_ORDER, n)
        else
            EndOfGameCeremony(TEAM_CHAOS, n)
        end
        return
    end
    if IsDampener(n) then
        barrack = GetLinkedBarrack(n)
        barrackTeam = GetTeamID(barrack)
        barrackLane = GetLane(n)
        DisableBarracksSpawn(barrackLane, barrackTeam)
        SetDampenerState(n, DampenerRegenerationState)
        SetInvulnerable(n, true)
        SetTargetable(n, false)
        DisactivatedCounter = DisactivatedCounter + 1
        D = GetTurret(barrackTeam, 1, HQ_TOWER1)
        E = GetTurret(barrackTeam, 1, HQ_TOWER2)
        if D ~= nil then
            SetInvulnerable(D, false)
            SetTargetable(D, true)
        end
        if E ~= nil then
            SetInvulnerable(E, false)
            SetTargetable(E, true)
        end
        if D == nil then
            if E == nil then
                F = GetHQ(barrackTeam)
                SetInvulnerable(F, false)
                SetTargetable(F, true)
            end
        end
        F = nil
        if barrackTeam == TEAM_CHAOS then
            F = TEAM_ORDER
        else
            F = TEAM_CHAOS
        end
        ApplyBarracksDestructionBonuses(F, barrackLane)
    end
    if IsTurretAI(n) then
        DeactivateCorrectStructure(GetTeamID(n), GetObjectLaneId(n), GetTurretPosition(n))
        return
    end
    D = GetDampenerType(n)
    if -1 < D then
        s = D % TEAM_CHAOS
        if s >= RIGHT_LANE and s <= LEFT_LANE then
            ChaosBuildingStatus[s + 1].Barracks = false
        else
            s = s - TEAM_ORDER
            OrderBuildingStatus[s + 1].Barracks = false
        end
    else
        Log("Could not find Linking barracks!")
    end
    return true
end
IncreaseCannonMinionSpawnRate = function()
    local G, D
    CANNON_MINION_SPAWN_FREQUENCY = 2
end
PostGameSetup = function(n)
    local D
    POST_GAME_EVENTS = {}
end
PostGameUpdate = function(n, o)
    local E, F, r, s, t, u, v, w, x, y
    r, s, t = pairs(POST_GAME_EVENTS)
    for u, v in r, s, t do
        if n > v.delay then
            ClientSide_CameraMoveCameraFromCurrentPositionToPoint(v.cameraLocation, v.travelTime)
            if v.soundFile then
                ClientSide_PlaySoundFile(v.soundFile)
            end
            table.remove(POST_GAME_EVENTS, u)
            break
        end
    end
end
