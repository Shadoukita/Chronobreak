local a, b, c, d
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
MINION_HEALTH_DENIAL_PERCENT = 0
MELEE_EXP_GIVEN = 64
MELEE_GOLD_GIVEN = 22
CASTER_EXP_GIVEN = 32
CASTER_GOLD_GIVEN = 16
CANNON_EXP_GIVEN = 100
CANNON_GOLD_GIVEN = 27
SUPER_EXP_GIVEN = 100
SUPER_GOLD_GIVEN = 27
MELEE_HEALTH_UPGRADE = 20
MELEE_DAMAGE_UPGRADE = 1
MELEE_GOLD_UPGRADE = 0.5
MELEE_EXP_UPGRADE = 5
MELEE_ARMOR_UPGRADE = 2
MELEE_MR_UPGRADE = 0.5
CASTER_HEALTH_UPGRADE = 15
CASTER_DAMAGE_UPGRADE = 2
CASTER_GOLD_UPGRADE = 0.5
CASTER_EXP_UPGRADE = 3
CASTER_ARMOR_UPGRADE = 0.5
CASTER_MR_UPGRADE = 2
CANNON_HEALTH_UPGRADE = 27
CANNON_DAMAGE_UPGRADE = 3
CANNON_GOLD_UPGRADE = 1
CANNON_EXP_UPGRADE = 7
CANNON_ARMOR_UPGRADE = 2
CANNON_MR_UPGRADE = 2
SUPER_HEALTH_UPGRADE = 200
SUPER_DAMAGE_UPGRADE = 10
SUPER_GOLD_UPGRADE = 0
SUPER_EXP_UPGRADE = 0
SUPER_ARMOR_UPGRADE = 0
SUPER_MR_UPGRADE = 0
MAXIMUM_MELEE_GOLD_BONUS = 10
MAXIMUM_CASTER_GOLD_BONUS = 10
MAXIMUM_CANNON_GOLD_BONUS = 20
MAXIMUM_SUPER_GOLD_BONUS = 20
UPGRADE_MINION_TIMER = 180
MELEE_HEALTH_INHIBITOR = 100
MELEE_DAMAGE_INHIBITOR = 10
MELEE_GOLD_INHIBITOR = 0
MELEE_EXP_INHIBITOR = 0
CASTER_HEALTH_INHIBITOR = 60
CASTER_DAMAGE_INHIBITOR = 18
CASTER_GOLD_INHIBITOR = 0
CASTER_EXP_INHIBITOR = 0
CANNON_HEALTH_INHIBITOR = 200
CANNON_DAMAGE_INHIBITOR = 25
CANNON_GOLD_INHIBITOR = 0
CANNON_EXP_INHIBITOR = 0
SUPER_HEALTH_INHIBITOR = 0
SUPER_DAMAGE_INHIBITOR = 0
SUPER_GOLD_INHIBITOR = 0
SUPER_EXP_INHIBITOR = 0
EXP_GIVEN_RADIUS = 1250
DISABLE_MINION_SPAWN_BASE_TIME = 300
DISABLE_MINION_SPAWN_MAG_TIME = 0
LAST_WAVE = -1
SPECIAL_MINION_MODE = "none"
a = {}
a.MeleeMinionName = "Blue_Minion_Basic"
a.CasterMinionName = "Blue_Minion_Wizard"
a.CannonMinionName = "Blue_Minion_MechCannon"
a.SuperMinionName = "Blue_Minion_MechMelee"
OrderNames = a
a = {}
a.MeleeMinionName = "Red_Minion_Basic"
a.CasterMinionName = "Red_Minion_Wizard"
a.CannonMinionName = "Red_Minion_MechCannon"
a.SuperMinionName = "Red_Minion_MechMelee"
ChaosNames = a
a = {}
a.WaveSpawnRate = 0
a.NumOfMeleeMinionsPerWave = 0
a.NumOfCasterMinionsPerWave = 0
a.NumOfCannonMinionsPerWave = 0
a.NumOfSuperMinionsPerWave = 0
a.SingleMinionSpawnDelay = 0
a.DidPowerGroup = 0
a.MeleeMinionName = 0
a.CasterMinionName = 0
a.CannonMinionName = 0
a.SuperMinionName = 0
SpawnTable = a
a = {}
a.IsDestroyed = false
a.MeleeMinionArmor = 0
a.MeleeMinionMagicResistance = 0
a.MeleeHPBonus = 0
a.MeleeDamageBonus = 0
a.MeleeGoldBonus = 0
a.MeleeExpBonus = 0
a.MeleeExpGiven = MELEE_EXP_GIVEN
a.MeleeGoldGiven = MELEE_GOLD_GIVEN
a.CasterMinionArmor = 0
a.CasterMinionMagicResistance = 0
a.CasterHPBonus = 0
a.CasterDamageBonus = 0
a.CasterGoldBonus = 0
a.CasterExpBonus = 0
a.CasterExpGiven = CASTER_EXP_GIVEN
a.CasterGoldGiven = CASTER_GOLD_GIVEN
a.CannonMinionArmor = 0
a.CannonMinionMagicResistance = 0
a.CannonHPBonus = 0
a.CannonDamageBonus = 0
a.CannonGoldBonus = 0
a.CannonExpBonus = 0
a.CannonExpGiven = CANNON_EXP_GIVEN
a.SuperMinionArmor = 0
a.SuperMinionMagicResistance = 0
a.SuperHPBonus = 0
a.SuperDamageBonus = 0
a.SuperGoldBonus = 0
a.SuperExpBonus = 0
a.SuperExpGiven = SUPER_EXP_GIVEN
a.NumOfSpawnDisables = 0
a.WillSpawnSuperMinion = 0
a.MeleeMinionName = 0
a.RangedMinionName = 0
a.CannonMinionName = 0
a.CannonGoldGiven = CANNON_GOLD_GIVEN
a.SuperMinionName = 0
a.SuperGoldGiven = CANNON_GOLD_GIVEN
ChaosBarrack0 = a
a = {}
a.IsDestroyed = false
a.MeleeMinionArmor = 0
a.MeleeMinionMagicResistance = 0
a.MeleeHPBonus = 0
a.MeleeDamageBonus = 0
a.MeleeGoldBonus = 0
a.MeleeExpBonus = 0
a.MeleeExpGiven = MELEE_EXP_GIVEN
a.MeleeGoldGiven = MELEE_GOLD_GIVEN
a.CasterMinionArmor = 0
a.CasterMinionMagicResistance = 0
a.CasterHPBonus = 0
a.CasterDamageBonus = 0
a.CasterGoldBonus = 0
a.CasterExpBonus = 0
a.CasterExpGiven = CASTER_EXP_GIVEN
a.CasterGoldGiven = CASTER_GOLD_GIVEN
a.CannonMinionArmor = 0
a.CannonMinionMagicResistance = 0
a.CannonHPBonus = 0
a.CannonDamageBonus = 0
a.CannonGoldBonus = 0
a.CannonExpBonus = 0
a.CannonExpGiven = CANNON_EXP_GIVEN
a.SuperMinionArmor = 0
a.SuperMinionMagicResistance = 0
a.SuperHPBonus = 0
a.SuperDamageBonus = 0
a.SuperGoldBonus = 0
a.SuperExpBonus = 0
a.SuperExpGiven = SUPER_EXP_GIVEN
a.NumOfSpawnDisables = 0
a.WillSpawnSuperMinion = 0
a.MeleeMinionName = 0
a.RangedMinionName = 0
a.CannonMinionName = 0
a.CannonGoldGiven = CANNON_GOLD_GIVEN
a.SuperMinionName = 0
a.SuperGoldGiven = CANNON_GOLD_GIVEN
ChaosBarrack1 = a
a = {}
a.IsDestroyed = false
a.MeleeMinionArmor = 0
a.MeleeMinionMagicResistance = 0
a.MeleeHPBonus = 0
a.MeleeDamageBonus = 0
a.MeleeGoldBonus = 0
a.MeleeExpBonus = 0
a.MeleeExpGiven = MELEE_EXP_GIVEN
a.MeleeGoldGiven = MELEE_GOLD_GIVEN
a.CasterMinionArmor = 0
a.CasterMinionMagicResistance = 0
a.CasterHPBonus = 0
a.CasterDamageBonus = 0
a.CasterGoldBonus = 0
a.CasterExpBonus = 0
a.CasterExpGiven = CASTER_EXP_GIVEN
a.CasterGoldGiven = CASTER_GOLD_GIVEN
a.CannonMinionArmor = 0
a.CannonMinionMagicResistance = 0
a.CannonHPBonus = 0
a.CannonDamageBonus = 0
a.CannonGoldBonus = 0
a.CannonExpBonus = 0
a.CannonExpGiven = CANNON_EXP_GIVEN
a.SuperMinionArmor = 0
a.SuperMinionMagicResistance = 0
a.SuperHPBonus = 0
a.SuperDamageBonus = 0
a.SuperGoldBonus = 0
a.SuperExpBonus = 0
a.SuperExpGiven = SUPER_EXP_GIVEN
a.NumOfSpawnDisables = 0
a.WillSpawnSuperMinion = 0
a.MeleeMinionName = 0
a.RangedMinionName = 0
a.CannonMinionName = 0
a.CannonGoldGiven = CANNON_GOLD_GIVEN
a.SuperMinionName = 0
a.SuperGoldGiven = CANNON_GOLD_GIVEN
ChaosBarrack2 = a
a = {}
a.IsDestroyed = false
a.MeleeMinionArmor = 0
a.MeleeMinionMagicResistance = 0
a.MeleeHPBonus = 0
a.MeleeDamageBonus = 0
a.MeleeGoldBonus = 0
a.MeleeExpBonus = 0
a.MeleeExpGiven = MELEE_EXP_GIVEN
a.MeleeGoldGiven = MELEE_GOLD_GIVEN
a.CasterMinionArmor = 0
a.CasterMinionMagicResistance = 0
a.CasterHPBonus = 0
a.CasterDamageBonus = 0
a.CasterGoldBonus = 0
a.CasterExpBonus = 0
a.CasterExpGiven = CASTER_EXP_GIVEN
a.CasterGoldGiven = CASTER_GOLD_GIVEN
a.CannonMinionArmor = 0
a.CannonMinionMagicResistance = 0
a.CannonHPBonus = 0
a.CannonDamageBonus = 0
a.CannonGoldBonus = 0
a.CannonExpBonus = 0
a.CannonExpGiven = CANNON_EXP_GIVEN
a.SuperMinionArmor = 0
a.SuperMinionMagicResistance = 0
a.SuperHPBonus = 0
a.SuperDamageBonus = 0
a.SuperGoldBonus = 0
a.SuperExpBonus = 0
a.SuperExpGiven = SUPER_EXP_GIVEN
a.NumOfSpawnDisables = 0
a.WillSpawnSuperMinion = 0
a.MeleeMinionName = 0
a.RangedMinionName = 0
a.CannonMinionName = 0
a.CannonGoldGiven = CANNON_GOLD_GIVEN
a.SuperMinionName = 0
a.SuperGoldGiven = CANNON_GOLD_GIVEN
OrderBarrack0 = a
a = {}
a.IsDestroyed = false
a.MeleeMinionArmor = 0
a.MeleeMinionMagicResistance = 0
a.MeleeHPBonus = 0
a.MeleeDamageBonus = 0
a.MeleeGoldBonus = 0
a.MeleeExpBonus = 0
a.MeleeExpGiven = MELEE_EXP_GIVEN
a.MeleeGoldGiven = MELEE_GOLD_GIVEN
a.CasterMinionArmor = 0
a.CasterMinionMagicResistance = 0
a.CasterHPBonus = 0
a.CasterDamageBonus = 0
a.CasterGoldBonus = 0
a.CasterExpBonus = 0
a.CasterExpGiven = CASTER_EXP_GIVEN
a.CasterGoldGiven = CASTER_GOLD_GIVEN
a.CannonMinionArmor = 0
a.CannonMinionMagicResistance = 0
a.CannonHPBonus = 0
a.CannonDamageBonus = 0
a.CannonGoldBonus = 0
a.CannonExpBonus = 0
a.CannonExpGiven = CANNON_EXP_GIVEN
a.SuperMinionArmor = 0
a.SuperMinionMagicResistance = 0
a.SuperHPBonus = 0
a.SuperDamageBonus = 0
a.SuperGoldBonus = 0
a.SuperExpBonus = 0
a.SuperExpGiven = SUPER_EXP_GIVEN
a.NumOfSpawnDisables = 0
a.WillSpawnSuperMinion = 0
a.MeleeMinionName = 0
a.RangedMinionName = 0
a.CannonMinionName = 0
a.CannonGoldGiven = CANNON_GOLD_GIVEN
a.SuperMinionName = 0
a.SuperGoldGiven = CANNON_GOLD_GIVEN
OrderBarrack1 = a
a = {}
a.IsDestroyed = false
a.MeleeMinionArmor = 0
a.MeleeMinionMagicResistance = 0
a.MeleeHPBonus = 0
a.MeleeDamageBonus = 0
a.MeleeGoldBonus = 0
a.MeleeExpBonus = 0
a.MeleeExpGiven = MELEE_EXP_GIVEN
a.MeleeGoldGiven = MELEE_GOLD_GIVEN
a.CasterMinionArmor = 0
a.CasterMinionMagicResistance = 0
a.CasterHPBonus = 0
a.CasterDamageBonus = 0
a.CasterGoldBonus = 0
a.CasterExpBonus = 0
a.CasterExpGiven = CASTER_EXP_GIVEN
a.CasterGoldGiven = CASTER_GOLD_GIVEN
a.CannonMinionArmor = 0
a.CannonMinionMagicResistance = 0
a.CannonHPBonus = 0
a.CannonDamageBonus = 0
a.CannonGoldBonus = 0
a.CannonExpBonus = 0
a.CannonExpGiven = CANNON_EXP_GIVEN
a.SuperMinionArmor = 0
a.SuperMinionMagicResistance = 0
a.SuperHPBonus = 0
a.SuperDamageBonus = 0
a.SuperGoldBonus = 0
a.SuperExpBonus = 0
a.SuperExpGiven = SUPER_EXP_GIVEN
a.NumOfSpawnDisables = 0
a.WillSpawnSuperMinion = 0
a.MeleeMinionName = 0
a.RangedMinionName = 0
a.CannonMinionName = 0
a.CannonGoldGiven = CANNON_GOLD_GIVEN
a.SuperMinionName = 0
a.SuperGoldGiven = CANNON_GOLD_GIVEN
OrderBarrack2 = a
a = {}
a[1] = ChaosBarrack0
a[2] = ChaosBarrack1
a[3] = ChaosBarrack2
ChaosBarracksBonuses = a
a = {}
a[1] = OrderBarrack0
a[2] = OrderBarrack1
a[3] = OrderBarrack2
OrderBarracksBonuses = a
a = {}
a[1] = "AncientGolem"
a[2] = "Dragon"
a[3] = "YoungLizard"
NeutralMinionNames = a
CreateLaneBuildingTable = function()
    local e, f
    e = {}
    e.Turret3 = true
    e.Turret2 = true
    e.Turret1 = true
    e.Barracks = true
    return e
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
OnLevelInit = function()
    local e, f, g, h, i, j, k
    e, f, g = pairs(OrderNames)
    for h, i in e, f, g do
        PreloadCharacter(i)
    end
    e, f, g = pairs(ChaosNames)
    for h, i in e, f, g do
        PreloadCharacter(i)
    end
    PreloadSpell("RespawnClassic")
    PreloadCharacter("crystal_platform")
    PreloadCharacter("OdinNeutralGuardian")
    PreloadSpell("odinguardianbuff")
    PreloadSpell("odinminiontaunt")
    PreloadSpell("odinguardianui")
    PreloadSpell("odinguardianuidamage")
    PreloadSpell("OdinCaptureChannel")
    PreloadCharacter("Odin_Blue_Minion_Caster")
    PreloadCharacter("OdinBlueSuperminion")
    PreloadCharacter("Odin_Red_Minion_Caster")
    PreloadCharacter("OdinRedSuperminion")
    PreloadSpell("OdinMinionSpellAttack")
    PreloadCharacter("OdinSpeedShrine")
    PreloadSpell("OdinSpeedShrineAura")
    PreloadSpell("OdinShrineAura")
    PreloadCharacter("OdinShieldRelic")
    PreloadCharacter("OdinCenterRelic")
    PreloadCharacter("OdinQuestIndicator")
    PreloadCharacter("OdinTestCubeRender")
    PreloadSpell("OdinCaptureChannelBomb")
    PreloadSpell("OdinChannelVision")
    PreloadSpell("OdinParticlePHBuff")
    PreloadSpell("OdinScoreSurvivor")
    PreloadCharacter("OdinQuestBuff")
    PreloadCharacter("OdinQuestIndicator")
    PreloadSpell("OdinQuestBuff")
    PreloadSpell("OdinQuestBuffParticle")
    PreloadSpell("OdinQuestIndicator")
    PreloadSpell("OdinQuestParticleRemover")
    PreloadSpell("SpellShieldMarker")
    PreloadSpell("hpbyplayerlevel")
    PreloadSpell("burning")
    PreloadSpell("monsterbuffs")
    PreloadSpell("monsterbuffs2")
    PreloadSpell("lifestealattack")
    f, g, h, i, j, k = os.time()
    math.randomseed(f, g, h, i, j, k)
    LoadLevelScriptIntoScript("EndOfGame.lua")
    SpawnTable.WaveSpawnRate = 30000
    SpawnTable.NumOfMeleeMinionsPerWave = 3
    SpawnTable.NumOfCasterMinionsPerWave = 3
    SpawnTable.SingleMinionSpawnDelay = 800
    SpawnTable.DidPowerGroup = false
