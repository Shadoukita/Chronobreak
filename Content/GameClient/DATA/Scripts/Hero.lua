local L0_1, L1_1, L2_1, L3_1, L4_1, L5_1, L6_1, L7_1, L8_1, L9_1, L10_1, L11_1, L12_1, L13_1, L14_1, L15_1, L16_1, L17_1, L18_1
L0_1 = 500
FEAR_WANDER_DISTANCE = L0_1
L0_1 = 2000
FLEE_RUN_DISTANCE = L0_1
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
STATE_STRINGS = L0_1
function L0_1()
  local L0_2, L1_2
  L0_2 = GetPercentAttackSpeedMod
  L0_2 = L0_2()
  L0_2 = L0_2 + 1
  L0_2 = 1.6 / L0_2
  checkAttackTimer = L0_2
  L0_2 = checkAttackTimer
  if L0_2 < 0.5 then
    L0_2 = 0.5
    checkAttackTimer = L0_2
  end
  L0_2 = checkAttackTimer
  return L0_2
end
CalculateAttackTimer = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = ClearTargetPosInPos
  L0_2()
  L0_2 = SetState
  L1_2 = AI_IDLE
  L0_2(L1_2)
  L0_2 = InitTimer
  L1_2 = "TimerDistanceScan"
  L2_2 = 0.2
  L3_2 = true
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = InitTimer
  L1_2 = "TimerCheckAttack"
  L2_2 = 0.2
  L3_2 = true
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = InitTimer
  L1_2 = "TimerFeared"
  L2_2 = 1
  L3_2 = true
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = InitTimer
  L1_2 = "TimerFlee"
  L2_2 = 0.5
  L3_2 = true
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = StopTimer
  L1_2 = "TimerFeared"
  L0_2(L1_2)
  L0_2 = StopTimer
  L1_2 = "TimerFlee"
  L0_2(L1_2)
  L0_2 = false
  return L0_2
end
OnInit = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2
  L3_2 = GetState()
  if L3_2 == AI_HALTED then
    return false
  end
  if L3_2 == AI_TAUNTED or L3_2 == AI_FEARED or L3_2 == AI_FLEEING or L3_2 == AI_CHARMED then
    return false
  end
  L4_2 = ORDER_TAUNT
  if A0_2 == L4_2 then
    L4_2 = SetStateAndCloseToTarget
    L5_2 = AI_HARDATTACK
    L6_2 = A1_2
    L4_2(L5_2, L6_2)
    L4_2 = ClearTargetPosInPos
    L4_2()
    L4_2 = true
    return L4_2
  end
  L4_2 = ORDER_ATTACKTO
  if A0_2 == L4_2 then
    L4_2 = SetStateAndCloseToTarget
    L5_2 = AI_HARDATTACK
    L6_2 = A1_2
    L4_2(L5_2, L6_2)
    L4_2 = AssignTargetPosInPos
    L4_2()
    L4_2 = TargetInAttackRange
    L4_2 = L4_2()
    if L4_2 == true then
      L4_2 = TurnOnAutoAttack
      L5_2 = GetTarget
      L5_2, L6_2, L7_2 = L5_2()
      L4_2(L5_2, L6_2, L7_2)
    else
      L4_2 = TurnOffAutoAttack
      L5_2 = STOPREASON_MOVING
      L4_2(L5_2)
    end
    L4_2 = true
    return L4_2
  end
  L4_2 = ORDER_ATTACKTERRAIN_SUSTAINED
  if A0_2 ~= L4_2 then
    L4_2 = ORDER_ATTACKTERRAIN_ONCE
    if A0_2 ~= L4_2 then
      goto lbl_100
    end
  end
  L4_2 = ORDER_ATTACKTERRAIN_SUSTAINED
  if A0_2 == L4_2 then
  else
    L4_2 = ORDER_ATTACKTERRAIN_ONCE
    if A0_2 == L4_2 then
    end
  end
  L4_2 = TargetPositionInAttackRange
  L5_2 = A2_2
  L4_2 = L4_2(L5_2)
  if L4_2 then
    L4_2 = ORDER_ATTACKTERRAIN_ONCE
    L4_2 = A0_2 == L4_2
    L5_2 = TurnOnAutoAttackTerrain
    L6_2 = A2_2
    L7_2 = L4_2
    L5_2(L6_2, L7_2)
  else
    L4_2 = TargetPositionInAttackRange
    L5_2 = A2_2
    L4_2 = L4_2(L5_2)
    if not L4_2 then
      L4_2 = SetStateAndMove
      L5_2 = AI_HARDATTACK
      L6_2 = A2_2
      L4_2(L5_2, L6_2)
      L4_2 = AssignTargetPosInPos
      L4_2()
      L4_2 = TurnOffAutoAttack
      L5_2 = STOPREASON_MOVING
      L4_2(L5_2)
    end
  end
  L4_2 = true
  do return L4_2 end
  ::lbl_100::
  L4_2 = ORDER_ATTACKMOVE
  if A0_2 == L4_2 then
    L4_2 = FindTargetInAcR
    L4_2 = L4_2()
    newTarget = L4_2
    L4_2 = newTarget
    if L4_2 ~= nil then
      L4_2 = SetStateAndCloseToTarget
      L5_2 = AI_SOFTATTACK
      L6_2 = newTarget
      L4_2(L5_2, L6_2)
      L4_2 = true
      return L4_2
    end
    L4_2 = SetStateAndMoveInPos
    L5_2 = AI_ATTACKMOVESTATE
    L4_2(L5_2)
    L4_2 = AssignTargetPosInPos
    L4_2()
    L4_2 = true
    return L4_2
  end
  L4_2 = ORDER_MOVETO
  if A0_2 == L4_2 then
    L4_2 = SetStateAndMoveInPos
    L5_2 = AI_MOVE
    L4_2(L5_2)
    L4_2 = AssignTargetPosInPos
    L4_2()
    L4_2 = true
    return L4_2
  end
  L4_2 = TimerCheckAttack
  L4_2()
  L4_2 = false
  return L4_2
