local a, b, c, d, e, f, g, h, i, j, k, l, m, n
MAX_MINIONS_EVER = 180
TEAM_UNKNOWN = 0
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
INCREASE_CANNON_RATE_TIMER = 1200
INCREASE_CANNON_RATE_TIMER2 = 2100
MINION_HEALTH_DENIAL_PERCENT = 0
UPGRADE_MINION_TIMER = 90
EXP_GIVEN_RADIUS = 1400
GOLD_GIVEN_RADIUS = 1250
DISABLE_MINION_SPAWN_BASE_TIME = 300
DISABLE_MINION_SPAWN_MAG_TIME = 0
CHAOS_INHIBITOR_RESPAWN_ANIMATION_DURATION = 11.67
ORDER_INHIBITOR_RESPAWN_ANIMATION_DURATION = 11
LAST_WAVE = -1
SPECIAL_MINION_MODE = "none"
HQTurretAttackable = false
DoLuaLevel("BBLuaConversionLibrary")
DoLuaLevel("LaneSigils")
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
a.HPUpgrade = 15
a.HPUpgradeGrowth = 0.2
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
a.HPUpgrade = 11
a.HPUpgradeGrowth = 0.2
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
a.HPUpgrade = 23
a.HPUpgradeGrowth = 0.3
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
a.HPUpgradeGrowth = 0
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
a.DefaultNumPerWave = 0
a.Armor = 0
a.ArmorUpgrade = 0
a.MagicResistance = 0
a.MagicResistanceUpgrade = 0
a.HPBonus = 0
a.HPUpgrade = 100
a.HPUpgradeGrowth = 0
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
TestDefaultMinionInfo = a
a = {}
a.MinionName = "SRU_OrderMinionMelee"
a.DefaultInfo = MeleeDefaultMinionInfo
OrderMeleeMinionInfo = a
a = {}
a.MinionName = "SRU_ChaosMinionMelee"
a.DefaultInfo = MeleeDefaultMinionInfo
ChaosMeleeMinionInfo = a
a = {}
a.MinionName = "SRU_OrderMinionRanged"
a.DefaultInfo = CasterDefaultMinionInfo
OrderCasterMinionInfo = a
a = {}
a.MinionName = "SRU_ChaosMinionRanged"
a.DefaultInfo = CasterDefaultMinionInfo
ChaosCasterMinionInfo = a
a = {}
a.MinionName = "SRU_OrderMinionSiege"
a.DefaultInfo = CannonDefaultMinionInfo
OrderCannonMinionInfo = a
a = {}
a.MinionName = "SRU_ChaosMinionSiege"
a.DefaultInfo = CannonDefaultMinionInfo
ChaosCannonMinionInfo = a
a = {}
a.MinionName = "SRU_OrderMinionSuper"
a.DefaultInfo = SuperDefaultMinionInfo
OrderSuperMinionInfo = a
a = {}
a.MinionName = "SRU_ChaosMinionSuper"
a.DefaultInfo = SuperDefaultMinionInfo
ChaosSuperMinionInfo = a
a = {}
a.MinionName = "Blue_Minion_Basic"
a.DefaultInfo = TestDefaultMinionInfo
OrderTestMinionInfo = a
a = {}
a.MinionName = "Red_Minion_Basic"
a.DefaultInfo = TestDefaultMinionInfo
ChaosTestMinionInfo = a
a = {}
a.IsDestroyed = false
a.NumOfSpawnDisables = 0
a.WillSpawnSuperMinion = 0
b = {}
b[1] = "Super"
b[2] = "Melee"
b[3] = "Cannon"
b[4] = "Caster"
b[5] = "Test"
a.SpawnOrderMinionNames = b
DefaultBarrackInfo = a
a = {}
a.Super = OrderSuperMinionInfo
a.Melee = OrderMeleeMinionInfo
a.Test = OrderTestMinionInfo
a.Caster = OrderCasterMinionInfo
a.Cannon = OrderCannonMinionInfo
DefaultOrderMinionInfoTable = a
a = {}
a.Super = ChaosSuperMinionInfo
a.Melee = ChaosMeleeMinionInfo
a.Test = ChaosTestMinionInfo
a.Caster = ChaosCasterMinionInfo
a.Cannon = ChaosCannonMinionInfo
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
AppendTable = function(o, p, q, r)
    local s, t, u, v, w, x, y, z, A, B, C, D
    if p ~= nil then
        if type(p) == "table" then
            if o == nil then
                o = {}
            end
            u, v, w = pairs(p)
            for x, y in u, v, w do
                if q == true and type(y) == "table" then
                    o[x] = AppendTable(o[x], y, r, r)
                else
                    o[x] = y
                end
            end
        end
    end
    return o
