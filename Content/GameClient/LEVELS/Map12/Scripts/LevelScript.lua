local a, b, c, d, e, f
MAX_MINIONS_EVER = 180
TEAM_ORDER = 100
TEAM_CHAOS = 200
ORDER_HQ = 1
CHAOS_HQ = 2
FRONT_TOWER = 4
MID_TOWER = 3
BACK_TOWER = 2
BACK_TOWER2 = 1
CENTER_LANE = 1
INITIAL_TIME_TO_SPAWN = 60
CANNON_MINION_SPAWN_FREQUENCY = 3
MINION_HEALTH_DENIAL_PERCENT = 0
UPGRADE_MINION_TIMER = 150
EXP_GIVEN_RADIUS = 1250
GOLD_GIVEN_RADIUS = 0
DISABLE_MINION_SPAWN_BASE_TIME = 300
DISABLE_MINION_SPAWN_MAG_TIME = 0
LAST_WAVE = -1
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
a.ArmorUpgrade = 2
a.MagicResistance = 0
a.MagicResistanceUpgrade = 0.5
a.HPBonus = 0
a.HPUpgrade = 20
a.HPInhibitor = 100
a.DamageBonus = 0
a.DamageUpgrade = 1
a.DamageInhibitor = 10
a.ExpGiven = 64
a.ExpBonus = 0
a.ExpUpgrade = 5
a.ExpInhibitor = 0
a.GoldGiven = 26
a.GoldBonus = 0
a.GoldUpgrade = 0.5
a.GoldInhibitor = 0
a.GoldShared = 0
a.GoldShareUpgrade = 0
a.GoldMaximumBonus = 10
a.LocalGoldGiven = 0
a.LocalGoldBonus = 0
MeleeDefaultMinionInfo = a
a = {}
a.DefaultNumPerWave = 3
a.Armor = 0
a.ArmorUpgrade = 0.5
a.MagicResistance = 0
a.MagicResistanceUpgrade = 2
a.HPBonus = 0
a.HPUpgrade = 15
a.HPInhibitor = 60
a.DamageBonus = 0
a.DamageUpgrade = 2
a.DamageInhibitor = 18
a.ExpGiven = 32
a.ExpBonus = 0
a.ExpUpgrade = 3
a.ExpInhibitor = 0
a.GoldGiven = 19
a.GoldBonus = 0
a.GoldUpgrade = 0.5
a.GoldInhibitor = 0
a.GoldShared = 0
a.GoldShareUpgrade = 0
a.GoldMaximumBonus = 10
a.LocalGoldGiven = 0
a.LocalGoldBonus = 0
CasterDefaultMinionInfo = a
a = {}
a.DefaultNumPerWave = 0
a.Armor = 0
a.ArmorUpgrade = 2
a.MagicResistance = 0
a.MagicResistanceUpgrade = 2
a.HPBonus = 0
a.HPUpgrade = 27
a.HPInhibitor = 200
a.DamageBonus = 0
a.DamageUpgrade = 3
a.DamageInhibitor = 25
a.ExpGiven = 100
a.ExpBonus = 0
a.ExpUpgrade = 7
a.ExpInhibitor = 0
a.GoldGiven = 30
a.GoldBonus = 0
a.GoldUpgrade = 1
a.GoldInhibitor = 0
a.GoldShared = 0
a.GoldShareUpgrade = 0
a.GoldMaximumBonus = 20
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
a.HPUpgrade = 200
a.HPInhibitor = 0
a.DamageBonus = 0
a.DamageUpgrade = 10
a.DamageInhibitor = 0
a.ExpGiven = 100
a.ExpBonus = 0
a.ExpUpgrade = 0
a.ExpInhibitor = 0
a.GoldGiven = 30
a.GoldBonus = 0
a.GoldUpgrade = 1
a.GoldInhibitor = 0
a.GoldShared = 0
a.GoldShareUpgrade = 0
a.GoldMaximumBonus = 20
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
AppendTable = function(g, h, i, j)
    local k, l, m, n, o, p, q, r, s, t, u, v
    if h ~= nil then
        if type(h) == "table" then
            if g == nil then
                g = {}
            end
            m, n, o = pairs(h)
            for p, q in m, n, o do
                if i == true and type(q) == "table" then
                    g[p] = AppendTable(g[p], q, j, j)
                else
                    g[p] = q
                end
            end
        end
    end
    return g