end
OnOrder = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetState
  L2_2 = L2_2()
  L3_2 = AI_HALTED
  if L2_2 == L3_2 then
    return
  end
  L3_2 = AI_ATTACK_GOING_TO_LAST_KNOWN_LOCATION
  if L3_2 ~= L2_2 then
    L3_2 = LOST_VISIBILITY
    if A0_2 == L3_2 then
      L3_2 = AI_SOFTATTACK
      if L2_2 ~= L3_2 and A1_2 ~= nil then
        L3_2 = SetStateAndCloseToTarget
        L4_2 = AI_ATTACK_GOING_TO_LAST_KNOWN_LOCATION
        L5_2 = A1_2
        L3_2(L4_2, L5_2)
    end
    else
      L3_2 = TimerCheckAttack
      L3_2()
    end
  end
end
OnTargetLost = L0_1
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
    L2_2 = AI_SOFTATTACK
    L3_2 = tauntTarget
    L1_2(L2_2, L3_2)
  else
    L1_2 = NetSetState
    L2_2 = AI_IDLE
    L1_2(L2_2)
    L1_2 = TimerDistanceScan
    L1_2()
    L1_2 = TimerCheckAttack
    L1_2()
  end
end
OnTauntEnd = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = MakeWanderPoint
  L2_2 = GetFearLeashPoint
  L2_2 = L2_2()
  L3_2 = FEAR_WANDER_DISTANCE
  L1_2 = L1_2(L2_2, L3_2)
  wanderPoint = L1_2
  L1_2 = SetStateAndMove
  L2_2 = AI_FEARED
  L3_2 = wanderPoint
  L1_2(L2_2, L3_2)
  L1_2 = TurnOffAutoAttack
  L2_2 = STOPREASON_MOVING
  L1_2(L2_2)
  L1_2 = ResetAndStartTimer
  L2_2 = "TimerFeared"
  L1_2(L2_2)
end
OnFearBegin = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = StopTimer
  L2_2 = "TimerFeared"
  L1_2(L2_2)
  L1_2 = NetSetState
  L2_2 = AI_IDLE
  L1_2(L2_2)
  L1_2 = TimerDistanceScan
  L1_2()
  L1_2 = TimerCheckAttack
  L1_2()
