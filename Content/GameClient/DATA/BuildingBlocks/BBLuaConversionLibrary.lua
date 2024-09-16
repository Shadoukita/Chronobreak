local L0_1, L1_1
function L0_1(A0_2, A1_2, A2_2, A3_2, A4_2)
  local L5_2, L6_2, L7_2
  L5_2 = {}
  L5_2.Pos = A0_2
  L5_2.Team = A4_2
  passThroughParams = L5_2
  L5_2 = {}
  L5_2.TeamVar = "Team"
  L5_2.Radius = A1_2
  L5_2.PosVar = "Pos"
  L5_2.Duration = A2_2
  L5_2.RevealSteath = A3_2
  perBlockParams = L5_2
  L5_2 = BBAddPosPerceptionBubble
  L6_2 = passThroughParams
  L7_2 = perBlockParams
  L5_2(L6_2, L7_2)
  L5_2 = passThroughParams
  L5_2 = L5_2.BubbleIDVar
  return L5_2
end
AddPosPerceptionBubble = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2
  L3_2 = {}
  L4_2 = GetHashedGameObjName
  L5_2 = "Target"
  L4_2 = L4_2(L5_2)
  L3_2[L4_2] = A1_2
  L4_2 = GetHashedGameObjName
  L5_2 = "Attacker"
  L4_2 = L4_2(L5_2)
  L3_2[L4_2] = A0_2
  passThroughParams = L3_2
  L3_2 = {}
  L3_2.AttackerVar = "Attacker"
  L3_2.TargetVar = "Target"
  L3_2.Damage = A2_2
  L4_2 = TRUE_DAMAGE
  L3_2.DamageType = L4_2
  L4_2 = DAMAGESOURCE_RAW
  L3_2.SourceDamageType = L4_2
  L3_2.PercentOfAttack = 1
  L3_2.SpellDamageRatio = 0
  perBlockParams = L3_2
  L3_2 = BBApplyDamage
  L4_2 = passThroughParams
  L5_2 = perBlockParams
  L3_2(L4_2, L5_2)
end
ApplyDamage = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2
  L3_2 = {}
  L4_2 = GetHashedGameObjName
  L5_2 = "Target"
  L4_2 = L4_2(L5_2)
  L3_2[L4_2] = A0_2
  passThroughParams = L3_2
  L3_2 = {}
  L3_2.UnitVar = "Target"
  L3_2.CreatedItemID = A1_2
  L3_2.CheckRange = A2_2
  perBlockParams = L3_2
  L3_2 = BBCreateItem
  L4_2 = passThroughParams
  L5_2 = perBlockParams
  L3_2(L4_2, L5_2)
end
CreateItem = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = {}
  L3_2 = GetHashedGameObjName
  L4_2 = "Target"
  L3_2 = L3_2(L4_2)
  L2_2[L3_2] = A0_2
  L2_2.Position = A1_2
  passThroughParams = L2_2
  L2_2 = {}
  L2_2.TargetVar = "Target"
  L2_2.LocationVar = "Position"
  perBlockParams = L2_2
  L2_2 = BBFaceDirection
  L3_2 = passThroughParams
  L4_2 = perBlockParams
  L2_2(L3_2, L4_2)
end
FaceDirection = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = {}
  passThroughParams = L2_2
  L2_2 = {}
  L2_2.Skin = A0_2
  L2_2.Team = A1_2
  L2_2.DestVar = "DestObj"
  perBlockParams = L2_2
  L2_2 = BBGetChampionBySkinName
  L3_2 = passThroughParams
  L4_2 = perBlockParams
  L2_2(L3_2, L4_2)
  L2_2 = passThroughParams
  L2_2 = L2_2.DestObj
  return L2_2
end
GetChampionBySkinName = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = {}
  L3_2 = GetHashedGameObjName
  L4_2 = "Owner"
  L3_2 = L3_2(L4_2)
  L2_2[L3_2] = A0_2
  passThroughParams = L2_2
  L2_2 = {}
  L2_2.TargetVar = "Owner"
  L2_2.Delta = A1_2
  perBlockParams = L2_2
  L2_2 = BBIncGold
  L3_2 = passThroughParams
  L4_2 = perBlockParams
  L2_2(L3_2, L4_2)