end
OnLevelInitServer = function()
    local e, f, g, h
    InitTimer("AllowDamageOnBuildings", 1, false)
end
OnPostLevelLoad = function()
    local e, f
    LoadLevelScriptIntoScript("CreateLevelProps.lua")
    CreateLevelProps()
end
OppositeTeam = function(l)
    local f
    if l == TEAM_CHAOS then
        return TEAM_ORDER
    else
        return TEAM_CHAOS
    end
end
UpgradeMinionTimer = function()
    local e, f
end
AllowDamageOnBuildings = function()
    local e, f, g, h
    orderTurret = GetTurret(TEAM_ORDER, 0, 1)
    SetInvulnerable(orderTurret, true)
    SetTargetable(orderTurret, false)
    chaosTurret = GetTurret(TEAM_CHAOS, 0, 1)
    SetInvulnerable(chaosTurret, true)
    SetTargetable(chaosTurret, false)
end
GetMinionSpawnInfo = function(l, m, n, o, p)
    local j, k, q, r
    TableForBarrack = SpawnTable
    if TableForBarrack.DidPowerGroup then
        TableForBarrack.NumOfCannonMinionsPerWave = TableForBarrack.NumOfCannonMinionsPerWave - 1
        TableForBarrack.DidPowerGroup = false
    end
    if m % CANNON_MINION_SPAWN_FREQUENCY == 0 then
        TableForBarrack.NumOfCannonMinionsPerWave = TableForBarrack.NumOfCannonMinionsPerWave + 1
        TableForBarrack.DidPowerGroup = true
    end
    j = 0
    if o == TEAM_ORDER then
        j = OrderBarracksBonuses[l + 1]
    else
        j = ChaosBarracksBonuses[l + 1]
    end
    TableForBarrack.ExpRadius = EXP_GIVEN_RADIUS
    lNumOfMeleeMinionsPerWave = TableForBarrack.NumOfMeleeMinionsPerWave
    lNumOfCasterMinionsPerWave = TableForBarrack.NumOfCasterMinionsPerWave
    lNumOfCannonMinionsPerWave = TableForBarrack.NumOfCannonMinionsPerWave
    lNumOfSuperMinionsPerWave = TableForBarrack.NumOfSuperMinionsPerWave
    if p ~= LAST_WAVE then
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
        LAST_WAVE = p
    end
    if SPECIAL_MINION_MODE == "2MeleeMinions" then
        lNumOfMeleeMinionsPerWave = math.max(2 - lNumOfSuperMinionsPerWave, 0)
        lNumOfCasterMinionsPerWave = 0
        lNumOfCannonMinionsPerWave = 0
    else
        if SPECIAL_MINION_MODE == "None" then
            lNumOfMeleeMinionsPerWave = 0
            lNumOfCasterMinionsPerWave = 0
            lNumOfCannonMinionsPerWave = 0
            lNumOfSuperMinionsPerWave = 0
        end
    end
    if j.WillSpawnSuperMinion == 1 then
        if o == TEAM_ORDER then
            if totalNumberOfChaosBarracks == 0 then
                lNumOfSuperMinionsPerWave = 2
            end
        end
        if o == TEAM_CHAOS then
            if totalNumberOfOrderBarracks == 0 then
                lNumOfSuperMinionsPerWave = 2
            end
        else
            lNumOfSuperMinionsPerWave = 1
        end
        lNumOfCannonMinionsPerWave = 0
    end
    if o == TEAM_ORDER then
        j.MeleeMinionName = OrderNames.MeleeMinionName
        j.CasterMinionName = OrderNames.CasterMinionName
        j.CannonMinionName = OrderNames.CannonMinionName
        j.SuperMinionName = OrderNames.SuperMinionName
    else
        j.MeleeMinionName = ChaosNames.MeleeMinionName
        j.CasterMinionName = ChaosNames.CasterMinionName
        j.CannonMinionName = ChaosNames.CannonMinionName
        j.SuperMinionName = ChaosNames.SuperMinionName
    end
    k = {}
    k.NumOfMeleeMinionsPerWave = lNumOfMeleeMinionsPerWave
    k.NumOfCasterMinionsPerWave = lNumOfCasterMinionsPerWave
    k.NumOfCannonMinionsPerWave = lNumOfCannonMinionsPerWave
    k.NumOfSuperMinionsPerWave = lNumOfSuperMinionsPerWave
    k.WaveSpawnRate = TableForBarrack.WaveSpawnRate
    k.SingleMinionSpawnDelay = TableForBarrack.SingleMinionSpawnDelay
    k.MeleeMinionName = j.MeleeMinionName
    k.CasterMinionName = j.CasterMinionName
    k.CannonMinionName = j.CannonMinionName
    k.SuperMinionName = j.SuperMinionName
    k.IsDestroyed = j.IsDestroyed
    k.MeleeMinionArmor = j.MeleeMinionArmor
    k.MeleeMinionMagicResistance = j.MeleeMinionMagicResistance
    k.MeleeHPBonus = j.MeleeHPBonus
    k.MeleeDamageBonus = j.MeleeDamageBonus
    k.MeleeExpGiven = j.MeleeExpGiven
    k.MeleeGoldGiven = j.MeleeGoldGiven
    k.CasterMinionArmor = j.CasterMinionArmor
    k.CasterMinionMagicResistance = j.CasterMinionMagicResistance
    k.CasterHPBonus = j.CasterHPBonus
    k.CasterDamageBonus = j.CasterDamageBonus
    k.CasterExpGiven = j.CasterExpGiven
    k.CasterGoldGiven = j.CasterGoldGiven
    k.CannonMinionArmor = j.CannonMinionArmor
    k.CannonMinionMagicResistance = j.CannonMinionMagicResistance
    k.CannonHPBonus = j.CannonHPBonus
    k.CannonDamageBonus = j.CannonDamageBonus
    k.CannonExpGiven = j.CannonExpGiven
    k.CannonGoldGiven = j.CannonGoldGiven
    k.SuperMinionArmor = j.SuperMinionArmor
    k.SuperMinionMagicResistance = j.SuperMinionMagicResistance
    k.SuperHPBonus = j.SuperHPBonus
    k.SuperDamageBonus = j.SuperDamageBonus
    k.SuperExpGiven = j.SuperExpGiven
    k.SuperGoldGiven = j.SuperGoldGiven
    k.ExperienceRadius = TableForBarrack.ExpRadius
    ReturnTable = k
    return ReturnTable
