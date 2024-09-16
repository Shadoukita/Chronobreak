local a, b, c, d, e, f, g, h
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
INITIAL_TIME_TO_SPAWN = 75
CANNON_MINION_SPAWN_FREQUENCY = 3
INCREASE_CANNON_RATE_TIMER = 720
MINION_HEALTH_DENIAL_PERCENT = 0
UPGRADE_MINION_TIMER = 90
EXP_GIVEN_RADIUS = 1250
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
AppendTable = function(i, j, k, l)
    local m, n, o, p, q, r, s, t, u, v, w, x
    if j ~= nil then
        if type(j) == "table" then
            if i == nil then
                i = {}
            end
            o, p, q = pairs(j)
            for r, s in o, p, q do
                if k == true and type(s) == "table" then
                    i[r] = AppendTable(i[r], s, l, l)
                else
                    i[r] = s
                end
            end
        end
    end
    return i
end
InitBarrackInfo = function(i)
    local y, z, A, m, n, o, p, q, r, s, t, u, v, w
    i = AppendTable(i, i.DefaultInfo, true, true)
    i.MinionInfoTable = AppendTable(i.MinionInfoTable, i.DefaultMinionInfoTable, true, false)
    if i.MinionInfoTable ~= nil then
        y = i.MinionInfoTable
        m, n, o = pairs(y)
        for p, q in m, n, o do
            r = y[p]
            if r.DefaultInfo ~= nil then
                AppendTable(r, r.DefaultInfo, true, true)
                r.NumPerWave = r.DefaultNumPerWave
                r.GoldGivenBase = r.GoldGiven
                r.ExpGivenBase = r.ExpGiven
            end
        end
    end
    if i.Overrides ~= nil then
        A, m, n = pairs(i.Overrides)
        for o, p in A, m, n do
            if type(i[o]) == "table" and type(p) == "table" then
                AppendTable(i[o], p, true, true)
            else
                if type(p) ~= "table" then
                    i[o] = p
                end
            end
        end
    end
    return i
end
a = {}
d, e, f, g, h = InitBarrackInfo(OrderBarrack2)
a[1] = InitBarrackInfo(OrderBarrack0)
a[2] = InitBarrackInfo(OrderBarrack1)
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
OrderBarracksBonuses = a
a = {}
d, e, f, g, h = InitBarrackInfo(ChaosBarrack2)
a[1] = InitBarrackInfo(ChaosBarrack0)
a[2] = InitBarrackInfo(ChaosBarrack1)
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
ChaosBarracksBonuses = a
a = {}
a[1] = "TT_NWolf"
a[2] = "TT_NWolf2"
a[3] = "TT_NGolem"
a[4] = "TT_NGolem2"
a[5] = "TT_NWraith"
a[6] = "TT_NWraith2"
a[7] = "TT_Spiderboss"
NeutralMinionNames = a
CreateLaneBuildingTable = function()
    local B, y
    B = {}
    B.Turret3 = false
    B.Turret2 = true
    B.Turret1 = true
    B.Barracks = true
    return B
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
totalNumberOfChaosBarracks = 2
totalNumberOfOrderBarracks = 2
PreloadBarrackCharacters = function(i)
    local y, z, A, m, n, o, p, q, r
    A, m, n = pairs(i.DefaultMinionInfoTable)
    for o, p in A, m, n do
        if p.PreloadedCharacter == nil then
            p.PreloadedCharacter = 1
            PreloadCharacter(p.MinionName)
        end
    end