end
OnFearEnd = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = MakeWanderPoint
  L2_2 = GetFearLeashPoint
  L2_2 = L2_2()
  L3_2 = FEAR_WANDER_DISTANCE
  L1_2 = L1_2(L2_2, L3_2)
  wanderPoint = L1_2
  L1_2 = SetStateAndMove
  L2_2 = AI_FEARED
  L3_2 = wanderPoint
  L1_2(L2_2, L3_2)
end
TimerFeared = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = MakeFleePoint
  L1_2 = L1_2()
  fleePoint = L1_2
  L1_2 = SetStateAndMove
  L2_2 = AI_FLEEING
  L3_2 = fleePoint
  L1_2(L2_2, L3_2)
  L1_2 = TurnOffAutoAttack
  L2_2 = STOPREASON_MOVING
  L1_2(L2_2)
  L1_2 = ResetAndStartTimer
  L2_2 = "TimerFlee"
  L1_2(L2_2)
end
OnFleeBegin = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = StopTimer
  L2_2 = "TimerFlee"
  L1_2(L2_2)
  L1_2 = NetSetState
  L2_2 = AI_IDLE
  L1_2(L2_2)
  L1_2 = TimerDistanceScan
  L1_2()
  L1_2 = TimerCheckAttack
  L1_2()
end
OnFleeEnd = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = MakeFleePoint
  L1_2 = L1_2()
  fleePoint = L1_2
  L1_2 = SetStateAndMove
  L2_2 = AI_FLEEING
  L3_2 = fleePoint
  L1_2(L2_2, L3_2)
end
TimerFlee = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = NetSetState
  L2_2 = AI_CHARMED
  L1_2(L2_2)
  L1_2 = TurnOffAutoAttack
  L2_2 = STOPREASON_IMMEDIATELY
  L1_2(L2_2)
  L1_2 = TimerCheckAttack
  L1_2()
end
OnCharmBegin = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = NetSetState
  L2_2 = AI_IDLE
  L1_2(L2_2)
  L1_2 = TimerDistanceScan
  L1_2()
  L1_2 = TimerCheckAttack
  L1_2()
end
OnCharmEnd = L0_1
function L0_1()
  local L0_2, L1_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = ClearTargetPosInPos
  L1_2()