end
InitBarrackInfo = function(g)
    local w, x, y, k, l, m, n, o, p, q, r, s, t, u
    g = AppendTable(g, g.DefaultInfo, true, true)
    g.MinionInfoTable = AppendTable(g.MinionInfoTable, g.DefaultMinionInfoTable, true, false)
    if g.MinionInfoTable ~= nil then
        w = g.MinionInfoTable
        k, l, m = pairs(w)
        for n, o in k, l, m do
            p = w[n]
            if p.DefaultInfo ~= nil then
                AppendTable(p, p.DefaultInfo, true, true)
                p.NumPerWave = p.DefaultNumPerWave
                p.GoldGivenBase = p.GoldGiven
                p.ExpGivenBase = p.ExpGiven
            end
        end
    end
    if g.Overrides ~= nil then
        y, k, l = pairs(g.Overrides)
        for m, n in y, k, l do
            if type(g[m]) == "table" then
                if type(n) == "table" then
                    AppendTable(g[m], n, true, true)
                end
            else
                if type(n) ~= "table" then
                    g[m] = n
                end
            end
        end
    end
    return g
end
a = {}
d, e, f = InitBarrackInfo(OrderBarrack2)
a[1] = InitBarrackInfo(OrderBarrack0)
a[2] = InitBarrackInfo(OrderBarrack1)
a[3] = d
a[4] = e
a[5] = f
OrderBarracksBonuses = a
a = {}
d, e, f = InitBarrackInfo(ChaosBarrack2)
a[1] = InitBarrackInfo(ChaosBarrack0)
a[2] = InitBarrackInfo(ChaosBarrack1)
a[3] = d
a[4] = e
a[5] = f
ChaosBarracksBonuses = a
a = {}
a.Turret4 = true
a.Turret3 = true
a.Turret2 = true
a.Turret1 = true
a.Barracks = true
a.HQ = true
OrderBuildingStatus = a
a = {}
a.Turret4 = true
a.Turret3 = true
a.Turret2 = true
a.Turret1 = true
a.Barracks = true
a.HQ = true
ChaosBuildingStatus = a
TotalNumberOfMinions = 0
PreloadBarrackCharacters = function(g)
    local w, x, y, k, l, m, n, o, p
    y, k, l = pairs(g.DefaultMinionInfoTable)
    for m, n in y, k, l do
        if n.PreloadedCharacter == nil then
            n.PreloadedCharacter = 1
            PreloadCharacter(n.MinionName)
        end
    end