end
InitBarrackInfo = function(o)
    local E, F, G, s, t, u, v, w, x, y, z, A, B, C
    o = AppendTable(o, o.DefaultInfo, true, true)
    o.MinionInfoTable = AppendTable(o.MinionInfoTable, o.DefaultMinionInfoTable, true, false)
    if o.MinionInfoTable ~= nil then
        E = o.MinionInfoTable
        s, t, u = pairs(E)
        for v, w in s, t, u do
            x = E[v]
            if x.DefaultInfo ~= nil then
                AppendTable(x, x.DefaultInfo, true, true)
                x.NumPerWave = x.DefaultNumPerWave
                x.GoldGivenBase = x.GoldGiven
                x.ExpGivenBase = x.ExpGiven
            end
        end
    end
    if o.Overrides ~= nil then
        G, s, t = pairs(o.Overrides)
        for u, v in G, s, t do
            if type(o[u]) == "table" then
                if type(v) == "table" then
                    AppendTable(o[u], v, true, true)
                end
            else
                if type(v) ~= "table" then
                    o[u] = v
                end
            end
        end
    end
    return o
end
a = {}
d, e, f, g, h, i, j, k, l, m, n = InitBarrackInfo(OrderBarrack2)
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
a[13] = n
OrderBarracksBonuses = a
a = {}
d, e, f, g, h, i, j, k, l, m, n = InitBarrackInfo(ChaosBarrack2)
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
a[13] = n
ChaosBarracksBonuses = a
a = {}
a[1] = "SRU_Dragon"
a[2] = "SRU_Krug"
a[3] = "SRU_KrugMini"
a[4] = "SRU_Red"
a[5] = "SRU_RedMini"
a[6] = "SRU_Razorbeak"
a[7] = "SRU_RazorbeakMini"
a[8] = "SRU_Murkwolf"
a[9] = "SRU_MurkwolfMini"
a[10] = "SRU_Blue"
a[11] = "SRU_BlueMini"
a[12] = "SRU_Gromp"
a[13] = "SRU_Baron"
NeutralMinionNames = a
CreateLaneBuildingTable = function()
    local H, E
    H = {}
    H.Turret3 = true
    H.Turret2 = true
    H.Turret1 = true
    H.Barracks = true
    return H
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
PreloadBarrackCharacters = function(o)
    local E, F, G, s, t, u, v, w, x
    G, s, t = pairs(o.DefaultMinionInfoTable)
    for u, v in G, s, t do
        if v.PreloadedCharacter == nil then
            v.PreloadedCharacter = 1
            PreloadCharacter(v.MinionName)
        end
    end