end
OnLevelInit = function()
    local B, y, z, A, m, n, o, p, q
    z, A, m = pairs(OrderBarracksBonuses)
    for n, o in z, A, m do
        PreloadBarrackCharacters(o)
    end
    z, A, m = pairs(ChaosBarracksBonuses)
    for n, o in z, A, m do
        PreloadBarrackCharacters(o)
    end
    PreloadCharacter("TT_ChaosTurret1")
    PreloadCharacter("TT_ChaosTurret2")
    PreloadCharacter("TT_ChaosTurret3")
    PreloadCharacter("TT_ChaosTurret4")
    PreloadCharacter("TT_OrderTurret1")
    PreloadCharacter("TT_OrderTurret2")
    PreloadCharacter("TT_OrderTurret3")
    PreloadCharacter("TT_OrderTurret4")
    PreloadCharacter("TT_NGolem")
    PreloadCharacter("TT_NGolem2")
    PreloadCharacter("TT_NWolf")
    PreloadCharacter("TT_NWolf2")
    PreloadCharacter("TT_NWraith")
    PreloadCharacter("TT_NWraith2")
    PreloadCharacter("TT_Spiderboss")
    PreloadSpell("TT_SpiderbossApplicator")
    PreloadSpell("TT_SpiderbossAttack")
    PreloadSpell("TT_SpiderbossAttack2")
    PreloadSpell("TT_SpiderbossAttack3")
    PreloadSpell("TT_SpiderbossAttackCaster")
    PreloadSpell("TT_SpiderbossAttackDebuff")
    PreloadSpell("TT_SpiderbossSmashCaster")
    PreloadSpell("TT_SpiderbossSmashCD")
    PreloadSpell("TT_SpiderbossSmashCheck")
    PreloadSpell("TT_SpiderbossSmashLine")
    PreloadSpell("TT_SpiderbossSmashSequence")
    PreloadSpell("TT_SpiderbossVortex")
    PreloadSpell("TT_SpiderbossVortexCaster")
    PreloadSpell("TT_SpiderbossVortexCD")
    PreloadSpell("TT_SpiderbossVortexStun")
    PreloadSpell("TT_ActionTimer")
    PreloadSpell("TT_ActionTimer2")
    PreloadSpell("TT_BossBuff")
    PreloadSpell("TT_BossCombat")
    PreloadCharacter("TT_Relic")
    PreloadSpell("TT_RelicAura")
    PreloadSpell("TT_RelicHeal")
    PreloadSpell("TT_SpeedShrine_Buff")
    PreloadCharacter("TT_Buffplat_L")
    PreloadCharacter("TT_Buffplat_R")
    PreloadSpell("TreelineLantern1")
    PreloadSpell("TreelineLantern2")
    PreloadSpell("TreelineLantern3")
    PreloadSpell("TreelineLantern4")
    PreloadSpell("TreelineLantern5")
    PreloadSpell("TreelineLantern6")
    PreloadSpell("TreelineLantern7")
    PreloadSpell("TreelineLantern8")
    PreloadSpell("TreelineLantern9")
    PreloadSpell("TreelineLanternAntilockChaos")
    PreloadSpell("TreelineLanternAntilockOrder")
    PreloadSpell("TreelineLanternCap")
    PreloadSpell("TreelineLanternCap2")
    PreloadSpell("TreelineLanternCapDamage")
    PreloadSpell("TreelineLanternCapInterrupt")
    PreloadSpell("TreelineLanternChargeBuffChaos")
    PreloadSpell("TreelineLanternChargeBuffOrder")
    PreloadSpell("TreelineLanternChargeScript")
    PreloadSpell("TreelineLanternFirstRun")
    PreloadSpell("TreelineLanternLeft")
    PreloadSpell("TreelineLanternLeftBuff")
    PreloadSpell("TreelineLanternLock")
    PreloadSpell("TreelineLanternLockParticle1")
    PreloadSpell("TreelineLanternLockParticle2")
    PreloadSpell("TreelineLanternOwnerChaos")
    PreloadSpell("TreelineLanternOwnerOrder")
    PreloadSpell("TreelineLanternOwnerBuff")
    PreloadSpell("TreelineLanternPostLock")
    PreloadSpell("TreelineLanternPostLockChaos")
    PreloadSpell("TreelineLanternPostLockNeutral")
    PreloadSpell("TreelineLanternPostLockOrder")
    PreloadSpell("TreelineLanternRight")
    PreloadSpell("TreelineLanternRightBuff")
    PreloadSpell("TreelineLanternTrackLeft")
    PreloadSpell("TreelineLanternTrackRight")
    PreloadSpell("TreelineLanternUnlockSound")
    PreloadSpell("TreelineMasterBuff")
    PreloadSpell("TreelineMasterBuffT0")
    PreloadSpell("TreelineMasterBuffT1Left")
    PreloadSpell("TreelineMasterBuffT1Right")
    PreloadSpell("TreelineMasterBuffT2")
    PreloadSpell("SpellShieldMarker")
    PreloadSpell("RespawnClassic")
    A, m, n, o, p, q = os.time()
    math.randomseed(A, m, n, o, p, q)
    LoadLevelScriptIntoScript("NeutralMinionSpawn.lua")
    NeutralMinionInit()
    LoadLevelScriptIntoScript("EndOfGame.lua")
    SpawnTable.WaveSpawnRate = 30000
    SpawnTable.SingleMinionSpawnDelay = 800
    SpawnTable.ExpRadius = EXP_GIVEN_RADIUS
    SpawnTable.GoldRadius = GOLD_GIVEN_RADIUS
