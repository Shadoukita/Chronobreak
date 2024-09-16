local L0_1, L1_1, L2_1, L3_1, L4_1, L5_1, L6_1, L7_1, L8_1, L9_1, L10_1, L11_1, L12_1, L13_1, L14_1, L15_1, L16_1, L17_1, L18_1, L19_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = SetState
  L1_2 = AI_HARDIDLE
  L0_2(L1_2)
  L0_2 = InitTimer
  L1_2 = "TimerFindEnemies"
  L2_2 = 0.15
  L3_2 = true
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = false
  return L0_2
end
OnInit = L0_1
L0_1 = {}
turretTargetList = L0_1
L0_1 = {}
L0_1[0] = "AI_IDLE"
L1_1 = "AI_SOFTATTACK"
L2_1 = "AI_HARDATTACK"
L3_1 = "AI_ATTACKMOVESTATE"
L4_1 = "AI_STANDING"
L5_1 = "AI_MOVE"
L6_1 = "AI_GUARD"
L7_1 = "AI_ATTACK"
L8_1 = "AI_RETREAT"
L9_1 = "AI_HARDIDLE"
L10_1 = "AI_HARDIDLE_ATTACKING"
L11_1 = "AI_TAUNTED"
L12_1 = "AI_ATTACKMOVE_ATTACKING"
L13_1 = "AI_FEARED"
L14_1 = "AI_CHARMED"
L15_1 = "AI_FLEEING"
L16_1 = "AI_ATTACK_GOING_TO_LAST_KNOWN_LOCATION"
L17_1 = "AI_HALTED"
L18_1 = "AI_SIEGEATTACK"
L19_1 = "AI_LAST_NONPET_AI_STATE"
L0_1[1] = L1_1
L0_1[2] = L2_1
L0_1[3] = L3_1
L0_1[4] = L4_1
L0_1[5] = L5_1
L0_1[6] = L6_1
L0_1[7] = L7_1
L0_1[8] = L8_1
L0_1[9] = L9_1
L0_1[10] = L10_1
L0_1[11] = L11_1
L0_1[12] = L12_1
L0_1[13] = L13_1
L0_1[14] = L14_1
L0_1[15] = L15_1
L0_1[16] = L16_1
L0_1[17] = L17_1
L0_1[18] = L18_1
L0_1[19] = L19_1
DEBUG_MINION_STATE_TABLE = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = GetState
  L2_2 = L2_2()
  L3_2 = AI_HALTED
  if L2_2 == L3_2 then
    L3_2 = false
    return L3_2
  end
  L3_2 = turretTargetList
  L3_2 = #L3_2
  if 0 < L3_2 then
    L3_2 = turretTargetList
    L3_2 = L3_2[1]
    newTarget = L3_2
  else
    L3_2 = FindTargetInAcR
    L3_2 = L3_2()
    newTarget = L3_2
  end
  L3_2 = newTarget
  if L3_2 == nil then
    L3_2 = AI_HARDIDLE_ATTACKING
    if L2_2 ~= L3_2 then
      L3_2 = AI_TAUNTED
    end
    if L2_2 == L3_2 then
      L3_2 = NetSetState
      L4_2 = AI_HARDIDLE
      L3_2(L4_2)
      L3_2 = true
      return L3_2
    end
  else
    if L2_2 == AI_HARDIDLE_ATTACKING or L2_2 == AI_TAUNTED then
      NetSetState(AI_HARDIDLE_ATTACKING)
      SetTarget(newTarget)
      return true
    end
  end
  NetSetState(AI_HARDIDLE)
  return true
end
OnTargetLost = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = GetState
  L2_2 = L2_2()
  L3_2 = AI_HALTED
  if L2_2 == L3_2 then
    return
  end
  if A1_2 then
    L3_2 = AI_HARDIDLE
    if L2_2 ~= L3_2 then
      L3_2 = AI_HARDIDLE_ATTACKING
      if L2_2 ~= L3_2 then
        return
      end
    end
    L3_2 = NetSetState
    L4_2 = AI_HARDIDLE_ATTACKING
    L3_2(L4_2)
    L3_2 = SetTarget
    L4_2 = A1_2
    L3_2(L4_2)
  end
end
OnCallForHelp = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2
  L2_2 = ObjectInAttackRange
  L3_2 = A1_2
  L2_2 = L2_2(L3_2)
  if L2_2 then
    L2_2 = PutTargetInTargetList
    L3_2 = A1_2
    L2_2(L3_2)
  end
end
OnReceiveImportantCallForHelp = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = GetTauntTarget
  L1_2 = L1_2()
  tauntTarget = L1_2
  L1_2 = tauntTarget
  if L1_2 ~= nil then
    L1_2 = NetSetState
    L2_2 = AI_TAUNTED
    L1_2(L2_2)
    L1_2 = SetTarget
    L2_2 = tauntTarget
    L1_2(L2_2)
  end