end
IncGold = L0_1
function L0_1(A0_2, A1_2, A2_2, A3_2)
  local L4_2, L5_2, L6_2
  L4_2 = {}
  L5_2 = GetHashedGameObjName
  L6_2 = "Owner"
  L5_2 = L5_2(L6_2)
  L4_2[L5_2] = A0_2
  L4_2.Position = A1_2
  passThroughParams = L4_2
  L4_2 = {}
  L4_2.UnitVar = "Owner"
  L4_2.TargetVar = "Position"
  L4_2.Speed = A2_2
  L4_2.Gravity = A3_2
  L4_2.MoveBackBy = 0
  perBlockParams = L4_2
  L4_2 = BBMove
  L5_2 = passThroughParams
  L6_2 = perBlockParams
  L4_2(L5_2, L6_2)
end
Move = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = {}
  L1_2.IDVar = A0_2
  passThroughParams = L1_2
  L1_2 = {}
  L1_2.BubbleIDVar = "IDVar"
  perBlockParams = L1_2
  L1_2 = BBRemovePerceptionBubble
  L2_2 = A0_2
  L1_2(L2_2)
end
RemovePerceptionBubble = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = {}
  L3_2 = GetHashedGameObjName
  L4_2 = "Owner"
  L3_2 = L3_2(L4_2)
  L2_2[L3_2] = A0_2
  L2_2.Position = A1_2
  passThroughParams = L2_2
  L2_2 = {}
  L2_2.Owner = "Owner"
  L2_2.PositionVar = "Position"
  perBlockParams = L2_2
  L2_2 = BBSetCameraPosition
  L3_2 = passThroughParams
  L4_2 = perBlockParams
  L2_2(L3_2, L4_2)
end
SetCameraPosition = L0_1
function L0_1(A0_2, A1_2, A2_2, A3_2, A4_2, A5_2, A6_2, A7_2, A8_2, A9_2)
  local L10_2, L11_2, L12_2
  L10_2 = {}
  L11_2 = GetHashedGameObjName
  L12_2 = "Attacker"
  L11_2 = L11_2(L12_2)
  L10_2[L11_2] = A0_2
  L11_2 = GetHashedGameObjName
  L12_2 = "Target"
  L11_2 = L11_2(L12_2)
  L10_2[L11_2] = A1_2
  L10_2.NextBuffVars = A8_2
  passThroughParams = L10_2
  L10_2 = {}
  L10_2.AttackerVar = "Attacker"
  L10_2.TargetVar = "Target"
  L10_2.BuffName = A2_2
  L10_2.BuffAddType = A3_2
  L10_2.BuffType = A4_2
  L10_2.MaxStack = A5_2
  L10_2.NumberStacks = A6_2
  L10_2.Duration = A7_2
  L10_2.BuffVarsTable = "NextBuffVars"
  L10_2.TickRate = A9_2
  perBlockParams = L10_2
  L10_2 = BBSpellBuffAdd
  L11_2 = passThroughParams
  L12_2 = perBlockParams
  L10_2(L11_2, L12_2)
end
TutorialSpellBuffAdd = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2
  L3_2 = {}
  L4_2 = GetHashedGameObjName
  L5_2 = "Attacker"
  L4_2 = L4_2(L5_2)
  L3_2[L4_2] = A0_2
  L4_2 = GetHashedGameObjName
  L5_2 = "Target"
  L4_2 = L4_2(L5_2)
  L3_2[L4_2] = A1_2
  passThroughParams = L3_2
  L3_2 = {}
  L3_2.TargetBar = "Target"
  L3_2.AttackerVar = "Attacker"
  L3_2.BuffName = A2_2
  perBlockParams = L3_2
  L3_2 = BBSpellBuffRemove
  L4_2 = passThroughParams
  L5_2 = perBlockParams
  L3_2(L4_2, L5_2)
