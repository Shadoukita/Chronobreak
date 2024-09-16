local L0_1, L1_1, L2_1, L3_1, L4_1, L5_1, L6_1, L7_1, L8_1, L9_1, L10_1, L11_1, L12_1, L13_1, L14_1, L15_1, L16_1, L17_1, L18_1
L0_1 = require
L1_1 = "os"
L0_1(L1_1)
L0_1 = {}
L1_1 = {}
L1_1.Creator = "bill.clark"
L2_1 = os
L2_1 = L2_1.time
L3_1 = {}
L3_1.month = 9
L3_1.day = 30
L3_1.year = 2014
L2_1 = L2_1(L3_1)
L1_1.Expiration = L2_1
L2_1 = {}
L3_1 = "SpearThrows"
L4_1 = "SpearChampHits"
L2_1[1] = L3_1
L2_1[2] = L4_1
L1_1.AccumulateVariables = L2_1
L2_1 = {}
L3_1 = "SpearAngle"
L4_1 = "SpearHit"
L5_1 = "SpearHitChampion"
L2_1[1] = L3_1
L2_1[2] = L4_1
L2_1[3] = L5_1
L1_1.SampleVariables = L2_1
L0_1.NidaleeSpear = L1_1
L1_1 = {}
L1_1.Creator = "bill.clark"
L2_1 = os
L2_1 = L2_1.time
L3_1 = {}
L3_1.month = 8
L3_1.day = 30
L3_1.year = 2014
L2_1 = L2_1(L3_1)
L1_1.Expiration = L2_1
L2_1 = {}
L3_1 = "TowerDamage"
L4_1 = "TowerHits"
L5_1 = "ChampDamage"
L6_1 = "ChampHits"
L2_1[1] = L3_1
L2_1[2] = L4_1
L2_1[3] = L5_1
L2_1[4] = L6_1
L1_1.AccumulateVariables = L2_1
L0_1.LichBane = L1_1
L1_1 = {}
L1_1.Creator = "bfeeney"
L2_1 = os
L2_1 = L2_1.time
L3_1 = {}
L3_1.month = 11
L3_1.day = 30
L3_1.year = 2014
L2_1 = L2_1(L3_1)
L1_1.Expiration = L2_1
L2_1 = {}
L3_1 = "AatroxWHealPerc"
L4_1 = "AatroxWHealPercChamps"
L5_1 = "AatroxWCastNumber"
L6_1 = "AatroxWCastEver"
L2_1[1] = L3_1
L2_1[2] = L4_1
L2_1[3] = L5_1
L2_1[4] = L6_1
L1_1.AccumulateVariables = L2_1
L0_1.AatroxWHit = L1_1
L1_1 = {}
L1_1.Creator = "jbulock"
L2_1 = os
L2_1 = L2_1.time
L3_1 = {}
L3_1.month = 4
L3_1.day = 1
L3_1.year = 2015
L2_1 = L2_1(L3_1)
L1_1.Expiration = L2_1
L2_1 = {}
L3_1 = "KrugsTaken"
L4_1 = "KrugsSmited"
L5_1 = "WolvesTaken"
L6_1 = "WolvesSmited"
L7_1 = "RazorbeaksTaken"
L8_1 = "RazorbeaksSmited"
L9_1 = "GrompTaken"
L10_1 = "GrompSmited"
L11_1 = "BlueBuffTaken"
L12_1 = "BlueBuffSmited"
L13_1 = "RedBuffTaken"
L14_1 = "RedBuffSmited"
L15_1 = "BotEelTaken"
L16_1 = "BotEelSmited"
L17_1 = "TopEelTaken"
L18_1 = "TopEelSmited"
L2_1[1] = L3_1
L2_1[2] = L4_1
L2_1[3] = L5_1
L2_1[4] = L6_1
L2_1[5] = L7_1
L2_1[6] = L8_1
L2_1[7] = L9_1
L2_1[8] = L10_1
L2_1[9] = L11_1
L2_1[10] = L12_1
L2_1[11] = L13_1
L2_1[12] = L14_1
L2_1[13] = L15_1
L2_1[14] = L16_1
L2_1[15] = L17_1
L2_1[16] = L18_1
L1_1.AccumulateVariables = L2_1
L0_1.SmiteTracker = L1_1
DataLoggingCategories = L0_1
L0_1 = {}
DataLoggingErroredCategories = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = DataLoggingErroredCategories
  L2_2 = L2_2[A0_2]
  if L2_2 == nil then
    L2_2 = DataLoggingErroredCategories
    L2_2[A0_2] = 1
    L2_2 = PrintToChat
    L3_2 = "DataLogging category "
    L4_2 = A0_2
    L5_2 = ": "
    L6_2 = A1_2
    L3_2 = L3_2 .. L4_2 .. L5_2 .. L6_2
    L2_2(L3_2)
  end