end
OnTauntBegin = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = GetTauntTarget
  L1_2 = L1_2()
  tauntTarget = L1_2
  L1_2 = tauntTarget
  if L1_2 ~= nil then
    L1_2 = NetSetState
    L2_2 = AI_HARDIDLE_ATTACKING
    L1_2(L2_2)
    L1_2 = SetTarget
    L2_2 = tauntTarget
    L1_2(L2_2)
  else
    L1_2 = NetSetState
    L2_2 = AI_HARDIDLE
    L1_2(L2_2)
    L1_2 = TimerFindEnemies
    L1_2()
  end
end
OnTauntEnd = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = NetSetState
  L2_2 = AI_HARDIDLE
  L1_2(L2_2)
  L1_2 = TimerFindEnemies
  L1_2()
end
OnCanMove = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = NetSetState
  L2_2 = AI_HARDIDLE
  L1_2(L2_2)
  L1_2 = TimerFindEnemies
  L1_2()
end
OnCanAttack = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = UpdateTargetList
  L1_2()
  L1_2 = AI_HARDIDLE
  if L0_2 == L1_2 then
    L1_2 = turretTargetList
    L1_2 = #L1_2
    if 0 < L1_2 then
      L1_2 = turretTargetList
      L1_2 = L1_2[1]
      newTarget = L1_2
    else
      L1_2 = FindTargetInAcR
      L1_2 = L1_2()
      newTarget = L1_2
    end
    L1_2 = newTarget
    if L1_2 == nil then
      L1_2 = TurnOffAutoAttack
      L2_2 = STOPREASON_TARGET_LOST
      L1_2(L2_2)
      return
    end
    L1_2 = AI_HARDIDLE
    if L0_2 == L1_2 then
      L1_2 = NetSetState
      L2_2 = AI_HARDIDLE_ATTACKING
      L1_2(L2_2)
      L1_2 = SetTarget
      L2_2 = newTarget
      L1_2(L2_2)
    end
  end
  L1_2 = AI_HARDIDLE_ATTACKING
  if L0_2 ~= L1_2 then
    L1_2 = AI_TAUNTED
    if L0_2 ~= L1_2 then
      return
    end
  end
  L1_2 = TargetInAttackRange
  L1_2 = L1_2()
  if L1_2 then
    L1_2 = TurnOnAutoAttack
    L2_2 = GetTarget
    L2_2 = L2_2()
    L1_2(L2_2)
  else
    L1_2 = NetSetState
    L2_2 = AI_HARDIDLE
    L1_2(L2_2)
    L1_2 = TurnOffAutoAttack
    L2_2 = STOPREASON_MOVING
    L1_2(L2_2)
  end
end
TimerFindEnemies = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2, L4_2, L5_2, L6_2
  L1_2 = false
  L2_2 = 1
  L3_2 = turretTargetList
  L3_2 = #L3_2
  L4_2 = 1
  for L5_2 = L2_2, L3_2, L4_2 do
    L6_2 = turretTargetList
    L6_2 = L6_2[L5_2]
    if L6_2 == A0_2 then
      L1_2 = true
      break
    end
  end
  if not L1_2 then
    L2_2 = turretTargetList
    L3_2 = turretTargetList
    L3_2 = #L3_2
    L3_2 = L3_2 + 1
    L2_2[L3_2] = A0_2
  end
end
PutTargetInTargetList = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2, L4_2, L5_2, L6_2, L7_2
  L0_2 = 1
  while true do
    L1_2 = turretTargetList
    L1_2 = #L1_2
    if not (L0_2 <= L1_2) then
      break
    end
    L1_2 = ObjectInAttackRange
    L2_2 = turretTargetList
    L2_2 = L2_2[L0_2]
    L1_2 = L1_2(L2_2)
    if not L1_2 then
      L1_2 = L0_2 + 1
      L2_2 = turretTargetList
      L2_2 = #L2_2
      L3_2 = 1
      for L4_2 = L1_2, L2_2, L3_2 do
        L5_2 = turretTargetList
        L6_2 = L4_2 - 1
        L7_2 = turretTargetList
        L7_2 = L7_2[L4_2]
        L5_2[L6_2] = L7_2
      end
      L1_2 = turretTargetList
      L2_2 = turretTargetList
      L2_2 = #L2_2
      L1_2[L2_2] = nil
    else
      L0_2 = L0_2 + 1
    end
  end
end
UpdateTargetList = L0_1
function L0_1()
  local L0_2, L1_2
  L0_2 = StopTimer
  L1_2 = "TimerFindEnemies"
  L0_2(L1_2)
  L0_2 = TurnOffAutoAttack
  L1_2 = STOPREASON_IMMEDIATELY
  L0_2(L1_2)
  L0_2 = NetSetState
  L1_2 = AI_HALTED
  L0_2(L1_2)
end
HaltAI = L0_1
