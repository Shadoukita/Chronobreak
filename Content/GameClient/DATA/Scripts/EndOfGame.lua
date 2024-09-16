local L0_1, L1_1
L0_1 = 0
TEAM_UNKNOWN = L0_1
L0_1 = 2
EOG_SCOREBOARD_PHASE_DELAY_TIME = L0_1
L0_1 = 10
EOG_NEXUS_REVIVE_TIME = L0_1
L0_1 = 1
EOG_MAIN_SFX_FADE_DELAY_TIME = L0_1
L0_1 = 2
EOG_MAIN_SFX_FADE_DURATION = L0_1
L0_1 = 0
EOG_ALIVE_NEXUS_SKIN = L0_1
L0_1 = 1
EOG_DESTROYED_NEXUS_SKIN = L0_1
L0_1 = 0
EOG_MINION_FADE_AMOUNT = L0_1
L0_1 = 2
EOG_MINION_FADE_TIME = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  winningTeam = A0_2
  L2_2 = winningTeam
  L3_2 = TEAM_ORDER
  if L2_2 == L3_2 then
    L2_2 = TEAM_CHAOS
    losingTeam = L2_2
  else
    L2_2 = TEAM_ORDER
    losingTeam = L2_2
  end
  L2_2 = GetPosition
  L3_2 = A1_2
  L2_2 = L2_2(L3_2)
  losingHQPosition = L2_2
  L2_2 = GetHQ
  L3_2 = TEAM_ORDER
  L2_2 = L2_2(L3_2)
  orderHQ = L2_2
  L2_2 = SetInvulnerable
  L3_2 = orderHQ
  L4_2 = true
  L2_2(L3_2, L4_2)
  L2_2 = SetTargetable
  L3_2 = orderHQ
  L4_2 = false
  L2_2(L3_2, L4_2)
  L2_2 = SetBuildingHealthRegenEnabled
  L3_2 = orderHQ
  L4_2 = false
  L2_2(L3_2, L4_2)
  L2_2 = GetHQ
  L3_2 = TEAM_CHAOS
  L2_2 = L2_2(L3_2)
  chaosHQ = L2_2
  L2_2 = SetInvulnerable
  L3_2 = chaosHQ
  L4_2 = true
  L2_2(L3_2, L4_2)
  L2_2 = SetTargetable
  L3_2 = chaosHQ
  L4_2 = false
  L2_2(L3_2, L4_2)
  L2_2 = SetBuildingHealthRegenEnabled
  L3_2 = chaosHQ
  L4_2 = false
  L2_2(L3_2, L4_2)
  L2_2 = SetInputLockFlag
  L3_2 = INPUT_LOCK_CAMERALOCKING
  L4_2 = true
  L2_2(L3_2, L4_2)
  L2_2 = SetInputLockFlag
  L3_2 = INPUT_LOCK_CAMERAMOVEMENT
  L4_2 = true
  L2_2(L3_2, L4_2)
  L2_2 = SetInputLockFlag
  L3_2 = INPUT_LOCK_ABILITIES
  L4_2 = true
  L2_2(L3_2, L4_2)
  L2_2 = SetInputLockFlag
  L3_2 = INPUT_LOCK_SUMMONERSPELLS
  L4_2 = true
  L2_2(L3_2, L4_2)
  L2_2 = SetInputLockFlag
  L3_2 = INPUT_LOCK_MOVEMENT
  L4_2 = true
  L2_2(L3_2, L4_2)
  L2_2 = SetInputLockFlag
  L3_2 = INPUT_LOCK_SHOP
  L4_2 = true
  L2_2(L3_2, L4_2)
  L2_2 = SetInputLockFlag
  L3_2 = INPUT_LOCK_MINIMAPMOVEMENT
  L4_2 = true
  L2_2(L3_2, L4_2)
  L2_2 = DisableHUDForEndOfGame
  L2_2()
  L2_2 = SetBarracksSpawnEnabled
  L3_2 = false
  L2_2(L3_2)
  L2_2 = CloseShop
  L2_2()
  L2_2 = HaltAllAI
  L2_2()
  L2_2 = LuaForEachChampion
  L3_2 = TEAM_UNKNOWN
  L4_2 = "ChampionEoGCeremony"
  L2_2(L3_2, L4_2)
  L2_2 = InitTimer
  L3_2 = "FadeOutMainSFXPhase"
  L4_2 = EOG_MAIN_SFX_FADE_DELAY_TIME
  L5_2 = false
  L2_2(L3_2, L4_2, L5_2)
  L2_2 = InitTimer
  L3_2 = "DestroyNexusPhase"
  L4_2 = GetEoGNexusChangeSkinTime
  L4_2 = L4_2()
  L5_2 = false
  L2_2(L3_2, L4_2, L5_2)
end
EndOfGameCeremony = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2, L4_2, L5_2
  L1_2 = MoveCameraFromCurrentPositionToPoint
  L2_2 = A0_2
  L3_2 = losingHQPosition
  L4_2 = GetEoGPanToHQTime
  L4_2 = L4_2()
  L5_2 = true
  L1_2(L2_2, L3_2, L4_2, L5_2)
  L1_2 = SetGreyscaleEnabledWhenDead
  L2_2 = A0_2
  L3_2 = false
  L1_2(L2_2, L3_2)
end
ChampionEoGCeremony = L0_1
function L0_1()
  local L0_2, L1_2
  L0_2 = FadeOutMainSFX
  L1_2 = EOG_MAIN_SFX_FADE_DURATION
  L0_2(L1_2)
end
FadeOutMainSFXPhase = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2
  L0_2 = GetEoGUseNexusDeathAnimation
  L0_2 = L0_2()
  if L0_2 == false then
    L0_2 = SetHQCurrentSkin
    L1_2 = losingTeam
    L2_2 = EOG_DESTROYED_NEXUS_SKIN
    L0_2(L1_2, L2_2)
  end
  L0_2 = FadeMinions
  L1_2 = losingTeam
  L2_2 = EOG_MINION_FADE_AMOUNT
  L3_2 = EOG_MINION_FADE_TIME
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = InitTimer
  L1_2 = "StopRenderingMinionsPhase"
  L2_2 = EOG_MINION_FADE_TIME
  L3_2 = false
  L0_2(L1_2, L2_2, L3_2)
  L0_2 = InitTimer
  L1_2 = "ScoreboardPhase"
  L2_2 = EOG_SCOREBOARD_PHASE_DELAY_TIME
  L3_2 = false
  L0_2(L1_2, L2_2, L3_2)
end
DestroyNexusPhase = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = SetMinionsNoRender
  L1_2 = losingTeam
  L2_2 = true
  L0_2(L1_2, L2_2)
end
StopRenderingMinionsPhase = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = SetInputLockFlag
  L1_2 = INPUT_LOCK_CHAT
  L2_2 = true
  L0_2(L1_2, L2_2)
  L0_2 = EndGame
  L1_2 = winningTeam
  L0_2(L1_2)
end
ScoreboardPhase = L0_1
function L0_1()
  local L0_2, L1_2, L2_2
  L0_2 = SetHQCurrentSkin
  L1_2 = losingTeam
  L2_2 = EOG_ALIVE_NEXUS_SKIN
  L0_2(L1_2, L2_2)
end
TestReviveNexus = L0_1