end
OnLevelInit = function()
    local z, w, x, y, k, l, m, n, o, p, q, r, s, t, u, v, A, B, C, D, E, F, G, H
    PreloadSpell("ExpirationTimer")
    PreloadSpell("SpellShieldMarker")
    PreloadCharacter("TestCubeRender")
    y, k, l = pairs(OrderBarracksBonuses)
    for m, n in y, k, l do
        PreloadBarrackCharacters(n)
    end
    y, k, l = pairs(ChaosBarracksBonuses)
    for m, n in y, k, l do
        PreloadBarrackCharacters(n)
    end
    SpawnTable.WaveSpawnRate = 30000
    SpawnTable.SingleMinionSpawnDelay = 800
    y = GetGameMode()
    if y == "FIRSTBLOOD" then
        k = {}
        k.ExpGiven = 64
        k.GoldGiven = 19.75
        k.GoldUpgrade = 0.25
        k.HPUpgrade = 10
        k.DamageUpgrade = 0.5
        k.ExpUpgrade = 2
        k.ArmorUpgrade = 1
        k.MagicResistanceUpgrade = 0.625
        k.GoldMaximumBonus = 12
        l = {}
        l.ExpGiven = 32
        l.GoldGiven = 14.75
        l.GoldUpgrade = 0.25
        l.HPUpgrade = 7.5
        l.DamageUpgrade = 1
        l.ExpUpgrade = 1.2
        l.ArmorUpgrade = 0.625
        l.MagicResistanceUpgrade = 1
        l.GoldMaximumBonus = 8
        m = {}
        m.ExpGiven = 100
        m.GoldGiven = 39.5
        m.GoldUpgrade = 0.5
        m.HPUpgrade = 10
        m.DamageUpgrade = 1.5
        m.ExpUpgrade = 3
        m.ArmorUpgrade = 1.5
        m.MagicResistanceUpgrade = 1.5
        m.GoldMaximumBonus = 30
        n = {}
        n.ExpGiven = 100
        n.GoldGiven = 27
        n.GoldUpgrade = 0
        n.HPUpgrade = 100
        n.DamageUpgrade = 5
        n.ExpUpgrade = 0
        n.ArmorUpgrade = 0
        n.MagicResistanceUpgrade = 0
        n.GoldMaximumBonus = 30
        o = {}
        o.Melee = k
        o.Caster = l
        o.Cannon = m
        o.Super = n
        p, q, r = pairs(o)
        for s, t in p, q, r do
            C, D, E = pairs(t)
            for F, G in C, D, E do
                OrderBarracksBonuses[1].MinionInfoTable[s][F] = G
                ChaosBarracksBonuses[1].MinionInfoTable[s][F] = G
            end
        end
        INITIAL_TIME_TO_SPAWN = 45
        UPGRADE_MINION_TIMER = 90
    end
    if y == "TUTORIAL" then
        PreloadCharacter("OrderTurretTutorial")
        PreloadCharacter("ChaosTurretTutorial")
        PreloadCharacter("Tutorial_Red_Minion_Wizard")
        PreloadCharacter("Dragon")
        PreloadCharacter("HA_AP_Poro")
        PreloadCharacter("HA_AP_PoroSpawner")
        PreloadSpell("Waypoint")
        PreloadSpell("DisableHPRegen")
        PreloadSpell("Flee")
        PreloadSpell("HowlingAbyssPoroAcceleration")
        PreloadSpell("HowlingAbyssPoroAction")
        PreloadSpell("HowlingAbyssPoroCooldown")
        PreloadSpell("HowlingAbyssPoroAnimation1")
        PreloadSpell("TutorialPhase2")
        PreloadSpell("TutorialPhase3")
        PreloadSpell("TutorialPhase4")
        PreloadSpell("TutorialMinionBuff")
        PreloadSpell("TutorialNearSight")
        PreloadSpell("TutorialPlayerBuff")
        PreloadSpell("TutorialPlayerBuff2")
        PreloadSpell("TutorialShoppingTime")
        PreloadSpell("TutorialShoppingTime2")
        INITIAL_TIME_TO_SPAWN = -90000000
        SpawnTable.NumOfMeleeMinionsPerWave = 0
        SpawnTable.NumOfCasterMinionsPerWave = 0
    else
        PreloadCharacter("HA_AP_BannerMidBridge")
        PreloadCharacter("HA_AP_BridgeLaneStatue")
        PreloadCharacter("HA_AP_Chains")
        PreloadCharacter("HA_AP_Chains_Long")
        PreloadCharacter("HA_AP_ChaosTurret")
        PreloadCharacter("HA_AP_ChaosTurret2")
        PreloadCharacter("HA_AP_ChaosTurret3")
        PreloadCharacter("HA_AP_ChaosTurretShrine")
        PreloadCharacter("HA_AP_Cutaway")
        PreloadCharacter("HA_AP_Hermit")
        PreloadCharacter("HA_AP_HeroTower")
        PreloadCharacter("HA_AP_OrderTurret")
        PreloadCharacter("HA_AP_OrderTurret2")
        PreloadCharacter("HA_AP_OrderTurret3")
        PreloadCharacter("HA_AP_Poro")
        PreloadCharacter("HA_AP_PoroSpawner")
        PreloadCharacter("HA_AP_ShpSouth")
        PreloadCharacter("HA_AP_Viking")
        PreloadSpell("HowlingAbyssAura")
        PreloadSpell("HowlingAbyssDamageTracker")
        PreloadSpell("HowlingAbyssDamageTrackerB")
        PreloadSpell("HowlingAbyssPoroAcceleration")
        PreloadSpell("HowlingAbyssPoroAction")
        PreloadSpell("HowlingAbyssPoroAnimation1")
        PreloadSpell("HowlingAbyssPoroCooldown")
        PreloadSpell("HowlingAbyssTurretHighWind")
        if y == "FIRSTBLOOD" then
            PreloadCharacter("HA_FB_HealthRelic")
            PreloadSpell("HowlingAbyssRelicAuraFB")
            PreloadSpell("HowlingAbyssFBHeal")
            PreloadSpell("HowlingAbyssFBDefeat")
            PreloadSpell("HowlingAbyssFBVictory")
        end
        PreloadCharacter("SpellBook1")
        PreloadCharacter("TestCube")
        SpawnTable.NumOfMeleeMinionsPerWave = 3
        SpawnTable.NumOfCasterMinionsPerWave = 3
        LoadLevelScriptIntoScript("NeutralMinionSpawn.lua")
        NeutralMinionInit()
    end
    PreloadSpell("RespawnClassic")
    l, m, n, o, p, q, r, s, t, u, v, A, B, C, D, E, F, G, H = os.time()
    math.randomseed(l, m, n, o, p, q, r, s, t, u, v, A, B, C, D, E, F, G, H)