end
OnLevelInitServer = function()
    local B, y, z, A
    InitTimer("UpgradeMinionTimer", UPGRADE_MINION_TIMER, true)
    InitTimer("IncreaseCannonMinionSpawnRate", INCREASE_CANNON_RATE_TIMER, false)
    InitTimer("AllowDamageOnBuildings", 10, false)
end
OnPostLevelLoad = function()
    local B, y
    LoadLevelScriptIntoScript("CreateLevelProps.lua")
    CreateLevelProps()
end
OppositeTeam = function(i)
    local y
    if i == TEAM_CHAOS then
        return TEAM_ORDER
    else
        return TEAM_CHAOS
    end
end
UpgradeMinionTimer = function()
    local B, y, z, A, m, n, o, p, q, r, s, t, u, v, w, x, C, D, E
    for m = 1, 2, 1 do
        if m == 1 then
            B = OrderBarracksBonuses
        else
            B = ChaosBarracksBonuses
        end
        for q = 1, 3, 1 do
            u, v, w = pairs(B[q].MinionInfoTable)
            for x, C in u, v, w do
                C.HPBonus = C.HPBonus + C.HPUpgrade
                C.DamageBonus = C.DamageBonus + C.DamageUpgrade
                C.GoldBonus = C.GoldBonus + C.GoldUpgrade
                if C.GoldBonus > C.GoldMaximumBonus then
                    C.GoldBonus = C.GoldMaximumBonus
                end
                C.LocalGoldBonus = C.LocalGoldBonus + C.GoldShareUpgrade
                C.Armor = C.Armor + C.ArmorUpgrade
                C.MagicResistance = C.MagicResistance + C.MagicResistanceUpgrade
                C.ExpBonus = C.ExpBonus + C.ExpUpgrade
                C.GoldGiven = C.GoldBonus + C.GoldGivenBase
                C.LocalGoldGiven = C.LocalGoldBonus + C.GoldShared
                C.ExpGiven = C.ExpBonus + C.ExpGivenBase
            end
        end
    end
end
AllowDamageOnBuildings = function()
    local B, y, z, A, m, n, o, p, q, r, s, t
    for A = RIGHT_LANE, LEFT_LANE, 1 do
        for p = BACK_TOWER, HQ_TOWER1, 1 do
            orderTurret = GetTurret(TEAM_ORDER, A, p)
            if orderTurret ~= nil then
                if p == MIDDLE_TOWER then
                    SetInvulnerable(orderTurret, false)
                    SetTargetable(orderTurret, true)
                else
                    SetInvulnerable(orderTurret, true)
                    SetNotTargetableToTeam(orderTurret, true, TEAM_CHAOS)
                end
            end
            chaosTurret = GetTurret(TEAM_CHAOS, A, p)
            if chaosTurret ~= nil then
                if p == MIDDLE_TOWER then
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
ResetToDefaultWaveCounts = function(i)
    local y, z, A, m, n, o, p, q
    A, m, n = pairs(i)
    for o, p in A, m, n do
        p.NumPerWave = p.DefaultNumPerWave
    end
end
ClearCurrentWaveCounts = function(i)
    local y, z, A, m, n, o, p, q
    A, m, n = pairs(i)
    for o, p in A, m, n do
        p.NumPerWave = 0
    end
end
GetInitSpawnInfo = function(i, j)
    local z, A, m
    z = 0
    if j == TEAM_ORDER then
        z = OrderBarracksBonuses[i + 1]
    else
        z = ChaosBarracksBonuses[i + 1]
    end
    A = {}
    A.WaveSpawnRate = SpawnTable.WaveSpawnRate
    A.SingleMinionSpawnDelay = SpawnTable.SingleMinionSpawnDelay
    A.IsDestroyed = z.IsDestroyed
    A.MinionInfoTable = z.MinionInfoTable
    ReturnTable = A
    return ReturnTable
