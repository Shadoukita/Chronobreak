local a, b, c, d, e, f, g, h
TUTORIAL_START_TIME = 20
DEFAULT_PROXIMITY_DISTANCE = 100
eventCameraStartPoint = Make3DPoint(10473.7, 9.04187, 7691.42)
playerStartPoint = Make3DPoint(780.75, 111.922, 1309.17)
eventMovingIWaypoint = Make3DPoint(1113.63, 51.8983, 2127.99)
eventMovingIIWaypoint = Make3DPoint(1947.05, -192.531, 3553.85)
fight1PingPosition = Make3DPoint(1602.44, -181.372, 3236.05)
fight2PingPosition = Make3DPoint(1423.57, -186.9, 3257.91)
a = {}
c, d, e, f, g, h = Make3DPoint(1759.03, -187.98, 3192.29)
a[1] = Make3DPoint(1558.27, -184.72, 3339.96)
a[2] = c
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
eventMinionFightSpawnPoints = a
a = {}
e, f, g, h = Make3DPoint(1515.29, -191.947, 3038.1)
a[1] = Make3DPoint(1262.17, -192.748, 3329.35)
a[2] = Make3DPoint(1275, -193.036, 3213.37)
a[3] = Make3DPoint(1379.9, -192.439, 3083.75)
a[4] = e
a[5] = f
a[6] = g
a[7] = h
eventChampionAbilitySpawnPoints = a
a = {}
b, c, d, e, f, g, h = Make3DPoint(3808, 88, 8770)
a[1] = b
a[2] = c
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
eventMinionAttackOrderSpawnTrigger = a
a = {}
e, f, g, h = Make3DPoint(3459.22, 1.15515, 3076.25)
a[1] = Make3DPoint(3271.96, 1.15515, 3854.94)
a[2] = Make3DPoint(2977, 1.15503, 3597.02)
a[3] = Make3DPoint(3771.14, 1.15515, 3321.89)
a[4] = e
a[5] = f
a[6] = g
a[7] = h
orderBasicSpawnPoints = a
a = {}
c, d, e, f, g, h = Make3DPoint(2734.6, 12.6903, 3358.48)
a[1] = Make3DPoint(3239.59, 18.6404, 2784.79)
a[2] = c
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
orderWizardSpawnPoints = a
a = {}
e, f, g, h = Make3DPoint(9248.14, 1.15515, 8666.6)
a[1] = Make3DPoint(8652.07, 1.15503, 8788.48)
a[2] = Make3DPoint(9045.61, 1.15503, 9051.93)
a[3] = Make3DPoint(8995.74, 1.15515, 8459.24)
a[4] = e
a[5] = f
a[6] = g
a[7] = h
eventMinionAttackChaosBasicSpawnPoints = a
a = {}
c, d, e, f, g, h = Make3DPoint(9543.33, 1.15515, 8944.89)
a[1] = Make3DPoint(9220.91, 1.15515, 9256.76)
a[2] = c
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
eventMinionAttackChaosWizardSpawnPoints = a
a = {}
c, d, e, f, g, h = Make3DPoint(11115.2, 57.071, 10171.3)
a[1] = Make3DPoint(10737.9, 57.0709, 10587.1)
a[2] = c
a[3] = d
a[4] = e
a[5] = f
a[6] = g
a[7] = h
chaosWizardSpawnPoints = a
a = {}
e, f, g, h = Make3DPoint(10351.6, 57.071, 10530)
a[1] = Make3DPoint(10669.4, 57.071, 9639.3)
a[2] = Make3DPoint(10959, 57.071, 9841.3)
a[3] = Make3DPoint(10034.9, 57.071, 10239.1)
a[4] = e
a[5] = f
a[6] = g
a[7] = h
chaosBasicSpawnPoints = a
shopPosition = Make3DPoint(524.807, -195.973, 1957.42)
singedGetsOwnedPos = Make3DPoint(6373.83, 1.15527, 6166.6)
a = {}
e, f, g, h = Make3DPoint(10911.6, 57.0711, 10078)
a[1] = Make3DPoint(11540.6, 57.0709, 9980.2)
a[2] = Make3DPoint(11508.3, 57.071, 10381)
a[3] = Make3DPoint(11041, 57.071, 10949.2)
a[4] = e
a[5] = f
a[6] = g
a[7] = h
chaosWizardFinalPoints = a
a = {}
e, f, g, h = Make3DPoint(11320.5, 57.0709, 9765.5)
a[1] = Make3DPoint(10183.3, 57.0709, 10891.9)
a[2] = Make3DPoint(10660.4, 57.0709, 10809.8)
a[3] = Make3DPoint(11283.6, 57.071, 10085.6)
a[4] = e
a[5] = f
a[6] = g
a[7] = h
chaosBasicFinalPoints = a
playerID = 0
firstOrderTurret = 0
minion = 0
botID = 0
allyID = 0
startingMinions = {}
avatarSpellSlot1 = 4
LogTutorial = function(i)
    local j
end
objectiveString = ""
ChangeObjectiveText = function(i)
    local j, k, l, m
    objectiveString = i
    HideObjectiveText()
    InitTimer("ChangeObjectiveTextHelper", 1, false)
end
ChangeObjectiveTextHelper = function()
    local n, j
    if objectiveString ~= "" then
        ShowObjectiveText(objectiveString)
    end