end
DeactivateCorrectStructure = function(l, m, n)
end
GetLuaBarracks = function(l, m)
    local g, h, i
    if l == TEAM_ORDER then
        g = OrderBarracksBonuses[m + 1]
    else
        g = ChaosBarracksBonuses[m + 1]
    end
    return g
end
GetDisableMinionSpawnTime = function(l, m)
    local g, h, i
    barrack = GetLuaBarracks(m, l)
    return DISABLE_MINION_SPAWN_BASE_TIME + DISABLE_MINION_SPAWN_MAG_TIME * barrack.NumOfSpawnDisables
end
DisableBarracksSpawn = function(l, m)
    local g, h, i, j, k
    cLangBarracks = GetBarracks(m, l)
    luaBarrack = GetLuaBarracks(m, l)
    i, j, k = GetDisableMinionSpawnTime(l, m)
    SetDisableMinionSpawn(cLangBarracks, i, j, k)
    luaBarrack.NumOfSpawnDisables = luaBarrack.NumOfSpawnDisables + 1
end
BonusesCounter = 0
ApplyBarracksDestructionBonuses = function(l, m)
end
ReductionCounter = 0
ApplyBarracksRespawnReductions = function(l, m)
end
ReactiveCounter = 0
BarrackReactiveEvent = function(l, m)
end
DisactivatedCounter = 0
HandleDestroyedObject = function(l)
    local f
    return true