end
DataLoggingCategoryError = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2, L4_2, L5_2
  L1_2 = DataLoggingCategories
  L1_2 = L1_2[A0_2]
  if L1_2 ~= nil then
    L1_2.Name = A0_2
    L2_2 = os
    L2_2 = L2_2.time
    L2_2 = L2_2()
    L3_2 = L1_2.Expiration
    if L3_2 ~= nil then
      L3_2 = L1_2.Expiration
    end
    if L2_2 > L3_2 then
      L1_2 = nil
      L3_2 = DataLoggingCategoryError
      L4_2 = A0_2
      L5_2 = "Expired Category"
      L3_2(L4_2, L5_2)
    end
  else
    L2_2 = DataLoggingCategoryError
    L3_2 = A0_2
    L4_2 = "Missing Category"
    L2_2(L3_2, L4_2)
  end
  return L1_2
end
GetDataLoggingCategory = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2, L13_2, L14_2, L15_2
  L3_2 = {}
  L4_2 = A0_2.AccumulateVariables
  if L4_2 ~= nil then
    L4_2 = ipairs
    L5_2 = A0_2.AccumulateVariables
    L4_2, L5_2, L6_2 = L4_2(L5_2)
    for L7_2, L8_2 in L4_2, L5_2, L6_2 do
      L9_2 = #L3_2
      if A1_2 > L9_2 then
        L9_2 = #L3_2
        L9_2 = L9_2 + 1
        L3_2[L9_2] = L8_2
      else
        L9_2 = DataLoggingCategoryError
        L10_2 = A0_2.Name
        L11_2 = "Too many variables"
        L9_2(L10_2, L11_2)
      end
    end
  end
  L4_2 = 0
  L5_2 = A0_2.SampleVariables
  if L5_2 ~= nil then
    L5_2 = #L3_2
    L5_2 = A1_2 - L5_2
    L6_2 = A0_2.SampleVariables
    L6_2 = #L6_2
    L7_2 = #L3_2
    L3_2.SampleStartIndex = L7_2
    L3_2.SampleValueCount = L6_2
    while A2_2 > L4_2 do
      L7_2 = #L3_2
      L7_2 = L7_2 + L6_2
      if not (A1_2 > L7_2) then
        break
      end
      L4_2 = L4_2 + 1
      L7_2 = ipairs
      L8_2 = A0_2.SampleVariables
      L7_2, L8_2, L9_2 = L7_2(L8_2)
      for L10_2, L11_2 in L7_2, L8_2, L9_2 do
        L12_2 = #L3_2
        L12_2 = L12_2 + 1
        L13_2 = GetDataLoggingSampleVariableName
        L14_2 = L11_2
        L15_2 = L4_2
        L13_2 = L13_2(L14_2, L15_2)
        L3_2[L12_2] = L13_2
      end
    end
    if L4_2 == 0 then
      L7_2 = DataLoggingCategoryError
      L8_2 = A0_2.Name
      L9_2 = "No room for samples"
      L7_2(L8_2, L9_2)
    end
  end
  L3_2.SampleCount = L4_2
  return L3_2
end
GetDataLoggingCategoryValueNames = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2
  L2_2 = {}
  L3_2 = A0_2.SampleVariables
  if L3_2 ~= nil then
    L3_2 = ipairs
    L4_2 = A0_2.SampleVariables
    L3_2, L4_2, L5_2 = L3_2(L4_2)
    for L6_2, L7_2 in L3_2, L4_2, L5_2 do
      L8_2 = #L2_2
      L8_2 = L8_2 + 1
      L9_2 = GetDataLoggingSampleVariableName
      L10_2 = L7_2
      L11_2 = A1_2
      L9_2 = L9_2(L10_2, L11_2)
      L2_2[L8_2] = L9_2
    end
  end
  return L2_2
end
GetDataLoggingSampleValueNames = L0_1
L0_1 = {}
DataLoggingConstructedVariableNames = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = A0_2
  L3_2 = tostring
  L4_2 = A1_2
  L3_2 = L3_2(L4_2)
  L2_2 = L2_2 .. L3_2
  L3_2 = DataLoggingConstructedVariableNames
  L3_2 = L3_2[L2_2]
  if L3_2 == nil then
    L3_2 = DataLoggingConstructedVariableNames
    L3_2[L2_2] = L2_2
  end
  L3_2 = DataLoggingConstructedVariableNames
  L3_2 = L3_2[L2_2]
  return L3_2
end
GetDataLoggingSampleVariableName = L0_1