end
OnTutorialInit = function()
    local n, j, k, l, m, o, p, q, r, s, t, u
    LoadLevelScriptIntoScript("BBLuaConversionLibrary.lua")
    LogTutorial("Getting the player ID")
    playerID = GetTutorialPlayer()
    TeleportToPosition(playerID, playerStartPoint)
    n = playerStartPoint
    n.z = playerStartPoint.z - 10
    FaceDirection(playerID, n)
    TutorialSpellBuffAdd(
        playerID,
        playerID,
        "TutorialPlayerBuff",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    LogTutorial("Disabling auto targeting")
    SetCanAutoAcquireTargets(playerID, false)
    LogTutorial("Getting bot ID")
    botID = GetChampionBySkinName("Trundle", TEAM_CHAOS)
    allyID = GetChampionBySkinName("Nunu", TEAM_ORDER)
    LogTutorial("Getting first order turret")
    firstOrderTurret = GetTurret(TEAM_ORDER, CENTER_LANE, FRONT_TOWER)
    secondOrderTurret = GetTurret(TEAM_ORDER, CENTER_LANE, MID_TOWER)
    firstChaosTurret = GetTurret(TEAM_CHAOS, CENTER_LANE, FRONT_TOWER)
    chaosDampener = GetDampener(TEAM_CHAOS, CENTER_LANE)
    SetInvulnerable(secondOrderTurret, true)
    SetTargetable(secondOrderTurret, false)
    SetInvulnerable(firstOrderTurret, false)
    SetTargetable(firstOrderTurret, true)
    SetInvulnerable(firstChaosTurret, true)
    SetTargetable(firstChaosTurret, false)
    SetInvulnerable(chaosDampener, true)
    SetTargetable(chaosDampener, false)
    SetInvulnerable(GetHQ(TEAM_CHAOS), true)
    SetTargetable(GetHQ(TEAM_CHAOS), false)
    TutorialSpellBuffAdd(
        playerID,
        firstOrderTurret,
        "TutorialPlayerBuff",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    TutorialSpellBuffAdd(
        playerID,
        secondOrderTurret,
        "InstaGibAttack",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    LogTutorial("Incrementing base gold")
    IncGold(playerID, 1000, "GOLD_STARTING")
    LogTutorial("Setting up timer to increase bot levels")
    InitTimer("SetBotLevels", 1, false)
    LogTutorial("Locking player input")
    SetInputLockFlag(INPUT_LOCK_CAMERA_LOCKING, true)
    SetInputLockFlag(INPUT_LOCK_CAMERA_MOVEMENT, true)
    SetInputLockFlag(INPUT_LOCK_ABILITIES, true)
    SetInputLockFlag(INPUT_LOCK_SUMMONER_SPELLS, true)
    SetInputLockFlag(INPUT_LOCK_MOVEMENT, true)
    SetInputLockFlag(INPUT_LOCK_SHOP, true)
    SetInputLockFlag(INPUT_LOCK_MINIMAP_MOVEMENT, true)
    TutorialSpellBuffAdd(playerID, botID, "Stun", BUFF_RENEW_EXISTING, BUFF_Internal, 1, 1, 25000, buffVarsTable, 0)
    TutorialSpellBuffAdd(
        playerID,
        botID,
        "SetNoRender",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    TutorialSpellBuffAdd(playerID, allyID, "Stun", BUFF_RENEW_EXISTING, BUFF_Internal, 1, 1, 25000, buffVarsTable, 0)
    TutorialSpellBuffAdd(
        playerID,
        allyID,
        "SetNoRender",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    TutorialSpellBuffAdd(
        playerID,
        allyID,
        "JudicatorIntervention",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    TutorialSpellBuffAdd(
        playerID,
        allyID,
        "TutorialNearSight",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    SetInvulnerable(botID, true)
    LogTutorial("Adding respawn buff")
    LogTutorial("Moving camera to initial flyover point")
    MoveCameraToPoint(playerID, eventCameraStartPoint, eventCameraStartPoint, 0.1)
    LogTutorial("Teleporting")
    TeleportToPosition(allyID, eventCameraStartPoint)
    ToggleFogOfWar()
    LogTutorial("Playing popup dialog")
    OpenTutorialPopup("game_messagebox_text_tutorial", "EventWelcome")
end
SetBotLevels = function()
    local n, j, k
    LogTutorial("Incrementing Ally Exp")
    IncExp(allyID, 90000)
    IncExp(allyID, 1)
    IncExp(allyID, 1)
    IncExp(allyID, 1)
    IncExp(allyID, 1)
    IncExp(allyID, 1)
    IncExp(allyID, 1)
    IncExp(allyID, 1)
    IncExp(allyID, 1)
    LogTutorial("Incrementing Bot Exp")
    IncExp(botID, 30000)
    IncExp(botID, 1)
    IncExp(botID, 1)
    IncExp(botID, 1)
    IncExp(botID, 1)
    IncExp(botID, 1)
end
SpawnPerceptionBubbles = function(i)
    local j, k, l, m, o, p, q, r, s, t
    for m = 500, 16500, 500 do
        AddPosPerceptionBubble(Make3DPoint(m, 107, m), 500, i, false, TEAM_ORDER)
    end
end
SpawnStartingMinions = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v, w, x
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "Dragon",
        "Dragon",
        "minion.lua",
        Make3DPoint(9460, 57.0709, 9190),
        TEAM_CHAOS,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "Cannon duder",
        "Blue_Minion_MechCannon",
        "minion.lua",
        Make3DPoint(9255, 58, 8530.6),
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "Cannon duder",
        "Blue_Minion_MechCannon",
        "minion.lua",
        Make3DPoint(8963.7, 58, 8844.9),
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "Cannon duder",
        "Blue_Minion_MechCannon",
        "minion.lua",
        Make3DPoint(8764.2, 58, 9054),
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "Walker",
        "Red_Minion_Wizard",
        "minion.lua",
        Make3DPoint(8193.92, 1.15503, 8008.48),
        TEAM_CHAOS,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "Walker",
        "Red_Minion_Wizard",
        "minion.lua",
        Make3DPoint(7993.82, 1.15503, 7808.21),
        TEAM_CHAOS,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "Walker",
        "Red_Minion_Basic",
        "minion.lua",
        Make3DPoint(7793.79, 1.15527, 7608.57),
        TEAM_CHAOS,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "Walker",
        "Red_Minion_Basic",
        "minion.lua",
        Make3DPoint(7593.37, 1.15515, 7408.46),
        TEAM_CHAOS,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "Walker",
        "Red_Minion_Basic",
        "minion.lua",
        Make3DPoint(7393, 1.15, 7208),
        TEAM_CHAOS,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "OrderMinion",
        "Blue_Minion_Wizard",
        "minion.lua",
        Make3DPoint(1114.91, 51.8982, 1858.97),
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "OrderMinion",
        "Blue_Minion_Wizard",
        "minion.lua",
        Make3DPoint(1372.04, 51.8981, 2111.01),
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "OrderMinion",
        "Blue_Minion_Basic",
        "minion.lua",
        Make3DPoint(1615.44, 51.8982, 2346.07),
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "OrderMinion",
        "Blue_Minion_Basic",
        "minion.lua",
        Make3DPoint(1852.58, 51.8755, 2588.91),
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    k, l, m, o, p, q, r, s, t, u, v, w, x =
        SpawnMinion(
        "OrderMinion",
        "Blue_Minion_Basic",
        "minion.lua",
        Make3DPoint(2000, 51.8, 2829),
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    table.insert(startingMinions, k, l, m, o, p, q, r, s, t, u, v, w, x)
    for l = 1, #startingMinions, 1 do
        TutorialSpellBuffAdd(
            playerID,
            startingMinions[l],
            "TutorialPlayerBuff",
            BUFF_RENEW_EXISTING,
            BUFF_Internal,
            1,
            1,
            25000,
            buffVarsTable,
            0
        )
    end
end
KillStartingMinions = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v, w, x
    for l = 1, #startingMinions, 1 do
        TutorialSpellBuffAdd(
            playerID,
            startingMinions[l],
            "ExpirationTimer",
            BUFF_RENEW_EXISTING,
            BUFF_CombatEnhancer,
            1,
            1,
            0.1,
            buffVarsTable,
            0
        )
    end
end
EventWelcome = function()
    local n, j, k, l, m, o
    LogTutorial("Creating initial items")
    CreateItem(playerID, 1029, false)
    CreateItem(playerID, 1031, false)
    LogTutorial("Setting up Welcome event.")
    LogTutorial("Teleporting")
    l, m, o = GetHQ(TEAM_ORDER)
    k, l, m, o = GetPosition(l, m, o)
    TeleportToPosition(allyID, k, l, m, o)
    LogTutorial("Moving the camera for " .. 4)
    m, o = GetHQ(TEAM_CHAOS)
    MoveCameraToPoint(playerID, eventCameraStartPoint, GetPosition(m, o), 4)
    InitTimer("EventTheObject", 4, false)
    PlayTutorialAudioEvent("Tutorial_Welcome", "")
end
EventTheObject = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_TheObject", "EventInLoL")
    j, k = GetHQ(TEAM_CHAOS)
    CreateUnitHighlight(j, k)
end
EventInLoL = function()
    local n, j, k, l, m
    SpawnStartingMinions()
    SpawnPerceptionBubbles(18)
    j, k, l, m = GetHQ(TEAM_CHAOS)
    RemoveUnitHighlight(j, k, l, m)
    l, m = GetHQ(TEAM_CHAOS)
    MoveCameraToPoint(playerID, GetPosition(l, m), GetPosition(playerID), 18)
    InitTimer("EventForThisBattle", 14, false)
    PlayTutorialAudioEvent("Tutorial_InLoL", "")
end
EventForThisBattle = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_ForThisBattle", "EventMoveChampion")
    KillStartingMinions()
    ToggleFogOfWar()
    CreateUnitHighlight(playerID)
end
EventMoveChampion = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_MoveChampion", "EventMoveAshe")
    ShowAuxiliaryText("game_tutorial_auxiliary_waypointI")
    LogTutorial("Re-locking camera")
    LockCamera(true)
    SetInputLockFlag(INPUT_LOCK_CAMERA_LOCKING, true)
    LogTutorial("Freeing player movement")
    SetInputLockFlag(INPUT_LOCK_MOVEMENT, false)
    SetInputLockFlag(INPUT_LOCK_MINIMAP_MOVEMENT, false)
end
EventMoveAshe = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v
    PlayTutorialAudioEvent("Tutorial_MoveAshe", "")
    LogTutorial("Removing player highlight")
    RemoveUnitHighlight(playerID)
    ChangeObjectiveText("game_tutorial_objective_waypointI")
    LogTutorial("Spawning first waypoint")
    waypoint1 =
        SpawnMinion(
        "MovingIWaypoint",
        "TestCubeRender",
        "idle.lua",
        eventMovingIWaypoint,
        TEAM_CHAOS,
        false,
        false,
        false,
        true,
        true,
        true,
        nil
    )
    TutorialSpellBuffAdd(
        playerID,
        waypoint1,
        "Waypoint",
        BUFF_RENEW_EXISTING,
        BUFF_CombatEnhancer,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    LogTutorial("Registering nearby callback")
    RegisterOnUnitNearPositionCallback(playerID, eventMovingIWaypoint, DEFAULT_PROXIMITY_DISTANCE, "Event2ndWaypoint")
end
Event2ndWaypoint = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v
    PlayTutorialAudioEvent("Tutorial_2ndWaypoint", "EventFoW")
    HideObjectiveText()
    Move(playerID, eventMovingIWaypoint, 300)
    LogTutorial("Locking movement")
    SetInputLockFlag(INPUT_LOCK_CAMERA_LOCKING, true)
    SetInputLockFlag(INPUT_LOCK_MOVEMENT, true)
    SetInputLockFlag(INPUT_LOCK_MINIMAP_MOVEMENT, true)
    LogTutorial("Remove waypoint1 with expiration timer")
    TutorialSpellBuffAdd(
        playerID,
        waypoint1,
        "ExpirationTimer",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        1,
        buffVarsTable,
        0
    )
    LogTutorial("Panning camera")
    MoveCameraToPoint(playerID, GetPosition(playerID), eventMovingIIWaypoint, 5)
    LogTutorial("Spawning second waypoint")
    waypoint2 =
        SpawnMinion(
        "MovingIIWaypoint",
        "TestCubeRender",
        "idle.lua",
        eventMovingIIWaypoint,
        TEAM_CHAOS,
        false,
        false,
        false,
        true,
        true,
        true,
        nil
    )
    TutorialSpellBuffAdd(
        playerID,
        waypoint2,
        "Waypoint",
        BUFF_RENEW_EXISTING,
        BUFF_CombatEnhancer,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
end
EventFoW = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_FoW", "EventWaypointIIReturnPan")
    LogTutorial("Adding Perception Bubble")
    bubble = AddPosPerceptionBubble(eventMovingIIWaypoint, 100, 8, false, TEAM_ORDER)
end
EventWaypointIIReturnPan = function()
    local n, j, k, l, m
    MoveCameraToPoint(playerID, eventMovingIIWaypoint, GetPosition(playerID), 5)
    InitTimer("EventMoveAshe2", 4.8, false)
end
EventMoveAshe2 = function()
    local n, j, k, l, m
    PlayTutorialAudioEvent("Tutorial_MoveAshe2", "")
    ChangeObjectiveText("game_tutorial_objective_waypointII")
    ShowAuxiliaryText("game_tutorial_auxiliary_waypointII")
    LogTutorial("Locking camera back after pan")
    LockCamera(true)
    LogTutorial("Unlocking movement")
    SetInputLockFlag(INPUT_LOCK_CAMERA_LOCKING, true)
    SetInputLockFlag(INPUT_LOCK_MOVEMENT, false)
    SetInputLockFlag(INPUT_LOCK_MINIMAP_MOVEMENT, false)
    LogTutorial("Setting up event trigger")
    RegisterOnUnitNearPositionCallback(
        playerID,
        eventMovingIIWaypoint,
        DEFAULT_PROXIMITY_DISTANCE,
        "EventMoveAshe2Congrats"
    )
end
EventMoveAshe2Congrats = function()
    local n, j, k, l, m, o, p, q, r, s, t
    LogTutorial("Congratulating player")
    PlayTutorialAudioEvent("ExtraGoodJob", "EventNormalBattle")
    HideObjectiveText()
    LogTutorial("Killing waypoint2 with buff")
    TutorialSpellBuffAdd(
        playerID,
        waypoint2,
        "ExpirationTimer",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        1,
        buffVarsTable,
        0
    )
end
EventNormalBattle = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v, w, x, y, z
    LogTutorial("Entering Minion Fight")
    levelupNotPlayed = true
    PlayTutorialAudioEvent("Tutorial_NormalBattle", "EventNormalBattle2")
    ShowAuxiliaryText("game_tutorial_auxiliary_minion_fight")
    LogTutorial("Getting current player position")
    playerPos = GetPosition(playerID)
    LogTutorial("Shortcircuiting player movement with move block")
    Move(playerID, playerPos, 1000, 0)
    for l = 1, #eventMinionFightSpawnPoints, 1 do
        testbear1 =
            SpawnMinion(
            "MovingIWaypoint",
            "Red_Minion_Wizard",
            "Aggro.lua",
            eventMinionFightSpawnPoints[l],
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
        TutorialSpellBuffAdd(
            playerID,
            testbear1,
            "TutorialMinionBuff",
            BUFF_RENEW_EXISTING,
            BUFF_CombatEnhancer,
            1,
            1,
            25000,
            buffVarsTable,
            0
        )
    end
    TutorialSpellBuffAdd(
        playerID,
        testbear1,
        "TutorialMinionBuff",
        BUFF_RENEW_EXISTING,
        BUFF_CombatEnhancer,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    LogTutorial("Pinging minion")
    PingMinimapPosition(playerID, fight1PingPosition)
    LogTutorial("Registering death callback")
    RegisterOnLastMinionKilledCallback(TEAM_CHAOS, "EventLevelUp")
end
EventNormalBattle2 = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_NormalBattle2", "EventAttackTraining")
end
EventAttackTraining = function()
    local n, j, k
    ShowObjectiveText("game_tutorial_objective_minion_fight")
    PlayTutorialAudioEvent("Tutorial_AttackTraining", "")
end
EventLevelUp = function()
    local n, j, k, l, m, o, p, q, r, s, t
    LogTutorial("Entering level up event")
    levelupNotPlayed = false
    PlayTutorialAudioEvent("Tutorial_LevelUp", "EventEachTime")
    IncExp(playerID, 2000)
    TutorialSpellBuffAdd(
        playerID,
        playerID,
        "DisableHPRegen",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    HideObjectiveText()
    ShowAuxiliaryText("game_tutorial_auxiliary_level_up")
    LogTutorial("Unregistering death callback")
    UnRegisterOnLastMinionKilledCallback(TEAM_CHAOS)
end
EventEachTime = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_EachTime", "EventTrackedIn")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, true, HIGHLIGHT_TYPE_XP, 0, 0)
end
EventTrackedIn = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_TrackedIn", "EventImprove")
end
EventImprove = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_Improve", "EventSpendAbilityPoint")
end
EventSpendAbilityPoint = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_SpendAbilityPoint", "")
    LogTutorial("Unlocking abilities")
    ToggleInputLockFlag(INPUT_LOCK_ABILITIES)
    ShowObjectiveText("game_tutorial_objective_level_up")
    RegisterOnSpellLevelupCallback(1, "EventLevelUpGrats")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_XP, 0, 0)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, true, HIGHLIGHT_TYPE_SPELL_LEVELUP, 1, 0)
end
EventLevelUpGrats = function()
    local n, j, k, l, m, o
    LogTutorial("Congratulating player for leveling up")
    spentAbilityPoint = true
    HideObjectiveText()
    HideAuxiliaryText()
    PlayTutorialAudioEvent("Tutorial_Extra_WellDone1", "EventShowKeyboardTipDialog")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_XP, 0, 0)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_SPELL_LEVELUP, 1, 0)
    LogTutorial("Unregistering levelup callback")
    UnRegisterOnSpellLevelupCallback(1)
end
EventShowKeyboardTipDialog = function()
    local n, j, k, l, m
    ActivateTipDialog(
        playerID,
        "game_tutorial_keyboard_title",
        "game_tutorial_keyboard_text",
        "tipDialogImage_hotkeys.dds"
    )
    RegisterOnTutorialPopupClosedCallback("EventSummonedAnother")
end
EventSummonedAnother = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v, w, x, y, z
    UnRegisterOnTutorialPopupClosedCallback()
    PlayTutorialAudioEvent("Tutorial_SummonedAnother", "EventUseVolley")
    ShowAuxiliaryText("game_tutorial_auxiliary_volley")
    TutorialSpellBuffRemove(playerID, playerID, "DisableHPRegen")
    LogTutorial("Unregistering ability callback")
    UnRegisterOnSpellAvatarCastCallback(avatarSpellSlot1)
    RegisterOnSpellCastCallback(1, "EventVolleyCast")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, true, HIGHLIGHT_TYPE_SPELL, 1, 0)
    for l = 1, #eventChampionAbilitySpawnPoints, 1 do
        testbear1 =
            SpawnMinion(
            "MovingIWaypoint",
            "Red_Minion_Wizard",
            "Aggro.lua",
            eventChampionAbilitySpawnPoints[l],
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
        ApplyDamage(allyID, testbear1, 200)
    end
    LogTutorial("Pinging minion")
    PingMinimapPosition(playerID, fight2PingPosition)
    LogTutorial("Registering death callback")
    killedSecondWave = false
    RegisterOnLastMinionKilledCallback(TEAM_CHAOS, "EventWounded")
end
EventUseVolley = function()
    local n, j, k
    if not killedSecondWave then
        ShowObjectiveText("game_tutorial_objective_volley")
        PlayTutorialAudioEvent("Tutorial_UseVolley", "EventSelectVolley")
    end
end
EventSelectVolley = function()
    local n, j, k
    if not killedSecondWave then
        PlayTutorialAudioEvent("Tutorial_SelectVolley", "EventConfirmVolley")
    end
end
EventConfirmVolley = function()
    local n, j, k
    if not killedSecondWave then
        PlayTutorialAudioEvent("Tutorial_ConfirmVolley", "")
    end
end
EventVolleyCast = function()
    local n, j, k, l, m, o
    UnRegisterOnSpellCastCallback(1)
    HideObjectiveText()
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_SPELL, 1, 0)
end
EventWounded = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_Wounded", "EventTwoSpells")
    ShowAuxiliaryText("game_tutorial_auxiliary_summoner_heal")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_SPELL, 1, 0)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, true, HIGHLIGHT_TYPE_HP, 0, 0)
end
EventTwoSpells = function()
    local n, j, k, l, m, o
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_HP, 0, 0)
    PlayTutorialAudioEvent("Tutorial_TwoSpells", "EventHeal")
end
EventHeal = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_Heal", "Tutorial_Replenish")
end
Tutorial_Replenish = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_Replenish", "")
    ShowObjectiveText("game_tutorial_objective_summoner_heal")
    LogTutorial("Unlocking summoner spells")
    ToggleInputLockFlag(INPUT_LOCK_SUMMONER_SPELLS)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, true, HIGHLIGHT_TYPE_AVATAR_SPELL, 0, 0)
    RegisterOnSpellAvatarCastCallback(avatarSpellSlot1, "EventSummonerHealCongrats")
end
EventSummonerHealCongrats = function()
    local n, j, k, l, m, o
    LogTutorial("Congratulating player")
    PlayTutorialAudioEvent("Tutorial_Extra_GoodJob1", "EventReturnPlatform")
    UnRegisterOnSpellAvatarCastCallback(avatarSpellSlot1, "Tutorial_Replenish")
    HideObjectiveText()
    HideAuxiliaryText()
    castSummonerHeal = true
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_HP, 0, 0)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_AVATAR_SPELL, 0, 0)
end
EventMoreDamage = function()
    local n, j, k, l, m, o, p, q, r, s, t
    HideObjectiveText()
    LogTutorial("Unregistering death callback")
    UnRegisterOnLastMinionKilledCallback(TEAM_CHAOS)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, true, HIGHLIGHT_TYPE_HP, 0, 0)
    TutorialSpellBuffAdd(
        playerID,
        playerID,
        "DisableHPRegen",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    killedSecondWave = true
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_SPELL, 1, 0)
    UnRegisterOnSpellCastCallback(1)
    EventReturnPlatform()
end
EventReturnPlatform = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_ReturnPlatform", "EventCastRecall")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_HP, 0, 0)
    PingMinimapPosition(playerID, playerStartPoint)
end
EventCastRecall = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_CastRecall", "EventRapidReplenish")
    ShowObjectiveText("game_tutorial_objective_recall")
    ShowAuxiliaryText("game_tutorial_auxiliary_recall")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, true, HIGHLIGHT_TYPE_RECALL, 0, 0)