end
OnLevelInit = function()
    local H, E, F, G, s, t, u, v, w
    F, G, s = pairs(OrderBarracksBonuses)
    for t, u in F, G, s do
        PreloadBarrackCharacters(u)
    end
    F, G, s = pairs(ChaosBarracksBonuses)
    for t, u in F, G, s do
        PreloadBarrackCharacters(u)
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
    PreloadCharacter("SRUAP_Turret_Order1")
    PreloadCharacter("SRUAP_Turret_Chaos1")
    PreloadCharacter("SRUAP_OrderInhibitor")
    PreloadCharacter("SRUAP_ChaosInhibitor")
    PreloadCharacter("SRUAP_OrderNexus")
    PreloadCharacter("SRUAP_ChaosNexus")
    PreloadCharacter("SRU_OrderMinionMelee")
    PreloadCharacter("SRU_OrderMinionRanged")
    PreloadCharacter("SRU_OrderMinionSiege")
    PreloadCharacter("SRU_OrderMinionSuper")
    PreloadCharacter("SRU_ChaosMinionMelee")
    PreloadCharacter("SRU_ChaosMinionRanged")
    PreloadCharacter("SRU_ChaosMinionSiege")
    PreloadCharacter("SRU_ChaosMinionSuper")
    PreloadCharacter("SRU_Blue")
    PreloadCharacter("SRU_BlueMini")
    PreloadCharacter("SRU_BlueMini2")
    PreloadCharacter("SRU_Red")
    PreloadCharacter("SRU_RedMini")
    PreloadCharacter("SRU_Murkwolf")
    PreloadCharacter("SRU_MurkwolfMini")
    PreloadCharacter("SRU_Razorbeak")
    PreloadCharacter("SRU_RazorbeakMini")
    PreloadCharacter("SRU_Krug")
    PreloadCharacter("SRU_KrugMini")
    PreloadCharacter("SRU_Gromp")
    PreloadCharacter("SRU_Dragon")
    PreloadCharacter("SRU_Baron")
    PreloadCharacter("SRU_BaronSpawn")
    PreloadCharacter("SRUAP_MageCrystal")
    PreloadCharacter("SRUAP_Flag")
    PreloadCharacter("SRU_Snail")
    PreloadCharacter("SRU_Dragon_prop")
    PreloadCharacter("SRU_Bird")
    PreloadCharacter("SRU_Antlermouse")
    PreloadCharacter("SRU_Gromp_prop")
    PreloadCharacter("SRU_Lizard")
    PreloadCharacter("SRU_StoreKeeperNorth")
    PreloadCharacter("SRU_StoreKeeperSouth")
    G, s, t, u, v, w = os.time()
    math.randomseed(G, s, t, u, v, w)
    LoadLevelScriptIntoScript("NeutralMinionSpawn.lua")
    NeutralMinionInit()
    LoadLevelScriptIntoScript("EndOfGame.lua")
    SpawnTable.WaveSpawnRate = 30000
    SpawnTable.SingleMinionSpawnDelay = 800
    SpawnTable.ExpRadius = EXP_GIVEN_RADIUS
    SpawnTable.GoldRadius = GOLD_GIVEN_RADIUS
end
OnLevelInitServer = function()
    local H, E, F, G
    InitTimer("UpgradeMinionTimer", UPGRADE_MINION_TIMER, true)
    InitTimer("IncreaseCannonMinionSpawnRate", INCREASE_CANNON_RATE_TIMER, false)
    InitTimer("IncreaseCannonMinionSpawnRateAgain", INCREASE_CANNON_RATE_TIMER2, false)
    InitTimer("AllowDamageOnBuildings", 10, false)
end
OnPostLevelLoad = function()
    local H, E, F, G, s, t, u, v
    LoadLevelScriptIntoScript("CreateLevelProps.lua")
    CreateLevelProps()
    for G = RIGHT_LANE, LEFT_LANE, 1 do
        SetDampenerRespawnAnimationDuration(GetDampener(TEAM_ORDER, G), ORDER_INHIBITOR_RESPAWN_ANIMATION_DURATION)
        SetDampenerRespawnAnimationDuration(GetDampener(TEAM_CHAOS, G), CHAOS_INHIBITOR_RESPAWN_ANIMATION_DURATION)
    end
    SetShieldBuffsOnSecondaryTurrets(TEAM_ORDER)
    SetShieldBuffsOnSecondaryTurrets(TEAM_CHAOS)
end
OnGameStartup = function()
    local H, E, F, G
    SpawnLaneSigilEffects()
    H = 0
    OrderHQPos = GetKeyLocation(H, TEAM_ORDER)
    ChaosHQPos = GetKeyLocation(H, TEAM_CHAOS)
end
SetShieldBuffsOnSecondaryTurrets = function(o)
    local E, F, G, s, t, u, v, w, x, y
    for s = RIGHT_LANE, LEFT_LANE, 1 do
        innerTurret = GetTurret(o, s, MIDDLE_TOWER)
        ApplyPersistentBuff(innerTurret, "SRTurretSecondaryShielder", false, 1, 1)
        AddBuffCounter(innerTurret, "SRTurretSecondaryShielder", s, 2)
    end