end
OnLevelInitServer = function()
    local z, w, x, y, k
    z = GetGameMode()
    if z == "TUTORIAL" then
        InitTimer("PlaceTutorialSpawnRunes", 1, false)
    else
        InitTimer("UpgradeMinionTimer", UPGRADE_MINION_TIMER, true)
        InitTimer("AllowDamageOnBuildings", 10, false)
        if z == "ARAM" then
            InitTimer("ApplyAramBuffs", 1, false)
        end
    end
end
ApplyAramBuffs = function()
    local z, w, x
    ApplyPersistentBuffToAllChampions("HowlingAbyssAura", true)
    ApplyPersistentBuffToAllChampions("AramShopDisableNoParticle", false)
end
OnPostLevelLoad = function()
    local z, w
    LoadLevelScriptIntoScript("CreateLevelProps.lua")
    if GetGameMode() == "TUTORIAL" then
        CreateLevelPropsTutorial()
    else
        CreateLevelProps()
    end
end
OppositeTeam = function(g)
    local w
    if g == TEAM_CHAOS then
        return TEAM_ORDER
    else
        return TEAM_CHAOS
    end
end
AllowDamageOnBuildings = function()
    local z, w, x, y, k, l, m, n, o, p, q
    z = CENTER_LANE
    for k = BACK_TOWER2, FRONT_TOWER, 1 do
        l = GetTurret(TEAM_ORDER, z, k)
        if l ~= nil then
            if k == FRONT_TOWER then
                SetInvulnerable(l, false)
                SetTargetable(l, true)
            else
                SetInvulnerable(l, true)
                SetNotTargetableToTeam(l, true, TEAM_CHAOS)
            end
        else
        end
        m = GetTurret(TEAM_CHAOS, z, k)
        if m ~= nil then
            if k == FRONT_TOWER then
                SetInvulnerable(m, false)
                SetTargetable(m, true)
            else
                SetInvulnerable(m, true)
                SetNotTargetableToTeam(m, true, TEAM_ORDER)
            end
        else
        end
    end
end
ResetToDefaultWaveCounts = function(g)
    local w, x, y, k, l, m, n, o
    y, k, l = pairs(g)
    for m, n in y, k, l do
        n.NumPerWave = n.DefaultNumPerWave
    end
end
ClearCurrentWaveCounts = function(g)
    local w, x, y, k, l, m, n, o
    y, k, l = pairs(g)
    for m, n in y, k, l do
        n.NumPerWave = 0
    end
end
GetInitSpawnInfo = function(g, h)
    local x, y, k
    x = 0
    if h == TEAM_ORDER then
        x = OrderBarracksBonuses[g + 1]
    else
        x = ChaosBarracksBonuses[g + 1]
    end
    y = {}
    y.WaveSpawnRate = SpawnTable.WaveSpawnRate
    y.SingleMinionSpawnDelay = SpawnTable.SingleMinionSpawnDelay
    y.IsDestroyed = x.IsDestroyed
    y.MinionInfoTable = x.MinionInfoTable
    ReturnTable = y
    return ReturnTable