end
EventRapidReplenish = function()
    local n, j, k, l, m
    PlayTutorialAudioEvent("Tutorial_RapidReplenish", "")
    LogTutorial("Registering recall point")
    RegisterOnUnitNearPositionCallback(playerID, playerStartPoint, 500, "EventGold")
end
EventGold = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_Gold", "EventGainGold")
    HideObjectiveText()
    LogTutorial("Locking movement")
    SetInputLockFlag(INPUT_LOCK_MOVEMENT, true)
    SetInputLockFlag(INPUT_LOCK_MINIMAPMOVEMENT, true)
    LogTutorial("While also moving the player")
    Move(playerID, playerStartPoint, 300, 0)
    ShowAuxiliaryText("game_tutorial_auxiliary_shop_intro")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_RECALL, 0, 0)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, true, HIGHLIGHT_TYPE_GOLD, 0, 0)
    TutorialSpellBuffRemove(playerID, playerID, "DisableHPRegen")
    RegisterOnShopOpenedCallback("EventPowerEquipment")
end
EventGainGold = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_GainGold", "EventSteadyGold")
end
EventSteadyGold = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_SteadyGold", "EventSpendGold")
end
EventSpendGold = function()
    local n, j, k, l, m, o, p, q, r, s, t
    PlayTutorialAudioEvent("Tutorial_SpendGold", "")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_GOLD, 0, 0)
    TutorialSpellBuffAdd(
        playerID,
        playerID,
        "TutorialShoppingTime",
        BUFF_RENEW_EXISTING,
        BUFF_Interal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    LogTutorial("Unlocking Item Shop")
    ToggleInputLockFlag(INPUT_LOCK_SHOP)
    PingMinimapPosition(playerID, shopPosition)
    ChangeObjectiveText("game_tutorial_objective_shop_intro")
end
EventPowerEquipment = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_PowerEquipment", "EventClothArmor")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_GOLD, 0, 0)
    ChangeObjectiveText("game_tutorial_objective_shop_thornmail")
    ShowAuxiliaryText("game_tutorial_auxiliary_shop_thornmail")
    LogTutorial("Registering item purchase callback")
    thornmailPurchased = false
    RegisterOnItemPurchasedCallback(3075, "EventHighAttackSpeed")
end
EventClothArmor = function()
    local n, j, k
    if not thornmailPurchased then
        PlayTutorialAudioEvent("Tutorial_ClothArmor", "EventThornmail")
    end
end
EventThornmail = function()
    local n, j, k
    if not thornmailPurchased then
        PlayTutorialAudioEvent("Tutorial_Thornmail", "EventPurchaseThornmail")
    end
end
EventPurchaseThornmail = function()
    local n, j, k, l, m, o
    if not thornmailPurchased then
        PlayTutorialAudioEvent("Tutorial_PurchaseThornmail", "")
        ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_SHOP, true, HIGHLIGHT_TYPE_RECOMMENDED_ITEM, 0, 0)
    end