end
GetMinionSpawnInfo = function(i, j, k, l, F)
    local n, o, p, q, r, s, t, u
    n = 0
    if l == TEAM_ORDER then
        n = OrderBarracksBonuses[i + 1]
    else
        n = ChaosBarracksBonuses[i + 1]
    end
    o = n.MinionInfoTable.Super
    p = n.MinionInfoTable.Cannon
    ResetToDefaultWaveCounts(n.MinionInfoTable)
    if j % CANNON_MINION_SPAWN_FREQUENCY == 0 then
        p.NumPerWave = p.NumPerWave + 1
    end
    if F ~= LAST_WAVE then
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
        LAST_WAVE = F
    end
    if n.WillSpawnSuperMinion == 1 then
        if l == TEAM_ORDER then
            if totalNumberOfChaosBarracks == 0 then
                o.NumPerWave = 2
            end
        end
        if l == TEAM_CHAOS then
            if totalNumberOfOrderBarracks == 0 then
                o.NumPerWave = 2
            end
        else
            o.NumPerWave = 1
        end
        p.NumPerWave = 0
    end
    if SPECIAL_MINION_MODE == "2MeleeMinions" then
        q = n.MinionInfoTable.Super.NumPerWave
        ClearCurrentWaveCounts(n.MinionInfoTable)
        o.NumPerWave = q
        n.MinionInfoTable.Melee.NumPerWave = math.max(2 - q, 0)
    else
        if SPECIAL_MINION_MODE == "None" then
            ClearCurrentWaveCounts(n.MinionInfoTable)
        end
    end
    q = {}
    q.NewFormat = true
    q.SpawnOrderMinionNames = n.SpawnOrderMinionNames
    q.IsDestroyed = n.IsDestroyed
    q.ExperienceRadius = SpawnTable.ExpRadius
    q.GoldRadius = SpawnTable.GoldRadius
    q.MinionInfoTable = n.MinionInfoTable
    ReturnTable = q
    return ReturnTable
end
DeactivateCorrectStructure = function(i, j, k)
    local A, m, n, o, p
    if i == TEAM_ORDER then
        A = OrderBuildingStatus
    else
        A = ChaosBuildingStatus
    end
    if k == FRONT_TOWER then
        A[j + 1].Turret3 = false
        m = GetTurret(i, j, MIDDLE_TOWER)
        SetInvulnerable(m, false)
        SetTargetable(m, true)
    else
        if k == MIDDLE_TOWER then
            A[j + 1].Turret2 = false
            m = GetTurret(i, j, BACK_TOWER)
            SetInvulnerable(m, false)
            SetTargetable(m, true)
        else
            if k == BACK_TOWER then
                A[j + 1].Turret1 = false
                m = GetDampener(i, j)
                SetInvulnerable(m, false)
                SetTargetable(m, true)
            else
                if k == HQ_TOWER2 then
                    A.HQTower2 = false
                    m = GetHQ(i)
                    SetInvulnerable(m, false)
                    SetTargetable(m, true)
                end
            end
        end
    end
end
GetLuaBarracks = function(i, j)
    local z, A, m
    if i == TEAM_ORDER then
        z = OrderBarracksBonuses[j + 1]
    else
        z = ChaosBarracksBonuses[j + 1]
    end
    return z
end
GetDisableMinionSpawnTime = function(i, j)
    local z, A, m
    barrack = GetLuaBarracks(j, i)
    return DISABLE_MINION_SPAWN_BASE_TIME + DISABLE_MINION_SPAWN_MAG_TIME * barrack.NumOfSpawnDisables
end
DisableBarracksSpawn = function(i, j)
    local z, A, m, n, o
    cLangBarracks = GetBarracks(j, i)
    luaBarrack = GetLuaBarracks(j, i)
    m, n, o = GetDisableMinionSpawnTime(i, j)
    SetDisableMinionSpawn(cLangBarracks, m, n, o)
    luaBarrack.NumOfSpawnDisables = luaBarrack.NumOfSpawnDisables + 1
end
BonusesCounter = 0
ApplyBarracksDestructionBonuses = function(i, j)
    local z, A, m, n, o, p, q, r, s, t, u, v, w, x, C, D
    BonusesCounter = BonusesCounter + 1
    for n = 1, 3, 1 do
        o = nil
        if i == TEAM_ORDER then
            o = OrderBarracksBonuses
            EnemyBarracks = ChaosBarracksBonuses
        else
            o = ChaosBarracksBonuses
            EnemyBarracks = OrderBarracksBonuses
        end
        t, u, v = pairs(o[n].MinionInfoTable)
        for w, x in t, u, v do
            x.HPBonus = x.HPBonus + x.HPInhibitor
            x.DamageBonus = x.DamageBonus + x.DamageInhibitor
            x.ExpGiven = x.ExpGiven - x.ExpInhibitor
            x.GoldGiven = x.GoldGiven - x.GoldInhibitor
        end
        if n == j + 1 then
            o[n].WillSpawnSuperMinion = 1
            if i == TEAM_ORDER then
                totalNumberOfChaosBarracks = totalNumberOfChaosBarracks - 1
            else
                totalNumberOfOrderBarracks = totalNumberOfOrderBarracks - 1
            end
        end
    end