end
GetMinionSpawnInfo = function(g, h, i, j, I)
    local l, m, n, o, p
    l = 0
    if j == TEAM_ORDER then
        l = OrderBarracksBonuses[1]
    else
        l = ChaosBarracksBonuses[1]
    end
    n = l.MinionInfoTable.Cannon
    ResetToDefaultWaveCounts(l.MinionInfoTable)
    if h % CANNON_MINION_SPAWN_FREQUENCY == 0 then
        n.NumPerWave = n.NumPerWave + 1
    end
    if I ~= LAST_WAVE then
        BARRACKSCOUNT = 6
        totalMinionsRemaining = MAX_MINIONS_EVER - GetTotalTeamMinionsSpawned()
        LAST_WAVE = I
    end
    if l.WillSpawnSuperMinion == 1 then
        l.MinionInfoTable.Super.NumPerWave = 1
        n.NumPerWave = 0
    end
    o = {}
    o.NewFormat = true
    o.SpawnOrderMinionNames = l.SpawnOrderMinionNames
    o.IsDestroyed = l.IsDestroyed
    o.ExperienceRadius = SpawnTable.ExpRadius
    o.GoldRadius = SpawnTable.GoldRadius
    o.MinionInfoTable = l.MinionInfoTable
    ReturnTable = o
    return ReturnTable
end
UpgradeMinionTimer = function()
    local z, w, x, y, k, l, m, n, o, p, q, r, s, t, u, v, A, B, C
    for k = 1, 2, 1 do
        if k == 1 then
            z = OrderBarracksBonuses
        else
            z = ChaosBarracksBonuses
        end
        for o = 1, 3, 1 do
            s, t, u = pairs(z[o].MinionInfoTable)
            for v, A in s, t, u do
                A.HPBonus = A.HPBonus + A.HPUpgrade
                A.DamageBonus = A.DamageBonus + A.DamageUpgrade
                A.GoldBonus = A.GoldBonus + A.GoldUpgrade
                if A.GoldBonus > A.GoldMaximumBonus then
                    A.GoldBonus = A.GoldMaximumBonus
                end
                A.LocalGoldBonus = A.LocalGoldBonus + A.GoldShareUpgrade
                A.Armor = A.Armor + A.ArmorUpgrade
                A.MagicResistance = A.MagicResistance + A.MagicResistanceUpgrade
                A.ExpBonus = A.ExpBonus + A.ExpUpgrade
                A.GoldGiven = A.GoldBonus + A.GoldGivenBase
                A.LocalGoldGiven = A.LocalGoldBonus + A.GoldShared
                A.ExpGiven = A.ExpBonus + A.ExpGivenBase
            end
        end
    end
end
DeactivateCorrectStructure = function(g, h, i)
    local y, k, l, m, n
    if GetGameMode() ~= "TUTORIAL" then
        if g == TEAM_ORDER then
            y = OrderBuildingStatus
        else
            y = ChaosBuildingStatus
        end
        if i == FRONT_TOWER then
            y.Turret4 = false
            k = GetTurret(g, h, MID_TOWER)
            SetInvulnerable(k, false)
            SetTargetable(k, true)
        else
            if i == MID_TOWER then
                y.Turret3 = false
                k = GetDampener(g, h)
                SetInvulnerable(k, false)
                SetTargetable(k, true)
            else
                if i ~= BACK_TOWER then
                    if i ~= BACK_TOWER2 then
                        return
                    end
                end
                if i == BACK_TOWER then
                    y.Turret2 = false
                else
                    y.Turret1 = false
                end
                if y.Turret1 == false then
                    if y.Turret2 == false then
                        k = GetHQ(g)
                        SetInvulnerable(k, false)
                        SetTargetable(k, true)
                    end
                end
            end
        end
    end