end
TutorialSpellBuffRemove = L0_1
function L0_1(A0_2, A1_2, A2_2, A3_2, A4_2, A5_2)
  local L6_2, L7_2, L8_2
  L6_2 = {}
  L7_2 = GetHashedGameObjName
  L8_2 = "Owner"
  L7_2 = L7_2(L8_2)
  L6_2[L7_2] = A1_2
  L6_2.Position = A2_2
  L7_2 = GetHashedGameObjName
  L8_2 = "Target"
  L7_2 = L7_2(L8_2)
  L6_2[L7_2] = A4_2
  L6_2.TargetPos = A5_2
  passThroughParams = L6_2
  L6_2 = {}
  L6_2.EffectName = A0_2
  L6_2.BindObjectVar = "Owner"
  L6_2.PosVar = "Position"
  L6_2.Flags = A3_2
  L6_2.TargetObjectVar = "Target"
  L6_2.TargetPosVar = "TargetPos"
  perBlockParams = L6_2
  L6_2 = BBSpellEffectCreate
  L7_2 = passThroughParams
  L8_2 = perBlockParams
  L6_2 = L6_2(L7_2, L8_2)
  outEffectID = L6_2
  L6_2 = outEffectID
  return L6_2
end
SpellEffectCreate = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2, L4_2, L5_2, L6_2
  L1_2 = {}
  L2_2 = GetHashedGameObjName
  L3_2 = "BindObject"
  L2_2 = L2_2(L3_2)
  L3_2 = A0_2.BindObject
  L1_2[L2_2] = L3_2
  L2_2 = GetHashedGameObjName
  L3_2 = "TargetObject"
  L2_2 = L2_2(L3_2)
  L3_2 = A0_2.TargetObject
  L1_2[L2_2] = L3_2
  L2_2 = GetHashedGameObjName
  L3_2 = "SpecificUnitOnly"
  L2_2 = L2_2(L3_2)
  L3_2 = A0_2.SpecificUnitOnly
  L1_2[L2_2] = L3_2
  L2_2 = GetHashedGameObjName
  L3_2 = "SpecificUnitToExclude"
  L2_2 = L2_2(L3_2)
  L3_2 = A0_2.SpecificUnitToExclude
  L1_2[L2_2] = L3_2
  L2_2 = GetHashedGameObjName
  L3_2 = "KeywordObject"
  L2_2 = L2_2(L3_2)
  L3_2 = A0_2.KeywordObject
  L1_2[L2_2] = L3_2
  L2_2 = A0_2.Pos
  L1_2.Pos = L2_2
  L2_2 = A0_2.TargetPos
  L1_2.TargetPos = L2_2
  L2_2 = A0_2.OrientTowards
  L1_2.OrientTowards = L2_2
  passThroughParams = L1_2
  L1_2 = {}
  L1_2.BindObjectVar = "BindObject"
  L1_2.TargetObjectVar = "TargetObject"
  L1_2.SpecificUnitOnlyVar = "SpecificUnitOnly"
  L1_2.SpecificUnitToExcludeVar = "SpecificUnitToExclude"
  L1_2.KeywordObjectVar = "KeywordObject"
  L1_2.PosVar = "Pos"
  L1_2.TargetPosVar = "TargetPos"
  L1_2.OrientTowardsVar = "OrientTowards"
  perBlockParams = L1_2
  L1_2 = pairs
  L2_2 = A0_2
  L1_2, L2_2, L3_2 = L1_2(L2_2)
  for L4_2, L5_2 in L1_2, L2_2, L3_2 do
    L6_2 = perBlockParams
    L6_2[L4_2] = L5_2
  end
  L1_2 = BBSpellEffectCreate
  L2_2 = passThroughParams
  L3_2 = perBlockParams
  L1_2 = L1_2(L2_2, L3_2)
  outEffectID = L1_2
  L1_2 = outEffectID
  return L1_2
end
SpellEffectCreateFromTable = L0_1
function L0_1(A0_2, A1_2, A2_2, A3_2, A4_2, A5_2, A6_2, A7_2, A8_2, A9_2, A10_2, A11_2)
  local L12_2, L13_2, L14_2
  L12_2 = {}
  L12_2.Pos = A3_2
  L13_2 = GetHashedGameObjName
  L14_2 = "GoldRedirectObj"
  L13_2 = L13_2(L14_2)
  L12_2[L13_2] = A11_2
  passThroughParams = L12_2
  L12_2 = {}
  L12_2.Name = A0_2
  L12_2.Skin = A1_2
  L12_2.AiScript = A2_2
  L12_2.PosVar = "Pos"
  L12_2.Team = A4_2
  L12_2.Stunned = A5_2
  L12_2.Rooted = A6_2
  L12_2.Silenced = A7_2
  L12_2.Invulnerable = A8_2
  L12_2.MagicImmune = A9_2
  L12_2.IgnoreCollision = A10_2
  L12_2.DestVar = "DestObj"
  L12_2.GoldRedirectTargetVar = "GoldRedirectObj"
  perBlockParams = L12_2
  L12_2 = BBSpawnMinion
  L13_2 = passThroughParams
  L14_2 = perBlockParams
  L12_2(L13_2, L14_2)
  L12_2 = passThroughParams
  L12_2 = L12_2.DestObj
  return L12_2