end
OnStopMove = L0_1
L0_1 = nil
function L1_1()
  local L0_2, L1_2, L2_2, L3_2, L4_2, L5_2, L6_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = L0_1
  if L0_2 ~= L1_2 then
    L0_1 = L0_2
  end
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = GetOrder
  L1_2 = L1_2()
  L2_2 = GetOrderPosition
  L2_2 = L2_2()
  L3_2 = IsLocationAutoAttacker
  L3_2 = L3_2()
  if L3_2 then
    L3_2 = ORDER_ATTACKTERRAIN_ONCE
    if L1_2 ~= L3_2 then
      L3_2 = ORDER_ATTACKTERRAIN_SUSTAINED
      if L1_2 ~= L3_2 then
        goto lbl_61
      end
    end
    L3_2 = ORDER_ATTACKTERRAIN_ONCE
    L3_2 = L1_2 == L3_2
    L4_2 = IsMoving
    L4_2 = L4_2()
    if L4_2 then
      L4_2 = TargetPositionInAttackRange
      L5_2 = L2_2
      L4_2 = L4_2(L5_2)
      if L4_2 then
        L4_2 = TurnOnAutoAttackTerrain
        L5_2 = L2_2
        L6_2 = L3_2
        L4_2(L5_2, L6_2)
        return
    end
    else
      L4_2 = TargetPositionInAttackRange
      L5_2 = L2_2
      L4_2 = L4_2(L5_2)
      if not L4_2 then
        L4_2 = SetStateAndMove
        L5_2 = AI_HARDATTACK
        L6_2 = L2_2
        L4_2(L5_2, L6_2)
        L4_2 = AssignTargetPosInPos
        L4_2()
        L4_2 = TurnOffAutoAttack
        L5_2 = STOPREASON_MOVING
        L4_2(L5_2)
        return
      end
    end
  ::lbl_61::
  else
    L3_2 = AI_SOFTATTACK
    if L0_2 ~= L3_2 then
      L3_2 = AI_HARDATTACK
      if L0_2 ~= L3_2 then
        L3_2 = AI_TAUNTED
        if L0_2 ~= L3_2 then
          L3_2 = AI_CHARMED
          if L0_2 ~= L3_2 then
            L3_2 = IsMoving
            L3_2 = L3_2()
            if L3_2 then
              L3_2 = false
              return L3_2
            end
          end
        end
      end
    end
    L3_2 = IsTargetLost
    L3_2 = L3_2()
    if L3_2 ~= true then
      L3_2 = GetTarget
      L3_2 = L3_2()
      if L3_2 ~= nil then
        L3_2 = TargetInAttackRange
        L3_2 = L3_2()
        if L3_2 == true then
          L3_2 = TurnOnAutoAttack
          L4_2 = GetTarget
          L4_2, L5_2, L6_2 = L4_2()
          L3_2(L4_2, L5_2, L6_2)
          return
        end
        L3_2 = TargetInCancelAttackRange
        L3_2 = L3_2()
        if L3_2 == false then
          L3_2 = TurnOffAutoAttack
          L4_2 = STOPREASON_MOVING
          L3_2(L4_2)
          goto lbl_160
        end
      end
    end
    L3_2 = LastAutoAttackFinished
    L3_2 = L3_2()
    if L3_2 == false then
      L3_2 = InitTimer
      L4_2 = "TimerCheckAttack"
      L5_2 = 0.1
      L6_2 = true
      L3_2(L4_2, L5_2, L6_2)
      return
    end
    L3_2 = IsAutoAcquireTargetEnabled
    L3_2 = L3_2()
    if L3_2 == false then
      L3_2 = AI_SOFTATTACK
      if L0_2 ~= L3_2 then
        return
      end
    end
    L3_2 = FindTargetInAcR
    L3_2 = L3_2()
    newTarget = L3_2
    L3_2 = newTarget
    if L3_2 ~= nil then
      L3_2 = AI_CHARMED
      if L0_2 == L3_2 then
        L3_2 = SetStateAndCloseToTarget
        L4_2 = AI_CHARMED
        L5_2 = newTarget
        L3_2(L4_2, L5_2)
      else
        L3_2 = CanSeeMe
        L4_2 = newTarget
        L3_2 = L3_2(L4_2)
        if L3_2 then
          L3_2 = SetStateAndCloseToTarget
          L4_2 = AI_SOFTATTACK
          L5_2 = newTarget
          L3_2(L4_2, L5_2)
        end
      end
      L3_2 = true
      return L3_2
    else
      L3_2 = AI_CHARMED
      if L0_2 == L3_2 then
        L3_2 = SpellBuffRemoveType
        L4_2 = me
        L5_2 = BUFF_Taunt
        L3_2(L4_2, L5_2)
      end
      L3_2 = NetSetState
      L4_2 = AI_STANDING
      L3_2(L4_2)
      return
    end

  end
  ::lbl_160::
  L3_2 = InitTimer
  L4_2 = "TimerCheckAttack"
  L5_2 = 0.1
  L6_2 = true
  L3_2(L4_2, L5_2, L6_2)