end
GetLuaBarracks = function(g, h)
    local x, y, k
    if g == TEAM_ORDER then
        x = OrderBarracksBonuses[h + 1]
    else
        x = ChaosBarracksBonuses[h + 1]
    end
    return x
end
GetDisableMinionSpawnTime = function(g, h)
    local x, y, k
    barrack = GetLuaBarracks(h, g)
    return DISABLE_MINION_SPAWN_BASE_TIME + DISABLE_MINION_SPAWN_MAG_TIME * barrack.NumOfSpawnDisables
end
DisableBarracksSpawn = function(g, h)
    local x, y, k, l, m
    cLangBarracks = GetBarracks(h, g)
    luaBarrack = GetLuaBarracks(h, g)
    k, l, m = GetDisableMinionSpawnTime(g, h)
    SetDisableMinionSpawn(cLangBarracks, k, l, m)
    luaBarrack.NumOfSpawnDisables = luaBarrack.NumOfSpawnDisables + 1
end
BonusesCounter = 0
ApplyBarracksDestructionBonuses = function(g, h)
    local x, y, k, l, m, n, o, p, q, r, s, t
    BonusesCounter = BonusesCounter + 1
    x = 1
    y = nil
    if g == TEAM_ORDER then
        y = OrderBarracksBonuses
    else
        y = ChaosBarracksBonuses
    end
    n, o, p = pairs(y[x].MinionInfoTable)
    for q, r in n, o, p do
        r.HPBonus = r.HPBonus + r.HPInhibitor
        r.DamageBonus = r.DamageBonus + r.DamageInhibitor
        r.ExpGiven = r.ExpGiven - r.ExpInhibitor
        r.GoldGiven = r.GoldGiven - r.GoldInhibitor
    end
    y[x].WillSpawnSuperMinion = 1
end
ApplyBarracksRespawnReductions = function(g, h)
    local x, y, k, l, m, n, o, p, q, r, s, t
    Log("Inhibitor respawn, barrack ID is: " .. h)
    x = CENTER_LANE
    y = nil
    if g == TEAM_ORDER then
        y = OrderBarracksBonuses
    else
        y = ChaosBarracksBonuses
    end
    n, o, p = pairs(y[x].MinionInfoTable)
    for q, r in n, o, p do
        r.HPBonus = r.HPBonus - r.HPInhibitor
        r.DamageBonus = r.DamageBonus - r.DamageInhibitor
        r.ExpGiven = r.ExpGiven + r.ExpInhibitor
        r.GoldGiven = r.GoldGiven + r.GoldInhibitor
    end
    y[x].WillSpawnSuperMinion = 0
    if g == TEAM_ORDER then
        n = GetHQ(TEAM_CHAOS)
        SetInvulnerable(n, true)
        SetTargetable(n, false)
        o = GetTurret(TEAM_CHAOS, x, BACK_TOWER)
        p = GetTurret(TEAM_CHAOS, x, BACK_TOWER2)
        if o ~= nil then
            SetInvulnerable(o, true)
            SetNotTargetableToTeam(o, true, g)
        end
        if p ~= nil then
            SetInvulnerable(p, true)
            SetNotTargetableToTeam(p, true, g)
        end
    else
        if g == TEAM_CHAOS then
            n = GetHQ(TEAM_ORDER)
            SetInvulnerable(n, true)
            SetTargetable(n, false)
            o = GetTurret(TEAM_ORDER, x, BACK_TOWER)
            p = GetTurret(TEAM_ORDER, x, BACK_TOWER2)
            if o ~= nil then
                SetInvulnerable(o, true)
                SetNotTargetableToTeam(o, true, g)
            end
            if p ~= nil then
                SetInvulnerable(p, true)
                SetNotTargetableToTeam(p, true, g)
            end
        end
    end
end
BarrackReactiveEvent = function(g, h)
    local x, y, k, l
    dampener = GetDampener(g, h)
    SetInvulnerable(dampener, false)
    SetTargetable(dampener, true)
    ApplyBarracksRespawnReductions(OppositeTeam(g), h)