end
SPELLBOOK_SUMMONER = 1
ApplyJungleTracker = function(o)
    local E, F, G, s, t, u
    if GetSlotSpellName(o, 0, SPELLBOOK_SUMMONER, 0) ~= "summonersmite" then
        if GetSlotSpellName(o, 1, SPELLBOOK_SUMMONER, 0) ~= "summonersmite" then
            return
        end
    end
    ApplyPersistentBuff(o, "ItemJungleStatsTracker", true, 1, 1)
end
ApplyBaseBuffToAllChampions = function()
    local H, E, F
    LuaForEachChampion(TEAM_CHAOS, "ApplyBaseBuffChaos")
    LuaForEachChampion(TEAM_ORDER, "ApplyBaseBuffOrder")
end
ApplyBaseBuffChaos = function(o)
    local E, F, G
    ApplyBaseBuff(o, ChaosHQPos)
end
ApplyBaseBuffOrder = function(o)
    local E, F, G
    ApplyBaseBuff(o, OrderHQPos)
end
baseBuffRadiusSquared = 12250000
BUFF_Aura = 1
ApplyBaseBuff = function(o, p)
    local F, G, s, t, u, v, w, x
    F = GetPosition(o) - p
    if F.lengthSq(F) < baseBuffRadiusSquared then
        SpellBuffAdd(o, o, "S5Test_BaseBuff", BUFF_Aura, 1, 0.5, nil)
    end
end
OppositeTeam = function(o)
    local E
    if o == TEAM_CHAOS then
        return TEAM_ORDER
    else
        return TEAM_CHAOS
    end
end
UpgradeMinionTimer = function()
    local H, E, F, G, s, t, u, v, w, x, y, z, A, B, C, D, I, J, K
    for s = 1, 2, 1 do
        if s == 1 then
            H = OrderBarracksBonuses
        else
            H = ChaosBarracksBonuses
        end
        for w = 1, 3, 1 do
            A, B, C = pairs(H[w].MinionInfoTable)
            for D, I in A, B, C do
                I.HPBonus = I.HPBonus + I.HPUpgrade
                I.DamageBonus = I.DamageBonus + I.DamageUpgrade
                I.GoldBonus = I.GoldBonus + I.GoldUpgrade
                I.HPUpgrade = I.HPUpgrade + I.HPUpgradeGrowth
                if I.GoldBonus > I.GoldMaximumBonus then
                    I.GoldBonus = I.GoldMaximumBonus
                end
                I.LocalGoldBonus = I.LocalGoldBonus + I.GoldShareUpgrade
                I.Armor = I.Armor
                I.MagicResistance = I.MagicResistance
                I.ExpBonus = I.ExpBonus + I.ExpUpgrade
                I.GoldGiven = I.GoldBonus + I.GoldGivenBase
                I.LocalGoldGiven = I.LocalGoldBonus + I.GoldShared
                I.ExpGiven = I.ExpBonus + I.ExpGivenBase
            end
        end
    end
end
AllowDamageOnBuildings = function()
    local H, E, F, G, s, t, u, v, w, x, y, z
    for G = RIGHT_LANE, LEFT_LANE, 1 do
        for v = BACK_TOWER, HQ_TOWER1, 1 do
            orderTurret = GetTurret(TEAM_ORDER, G, v)
            if orderTurret ~= nil then
                if v == FRONT_TOWER then
                    SetInvulnerable(orderTurret, false)
                    SetTargetable(orderTurret, true)
                else
                    SetInvulnerable(orderTurret, true)
                    SetNotTargetableToTeam(orderTurret, true, TEAM_CHAOS)
                end
            end
            chaosTurret = GetTurret(TEAM_CHAOS, G, v)
            if chaosTurret ~= nil then
                if v == FRONT_TOWER then
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
ResetToDefaultWaveCounts = function(o)
    local E, F, G, s, t, u, v, w
    G, s, t = pairs(o)
    for u, v in G, s, t do
        v.NumPerWave = v.DefaultNumPerWave
    end