end
ReductionCounter = 0
ApplyBarracksRespawnReductions = function(i, j)
    local z, A, m, n, o, p, q, r, s, t, u, v, w, x, C, D, E, G, H, I, J
    ReductionCounter = ReductionCounter + 1
    for n = 1, 3, 1 do
        o = nil
        q = nil
        if i == TEAM_ORDER then
            q = TEAM_CHAOS
            o = OrderBarracksBonuses
        else
            q = TEAM_ORDER
            o = ChaosBarracksBonuses
        end
        u, v, w = pairs(o[n].MinionInfoTable)
        for x, C in u, v, w do
            C.HPBonus = C.HPBonus - C.HPInhibitor
            C.DamageBonus = C.DamageBonus - C.DamageInhibitor
            C.ExpGiven = C.ExpGiven + C.ExpInhibitor
            C.GoldGiven = C.GoldGiven + C.GoldInhibitor
        end
        u = nil
        if n == j + 1 then
            if i == TEAM_ORDER then
                totalNumberOfChaosBarracks = totalNumberOfChaosBarracks + 1
                u = totalNumberOfChaosBarracks
            else
                totalNumberOfOrderBarracks = totalNumberOfOrderBarracks + 1
                u = totalNumberOfOrderBarracks
            end
            o[n].WillSpawnSuperMinion = 0
        end
        if u == 2 then
            HQ = GetHQ(q)
            SetInvulnerable(HQ, true)
            SetTargetable(HQ, false)
            for C = RIGHT_LANE, LEFT_LANE, 1 do
                D = GetTurret(q, C, HQ_TOWER1)
                E = GetTurret(q, C, HQ_TOWER2)
                if D ~= Nil then
                    SetInvulnerable(D, true)
                    SetNotTargetableToTeam(D, true, i)
                end
                if E ~= Nil then
                    SetInvulnerable(E, true)
                    SetNotTargetableToTeam(E, true, i)
                end
            end
        end
    end
end
ReactiveCounter = 0
BarrackReactiveEvent = function(i, j)
    local z, A, m, n
    ReactiveCounter = ReactiveCounter + 1
    dampener = GetDampener(i, j)
    SetInvulnerable(dampener, false)
    SetTargetable(dampener, true)
    ApplyBarracksRespawnReductions(OppositeTeam(i), j)
end
DisableSuperMinions = function(i, j)
    local z, A, m, n, o
    if i == TEAM_ORDER then
        A = ChaosBarracksBonuses[j + 1]
    else
        if i == TEAM_CHAOS then
            A = OrderBarracksBonuses[j + 1]
        end
    end
    if A then
        A.WillSpawnSuperMinion = 0
    end
end
DisactivatedCounter = 0
HandleDestroyedObject = function(i)
    local y, z, A, m, n, o, p
    HQType = GetHQType(i)
    if HQType == ORDER_HQ or HQType == CHAOS_HQ then
        if HQType == CHAOS_HQ then
            EndOfGameCeremony(TEAM_ORDER, i)
        else
            EndOfGameCeremony(TEAM_CHAOS, i)
        end
        return
    end
    if IsDampener(i) then
        barrack = GetLinkedBarrack(i)
        barrackTeam = GetTeamID(barrack)
        barrackLane = GetLane(i)
        DisableBarracksSpawn(barrackLane, barrackTeam)
        SetDampenerState(i, DampenerRegenerationState)
        SetInvulnerable(i, true)
        SetTargetable(i, false)
        DisactivatedCounter = DisactivatedCounter + 1
        y = GetTurret(barrackTeam, 0, 4)
        if y ~= Nil then
            SetInvulnerable(y, false)
            SetTargetable(y, true)
        else
            z = GetHQ(barrackTeam)
            SetInvulnerable(z, false)
            SetTargetable(z, true)
        end
        z = nil
        if barrackTeam == TEAM_CHAOS then
            z = TEAM_ORDER
        else
            z = TEAM_CHAOS
        end
        ApplyBarracksDestructionBonuses(z, barrackLane)
    end
    if IsTurretAI(i) then
        DeactivateCorrectStructure(GetTeamID(i), GetObjectLaneId(i), GetTurretPosition(i))
        return
    end
    y = GetDampenerType(i)
    if -1 < y then
        n = y % TEAM_CHAOS
        if n >= RIGHT_LANE then
            if n <= LEFT_LANE then
                ChaosBuildingStatus[n + 1].Barracks = false
            end
        else
            n = n - TEAM_ORDER
            OrderBuildingStatus[n + 1].Barracks = false
        end
    else
        Log("Could not find Linking barracks!")
    end
    return true
end
IncreaseCannonMinionSpawnRate = function()
    local B, y
    CANNON_MINION_SPAWN_FREQUENCY = 2
end