end
TEAM_UNKNOWN = 0
EOG_PAN_TO_NEXUS_TIME = 3
EOG_NEXUS_EXPLOSION_TIME = EOG_PAN_TO_NEXUS_TIME + 0.5
EOG_SCOREBOARD_PHASE_DELAY_TIME = 3
EOG_NEXUS_REVIVE_TIME = 5
EOG_ALIVE_NEXUS_SKIN = 0
EOG_DESTROYED_NEXUS_SKIN = 1
EndOfGameCeremony = function(l, m)
    local g, h, i, j
    winningTeam = l
    if winningTeam == TEAM_ORDER then
        losingTeam = TEAM_CHAOS
    else
        losingTeam = TEAM_ORDER
    end
    losingHQPosition = GetPosition(m)
    orderHQ = GetHQ(TEAM_ORDER)
    SetInvulnerable(orderHQ, true)
    SetTargetable(orderHQ, false)
    SetBuildingHealthRegenEnabled(orderHQ, false)
    chaosHQ = GetHQ(TEAM_CHAOS)
    SetInvulnerable(chaosHQ, true)
    SetTargetable(chaosHQ, false)
    SetBuildingHealthRegenEnabled(chaosHQ, false)
    SetInputLockFlag(INPUT_LOCK_CAMERA_LOCKING, true)
    SetInputLockFlag(INPUT_LOCK_CAMERA_MOVEMENT, true)
    SetInputLockFlag(INPUT_LOCK_ABILITIES, true)
    SetInputLockFlag(INPUT_LOCK_SUMMONER_SPELLS, true)
    SetInputLockFlag(INPUT_LOCK_MOVEMENT, true)
    SetInputLockFlag(INPUT_LOCK_SHOP, true)
    SetInputLockFlag(INPUT_LOCK_CHAT, true)
    SetInputLockFlag(INPUT_LOCK_MINIMAP_MOVEMENT, true)
    DisableHUDForEndOfGame()
    ToggleBarracks()
    CloseShop()
    HaltAllAI()
    LuaForEachChampion(TEAM_UNKNOWN, "ChampionEoGCeremony")
    InitTimer("DestroyNexusPhase", EOG_NEXUS_EXPLOSION_TIME, false)
end
ChampionEoGCeremony = function(l)
    local f, g, h, i, j
    MoveCameraFromCurrentPositionToPoint(l, losingHQPosition, EOG_PAN_TO_NEXUS_TIME, true)
    SetGreyscaleEnabledWhenDead(l, false)
end
DestroyNexusPhase = function()
    local e, f, g, h
    SetHQCurrentSkin(losingTeam, EOG_DESTROYED_NEXUS_SKIN)
    InitTimer("ScoreboardPhase", EOG_SCOREBOARD_PHASE_DELAY_TIME, false)
end
ScoreboardPhase = function()
    local e, f
    EndGame(winningTeam)
end
TestReviveNexus = function()
    local e, f, g
    SetHQCurrentSkin(losingTeam, EOG_ALIVE_NEXUS_SKIN)
end