end
HandleDestroyedObject = function(g)
    local w, x, y, k, l, m, n
    HQType = GetHQType(g)
    if HQType == ORDER_HQ or HQType == CHAOS_HQ then
        if HQType == CHAOS_HQ then
            EndOfGameCeremony(TEAM_ORDER, g)
        else
            EndOfGameCeremony(TEAM_CHAOS, g)
        end
        return
    end
    if IsDampener(g) then
        barrack = GetLinkedBarrack(g)
        barrackTeam = GetTeamID(barrack)
        barrackLane = GetLane(g)
        DisableBarracksSpawn(barrackLane, barrackTeam)
        SetDampenerState(g, DampenerRegenerationState)
        SetInvulnerable(g, true)
        SetTargetable(g, false)
        if GetGameMode() == "TUTORIAL" then
            w = GetHQ(Team)
            SetInvulnerable(w, false)
            SetTargetable(w, true)
        else
            w = GetTurret(barrackTeam, barrackLane, BACK_TOWER)
            x = GetTurret(barrackTeam, barrackLane, BACK_TOWER2)
            if w ~= nil then
                SetInvulnerable(w, false)
                SetTargetable(w, true)
            end
            if x ~= nil then
                SetInvulnerable(x, false)
                SetTargetable(x, true)
            end
            if w == nil then
                if x == nil then
                    y = GetHQ(barrackTeam)
                    SetInvulnerable(y, false)
                    SetTargetable(y, true)
                end
            end
        end
        w = nil
        if barrackTeam == TEAM_CHAOS then
            w = TEAM_ORDER
        else
            w = TEAM_CHAOS
        end
        ApplyBarracksDestructionBonuses(w, barrackLane)
    end
    if IsTurretAI(g) then
        DeactivateCorrectStructure(GetTeamID(g), GetObjectLaneId(g), GetTurretPosition(g))
        return
    end
    w = GetDampenerType(g)
    if -1 < w then
        l = w % TEAM_CHAOS
        if l == CENTER_LANE then
            ChaosBuildingStatus.Barracks = false
        else
            OrderBuildingStatus.Barracks = false
        end
    else
        Log("Could not find Linking barracks!")
    end
    return true
end
TEAM_UNKNOWN = 0
EOG_PAN_TO_NEXUS_TIME = 3
EOG_NEXUS_EXPLOSION_TIME = EOG_PAN_TO_NEXUS_TIME + 0.5
EOG_SCOREBOARD_PHASE_DELAY_TIME = 5
EOG_NEXUS_REVIVE_TIME = 10
EOG_ALIVE_NEXUS_SKIN = 0
EOG_DESTROYED_NEXUS_SKIN = 1
EOG_MINION_FADE_AMOUNT = 0
EOG_MINION_FADE_TIME = 2
EndOfGameCeremony = function(g, h)
    local x, y, k, l
    orderHQ = GetHQ(TEAM_ORDER)
    SetInvulnerable(orderHQ, true)
    SetTargetable(orderHQ, false)
    SetBuildingHealthRegenEnabled(orderHQ, false)
    chaosHQ = GetHQ(TEAM_CHAOS)
    SetInvulnerable(chaosHQ, true)
    SetTargetable(chaosHQ, false)
    SetBuildingHealthRegenEnabled(chaosHQ, false)
    winningTeam = g
    if winningTeam == TEAM_ORDER then
        losingTeam = TEAM_CHAOS
        losingHQPosition = GetPosition(chaosHQ)
        winningHQPosition = GetPosition(orderHQ)
    else
        losingTeam = TEAM_ORDER
        losingHQPosition = GetPosition(orderHQ)
        winningHQPosition = GetPosition(chaosHQ)
    end
    SetInputLockFlag(INPUT_LOCK_CAMERA_LOCKING, true)
    SetInputLockFlag(INPUT_LOCK_CAMERA_MOVEMENT, true)
    SetInputLockFlag(INPUT_LOCK_ABILITIES, true)
    SetInputLockFlag(INPUT_LOCK_SUMMONER_SPELLS, true)
    SetInputLockFlag(INPUT_LOCK_MOVEMENT, true)
    SetInputLockFlag(INPUT_LOCK_SHOP, true)
    SetInputLockFlag(INPUT_LOCK_MINIMAP_MOVEMENT, true)
    DisableHUDForEndOfGame()
    SetBarracksSpawnEnabled(false)
    CloseShop()
    HaltAllAI()
    LuaForEachChampion(TEAM_UNKNOWN, "ChampionEoGCeremony")
    Log("Destroy Nexus Called")
    InitTimer("DestroyNexusPhase", EOG_NEXUS_EXPLOSION_TIME, false)