end
EventHighAttackSpeed = function()
    local n, j, k, l, m, o
    HideObjectiveText()
    PlayTutorialAudioEvent("Tutorial_HighAttackSpeed", "")
    thornmailPurchased = true
    LogTutorial("Unregistering Item callback")
    UnRegisterOnItemPurchasedCallback(3075)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_SHOP, false, HIGHLIGHT_TYPE_RECOMMENDED_ITEM, 0, 0)
    InitTimer("EventEnemyDefenses", 8, false)
end
EventEnemyDefenses = function()
    local n, j, k, l
    PlayTutorialAudioEvent("Tutorial_EnemyDefenses", "EventUnlockCamera")
    SetInputLockFlag(INPUT_LOCK_CAMERA_LOCKING, false)
    SetInputLockFlag(INPUT_LOCK_CAMERA_MOVEMENT, false)
    LogTutorial("Closing shop window")
    CloseShop()
    LogTutorial("Enabling auto targeting")
    SetCanAutoAcquireTargets(playerID, true)
    LogTutorial("Moving Ally")
    TeleportToPosition(allyID, singedGetsOwnedPos)
    TutorialSpellBuffRemove(playerID, allyID, "SetNoRender")
    continueNunuPinging = 1
    PingNunuRepeatedly()
end
PingNunuRepeatedly = function()
    local n, j, k, l
    if continueNunuPinging == 1 then
        PingMinimapUnit(playerID, allyID)
        TutorialSpellBuffRemove(playerID, allyID, "TutorialNearSight")
        InitTimer("PingNunuRepeatedly", 10, false)
    end