end
ClearCurrentWaveCounts = function(o)
    local E, F, G, s, t, u, v, w
    G, s, t = pairs(o)
    for u, v in G, s, t do
        v.NumPerWave = 0
    end
end
GetInitSpawnInfo = function(o, p)
    local F, G, s
    F = 0
    if p == TEAM_ORDER then
        F = OrderBarracksBonuses[o + 1]
    else
        F = ChaosBarracksBonuses[o + 1]
    end
    G = {}
    G.WaveSpawnRate = SpawnTable.WaveSpawnRate
    G.SingleMinionSpawnDelay = SpawnTable.SingleMinionSpawnDelay
    G.IsDestroyed = F.IsDestroyed
    G.MinionInfoTable = F.MinionInfoTable
    ReturnTable = G
    return ReturnTable
end
GetMinionSpawnInfo = function(o, p, q, r, L)
    local t, u, v, w, x, y, z, A, B, C, D
    t = 0
    if r == TEAM_ORDER then
        t = OrderBarracksBonuses[o + 1]
    else
        t = ChaosBarracksBonuses[o + 1]
    end
    u = t.MinionInfoTable.Super
    v = t.MinionInfoTable.Cannon
    ResetToDefaultWaveCounts(t.MinionInfoTable)
    if p % CANNON_MINION_SPAWN_FREQUENCY == 0 then
        v.NumPerWave = v.NumPerWave + 1
    end
    if L ~= LAST_WAVE then
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
        LAST_WAVE = L
    end
    if t.WillSpawnSuperMinion == 1 then
        if r == TEAM_ORDER then
            if totalNumberOfChaosBarracks == 0 then
                u.NumPerWave = 2
            end
        end
        if r == TEAM_CHAOS then
            if totalNumberOfOrderBarracks == 0 then
                u.NumPerWave = 2
            end
        else
            u.NumPerWave = 1
        end
        v.NumPerWave = 0
    end
    if SPECIAL_MINION_MODE == "2MeleeMinions" then
        z = t.MinionInfoTable.Super.NumPerWave
        ClearCurrentWaveCounts(t.MinionInfoTable)
        u.NumPerWave = z
        t.MinionInfoTable.Melee.NumPerWave = math.max(2 - z, 0)
    else
        if SPECIAL_MINION_MODE == "None" then
            ClearCurrentWaveCounts(t.MinionInfoTable)
        end
    end
    z = {}
    z.NewFormat = true
    z.SpawnOrderMinionNames = t.SpawnOrderMinionNames
    z.IsDestroyed = t.IsDestroyed
    z.ExperienceRadius = SpawnTable.ExpRadius
    z.GoldRadius = SpawnTable.GoldRadius
    z.MinionInfoTable = t.MinionInfoTable
    ReturnTable = z
    return ReturnTable
end
DeactivateCorrectStructure = function(o, p, q)
    local G, s, t, u, v
    if o == TEAM_ORDER then
        G = OrderBuildingStatus
    else
        G = ChaosBuildingStatus
    end
    if q == FRONT_TOWER then
        G[p + 1].Turret3 = false
        s = GetTurret(o, p, MIDDLE_TOWER)
        SetInvulnerable(s, false)
        SetTargetable(s, true)
    else
        if q == MIDDLE_TOWER then
            G[p + 1].Turret2 = false
            s = GetTurret(o, p, BACK_TOWER)
            SetInvulnerable(s, false)
            SetTargetable(s, true)
        else
            if q == BACK_TOWER then
                G[p + 1].Turret1 = false
                s = GetDampener(o, p)
                SetInvulnerable(s, false)
                SetTargetable(s, true)
            else
                if q == HQ_TOWER2 then
                    G.HQTower2 = false
                    if G.HQTower1 == false then
                        s = GetHQ(o)
                        SetInvulnerable(s, false)
                        SetTargetable(s, true)
                    end
                else
                    if q == HQ_TOWER1 then
                        G.HQTower1 = false
                        if G.HQTower2 == false then
                            s = GetHQ(o)
                            SetInvulnerable(s, false)
                            SetTargetable(s, true)
                        end
                    end
                end
            end
        end
    end
