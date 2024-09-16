local L0_1, L1_1
L0_1 = 2500
MAX_ENGAGE_DISTANCE = L0_1
L0_1 = 500
FEAR_WANDER_DISTANCE = L0_1
L0_1 = 0.25
DELAY_FIND_ENEMIES = L0_1
L0_1 = DoLuaShared
L1_1 = "Minions"
L0_1(L1_1)
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
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
    L1_2 = SetStateAndCloseToTarget
    L2_2 = AI_TAUNTED
    L3_2 = tauntTarget
    L1_2(L2_2, L3_2)
    L1_2 = StopTimer
    L2_2 = "TimerAntiKite"
    L1_2(L2_2)
  end
end
OnTauntBegin = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
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
    L1_2 = SetStateAndCloseToTarget
    L2_2 = AI_ATTACKMOVE_ATTACKING
    L3_2 = tauntTarget
    L1_2(L2_2, L3_2)
    L1_2 = ResetAndStartTimer
    L2_2 = "TimerAntiKite"
    L1_2(L2_2)
  else
    L1_2 = FindTargetOrMove
    L1_2()
  end
end
OnTauntEnd = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2, L4_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = AI_ATTACKMOVESTATE
  if L0_2 == L1_2 then
    L1_2 = FindTargetInAcR
    L1_2 = L1_2()
    if L1_2 == nil then
      L2_2 = TurnOffAutoAttack
      L3_2 = STOPREASON_TARGET_LOST
      L2_2(L3_2)
      return
    end
    L2_2 = SetStateAndCloseToTarget
    L3_2 = AI_ATTACKMOVE_ATTACKING
    L4_2 = L1_2
    L2_2(L3_2, L4_2)
    L2_2 = ResetAndStartTimer
    L3_2 = "TimerAntiKite"
    L2_2(L3_2)
  else
    L1_2 = AI_TAUNTED
    if L0_2 == L1_2 then
      L1_2 = GetTauntTarget
      L1_2 = L1_2()
      if L1_2 ~= nil then
        L1_2 = SetStateAndCloseToTarget
        L2_2 = AI_TAUNTED
        L3_2 = GetTauntTarget
        L3_2, L4_2 = L3_2()
        L1_2(L2_2, L3_2, L4_2)
      end
    end
  end
  L1_2 = AI_ATTACKMOVE_ATTACKING
  if L0_2 ~= L1_2 then
    L1_2 = AI_TAUNTED
    if L0_2 ~= L1_2 then
      return
    end
  end
  L1_2 = GetTarget
  L1_2 = L1_2()
  if L1_2 ~= nil then
    L1_2 = DistanceBetweenMeAndObject
    L2_2 = GetTarget
    L2_2, L3_2, L4_2 = L2_2()
    L1_2 = L1_2(L2_2, L3_2, L4_2)
    L2_2 = MAX_ENGAGE_DISTANCE
    if not (L1_2 > L2_2) then
      goto lbl_57
    end
  end
  L1_2 = FindTargetOrMove
  L1_2()
  ::lbl_57::
  L1_2 = TargetInAttackRange
  L1_2 = L1_2()
  if L1_2 then
    L1_2 = AI_TAUNTED
    if L0_2 ~= L1_2 then
      L1_2 = ResetAndStartTimer
      L2_2 = "TimerAntiKite"
      L1_2(L2_2)
    end
    L1_2 = TurnOnAutoAttack
    L2_2 = GetTarget
    L2_2, L3_2, L4_2 = L2_2()
    L1_2(L2_2, L3_2, L4_2)
  else
    L1_2 = TargetInCancelAttackRange
    L1_2 = L1_2()
    if L1_2 == false then
      L1_2 = TurnOffAutoAttack
      L2_2 = STOPREASON_MOVING
      L1_2(L2_2)
      L1_2 = 0
      LastAttackScan = L1_2
    end
  end
end
TimerFindEnemies = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2, L4_2, L5_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = FindTargetInAcR
  L1_2 = L1_2()
  if L1_2 then
    L2_2 = LastAutoAttackFinished
    L2_2 = L2_2()
    if L2_2 == false then
      L2_2 = InitTimer
      L3_2 = "TimerFindEnemies"
      L4_2 = DELAY_FIND_ENEMIES
      L5_2 = true
      L2_2(L3_2, L4_2, L5_2)
      return
    end
    L2_2 = SetStateAndCloseToTarget
    L3_2 = AI_ATTACKMOVE_ATTACKING
    L4_2 = L1_2
    L2_2(L3_2, L4_2)
    L2_2 = ResetAndStartTimer
    L3_2 = "TimerAntiKite"
    L2_2(L3_2)
  else
    L2_2 = SetStateAndMoveToForwardNav
    L3_2 = AI_ATTACKMOVESTATE
    L2_2(L3_2)
    L2_2 = StopTimer
    L3_2 = "TimerAntiKite"
    L2_2(L3_2)
    L2_2 = 0
    LastAttackScan = L2_2
  end
end
FindTargetOrMove = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = AI_IDLE
  if L0_2 == L1_2 then
    L1_2 = FindTargetOrMove
    L1_2()
  else
    L1_2 = AI_ATTACKMOVESTATE
    if L0_2 == L1_2 then
      L1_2 = SetStateAndMoveToForwardNav
      L2_2 = AI_ATTACKMOVESTATE
      L1_2(L2_2)
      L1_2 = 0
      LastAttackScan = L1_2
    end
  end
end
TimerMoveForward = L0_1