end
EventUnlockCamera = function()
    local n, j, k, l, m, o
    PlayTutorialAudioEvent("Tutorial_UnlockCamera", "")
    ShowObjectiveText("game_tutorial_objective_camera_movement")
    ShowAuxiliaryText("game_tutorial_auxiliary_camera_movement")
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_TITANBAR, true, HIGHLIGHT_TYPE_CAMERA, 0, 0)
    RegisterOnCameraNearUnitCallback(playerID, allyID, 950, "EventOhNo")
end
EventOhNo = function()
    local n, j, k, l, m, o, p, q, r, s, t
    HideObjectiveText()
    SetInvulnerable(allyID, false)
    SetInvulnerable(firstChaosTurret, false)
    SetTargetable(firstChaosTurret, true)
    SetInvulnerable(firstOrderTurret, false)
    LockCamera(false)
    continueNunuPinging = 0
    SetInputLockFlag(INPUT_LOCK_CAMERA_LOCKING, true)
    SetInputLockFlag(INPUT_LOCK_CAMERA_MOVEMENT, true)
    MoveCameraFromCurrentPositionToPoint(playerID, GetPosition(firstChaosTurret), 3, false)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_TITANBAR, false, HIGHLIGHT_TYPE_CAMERA, 0, 0)
    TutorialSpellBuffAdd(
        playerID,
        playerID,
        "TutorialPhase2",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        1,
        buffVarsTable,
        0
    )
    PlayTutorialAudioEvent("Tutorial_OhNo", "EventNoSupport")
    TutorialSpellBuffRemove(playerID, allyID, "JudicatorIntervention")
    TutorialSpellBuffRemove(playerID, allyID, "Stun")
    RegisterOnUnitBelowPercentHealthCallback(allyID, 0.5, "EnemyMinionSetup")