end
GetLuaBarracks = function(o, p)
    local F, G, s
    if o == TEAM_ORDER then
        F = OrderBarracksBonuses[p + 1]
    else
        F = ChaosBarracksBonuses[p + 1]
    end
    return F
end
GetDisableMinionSpawnTime = function(o, p)
    local F, G, s
    barrack = GetLuaBarracks(p, o)
    return DISABLE_MINION_SPAWN_BASE_TIME + DISABLE_MINION_SPAWN_MAG_TIME * barrack.NumOfSpawnDisables
end
DisableBarracksSpawn = function(o, p)
    local F, G, s, t, u
    cLangBarracks = GetBarracks(p, o)
    luaBarrack = GetLuaBarracks(p, o)
    s, t, u = GetDisableMinionSpawnTime(o, p)
    SetDisableMinionSpawn(cLangBarracks, s, t, u)
    luaBarrack.NumOfSpawnDisables = luaBarrack.NumOfSpawnDisables + 1
end
BonusesCounter = 0
ApplyBarracksDestructionBonuses = function(o, p)
    local F, G, s, t, u, v, w, x, y, z, A, B, C, D, I, J
    BonusesCounter = BonusesCounter + 1
    for t = 1, 3, 1 do
        u = nil
        if o == TEAM_ORDER then
            u = OrderBarracksBonuses
            EnemyBarracks = ChaosBarracksBonuses
        else
            u = ChaosBarracksBonuses
            EnemyBarracks = OrderBarracksBonuses
        end
        z, A, B = pairs(u[t].MinionInfoTable)
        for C, D in z, A, B do
            D.HPBonus = D.HPBonus + D.HPInhibitor
            D.DamageBonus = D.DamageBonus + D.DamageInhibitor
            D.ExpGiven = D.ExpGiven - D.ExpInhibitor
            D.GoldGiven = D.GoldGiven - D.GoldInhibitor
        end
        if t == p + 1 then
            u[t].WillSpawnSuperMinion = 1
            if o == TEAM_ORDER then
                totalNumberOfChaosBarracks = totalNumberOfChaosBarracks - 1
            else
                totalNumberOfOrderBarracks = totalNumberOfOrderBarracks - 1
            end
        end
    end
end
ReductionCounter = 0
ApplyBarracksRespawnReductions = function(o, p)
    local F, G, s, t, u, v, w, x, y, z, A, B, C, D, I, J, K, M, N, O, P
    ReductionCounter = ReductionCounter + 1
    for t = 1, 3, 1 do
        u = nil
        w = nil
        if o == TEAM_ORDER then
            w = TEAM_CHAOS
            u = OrderBarracksBonuses
        else
            w = TEAM_ORDER
            u = ChaosBarracksBonuses
        end
        A, B, C = pairs(u[t].MinionInfoTable)
        for D, I in A, B, C do
            I.HPBonus = I.HPBonus - I.HPInhibitor
            I.DamageBonus = I.DamageBonus - I.DamageInhibitor
            I.ExpGiven = I.ExpGiven + I.ExpInhibitor
            I.GoldGiven = I.GoldGiven + I.GoldInhibitor
        end
        A = nil
        if t == p + 1 then
            if o == TEAM_ORDER then
                totalNumberOfChaosBarracks = totalNumberOfChaosBarracks + 1
                A = totalNumberOfChaosBarracks
            else
                totalNumberOfOrderBarracks = totalNumberOfOrderBarracks + 1
                A = totalNumberOfOrderBarracks
            end
            u[t].WillSpawnSuperMinion = 0
        end
        if A == 3 then
            HQ = GetHQ(w)
            SetInvulnerable(HQ, true)
            SetTargetable(HQ, false)
            for I = RIGHT_LANE, LEFT_LANE, 1 do
                J = GetTurret(w, I, HQ_TOWER1)
                K = GetTurret(w, I, HQ_TOWER2)
                if J ~= nil then
                    SetInvulnerable(J, true)
                    SetNotTargetableToTeam(J, true, o)
                end
                if K ~= nil then
                    SetInvulnerable(K, true)
                    SetNotTargetableToTeam(K, true, o)
                end
            end
        end
    end