end
ChampionEoGCeremony = function(g)
    local w, x, y, k, l
    MoveCameraFromCurrentPositionToPoint(g, losingHQPosition, EOG_PAN_TO_NEXUS_TIME, true)
    SetGreyscaleEnabledWhenDead(g, false)
end
DestroyNexusPhase = function()
    local z, w, x, y
    SetHQCurrentSkin(losingTeam, EOG_DESTROYED_NEXUS_SKIN)
    FadeMinions(losingTeam, EOG_MINION_FADE_AMOUNT, EOG_MINION_FADE_TIME)
    InitTimer("StopRenderingMinionsPhase", EOG_MINION_FADE_TIME, false)
    InitTimer("ScoreboardPhase", EOG_SCOREBOARD_PHASE_DELAY_TIME, false)
    Log("Destroy Nexus Reached")
end
StopRenderingMinionsPhase = function()
    local z, w, x
    SetMinionsNoRender(losingTeam, true)
end
ScoreboardPhase = function()
    local z, w, x
    Log("Scoreboard reached.")
    SetInputLockFlag(INPUT_LOCK_CHAT, true)
    EndGame(winningTeam)
end
EOG_EASTER_EGG_PAN_TO_NEXUS_DELAY = 20
EOG_EASTER_EGG_CAMERA_PATH_TIME = 154
EOG_EASTER_EGG_MUSIC_FADE_TIME = 4
PostGameSetup = function(g)
    local w, x, y, k, l, m, n, o, p, q, r
    if GetGameMode() == "TUTORIAL" then
        return
    end
    k = {}
    n, o, p, q, r = Make3DPoint(2500, 0, 2000)
    k[1] = Make3DPoint(5700, 0, 6900)
    k[2] = Make3DPoint(2500, 0, 2000)
    k[3] = n
    k[4] = o
    k[5] = p
    k[6] = q
    k[7] = r
    l = {}
    o, p, q, r = Make3DPoint(10700, 0, 10700)
    l[1] = Make3DPoint(5700, 0, 6900)
    l[2] = Make3DPoint(10700, 0, 10700)
    l[3] = o
    l[4] = p
    l[5] = q
    l[6] = r
    n = k
    if g == TEAM_CHAOS then
        n = l
    end
    o = {}
    p = {}
    p.delay = EOG_EASTER_EGG_PAN_TO_NEXUS_DELAY
    o[1] = p
    p = {}
    p.delay = EOG_EASTER_EGG_PAN_TO_NEXUS_DELAY + EOG_EASTER_EGG_MUSIC_FADE_TIME
    p.soundCharacterName = "Lissandra"
    p.soundName = "freljordlore"
    p.cameraPath = n
    p.travelTime = EOG_EASTER_EGG_CAMERA_PATH_TIME
    o[2] = p
    POST_GAME_EVENTS = o
end
PostGameUpdate = function(g, h, i)
    local y, k, l, m, n, o, p, q
    if h ~= i then
        return
    end
    y, k, l = pairs(POST_GAME_EVENTS)
    for m, n in y, k, l do
        if g > n.delay then
            if n.cameraLocation then
                ClientSide_CameraMoveCameraFromCurrentPositionToPoint(n.cameraLocation, n.travelTime)
            end
            if n.cameraPath then
                ClientSide_CameraMoveCameraFromCurrentPositionAlongSpline(n.cameraPath, n.travelTime)
            end
            if n.soundName then
                if n.soundCharacterName then
                    ClientSide_PlayGenericVOSoundFile(n.soundCharacterName, n.soundName)
                else
                    ClientSide_PlaySoundFile(n.soundName)
                end
            end
            table.remove(POST_GAME_EVENTS, m)
            break
        end
    end
end