end
TimerCheckAttack = L1_1
function L1_1()
  local L0_2, L1_2, L2_2, L3_2, L4_2
  L0_2 = GetState()
  if L0_2 == AI_HALTED or L0_2 == AI_HARDIDLE then
    return
  end
  L1_2 = AI_STANDING
  if L0_2 ~= L1_2 then
    L1_2 = AI_IDLE
    if L0_2 ~= L1_2 then
      goto lbl_35
    end
  end
  L1_2 = IsAutoAcquireTargetEnabled
  L1_2 = L1_2()
  if L1_2 then
    L1_2 = GetTargetOrFindTargetInAcR
    L1_2 = L1_2()
    if L1_2 ~= nil then
      L2_2 = CanSeeMe
      L3_2 = L1_2
      L2_2 = L2_2(L3_2)
      if L2_2 then
        L2_2 = SetStateAndCloseToTarget
        L3_2 = AI_SOFTATTACK
        L4_2 = L1_2
        L2_2(L3_2, L4_2)
        L2_2 = true
        return L2_2
      end
    end
  end
  ::lbl_35::
  L1_2 = AI_MOVE
  if L0_2 == L1_2 then
    L1_2 = IsMovementStopped
    L1_2 = L1_2()
    if L1_2 then
      L1_2 = IsAutoAcquireTargetEnabled
      L1_2 = L1_2()
      if L1_2 then
        L1_2 = GetTargetOrFindTargetInAcR
        L1_2 = L1_2()
        if L1_2 ~= nil then
          L2_2 = CanSeeMe
          L3_2 = L1_2
          L2_2 = L2_2(L3_2)
          if L2_2 then
            L2_2 = SetStateAndCloseToTarget
            L3_2 = AI_SOFTATTACK
            L4_2 = L1_2
            L2_2(L3_2, L4_2)
            L2_2 = TurnOnAutoAttack
            L3_2 = L1_2
            L2_2(L3_2)
            L2_2 = true
            return L2_2
          end
        end
        L2_2 = NetSetState
        L3_2 = AI_IDLE
        L2_2(L3_2)
        L2_2 = false
        return L2_2
      end
    end
  end
  L1_2 = AI_ATTACKMOVESTATE
  if L0_2 == L1_2 then
    L1_2 = GetTargetOrFindTargetInAcR
    L1_2 = L1_2()
    if L1_2 ~= nil then
      L2_2 = SetStateAndCloseToTarget
      L3_2 = AI_SOFTATTACK
      L4_2 = L1_2
      L2_2(L3_2, L4_2)
      L2_2 = true
      return L2_2
    else
      L2_2 = DistanceBetweenObjectAndTargetPosSq
      L3_2 = me
      L2_2 = L2_2(L3_2)
      if L2_2 <= 100 then
        L2_2 = NetSetState
        L3_2 = AI_STANDING
        L2_2(L3_2)
        L2_2 = ClearTargetPosInPos
        L2_2()
        L2_2 = true
        return L2_2
      end
    end
  end
  L1_2 = AI_ATTACK_GOING_TO_LAST_KNOWN_LOCATION
  if L0_2 == L1_2 then
    L1_2 = GetLostTargetIfVisible
    L1_2 = L1_2()
    if L1_2 ~= nil then
      L2_2 = SetStateAndCloseToTarget
      L3_2 = AI_HARDATTACK
      L4_2 = L1_2
      L2_2(L3_2, L4_2)
    end
  end
end
TimerDistanceScan = L1_1
function L1_1(A0_2, A1_2)
  local L2_2, L3_2
  L2_2 = GetState
  L2_2 = L2_2()
  L3_2 = AI_HALTED
  if L2_2 == L3_2 then
    return
  end
end
OnAICommand = L1_1
function L1_1()
  local L0_2, L1_2, L2_2
  L0_2 = GetState
  L0_2 = L0_2()
  L1_2 = AI_HALTED
  if L0_2 == L1_2 then
    return
  end
  L1_2 = NetSetState
  L2_2 = AI_IDLE
  L1_2(L2_2)
  L1_2 = TimerDistanceScan
  L1_2()
  L1_2 = TimerCheckAttack
  L1_2()
end
OnReachedDestinationForGoingToLastLocation = L1_1
function L1_1()
  local L0_2, L1_2
  L0_2 = StopTimer
  L1_2 = "TimerDistanceScan"
  L0_2(L1_2)
  L0_2 = StopTimer
  L1_2 = "TimerCheckAttack"
  L0_2(L1_2)
  L0_2 = StopTimer
  L1_2 = "TimerFeared"
  L0_2(L1_2)
  L0_2 = TurnOffAutoAttack
  L1_2 = STOPREASON_IMMEDIATELY
  L0_2(L1_2)
  L0_2 = NetSetState
  L1_2 = AI_HALTED
  L0_2(L1_2)
end
HaltAI = L1_1