end
ReactiveCounter = 0
BarrackReactiveEvent = function(o, p)
    local F, G, s, t
    ReactiveCounter = ReactiveCounter + 1
    dampener = GetDampener(o, p)
    SetInvulnerable(dampener, false)
    SetTargetable(dampener, true)
    ApplyBarracksRespawnReductions(OppositeTeam(o), p)
end
DisableSuperMinions = function(o, p)
    local F, G, s, t, u
    if o == TEAM_ORDER then
        G = ChaosBarracksBonuses[p + 1]
    else
        if o == TEAM_CHAOS then
            G = OrderBarracksBonuses[p + 1]
        end
    end
    if G then
        G.WillSpawnSuperMinion = 0
    end
end
OnJumpToMidGameCheat = function(o)
    local E, F, G, s, t, u, v
    E = GetTurret(TEAM_ORDER, 1, HQ_TOWER1)
    if o == 0 then
        ApplyPersistentBuff(E, "DebugMidGameBuff", false, 1, 1)
    else
        ApplyPersistentBuff(E, "DebugLateGameBuff", false, 1, 1)
    end
end
DisactivatedCounter = 0
HandleDestroyedObject = function(o)
    local E, F, G, s, t, u, v
    HQType = GetHQType(o)
    if HQType == ORDER_HQ or HQType == CHAOS_HQ then
        if HQType == CHAOS_HQ then
            EndOfGameCeremony(TEAM_ORDER, o)
        else
            EndOfGameCeremony(TEAM_CHAOS, o)
        end
        return
    end
    if IsDampener(o) then
        barrack = GetLinkedBarrack(o)
        barrackTeam = GetTeamID(barrack)
        barrackLane = GetLane(o)
        DisableBarracksSpawn(barrackLane, barrackTeam)
        SetDampenerState(o, DampenerRegenerationState)
        SetInvulnerable(o, true)
        SetTargetable(o, false)
        DisactivatedCounter = DisactivatedCounter + 1
        E = GetTurret(barrackTeam, 1, HQ_TOWER1)
        F = GetTurret(barrackTeam, 1, HQ_TOWER2)
        if E ~= nil then
            SetInvulnerable(E, false)
            SetTargetable(E, true)
        end
        if F ~= nil then
            SetInvulnerable(F, false)
            SetTargetable(F, true)
        end
        if E == nil then
            if F == nil then
                G = GetHQ(barrackTeam)
                SetInvulnerable(G, false)
                SetTargetable(G, true)
            end
        end
        G = nil
        if barrackTeam == TEAM_CHAOS then
            G = TEAM_ORDER
        else
            G = TEAM_CHAOS
        end
        ApplyBarracksDestructionBonuses(G, barrackLane)
    end
    if IsTurretAI(o) then
        DeactivateCorrectStructure(GetTeamID(o), GetObjectLaneId(o), GetTurretPosition(o))
        return
    end
    E = GetDampenerType(o)
    if -1 < E then
        t = E % TEAM_CHAOS
        if t >= RIGHT_LANE then
            if t <= LEFT_LANE then
                ChaosBuildingStatus[t + 1].Barracks = false
            end
        else
            t = t - TEAM_ORDER
            OrderBuildingStatus[t + 1].Barracks = false
        end
    else
        Log("Could not find Linking barracks!")
    end
    return true
end
IncreaseCannonMinionSpawnRate = function()
    local H, E
    CANNON_MINION_SPAWN_FREQUENCY = 2
end
IncreaseCannonMinionSpawnRateAgain = function()
    local H, E
    CANNON_MINION_SPAWN_FREQUENCY = 1
end
PostGameSetup = function(o)
    local E
    POST_GAME_EVENTS = {}
end
PostGameUpdate = function(o, p)
    local F, G, s, t, u, v, w, x, y, z
    s, t, u = pairs(POST_GAME_EVENTS)
    for v, w in s, t, u do
        if o > w.delay then
            ClientSide_CameraMoveCameraFromCurrentPositionToPoint(w.cameraLocation, w.travelTime)
            if w.soundFile then
                ClientSide_PlaySoundFile(w.soundFile)
            end
            table.remove(POST_GAME_EVENTS, v)
            break
        end
    end
end