end
EnemyMinionSetup = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v, w, x, y, z
    LogTutorial("Unregistering callback")
    UnRegisterOnUnitBelowPercentHealthCallback(allyID)
    turretAlive = true
    LogTutorial("Spawning Minions")
    for l = 1, #eventMinionAttackChaosBasicSpawnPoints, 1 do
        minion =
            SpawnMinion(
            "ChaosMinion",
            "Red_Minion_Basic",
            "minion.lua",
            eventMinionAttackChaosBasicSpawnPoints[l],
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
        RegisterOnUnitKilledCallback(minion, "ReplaceBasicChaosMinion")
    end
    for l = 1, #eventMinionAttackChaosWizardSpawnPoints, 1 do
        minion =
            SpawnMinion(
            "ChaosMinion",
            "Red_Minion_Wizard",
            "minion.lua",
            eventMinionAttackChaosWizardSpawnPoints[l],
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
        RegisterOnUnitKilledCallback(minion, "ReplaceWizardChaosMinion")
    end
    RegisterOnUnitKilledCallback(allyID, "PostDeathBubble")
end
PostDeathBubble = function()
    local n, j, k, l, m, o
    UnRegisterOnUnitKilledCallback(allyID)
    SetInputLockFlag(INPUT_LOCK_CAMERA_LOCKING, false)
    SetInputLockFlag(INPUT_LOCK_CAMERA_MOVEMENT, false)
    AddPosPerceptionBubble(GetPosition(firstChaosTurret), 1000, 4, false, TEAM_ORDER)
    InitTimer("CleanupInAisle", 4, false)
end
CleanupInAisle = function()
    local n, j, k, l, m, o, p, q, r, s, t
    TutorialSpellBuffAdd(
        playerID,
        allyID,
        "SetNoRender",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        25000,
        buffVarsTable,
        0
    )
    TutorialSpellBuffAdd(playerID, allyID, "Stun", BUFF_RENEW_EXISTING, BUFF_Internal, 1, 1, 25000, buffVarsTable, 0)
    l, m, o, p, q, r, s, t = GetHQ(TEAM_ORDER)
    k, l, m, o, p, q, r, s, t = GetPosition(l, m, o, p, q, r, s, t)
    TeleportToPosition(allyID, k, l, m, o, p, q, r, s, t)
end
EventNoSupport = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_NoSupport", "EventBeware")
end
EventBeware = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_Beware", "EventDefendingFriendlies")
end
EventDefendingFriendlies = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_DefendingFriendlies", "EventTakeUpArms")
end
EventTakeUpArms = function()
    local n, j, k, l, m
    PlayTutorialAudioEvent("Tutorial_TakeUpArms", "EventHeadOn")
    LockCamera(true)
    LogTutorial("Unlocking movement")
    SetInputLockFlag(INPUT_LOCK_MOVEMENT, false)
    SetInputLockFlag(INPUT_LOCK_MINIMAP_MOVEMENT, false)
    SetInvulnerable(firstChaosTurret, true)
    SetTargetable(firstChaosTurret, false)
    turretPos = GetPosition(firstOrderTurret)
    RegisterOnUnitNearUnitCallback(playerID, firstOrderTurret, 1000, "EventMinionAttack")
    RegisterOnUnitNearUnitCallback(playerID, secondOrderTurret, 700, "EventStayBehind")
end
EventHeadOn = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_HeadOn", "")
    ShowObjectiveText("game_tutorial_objective_minion_attack")
    ShowAuxiliaryText("game_tutorial_auxiliary_minion_attack")
    continueHeadOnPinging = 1
    PingHeadOnRepeatedly()
end
PingHeadOnRepeatedly = function()
    local n, j, k, l
    if continueHeadOnPinging == 1 then
        k, l = GetPosition(firstOrderTurret)
        PingMinimapPosition(playerID, k, l)
        InitTimer("PingHeadOnRepeatedly", 10, false)
    end
end
SpawnOrderMinions = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v, w, x, y, z
    LogTutorial("Spawning order minions")
    for l = 1, #orderBasicSpawnPoints, 1 do
        minion =
            SpawnMinion(
            "OrderMinion",
            "Blue_Minion_Basic",
            "minion.lua",
            orderBasicSpawnPoints[l],
            TEAM_ORDER,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
        RegisterOnUnitKilledCallback(minion, "ReplaceBasicOrderMinion")
    end
    for l = 1, #orderWizardSpawnPoints, 1 do
        minion =
            SpawnMinion(
            "OrderMinion",
            "Blue_Minion_Wizard",
            "minion.lua",
            orderWizardSpawnPoints[l],
            TEAM_ORDER,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
        RegisterOnUnitKilledCallback(minion, "ReplaceWizardOrderMinion")
    end
end
EventStayBehind = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_StayBehind", "EventTakeTheBrunt")
    continueHeadOnPinging = 0
    SpawnOrderMinions()
end
EventTakeTheBrunt = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_TakeTheBrunt", "")
end
ReplaceBasicOrderMinion = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v
    LogTutorial("Getting player position for ReplaceMinion order")
    playerPos = GetPosition(playerID)
    playerPos.x = playerPos.x - 1000
    playerPos.z = playerPos.z - 1000
    duder =
        SpawnMinion(
        "OrderMinion",
        "Blue_Minion_Basic",
        "minion.lua",
        playerPos,
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    RegisterOnUnitKilledCallback(duder, "ReplaceBasicOrderMinion")
end
ReplaceBasicChaosMinion = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v
    if turretAlive == true then
        LogTutorial("Getting player position for ReplaceMinion order")
        playerPos = GetPosition(firstOrderTurret)
        playerPos.x = playerPos.x + 1000
        playerPos.z = playerPos.z + 1000
        duder =
            SpawnMinion(
            "OrderMinion",
            "Red_Minion_Basic",
            "minion.lua",
            playerPos,
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
        RegisterOnUnitKilledCallback(duder, "ReplaceBasicChaosMinion")
    end
end
ReplaceWizardOrderMinion = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v
    LogTutorial("Getting player position for ReplaceMinion order")
    playerPos = GetPosition(playerID)
    playerPos.x = playerPos.x - 1000
    playerPos.z = playerPos.z - 1000
    dudette =
        SpawnMinion(
        "OrderMinion",
        "Blue_Minion_Wizard",
        "minion.lua",
        playerPos,
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    RegisterOnUnitKilledCallback(dudette, "ReplaceWizardOrderMinion")
end
ReplaceWizardChaosMinion = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v
    if turretAlive == true then
        LogTutorial("Getting player position for ReplaceMinion order")
        playerPos = GetPosition(firstOrderTurret)
        playerPos.x = playerPos.x + 1000
        playerPos.z = playerPos.z + 1000
        duder =
            SpawnMinion(
            "OrderMinion",
            "Red_Minion_Wizard",
            "minion.lua",
            playerPos,
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
        RegisterOnUnitKilledCallback(duder, "ReplaceWizardChaosMinion")
    end
end
SpawnChaosMinions = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v, w, x, y, z
    LogTutorial("Spawning chaos minions")
    for l = 1, #chaosBasicSpawnPoints, 1 do
        SpawnMinion(
            "ChaosMinion",
            "Red_Minion_Basic",
            "minion.lua",
            chaosBasicSpawnPoints[l],
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
    end
    for l = 1, #chaosWizardSpawnPoints, 1 do
        SpawnMinion(
            "ChaosMinion",
            "Red_Minion_Wizard",
            "minion.lua",
            chaosWizardSpawnPoints[l],
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
    end
end
EventMinionAttack = function()
    local n, j, k, l
    LogTutorial("Entering Minion Fight")
    LogTutorial("Applying fatal damage to order turret")
    ApplyDamage(minion, firstOrderTurret, 20000)
    SetInvulnerable(secondOrderTurret, false)
    SetTargetable(secondOrderTurret, true)
    turretAlive = false
    LogTutorial("Registering callback for minion death")
    RegisterOnLastMinionKilledCallback(TEAM_CHAOS, "EventTurnedTide")
end
EventTurnedTide = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_TurnedTide", "EventPressForward")
    ChangeObjectiveText("game_tutorial_objective_turret_push")
    ShowAuxiliaryText("game_tutorial_auxiliary_turret_push")
    SetInvulnerable(firstChaosTurret, false)
    SetTargetable(firstChaosTurret, true)
    UnRegisterOnLastMinionKilledCallback(TEAM_CHAOS)
    RegisterOnUnitKilledCallback(firstChaosTurret, "EventTurretDeath")
end
EventPressForward = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_PressForward", "EventRememberToUse")
end
EventRememberToUse = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_RememberToUse", "")
end
EventTurretDeath = function()
    local n, j, k, l, m
    PlayTutorialAudioEvent("Tutorial_Extra_Commendable2", "EventSupportOfOthers")
    ChangeObjectiveText("game_tutorial_objective_champion_setup")
    ShowAuxiliaryText("game_tutorial_auxiliary_champion_setup")
    LogTutorial("Unstunning champion")
    TutorialSpellBuffRemove(playerID, botID, "Stun")
    TutorialSpellBuffRemove(playerID, botID, "SetNoRender")
    LogTutorial("Spawning Chaos Minions")
    SpawnChaosMinions()
    RegisterOnCameraNearUnitCallback(playerID, botID, 1200, "EventLookOut")
end
EventSupportOfOthers = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_SupportOfOthers", "EventLayWaste")
end
EventLayWaste = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_LayWaste", "")
end
EventLookOut = function()
    local n, j, k, l, m, o, p, q, r, s, t
    TutorialSpellBuffAdd(
        playerID,
        playerID,
        "TutorialPhase2",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        1,
        buffVarsTable,
        0
    )
    PlayTutorialAudioEvent("Tutorial_LookOut", "EventEnemyChampion")
    SetInvulnerable(botID, false)
    ChangeObjectiveText("game_tutorial_objective_champion_battle")
    ShowAuxiliaryText("game_tutorial_auxiliary_champion_battle")
    CreateUnitHighlight(botID)
    RegisterOnUnitKilledCallback(botID, "EventGreatWork")
end
EventEnemyChampion = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_EnemyChampion", "EventGreaterThreat")
end
EventGreaterThreat = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_GreaterThreat", "EventControlledByAnother")
    RemoveUnitHighlight(botID)
end
EventControlledByAnother = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_ControlledByAnother", "")
end
EventGreatWork = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_GreatWork", "EventBecomingALegend")
    HideObjectiveText()
    HideAuxiliaryText()
    SetInvulnerable(chaosDampener, false)
    SetTargetable(chaosDampener, true)
    LogTutorial("Unregistering callback")
    UnRegisterOnUnitKilledCallback(botID)
end
EventBecomingALegend = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_BecomingALegend", "EventDestroyInhibitor")
end
EventDestroyInhibitor = function()
    local n, j, k, l
    PlayTutorialAudioEvent("Tutorial_DestroyInhibitor", "")
    LogTutorial("Spawning Chaos Minions")
    SpawnChaosMinions()
    ShowObjectiveText("game_tutorial_objective_inhibitor_push")
    ShowAuxiliaryText("game_tutorial_auxiliary_inhibitor_push")
    LogTutorial("Pinging inhibitor")
    k, l = GetPosition(chaosDampener)
    PingMinimapPosition(playerID, k, l)
    RegisterOnUnitKilledCallback(chaosDampener, "EventInhibitorDisabled")
end
EventInhibitorDisabled = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v
    TutorialSpellBuffAdd(
        playerID,
        playerID,
        "TutorialPhase2",
        BUFF_RENEW_EXISTING,
        BUFF_Internal,
        1,
        1,
        1,
        buffVarsTable,
        0
    )
    PlayTutorialAudioEvent("Tutorial_InhibitorDisabled", "EventClaimVictory")
    playerPos = GetPosition(playerID)
    playerPos.x = playerPos.x - 1000
    playerPos.z = playerPos.z - 1000
    super =
        SpawnMinion(
        "OrderMinion",
        "Blue_Minion_MechMelee",
        "minion.lua",
        playerPos,
        TEAM_ORDER,
        false,
        false,
        false,
        false,
        false,
        false,
        nil
    )
    SetInvulnerable(GetHQ(TEAM_CHAOS), false)
    SetTargetable(GetHQ(TEAM_CHAOS), true)
    CreateUnitHighlight(super)
    ChangeObjectiveText("game_tutorial_objective_HQ_push")
    ShowAuxiliaryText("game_tutorial_auxiliary_HQ_push")
    LogTutorial("Unregistering callback")
    UnRegisterOnUnitKilledCallback(chaosDampener)
    InitTimer("MinionSwarm", 8, false)
    RegisterOnUnitBelowPercentHealthCallback(GetHQ(TEAM_CHAOS), 0.4, "EventAlmostThere")
end
MinionSwarm = function()
    local n, j, k, l, m, o, p, q, r, s, t, u, v, w, x, y, z
    LogTutorial("Swarming minions")
    for l = 1, #chaosBasicFinalPoints, 1 do
        SpawnMinion(
            "ChaosMinion",
            "Red_Minion_Basic",
            "minion.lua",
            chaosBasicFinalPoints[l],
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
    end
    for l = 1, #chaosWizardFinalPoints, 1 do
        SpawnMinion(
            "ChaosMinion",
            "Red_Minion_Wizard",
            "minion.lua",
            chaosWizardFinalPoints[l],
            TEAM_CHAOS,
            false,
            false,
            false,
            false,
            false,
            false,
            nil
        )
    end
end
EventClaimVictory = function()
    local n, j, k
    LogTutorial("Claim victory!")
    PlayTutorialAudioEvent("Tutorial_ClaimVictory", "")
    RemoveUnitHighlight(super)
end
EventAlmostThere = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_AlmostThere", "EventFinishOff")
    LogTutorial("Almost there!")
    RegisterOnUnitKilledCallback(GetHQ(TEAM_CHAOS), "EventSucceedDelay")
end
EventFinishOff = function()
    local n, j, k
    LogTutorial("Finish off!")
    PlayTutorialAudioEvent("Tutorial_FinishOff", "")
end
EventSucceedDelay = function()
    local n, j, k, l
    InitTimer("EventSucceededCombined", 6, false)
end
EventSucceededCombined = function()
    local n, j, k
    PlayTutorialAudioEvent("Tutorial_SucceededCombined", "")
    HideObjectiveText()
    HideAuxiliaryText()
    MuteVolumeCategory(VOLUME_MUSIC, true)
    MuteVolumeCategory(VOLUME_SFX, true)
    MuteVolumeCategory(VOLUME_ANNOUNCER, true)
    MuteVolumeCategory(VOLUME_UNIT_RESPONSES, true)
end
EventEndGame = function()
    local n, j
    ResumeGame()
    EndGame(TEAM_ORDER)
end
GenericLuaCallbackTest = function()
    local n, j
    ToggleFogOfWar()
end
InitialFlyover = function()
    local n, j, k, l, m, o
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_SHOP, true, HIGHLIGHT_TYPE_RECOMMENDED_ITEM, 4, 0)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_TITANBAR, true, HIGHLIGHT_TYPE_CAMERA, 0, 0)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, true, HIGHLIGHT_TYPE_SPELL, 1, 0)
    HighlightHUDElement(HIGHLIGHT_TYPE_HP, 0)
    ReplaceObjectiveText("game_tutorial_objective_waypointII")
    RefreshAuxiliaryText("game_tutorial_auxiliary_waypointII")
end
TutorialPhase1 = function()
    local n, j, k, l, m, o
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_SHOP, false, HIGHLIGHT_TYPE_RECOMMENDED_ITEM, 4, 0)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_TITANBAR, false, HIGHLIGHT_TYPE_CAMERA, 0, 0)
    ToggleUIHighlight(HIGHLIGHT_UI_ELEMENT_HUD, false, HIGHLIGHT_TYPE_SPELL, 1, 0)
    HideAuxiliaryText()
    HideObjectiveText()
end
TutorialPhase2 = function()
    local n, j
    ShowObjectiveText("game_tutorial_objective_volley")
    ShowAuxiliaryText("game_tutorial_auxiliary_volley")
end
TutorialPhaseFinal = function()
    local n, j
end