end
SpawnMinion = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = {}
  L2_2.Pos = A1_2
  L3_2 = GetHashedGameObjName
  L4_2 = "Owner"
  L3_2 = L3_2(L4_2)
  L2_2[L3_2] = A0_2
  passThroughParams = L2_2
  L2_2 = {}
  L2_2.OwnerVar = "Owner"
  L2_2.CastPositionName = "Pos"
  perBlockParams = L2_2
  L2_2 = BBTeleportToPositionHelper
  L3_2 = passThroughParams
  L4_2 = perBlockParams
  L2_2(L3_2, L4_2)
end
TeleportToPosition = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = {}
  L3_2 = GetHashedGameObjName
  L4_2 = "Unit1"
  L3_2 = L3_2(L4_2)
  L2_2[L3_2] = A0_2
  L3_2 = GetHashedGameObjName
  L4_2 = "Unit2"
  L3_2 = L3_2(L4_2)
  L2_2[L3_2] = A1_2
  passThroughParams = L2_2
  L2_2 = {}
  L2_2.Unit1Var = "Unit1"
  L2_2.Unit2Var = "Unit2"
  perBlockParams = L2_2
  L2_2 = BBLinkVisibility
  L3_2 = passThroughParams
  L4_2 = perBlockParams
  L2_2(L3_2, L4_2)
end
LinkVisibility = L0_1
function L0_1(A0_2, A1_2, A2_2, A3_2, A4_2, A5_2)
  local L6_2, L7_2, L8_2
  L6_2 = {}
  L6_2.Center = A0_2
  passThroughParams = L6_2
  L6_2 = {}
  L6_2.UnitVar = "Nothing"
  L6_2.CenterVar = "Center"
  L6_2.Radius = A1_2
  L6_2.ColorR = A2_2
  L6_2.ColorG = A3_2
  L6_2.ColorB = A4_2
  L6_2.ColorA = A5_2
  L6_2.DebugCircleIDVar = "DebugCircleID"
  perBlockParams = L6_2
  L6_2 = BBAddDebugCircle
  L7_2 = passThroughParams
  L8_2 = perBlockParams
  L6_2(L7_2, L8_2)
  L6_2 = passThroughParams
  L6_2 = L6_2.DebugCircleID
  return L6_2
end
AddDebugCircle = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2
  L1_2 = {}
  L1_2.DebugObjectID = A0_2
  passThroughParams = L1_2
  L1_2 = {}
  perBlockParams = L1_2
  L1_2 = BBRemoveDebugObject
  L2_2 = passThroughParams
  L3_2 = perBlockParams
  L1_2(L2_2, L3_2)
end
RemoveDebugObject = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = {}
  L2_2.DebugCircleID = A0_2
  passThroughParams = L2_2
  L2_2 = {}
  L2_2.Radius = A1_2
  perBlockParams = L2_2
  L2_2 = BBModifyDebugCircleRadius
  L3_2 = passThroughParams
  L4_2 = perBlockParams
  L2_2(L3_2, L4_2)
end
ModifyDebugCircleRadius = L0_1
function L0_1(A0_2, A1_2, A2_2, A3_2, A4_2)
  local L5_2, L6_2, L7_2
  L5_2 = {}
  L5_2.DebugObjectID = A0_2
  passThroughParams = L5_2
  L5_2 = {}
  L5_2.ColorR = A1_2
  L5_2.ColorG = A2_2
  L5_2.ColorB = A3_2
  L5_2.ColorA = A4_2
  perBlockParams = L5_2
  L5_2 = BBModifyDebugObjectColor
  L6_2 = passThroughParams
  L7_2 = perBlockParams
  L5_2(L6_2, L7_2)
end
ModifyDebugObjectColor = L0_1
