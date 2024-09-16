local L0_1, L1_1
L0_1 = -1
gCurrentBuildingBlockNumber = L0_1
L0_1 = ""
gCurrentBuildingBlockString = L0_1
L0_1 = {}
functionToStringMap = L0_1
L0_1 = false
functionToStringMapInitialized = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2, L13_2, L14_2
  if A0_2 ~= nil then
    L2_2 = gCurrentBuildingBlockString
    A1_2.___BreakExecution___ = false
    L3_2 = 1
    L4_2 = #A0_2
    L5_2 = 1
    for L6_2 = L3_2, L4_2, L5_2 do
      L7_2 = L2_2
      L8_2 = A1_2.___BreakExecution___
      if false == L8_2 then
        gCurrentBuildingBlockNumber = L6_2
        L8_2 = gCurrentBuildingBlockNumber
        L8_2 = A0_2[L8_2]
        gCurrentBuildingBlock = L8_2
        L8_2 = gDebugMode
        if L8_2 then
          L8_2 = functionToStringMapInitialized
          if not L8_2 then
            L8_2 = pairs
            L9_2 = _G
            L8_2, L9_2, L10_2 = L8_2(L9_2)
            for L11_2, L12_2 in L8_2, L9_2, L10_2 do
              L13_2 = type
              L14_2 = L12_2
              L13_2 = L13_2(L14_2)
              if L13_2 == "function" then
                L13_2 = functionToStringMap
                L13_2[L12_2] = L11_2
                L13_2 = gCurrentBuildingBlock
                L13_2 = L13_2.Function
                if L12_2 == L13_2 then
                  gCurrentBuildingBlockString = L11_2
                end
              end
            end
            L8_2 = true
            functionToStringMapInitialized = L8_2
          else
            L8_2 = functionToStringMap
            L9_2 = gCurrentBuildingBlock
            L9_2 = L9_2.Function
            L8_2 = L8_2[L9_2]
            gCurrentBuildingBlockString = L8_2
          end
        else
          L8_2 = L7_2
          L9_2 = "."
          L10_2 = gCurrentBuildingBlockNumber
          L8_2 = L8_2 .. L9_2 .. L10_2
          gCurrentBuildingBlockString = L8_2
        end
        L8_2 = gCurrentBuildingBlock
        L8_2 = L8_2.Function
        L9_2 = A1_2
        L10_2 = gCurrentBuildingBlock
        L10_2 = L10_2.Params
        L11_2 = gCurrentBuildingBlock
        L11_2 = L11_2.SubBlocks
        L8_2(L9_2, L10_2, L11_2)
      end
    end
    gCurrentBuildingBlockString = L2_2
    L3_2 = -1
    gCurrentBuildingBlockNumber = L3_2
    L3_2 = nil
    gCurrentBuildingBlock = L3_2
  end
end
ExecuteBuildingBlocks = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2
  L2_2 = GetParam
  L3_2 = "Required"
  L4_2 = A0_2
  L5_2 = A1_2
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = gDebugMode
  if L3_2 ~= nil and L2_2 == nil then
    L3_2 = ALREADY_WARNED
    L4_2 = A1_2.RequiredVar
    L3_2 = L3_2[L4_2]
    if L3_2 == nil then
      L3_2 = A1_2.RequiredVar
      if L3_2 ~= nil then
        L3_2 = A1_2.RequiredVarTable
        if L3_2 == nil then
          L3_2 = "PassThroughParams"
        end
        L4_2 = DebugClientPrint
        L5_2 = "Missing Required Variable: "
        L6_2 = L3_2
        L7_2 = "."
        L8_2 = A1_2.RequiredVar
        L9_2 = " Current block data is: "
        L10_2 = tostring
        L11_2 = gCurrentBuildingBlockString
        L10_2 = L10_2(L11_2)
        L5_2 = L5_2 .. L6_2 .. L7_2 .. L8_2 .. L9_2 .. L10_2
        L4_2(L5_2)
        L4_2 = ALREADY_WARNED
        L5_2 = A1_2.RequiredVar
        L4_2[L5_2] = true
      end
    end
  end
end
BBRequireVar = L0_1
L0_1 = {}
ALREADY_WARNED = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2
  L3_2 = ExecuteBuildingBlocks
  L4_2 = A2_2
  L5_2 = A0_2
  L3_2(L4_2, L5_2)
end
BBCom = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2
  L3_2 = A1_2.IsConditionTrue
  L4_2 = A0_2
  L3_2 = L3_2(L4_2)
  if L3_2 then
    L3_2 = ExecuteBuildingBlocks
    L4_2 = A2_2
    L5_2 = A0_2
    L3_2(L4_2, L5_2)
  end
end
BBConditionallyExecute = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.SrcVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.ToSay
  if not L3_2 then
    L3_2 = ""
  end
  L4_2 = ClientPrint
  L5_2 = tostring
  L6_2 = L3_2
  L5_2 = L5_2(L6_2)
  L6_2 = ": "
  L7_2 = type
  L8_2 = A1_2.SrcVar
  L8_2 = L2_2[L8_2]
  L7_2 = L7_2(L8_2)
  L5_2 = L5_2 .. L6_2 .. L7_2
  L4_2(L5_2)
end
BBPrintTypeToChat = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2
  L2_2 = A1_2.ToSay
  if not L2_2 then
    L2_2 = ""
  end
  L3_2 = A1_2.Color
  if not L3_2 then
    L3_2 = "FFFFFF"
  end
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.SrcVarTable
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.SrcVar
  L5_2 = L4_2[L5_2]
  L6_2 = "<font color=\""
  L7_2 = L3_2
  L8_2 = "\">"
  L6_2 = L6_2 .. L7_2 .. L8_2
  if L2_2 == "" then
    L7_2 = A1_2.SrcVarTable
    if L7_2 ~= nil then
      L7_2 = L6_2
      L8_2 = A1_2.SrcVarTable
      L9_2 = "."
      L10_2 = A1_2.SrcVar
      if not L10_2 then
        L10_2 = ""
      end
      L6_2 = L7_2 .. L8_2 .. L9_2 .. L10_2
    else
      L7_2 = L6_2
      L8_2 = A1_2.SrcVar
      if not L8_2 then
        L8_2 = ""
      end
      L6_2 = L7_2 .. L8_2
    end
  else
    L7_2 = L6_2
    L8_2 = L2_2
    L6_2 = L7_2 .. L8_2
  end
  L7_2 = type
  L8_2 = L5_2
  L7_2 = L7_2(L8_2)
  if L7_2 ~= "table" then
    L7_2 = type
    L8_2 = L5_2
    L7_2 = L7_2(L8_2)
    if L7_2 ~= "nil" then
      L7_2 = L6_2
      L8_2 = " "
      L9_2 = tostring
      L10_2 = L5_2
      L9_2 = L9_2(L10_2)
      L6_2 = L7_2 .. L8_2 .. L9_2
    end
  end
  L7_2 = L6_2
  L8_2 = "</font>"
  L6_2 = L7_2 .. L8_2
  L7_2 = ClientPrint
  L8_2 = L6_2
  L7_2(L8_2)
  L7_2 = type
  L8_2 = L5_2
  L7_2 = L7_2(L8_2)
  if L7_2 == "table" then
    L7_2 = PrintTableToChat
    L8_2 = L5_2
    L7_2(L8_2)
  end
end
BBPrintToChat = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.TableName
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TableName
  if not L3_2 then
    L3_2 = "PassThroughParams"
  end
  L4_2 = ClientPrint
  L5_2 = L3_2
  L4_2(L5_2)
  L4_2 = PrintTableToChat
  L5_2 = L2_2
  L4_2(L5_2)
end
BBPrintTableToChat = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2, L13_2, L14_2, L15_2, L16_2, L17_2
  if not A1_2 then
    A1_2 = 0
  end
  if 10 < A1_2 then
    return
  end
  L2_2 = nil
  L3_2 = "|"
  L4_2 = 1
  L5_2 = A1_2
  L6_2 = 1
  for L7_2 = L4_2, L5_2, L6_2 do
    L8_2 = L3_2
    L9_2 = "----"
    L3_2 = L8_2 .. L9_2
  end
  L4_2 = L3_2
  L5_2 = "{"
  L4_2 = L4_2 .. L5_2
  L5_2 = L3_2
  L6_2 = "}"
  L5_2 = L5_2 .. L6_2
  L6_2 = ClientPrint
  L7_2 = L4_2
  L6_2(L7_2)
  L6_2 = pairs
  L7_2 = A0_2
  L6_2, L7_2, L8_2 = L6_2(L7_2)
  for L9_2, L10_2 in L6_2, L7_2, L8_2 do
    L11_2 = nil
    L12_2 = tostring
    L13_2 = L9_2
    L12_2 = L12_2(L13_2)
    if L12_2 == "PassThroughParams" then
      L11_2 = "{{ PassThroughParams }}"
    else
      L13_2 = type
      L14_2 = L10_2
      L13_2 = L13_2(L14_2)
      if L13_2 == "table" then
        L11_2 = "Table"
      else
        L13_2 = tostring
        L14_2 = L10_2
        L13_2 = L13_2(L14_2)
        L11_2 = L13_2
      end
    end
    L13_2 = L3_2
    L14_2 = L13_2
    L15_2 = L12_2
    L16_2 = ": "
    L17_2 = L11_2
    L13_2 = L14_2 .. L15_2 .. L16_2 .. L17_2
    L14_2 = ClientPrint
    L15_2 = L13_2
    L14_2(L15_2)
    L14_2 = type
    L15_2 = L10_2
    L14_2 = L14_2(L15_2)
    if L14_2 == "table" then
      L14_2 = PrintTableToChat
      L15_2 = L10_2
      L16_2 = A1_2 + 1
      L14_2(L15_2, L16_2)
    end
  end
  L6_2 = ClientPrint
  L7_2 = L5_2
  L6_2(L7_2)
end
PrintTableToChat = L0_1
function L0_1(A0_2, A1_2)
end
BBDebugPrintToChat = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.TableName
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TableName
  if not L3_2 then
    L3_2 = "PassThroughParams"
  end
  L4_2 = DebugClientPrint
  L5_2 = L3_2
  L4_2(L5_2)
  L4_2 = DebugPrintTableToChat
  L5_2 = L2_2
  L4_2(L5_2)
end
BBDebugPrintTableToChat = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2, L13_2, L14_2, L15_2, L16_2, L17_2
  if not A1_2 then
    A1_2 = 0
  end
  if 10 < A1_2 then
    return
  end
  L2_2 = nil
  L3_2 = "|"
  L4_2 = 1
  L5_2 = A1_2
  L6_2 = 1
  for L7_2 = L4_2, L5_2, L6_2 do
    L8_2 = L3_2
    L9_2 = "----"
    L3_2 = L8_2 .. L9_2
  end
  L4_2 = L3_2
  L5_2 = "{"
  L4_2 = L4_2 .. L5_2
  L5_2 = L3_2
  L6_2 = "}"
  L5_2 = L5_2 .. L6_2
  L6_2 = DebugClientPrint
  L7_2 = L4_2
  L6_2(L7_2)
  L6_2 = pairs
  L7_2 = A0_2
  L6_2, L7_2, L8_2 = L6_2(L7_2)
  for L9_2, L10_2 in L6_2, L7_2, L8_2 do
    L11_2 = nil
    L12_2 = tostring
    L13_2 = L9_2
    L12_2 = L12_2(L13_2)
    if L12_2 == "PassThroughParams" then
      L11_2 = "{{ PassThroughParams }}"
    else
      L13_2 = type
      L14_2 = L10_2
      L13_2 = L13_2(L14_2)
      if L13_2 == "table" then
        L11_2 = "Table"
      else
        L13_2 = tostring
        L14_2 = L10_2
        L13_2 = L13_2(L14_2)
        L11_2 = L13_2
      end
    end
    L13_2 = L3_2
    L14_2 = L13_2
    L15_2 = L12_2
    L16_2 = ": "
    L17_2 = L11_2
    L13_2 = L14_2 .. L15_2 .. L16_2 .. L17_2
    L14_2 = DebugClientPrint
    L15_2 = L13_2
    L14_2(L15_2)
    L14_2 = type
    L15_2 = L10_2
    L14_2 = L14_2(L15_2)
    if L14_2 == "table" then
      L14_2 = DebugPrintTableToChat
      L15_2 = L10_2
      L16_2 = A1_2 + 1
      L14_2(L15_2, L16_2)
    end
  end
  L6_2 = DebugClientPrint
  L7_2 = L5_2
  L6_2(L7_2)
end
DebugPrintTableToChat = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2
  if A1_2 ~= nil then
    L3_2 = A0_2[A1_2]
    if L3_2 == nil and A2_2 then
      L4_2 = {}
      A0_2[A1_2] = L4_2
      L4_2 = A0_2[A1_2]
      return L4_2
    end
  end
  if L3_2 == nil then
    L3_2 = A0_2
  end
  return L3_2
end
GetTable = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2
  L3_2 = GetTable
  L4_2 = A1_2
  L5_2 = A0_2
  L6_2 = "VarTable"
  L5_2 = L5_2 .. L6_2
  L5_2 = A2_2[L5_2]
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = A0_2
  L5_2 = "Var"
  L4_2 = L4_2 .. L5_2
  L4_2 = A2_2[L4_2]
  if L4_2 ~= nil and L3_2 ~= nil then
    L4_2 = A0_2
    L5_2 = "Var"
    L4_2 = L4_2 .. L5_2
    L4_2 = A2_2[L4_2]
    L4_2 = L3_2[L4_2]
    return L4_2
  else
    L4_2 = A2_2[A0_2]
    return L4_2
  end
end
GetParam = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.DestVar
  L4_2 = {}
  L2_2[L3_2] = L4_2
end
BBCreateCustomTable = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.TableNameVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TableNameVar
  L2_2[L3_2] = nil
end
BBDestroyCustomTable = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestTableVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetParam
  L4_2 = "Key"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetParam
  L5_2 = "Value"
  L6_2 = A0_2
  L7_2 = A1_2
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = type
  L6_2 = A1_2.DestTableVar
  L6_2 = L2_2[L6_2]
  L5_2 = L5_2(L6_2)
  if L5_2 == "nil" then
    L5_2 = A1_2.DestTableVar
    L6_2 = {}
    L2_2[L5_2] = L6_2
  end
  L5_2 = type
  L6_2 = A1_2.DestTableVar
  L6_2 = L2_2[L6_2]
  L5_2 = L5_2(L6_2)
  if L5_2 == "table" then
    L5_2 = A1_2.DestTableVar
    L5_2 = L2_2[L5_2]
    L5_2[L3_2] = L4_2
  end
end
BBSetKeyValueInCustomTable = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.SrcTableVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetParam
  L4_2 = "SrcKey"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = type
  L5_2 = A1_2.SrcTableVar
  L5_2 = L2_2[L5_2]
  L4_2 = L4_2(L5_2)
  if L4_2 == "table" then
    L4_2 = GetTable
    L5_2 = A0_2
    L6_2 = A1_2.DestVarTable
    L7_2 = true
    L4_2 = L4_2(L5_2, L6_2, L7_2)
    L5_2 = A1_2.DestVar
    L6_2 = A1_2.SrcTableVar
    L6_2 = L2_2[L6_2]
    L6_2 = L6_2[L3_2]
    L4_2[L5_2] = L6_2
  end
end
BBGetKeyValueInCustomTable = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestTableVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetParam
  L4_2 = "DestIndex"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetParam
  L5_2 = "Value"
  L6_2 = A0_2
  L7_2 = A1_2
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = type
  L6_2 = A1_2.DestTableVar
  L6_2 = L2_2[L6_2]
  L5_2 = L5_2(L6_2)
  if L5_2 == "nil" then
    L5_2 = A1_2.DestTableVar
    L6_2 = {}
    L2_2[L5_2] = L6_2
  end
  L5_2 = type
  L6_2 = A1_2.DestTableVar
  L6_2 = L2_2[L6_2]
  L5_2 = L5_2(L6_2)
  if L5_2 == "table" then
    if L3_2 then
      L5_2 = table
      L5_2 = L5_2.insert
      L6_2 = A1_2.DestTableVar
      L6_2 = L2_2[L6_2]
      L7_2 = L3_2
      L8_2 = L4_2
      L5_2(L6_2, L7_2, L8_2)
      L5_2 = A1_2.OutIndexVar
      if L5_2 then
        L5_2 = GetTable
        L6_2 = A0_2
        L7_2 = A1_2.OutIndexVarTable
        L8_2 = true
        L5_2 = L5_2(L6_2, L7_2, L8_2)
        L6_2 = A1_2.OutIndexVar
        L5_2[L6_2] = L3_2
      end
    else
      L5_2 = table
      L5_2 = L5_2.insert
      L6_2 = A1_2.DestTableVar
      L6_2 = L2_2[L6_2]
      L7_2 = L4_2
      L5_2(L6_2, L7_2)
      L5_2 = A1_2.OutIndexVar
      if L5_2 then
        L5_2 = GetTable
        L6_2 = A0_2
        L7_2 = A1_2.OutIndexVarTable
        L8_2 = true
        L5_2 = L5_2(L6_2, L7_2, L8_2)
        L6_2 = A1_2.OutIndexVar
        L7_2 = table
        L7_2 = L7_2.getn
        L8_2 = A1_2.DestTableVar
        L8_2 = L2_2[L8_2]
        L7_2 = L7_2(L8_2)
        L5_2[L6_2] = L7_2
      end
    end
  end
end
BBInsertIntoInCustomTable = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.TableVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetParam
  L4_2 = "Index"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetParam
  L5_2 = "Key"
  L6_2 = A0_2
  L7_2 = A1_2
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = type
  L6_2 = A1_2.TableVar
  L6_2 = L2_2[L6_2]
  L5_2 = L5_2(L6_2)
  if L5_2 == "nil" then
    L5_2 = DebugClientPrint
    L6_2 = "Table specified does not exist: "
    L7_2 = tostring
    L8_2 = A1_2.TableVarTable
    L7_2 = L7_2(L8_2)
    L8_2 = "."
    L9_2 = tostring
    L10_2 = A1_2.TableVar
    L9_2 = L9_2(L10_2)
    L6_2 = L6_2 .. L7_2 .. L8_2 .. L9_2
    L5_2(L6_2)
    return
  end
  L5_2 = type
  L6_2 = A1_2.TableVar
  L6_2 = L2_2[L6_2]
  L5_2 = L5_2(L6_2)
  if L5_2 == "table" then
    if L4_2 then
      L5_2 = A1_2.TableVar
      L5_2 = L2_2[L5_2]
      L5_2[L4_2] = nil
    elseif L3_2 then
      L5_2 = table
      L5_2 = L5_2.remove
      L6_2 = A1_2.TableVar
      L6_2 = L2_2[L6_2]
      L7_2 = L3_2
      L5_2(L6_2, L7_2)
    else
      L5_2 = DebugClientPrint
      L6_2 = "Specified index/key was nil: "
      L7_2 = tostring
      L8_2 = A1_2.IndexVarTable
      L7_2 = L7_2(L8_2)
      L8_2 = "."
      L9_2 = tostring
      L10_2 = A1_2.IndexVar
      L9_2 = L9_2(L10_2)
      L6_2 = L6_2 .. L7_2 .. L8_2 .. L9_2
      L5_2(L6_2)
    end
  end
end
BBRemoveFromCustomTable = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2, L13_2, L14_2, L15_2
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.TableVarTable
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = type
  L5_2 = A1_2.TableVar
  L5_2 = L3_2[L5_2]
  L4_2 = L4_2(L5_2)
  if L4_2 == "table" then
    L4_2 = A1_2.SortedByKeys
    if L4_2 then
      L4_2 = {}
      L5_2 = pairs
      L6_2 = A1_2.TableVar
      L6_2 = L3_2[L6_2]
      L5_2, L6_2, L7_2 = L5_2(L6_2)
      for L8_2, L9_2 in L5_2, L6_2, L7_2 do
        L10_2 = table
        L10_2 = L10_2.insert
        L11_2 = L4_2
        L12_2 = L8_2
        L10_2(L11_2, L12_2)
      end
      L5_2 = table
      L5_2 = L5_2.sort
      L6_2 = L4_2
      L5_2(L6_2)
      L5_2 = pairs
      L6_2 = L4_2
      L5_2, L6_2, L7_2 = L5_2(L6_2)
      for L8_2, L9_2 in L5_2, L6_2, L7_2 do
        L10_2 = A1_2.TableVar
        L10_2 = L3_2[L10_2]
        L10_2 = L10_2[L9_2]
        L11_2 = GetTable
        L12_2 = A0_2
        L13_2 = A1_2.DestKeyVarTable
        L14_2 = true
        L11_2 = L11_2(L12_2, L13_2, L14_2)
        L12_2 = GetTable
        L13_2 = A0_2
        L14_2 = A1_2.DestValueVarTable
        L15_2 = true
        L12_2 = L12_2(L13_2, L14_2, L15_2)
        L13_2 = A1_2.DestKeyVar
        L11_2[L13_2] = L9_2
        L13_2 = A1_2.DestValueVar
        L12_2[L13_2] = L10_2
        L13_2 = ExecuteBuildingBlocks
        L14_2 = A2_2
        L15_2 = A0_2
        L13_2(L14_2, L15_2)
      end
    else
      L4_2 = pairs
      L5_2 = A1_2.TableVar
      L5_2 = L3_2[L5_2]
      L4_2, L5_2, L6_2 = L4_2(L5_2)
      for L7_2, L8_2 in L4_2, L5_2, L6_2 do
        L9_2 = GetTable
        L10_2 = A0_2
        L11_2 = A1_2.DestKeyVarTable
        L12_2 = true
        L9_2 = L9_2(L10_2, L11_2, L12_2)
        L10_2 = GetTable
        L11_2 = A0_2
        L12_2 = A1_2.DestValueVarTable
        L13_2 = true
        L10_2 = L10_2(L11_2, L12_2, L13_2)
        L11_2 = A1_2.DestKeyVar
        L9_2[L11_2] = L7_2
        L11_2 = A1_2.DestValueVar
        L10_2[L11_2] = L8_2
        L11_2 = ExecuteBuildingBlocks
        L12_2 = A2_2
        L13_2 = A0_2
        L11_2(L12_2, L13_2)
      end
    end
  end
end
BBForEachInCustomTable = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.SrcTableVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetParam
  L4_2 = "Value"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.MatchingKeyVarTable
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = GetTable
  L6_2 = A0_2
  L7_2 = A1_2.WasFoundVarTable
  L8_2 = false
  L5_2 = L5_2(L6_2, L7_2, L8_2)
  L6_2 = A1_2.WasFoundVar
  L5_2[L6_2] = false
  L6_2 = type
  L7_2 = A1_2.SrcTableVar
  L7_2 = L2_2[L7_2]
  L6_2 = L6_2(L7_2)
  if L6_2 == "table" then
    L6_2 = GetTable
    L7_2 = A0_2
    L8_2 = A1_2.DestVarTable
    L9_2 = true
    L6_2 = L6_2(L7_2, L8_2, L9_2)
    L7_2 = pairs
    L8_2 = L6_2
    L7_2, L8_2, L9_2 = L7_2(L8_2)
    for L10_2, L11_2 in L7_2, L8_2, L9_2 do
      if L11_2 == L3_2 then
        L12_2 = A1_2.WasFoundVar
        L5_2[L12_2] = true
        L12_2 = A1_2.MatchingKeyVar
        L4_2[L12_2] = L10_2
      end
    end
  end
end
BBCustomTableContainsValue = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.TableVarTable
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.SizeVarTable
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = 0
  L6_2 = pairs
  L7_2 = A1_2.TableVar
  L7_2 = L3_2[L7_2]
  L6_2, L7_2, L8_2 = L6_2(L7_2)
  for L9_2, L10_2 in L6_2, L7_2, L8_2 do
    L5_2 = L5_2 + 1
  end
  L6_2 = A1_2.SizeVar
  L4_2[L6_2] = L5_2
end
BBGetSizeOfCustomTable = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2, L13_2, L14_2, L15_2, L16_2, L17_2, L18_2, L19_2
  L2_2 = GetParam
  L3_2 = "X"
  L4_2 = A0_2
  L5_2 = A1_2
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetParam
  L4_2 = "Z"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = 2
  L5_2 = 1
  L6_2 = 0
  L7_2 = 4
  L8_2 = 3
  L9_2 = L3_2 - L2_2
  L10_2 = 14500 - L3_2
  L11_2 = L2_2 * 0.965
  L10_2 = L10_2 - L11_2
  L11_2 = L5_2
  L12_2 = L5_2
  L13_2 = true
  if 12250 < L3_2 then
    L12_2 = L4_2
  elseif L3_2 < 2650 then
    L12_2 = L6_2
  elseif 12250 < L2_2 then
    L12_2 = L6_2
  elseif L2_2 < 2650 then
    L12_2 = L4_2
  elseif 4500 < L9_2 then
    L12_2 = L4_2
  elseif L9_2 < -4500 then
    L12_2 = L6_2
  end
  if 13000 < L3_2 then
    L11_2 = L4_2
  elseif L3_2 < 1800 then
    L11_2 = L6_2
  elseif 13000 < L2_2 then
    L11_2 = L6_2
  elseif L2_2 < 1800 then
    L11_2 = L4_2
  elseif 1150 < L9_2 then
    L11_2 = L7_2
  elseif L9_2 < -1150 then
    L11_2 = L8_2
  end
  if 0 < L10_2 then
    L13_2 = false
  end
  L14_2 = GetTable
  L15_2 = A0_2
  L16_2 = A1_2.NearLaneVarTable
  L17_2 = false
  L14_2 = L14_2(L15_2, L16_2, L17_2)
  L15_2 = A1_2.NearLaneVar
  L14_2[L15_2] = L12_2
  L15_2 = GetTable
  L16_2 = A0_2
  L17_2 = A1_2.NearSectionVarTable
  L18_2 = false
  L15_2 = L15_2(L16_2, L17_2, L18_2)
  L16_2 = A1_2.NearSectionVar
  L15_2[L16_2] = L11_2
  L16_2 = GetTable
  L17_2 = A0_2
  L18_2 = A1_2.BlueSideVarTable
  L19_2 = false
  L16_2 = L16_2(L17_2, L18_2, L19_2)
  L17_2 = A1_2.BlueSideVar
  L16_2[L17_2] = L13_2
end
BBGetLocationHints = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = nil
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.SpellSlotVarTable
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.SpellSlotVar
  if L5_2 ~= nil and L4_2 ~= nil then
    L5_2 = A1_2.SpellSlotVar
    L3_2 = L4_2[L5_2]
  else
    L3_2 = A1_2.SpellSlotValue
  end
  L5_2 = A1_2.Function
  L6_2 = A1_2.OwnerVar
  L6_2 = A0_2[L6_2]
  L7_2 = L3_2
  L8_2 = A1_2.SpellbookType
  L9_2 = A1_2.SlotType
  L5_2 = L5_2(L6_2, L7_2, L8_2, L9_2)
  L6_2 = SetVarInTable
  L7_2 = A0_2
  L8_2 = A1_2
  L9_2 = L5_2
  L6_2(L7_2, L8_2, L9_2)
end
BBGetSlotSpellInfo = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.SrcVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = nil
  L4_2 = A1_2.SrcVar
  if L4_2 ~= nil and L2_2 ~= nil then
    L4_2 = A1_2.SrcVar
    L3_2 = L2_2[L4_2]
  else
    L3_2 = A1_2.SrcValue
  end
  L4_2 = nil
  L5_2 = GetTable
  L6_2 = A0_2
  L7_2 = A1_2.SpellSlotVarTable
  L8_2 = false
  L5_2 = L5_2(L6_2, L7_2, L8_2)
  L6_2 = A1_2.SpellSlotVar
  if L6_2 ~= nil and L5_2 ~= nil then
    L6_2 = A1_2.SpellSlotVar
    L4_2 = L5_2[L6_2]
  else
    L4_2 = A1_2.SpellSlotValue
  end
  L6_2 = SetSlotSpellCooldownTime
  L7_2 = A1_2.OwnerVar
  L7_2 = A0_2[L7_2]
  L8_2 = L4_2
  L9_2 = A1_2.SpellbookType
  L10_2 = A1_2.SlotType
  L11_2 = L3_2
  L6_2(L7_2, L8_2, L9_2, L10_2, L11_2)
end
BBSetSlotSpellCooldownTime = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = A0_2.Level
  if L2_2 ~= nil then
    L2_2 = A1_2.SrcValueByLevel
    if L2_2 ~= nil then
      L2_2 = A1_2.SrcValueByLevel
      L3_2 = A0_2.Level
      L2_2 = L2_2[L3_2]
      A0_2.ReturnValue = L2_2
  end
  else
    L2_2 = GetTable
    L3_2 = A0_2
    L4_2 = A1_2.SrcVarTable
    L5_2 = false
    L2_2 = L2_2(L3_2, L4_2, L5_2)
    L3_2 = A1_2.SrcVar
    if L3_2 ~= nil and L2_2 ~= nil then
      L3_2 = A1_2.SrcVar
      L3_2 = L2_2[L3_2]
      A0_2.ReturnValue = L3_2
    else
      L3_2 = A1_2.SrcValue
      A0_2.ReturnValue = L3_2
    end
  end
end
BBSetReturnValue = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2
  L2_2 = type
  L3_2 = A0_2
  L2_2 = L2_2(L3_2)
  if L2_2 == "string" then
    L2_2 = type
    L3_2 = A1_2
    L2_2 = L2_2(L3_2)
    if L2_2 == "string" then
      L2_2 = string
      L2_2 = L2_2.lower
      L3_2 = A0_2
      L2_2 = L2_2(L3_2)
      A0_2 = L2_2
      L2_2 = string
      L2_2 = L2_2.lower
      L3_2 = A1_2
      L2_2 = L2_2(L3_2)
      A1_2 = L2_2
    end
  end
  L2_2 = A0_2 == A1_2
  return L2_2
end
CO_EQUAL = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2
  L2_2 = type
  L3_2 = A0_2
  L2_2 = L2_2(L3_2)
  if L2_2 == "string" then
    L2_2 = type
    L3_2 = A1_2
    L2_2 = L2_2(L3_2)
    if L2_2 == "string" then
      L2_2 = string
      L2_2 = L2_2.lower
      L3_2 = A0_2
      L2_2 = L2_2(L3_2)
      A0_2 = L2_2
      L2_2 = string
      L2_2 = L2_2.lower
      L3_2 = A1_2
      L2_2 = L2_2(L3_2)
      A1_2 = L2_2
    end
  end
  L2_2 = A0_2 ~= A1_2
  return L2_2
end
CO_NOT_EQUAL = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A0_2 < A1_2
  return L2_2
end
CO_LESS_THAN = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A1_2 < A0_2
  return L2_2
end
CO_GREATER_THAN = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A0_2 <= A1_2
  return L2_2
end
CO_LESS_THAN_OR_EQUAL = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A1_2 <= A0_2
  return L2_2
end
CO_GREATER_THAN_OR_EQUAL = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = GetTeamID
  L3_2 = A0_2
  L2_2 = L2_2(L3_2)
  L3_2 = GetTeamID
  L4_2 = A1_2
  L3_2 = L3_2(L4_2)
  L2_2 = L2_2 == L3_2
  return L2_2
end
CO_SAME_TEAM = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = GetTeamID
  L3_2 = A0_2
  L2_2 = L2_2(L3_2)
  L3_2 = GetTeamID
  L4_2 = A1_2
  L3_2 = L3_2(L4_2)
  L2_2 = L2_2 ~= L3_2
  return L2_2
end
CO_DIFFERENT_TEAM = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = GetSourceType
  L2_2 = L2_2()
  L2_2 = L2_2 == A1_2
  return L2_2
end
CO_DAMAGE_SOURCETYPE_IS = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = GetSourceType
  L2_2 = L2_2()
  L2_2 = L2_2 ~= A1_2
  return L2_2
end
CO_DAMAGE_SOURCETYPE_IS_NOT = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsObjectAI
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_TYPE_AI = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsObjectAI
  L2_2 = A0_2
  L1_2 = L1_2(L2_2)
  L1_2 = L1_2 ~= true
  return L1_2
end
CO_IS_NOT_AI = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsObjectHero
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_TYPE_HERO = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsObjectHero
  L2_2 = A0_2
  L1_2 = L1_2(L2_2)
  L1_2 = L1_2 ~= true
  return L1_2
end
CO_IS_NOT_HERO = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsClone
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_CLONE = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsClone
  L2_2 = A0_2
  L1_2 = L1_2(L2_2)
  L1_2 = L1_2 ~= true
  return L1_2
end
CO_IS_NOT_CLONE = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsMelee
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_MELEE = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsMelee
  L2_2 = A0_2
  L1_2 = L1_2(L2_2)
  L1_2 = L1_2 ~= true
  return L1_2
end
CO_IS_RANGED = L0_1
function L0_1(A0_2)
  local L1_2
  L1_2 = math
  L1_2 = L1_2.random
  L1_2 = L1_2()
  L1_2 = A0_2 > L1_2
  return L1_2
end
CO_RANDOM_CHANCE_LESS_THAN = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsTurretAI
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_TYPE_TURRET = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsTurretAI
  L2_2 = A0_2
  L1_2 = L1_2(L2_2)
  L1_2 = L1_2 ~= true
  return L1_2
end
CO_IS_NOT_TURRET = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsDampener
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_TYPE_INHIBITOR = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsDampener
  L2_2 = A0_2
  L1_2 = L1_2(L2_2)
  L1_2 = L1_2 ~= true
  return L1_2
end
CO_IS_NOT_INHIBITOR = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsHQ
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_TYPE_NEXUS = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsHQ
  L2_2 = A0_2
  L1_2 = L1_2(L2_2)
  L1_2 = L1_2 ~= true
  return L1_2
end
CO_IS_NOT_NEXUS = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsDead
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_DEAD = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsDead
  L2_2 = A0_2
  L1_2 = L1_2(L2_2)
  L1_2 = L1_2 ~= true
  return L1_2
end
CO_IS_NOT_DEAD = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsDeadOrZombie
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_DEAD_OR_ZOMBIE = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = BBIsTargetInFrontOfMe
  L3_2 = A0_2
  L4_2 = A1_2
  return L2_2(L3_2, L4_2)
end
CO_IS_TARGET_IN_FRONT_OF_ME = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = BBIsTargetBehindMe
  L3_2 = A0_2
  L4_2 = A1_2
  return L2_2(L3_2, L4_2)
end
CO_IS_TARGET_BEHIND_ME = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsWard
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_WARD = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsStructure
  L2_2 = A0_2
  return L1_2(L2_2)
end
CO_IS_STRUCTURE = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = IsStructure
  L2_2 = A0_2
  L1_2 = L1_2(L2_2)
  L1_2 = L1_2 ~= true
  return L1_2
end
CO_IS_NOT_STRUCTURE = L0_1
function L0_1(A0_2)
  local L1_2
  L1_2 = A0_2 ~= nil
  return L1_2
end
CO_IS_NOT_NIL = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.MacroVarTable
  L6_2 = true
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = A1_2.MacroVar
  L3_2[L4_2] = A2_2
end
BBCreateMacro = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = GetParam
  L3_2 = "Macro"
  L4_2 = A0_2
  L5_2 = A1_2
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  if L2_2 ~= nil then
    L3_2 = type
    L4_2 = L2_2
    L3_2 = L3_2(L4_2)
    if L3_2 == "table" then
      L3_2 = ExecuteBuildingBlocks
      L4_2 = L2_2
      L5_2 = A0_2
      L3_2(L4_2, L5_2)
  end
  else
    L3_2 = DebugClientPrint
    L4_2 = "Designer Error: Macro variable is not initialized inside of event. Macro Variable was: "
    L5_2 = tostring
    L6_2 = A1_2.MacroVar
    L5_2 = L5_2(L6_2)
    L6_2 = " and current block data is: "
    L7_2 = gCurrentBuildingBlockString
    L8_2 = ". This error probably occurred due to variable not being initialized yet, being misnamed or two events being called out of sequence. It is highly recommended that script macros either be initialized OnBuffActivate, OnCharActivate or in the same event. Pro Devs may violate this rule at the risk of being forced to read this error message again."
    L4_2 = L4_2 .. L5_2 .. L6_2 .. L7_2 .. L8_2
    L3_2(L4_2)
  end
end
BBRunMacro = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.Src1VarTable
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.Src2VarTable
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = false
  L6_2 = nil
  if L3_2 ~= nil then
    L7_2 = A1_2.Src1Var
    if L7_2 ~= nil then
      L7_2 = A1_2.Src1Var
      L6_2 = L3_2[L7_2]
  end
  else
    L6_2 = A1_2.Value1
  end
  if L4_2 ~= nil then
    L7_2 = A1_2.Src2Var
    if L7_2 ~= nil then
      L7_2 = A1_2.CompareOp
      L8_2 = L6_2
      L9_2 = A1_2.Src2Var
      L9_2 = L4_2[L9_2]
      L7_2 = L7_2(L8_2, L9_2)
      L5_2 = L7_2
  end
  else
    L7_2 = A1_2.CompareOp
    L8_2 = L6_2
    L9_2 = A1_2.Value2
    L7_2 = L7_2(L8_2, L9_2)
    L5_2 = L7_2
  end
  if L5_2 then
    L7_2 = ExecuteBuildingBlocks
    L8_2 = A2_2
    L9_2 = A0_2
    L7_2(L8_2, L9_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIf = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2
  L3_2 = A0_2.LastIfSucceeded
  if L3_2 == false then
    L3_2 = ExecuteBuildingBlocks
    L4_2 = A2_2
    L5_2 = A0_2
    L3_2(L4_2, L5_2)
    A0_2.LastIfSucceeded = true
  end
end
BBElse = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2
  L3_2 = A0_2.LastIfSucceeded
  if L3_2 == true then
    L3_2 = ExecuteBuildingBlocks
    L4_2 = A2_2
    L5_2 = A0_2
    L3_2(L4_2, L5_2)
    A0_2.LastIfSucceeded = true
  else
    L3_2 = BBIf
    L4_2 = A0_2
    L5_2 = A1_2
    L6_2 = A2_2
    L3_2(L4_2, L5_2, L6_2)
  end
end
BBOrIf = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2
  L3_2 = A0_2.LastIfSucceeded
  if L3_2 == true then
    L3_2 = BBIf
    L4_2 = A0_2
    L5_2 = A1_2
    L6_2 = A2_2
    L3_2(L4_2, L5_2, L6_2)
  end
end
BBAndIf = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L3_2 = A0_2.LastIfSucceeded
  if L3_2 == false then
    L3_2 = GetTable
    L4_2 = A0_2
    L5_2 = A1_2.Src1VarTable
    L6_2 = false
    L3_2 = L3_2(L4_2, L5_2, L6_2)
    L4_2 = GetTable
    L5_2 = A0_2
    L6_2 = A1_2.Src2VarTable
    L7_2 = false
    L4_2 = L4_2(L5_2, L6_2, L7_2)
    L5_2 = false
    L6_2 = nil
    if L3_2 ~= nil then
      L7_2 = A1_2.Src1Var
      if L7_2 ~= nil then
        L7_2 = A1_2.Src1Var
        L6_2 = L3_2[L7_2]
    end
    else
      L6_2 = A1_2.Value1
    end
    if L4_2 ~= nil then
      L7_2 = A1_2.Src2Var
      if L7_2 ~= nil then
        L7_2 = A1_2.CompareOp
        L8_2 = L6_2
        L9_2 = A1_2.Src2Var
        L9_2 = L4_2[L9_2]
        L7_2 = L7_2(L8_2, L9_2)
        L5_2 = L7_2
    end
    else
      L7_2 = A1_2.CompareOp
      L8_2 = L6_2
      L9_2 = A1_2.Value2
      L7_2 = L7_2(L8_2, L9_2)
      L5_2 = L7_2
    end
    if L5_2 then
      L7_2 = ExecuteBuildingBlocks
      L8_2 = A2_2
      L9_2 = A0_2
      L7_2(L8_2, L9_2)
      A0_2.LastIfSucceeded = true
    else
      A0_2.LastIfSucceeded = false
    end
  end
end
BBElseIf = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2
  L4_2 = A1_2.TargetVar
  if L4_2 ~= nil then
    L4_2 = A1_2.TargetVar
    L3_2 = A0_2[L4_2]
  else
    L3_2 = A0_2.Target
  end
  L4_2 = HasBuffOfType
  L5_2 = L3_2
  L6_2 = A1_2.BuffType
  L4_2 = L4_2(L5_2, L6_2)
  if L4_2 then
    L5_2 = ExecuteBuildingBlocks
    L6_2 = A2_2
    L7_2 = A0_2
    L5_2(L6_2, L7_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfHasBuffOfType = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L5_2 = A1_2.OwnerVar
  if L5_2 ~= nil then
    L5_2 = A1_2.OwnerVar
    L3_2 = A0_2[L5_2]
  else
    L3_2 = A0_2.Owner
  end
  L5_2 = A1_2.AttackerVar
  if L5_2 ~= nil then
    L5_2 = A1_2.AttackerVar
    L4_2 = A0_2[L5_2]
  else
    L4_2 = A0_2.Attacker
  end
  L5_2 = SpellBuffCount
  L6_2 = L3_2
  L7_2 = A1_2.BuffName
  L8_2 = L4_2
  L5_2 = L5_2(L6_2, L7_2, L8_2)
  L5_2 = 0 < L5_2
  if L5_2 then
    L6_2 = ExecuteBuildingBlocks
    L7_2 = A2_2
    L8_2 = A0_2
    L6_2(L7_2, L8_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfHasBuff = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2
  L3_2 = BBIsMissileAutoAttack
  L4_2 = A0_2
  L5_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2)
  if L3_2 then
    L4_2 = ExecuteBuildingBlocks
    L5_2 = A2_2
    L6_2 = A0_2
    L4_2(L5_2, L6_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfMissileIsAutoAttack = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2
  L3_2 = BBIsMissileConsideredAsAutoAttack
  L4_2 = A0_2
  L5_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2)
  if L3_2 then
    L4_2 = ExecuteBuildingBlocks
    L5_2 = A2_2
    L6_2 = A0_2
    L4_2(L5_2, L6_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfMissileConsideredAsAutoAttack = L0_1
function L0_1(A0_2, A1_2)
  A0_2.___BreakExecution___ = true
end
BBBreakExecution = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L5_2 = A1_2.OwnerVar
  if L5_2 ~= nil then
    L5_2 = A1_2.OwnerVar
    L3_2 = A0_2[L5_2]
  else
    L3_2 = A0_2.Owner
  end
  L5_2 = A1_2.CasterVar
  if L5_2 ~= nil then
    L5_2 = A1_2.CasterVar
    L4_2 = A0_2[L5_2]
  else
    L4_2 = A0_2.Caster
  end
  L5_2 = SpellBuffCount
  L6_2 = L3_2
  L7_2 = A1_2.BuffName
  L8_2 = L4_2
  L5_2 = L5_2(L6_2, L7_2, L8_2)
  L5_2 = L5_2 <= 0
  if L5_2 then
    L6_2 = ExecuteBuildingBlocks
    L7_2 = A2_2
    L8_2 = A0_2
    L6_2(L7_2, L8_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfNotHasBuff = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2
  L4_2 = A1_2.TargetVar
  if L4_2 ~= nil then
    L4_2 = A1_2.TargetVar
    L3_2 = A0_2[L4_2]
  else
    L3_2 = A0_2.Owner
  end
  L4_2 = HasUnitTag
  L5_2 = L3_2
  L6_2 = A1_2.UnitTag
  L4_2 = L4_2(L5_2, L6_2)
  L5_2 = A1_2.InvertResult
  if L4_2 ~= L5_2 then
    L4_2 = ExecuteBuildingBlocks
    L5_2 = A2_2
    L6_2 = A0_2
    L4_2(L5_2, L6_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfHasUnitTag = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L5_2 = A1_2.OwnerVar
  if L5_2 ~= nil then
    L5_2 = A1_2.OwnerVar
    L3_2 = A0_2[L5_2]
  else
    L3_2 = A0_2.Owner
  end
  L5_2 = HasPARType
  L6_2 = L3_2
  L7_2 = A1_2.PARType
  L5_2 = L5_2(L6_2, L7_2)
  if L5_2 then
    L6_2 = ExecuteBuildingBlocks
    L7_2 = A2_2
    L8_2 = A0_2
    L6_2(L7_2, L8_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfPARTypeEquals = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L5_2 = A1_2.OwnerVar
  if L5_2 ~= nil then
    L5_2 = A1_2.OwnerVar
    L3_2 = A0_2[L5_2]
  else
    L3_2 = A0_2.Owner
  end
  L5_2 = HasPARType
  L6_2 = L3_2
  L7_2 = A1_2.PARType
  L5_2 = L5_2(L6_2, L7_2)
  L5_2 = not L5_2
  if L5_2 then
    L6_2 = ExecuteBuildingBlocks
    L7_2 = A2_2
    L8_2 = A0_2
    L6_2(L7_2, L8_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfPARTypeNotEquals = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.Src1VarTable
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.Src2VarTable
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = true
  while L5_2 do
    L6_2 = false
    L7_2 = nil
    if L3_2 ~= nil then
      L8_2 = A1_2.Src1Var
      if L8_2 ~= nil then
        L8_2 = A1_2.Src1Var
        L7_2 = L3_2[L8_2]
    end
    else
      L7_2 = A1_2.Value1
    end
    if L4_2 ~= nil then
      L8_2 = A1_2.Src2Var
      if L8_2 ~= nil then
        L8_2 = A1_2.CompareOp
        L9_2 = L7_2
        L10_2 = A1_2.Src2Var
        L10_2 = L4_2[L10_2]
        L8_2 = L8_2(L9_2, L10_2)
        L6_2 = L8_2
    end
    else
      L8_2 = A1_2.CompareOp
      L9_2 = L7_2
      L10_2 = A1_2.Value2
      L8_2 = L8_2(L9_2, L10_2)
      L6_2 = L8_2
    end
    if L6_2 then
      L8_2 = ExecuteBuildingBlocks
      L9_2 = A2_2
      L10_2 = A0_2
      L8_2(L9_2, L10_2)
    else
      L5_2 = false
    end
  end
end
BBWhile = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A0_2 * A1_2
  return L2_2
end
MO_MULTIPLY = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A0_2 + A1_2
  return L2_2
end
MO_ADD = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A0_2 - A1_2
  return L2_2
end
MO_SUBTRACT = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A0_2 / A1_2
  return L2_2
end
MO_DIVIDE = L0_1
function L0_1(A0_2, A1_2)
  if A0_2 < A1_2 then
    return A0_2
  else
    return A1_2
  end
end
MO_MIN = L0_1
function L0_1(A0_2, A1_2)
  if A1_2 < A0_2 then
    return A0_2
  else
    return A1_2
  end
end
MO_MAX = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A0_2 % A1_2
  return L2_2
end
MO_MODULO = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = math
  L1_2 = L1_2.floor
  L2_2 = A0_2 + 0.5
  return L1_2(L2_2)
end
MO_ROUND = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = math
  L1_2 = L1_2.ceil
  L2_2 = A0_2
  return L1_2(L2_2)
end
MO_ROUNDUP = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = math
  L1_2 = L1_2.floor
  L2_2 = A0_2
  return L1_2(L2_2)
end
MO_ROUNDDOWN = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2
  L1_2 = math
  L1_2 = L1_2.sin
  L2_2 = math
  L2_2 = L2_2.rad
  L3_2 = A0_2
  L2_2, L3_2 = L2_2(L3_2)
  return L1_2(L2_2, L3_2)
end
MO_SIN = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2
  L1_2 = math
  L1_2 = L1_2.cos
  L2_2 = math
  L2_2 = L2_2.rad
  L3_2 = A0_2
  L2_2, L3_2 = L2_2(L3_2)
  return L1_2(L2_2, L3_2)
end
MO_COSINE = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2
  L1_2 = math
  L1_2 = L1_2.tan
  L2_2 = math
  L2_2 = L2_2.rad
  L3_2 = A0_2
  L2_2, L3_2 = L2_2(L3_2)
  return L1_2(L2_2, L3_2)
end
MO_TANGENT = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2
  L1_2 = math
  L1_2 = L1_2.deg
  L2_2 = math
  L2_2 = L2_2.asin
  L3_2 = A0_2
  L2_2, L3_2 = L2_2(L3_2)
  return L1_2(L2_2, L3_2)
end
MO_ASIN = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2
  L1_2 = math
  L1_2 = L1_2.deg
  L2_2 = math
  L2_2 = L2_2.acos
  L3_2 = A0_2
  L2_2, L3_2 = L2_2(L3_2)
  return L1_2(L2_2, L3_2)
end
MO_ACOS = L0_1
function L0_1(A0_2)
  local L1_2, L2_2, L3_2
  L1_2 = math
  L1_2 = L1_2.deg
  L2_2 = math
  L2_2 = L2_2.atan
  L3_2 = A0_2
  L2_2, L3_2 = L2_2(L3_2)
  return L1_2(L2_2, L3_2)
end
MO_ATAN = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = math
  L2_2 = L2_2.pow
  L3_2 = A0_2
  L4_2 = A1_2
  return L2_2(L3_2, L4_2)
end
MO_POW = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = math
  L1_2 = L1_2.sqrt
  L2_2 = A0_2
  return L1_2(L2_2)
end
MO_SQUARE_ROOT = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A0_2 or nil
  if A0_2 then
    L2_2 = A1_2
  end
  return L2_2
end
MO_BOOLEAN_AND = L0_1
function L0_1(A0_2, A1_2)
  local L2_2
  L2_2 = A0_2 or nil
  if not A0_2 then
    L2_2 = A1_2
  end
  return L2_2
end
MO_BOOLEAN_OR = L0_1
function L0_1(A0_2)
  local L1_2
  L1_2 = not A0_2
  return L1_2
end
MO_BOOLEAN_NOT = L0_1
function L0_1(A0_2)
  local L1_2, L2_2
  L1_2 = math
  L1_2 = L1_2.abs
  L2_2 = A0_2
  return L1_2(L2_2)
end
MO_ABS = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = math
  L2_2 = L2_2.random
  L3_2 = A0_2
  L4_2 = A1_2
  return L2_2(L3_2, L4_2)
end
MO_RAND_INT_RANGE = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = GetMathNumber
  L3_2 = A0_2
  L4_2 = A1_2.Src1VarTable
  L5_2 = A1_2.Src1Var
  L6_2 = A1_2.Src1Value
  L2_2 = L2_2(L3_2, L4_2, L5_2, L6_2)
  L3_2 = GetMathNumber
  L4_2 = A0_2
  L5_2 = A1_2.Src2VarTable
  L6_2 = A1_2.Src2Var
  L7_2 = A1_2.Src2Value
  L3_2 = L3_2(L4_2, L5_2, L6_2, L7_2)
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.DestVarTable
  L4_2 = L4_2(L5_2, L6_2)
  L5_2 = A1_2.MathOp
  L6_2 = L2_2
  L7_2 = L3_2
  L5_2 = L5_2(L6_2, L7_2)
  L6_2 = SetVarInTable
  L7_2 = A0_2
  L8_2 = A1_2
  L9_2 = L5_2
  L6_2(L7_2, L8_2, L9_2)
end
BBMath = L0_1
function L0_1(A0_2, A1_2, A2_2, A3_2)
  local L4_2, L5_2, L6_2
  if A2_2 ~= nil then
    L4_2 = GetTable
    L5_2 = A0_2
    L6_2 = A1_2
    L4_2 = L4_2(L5_2, L6_2)
    L5_2 = L4_2[A2_2]
    if L5_2 ~= nil then
      return L5_2
    end
  end
  return A3_2
end
GetMathNumber = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = type
  L3_2 = A1_2
  L2_2 = L2_2(L3_2)
  if L2_2 == "number" then
    return A1_2
  elseif L2_2 == "function" then
    L3_2 = A1_2
    L4_2 = A0_2
    return L3_2(L4_2)
  elseif L2_2 == "string" then
    L3_2 = A0_2[A1_2]
    return L3_2
  end
end
GetNumber = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = VectorAdd
  L3_2 = A0_2
  L4_2 = A1_2
  return L2_2(L3_2, L4_2)
end
VEC_ADD = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = VectorSubtract
  L3_2 = A0_2
  L4_2 = A1_2
  return L2_2(L3_2, L4_2)
end
VEC_SUBTRACT = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = VectorScalarMultiply
  L3_2 = A0_2
  L4_2 = A1_2
  return L2_2(L3_2, L4_2)
end
VEC_SCALAR_MULTIPLY = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = VectorScalarDivide
  L3_2 = A0_2
  L4_2 = A1_2
  return L2_2(L3_2, L4_2)
end
VEC_SCALAR_DIVIDE = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = VectorRotateY
  L3_2 = A0_2
  L4_2 = A1_2
  return L2_2(L3_2, L4_2)
end
VEC_ROTATE = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = BBMath
  L3_2 = A0_2
  L4_2 = A1_2
  L2_2(L3_2, L4_2)
end
BBVectorMath = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = A0_2.InstanceVars
  L3_2 = L2_2.InterpDelta
  if L3_2 == nil then
    L3_2 = A1_2.Amount
    L4_2 = A1_2.AmountVar
    if L4_2 ~= nil then
      L4_2 = GetTable
      L5_2 = A0_2
      L6_2 = A1_2.AmountVarTable
      L4_2 = L4_2(L5_2, L6_2)
      L5_2 = A1_2.AmountVar
      L3_2 = L4_2[L5_2]
    end
    L4_2 = GetPosition
    L5_2 = A1_2.TargetVar
    L5_2 = A0_2[L5_2]
    L4_2 = L4_2(L5_2)
    L2_2.KnockBackStart = L4_2
    L4_2 = GetNormalizedPositionDelta
    L5_2 = A1_2.TargetVar
    L5_2 = A0_2[L5_2]
    L6_2 = A1_2.AttackerVar
    L6_2 = A0_2[L6_2]
    L7_2 = true
    L4_2 = L4_2(L5_2, L6_2, L7_2)
    L5_2 = {}
    L6_2 = L4_2.x
    L6_2 = L6_2 * L3_2
    L5_2.x = L6_2
    L5_2.y = 0
    L6_2 = L4_2.z
    L6_2 = L6_2 * L3_2
    L5_2.z = L6_2
    L2_2.InterpDelta = L5_2
    L5_2 = GetTime
    L5_2 = L5_2()
    L2_2.StartTime = L5_2
    L5_2 = A1_2.KnockBackDuration
    L2_2.KnockBackDuration = L5_2
  end
  L3_2 = A1_2.TargetVar
  L3_2 = A0_2[L3_2]
  L4_2 = GetTime
  L4_2 = L4_2()
  L5_2 = L2_2.StartTime
  L4_2 = L4_2 - L5_2
  L5_2 = L2_2.KnockBackDuration
  L4_2 = L4_2 / L5_2
  L5_2 = {}
  L6_2 = L2_2.KnockBackStart
  L6_2 = L6_2.x
  L7_2 = L2_2.InterpDelta
  L7_2 = L7_2.x
  L7_2 = L7_2 * L4_2
  L6_2 = L6_2 + L7_2
  L5_2.x = L6_2
  L6_2 = L2_2.KnockBackStart
  L6_2 = L6_2.y
  L5_2.y = L6_2
  L6_2 = L2_2.KnockBackStart
  L6_2 = L6_2.z
  L7_2 = L2_2.InterpDelta
  L7_2 = L7_2.z
  L7_2 = L7_2 * L4_2
  L6_2 = L6_2 + L7_2
  L5_2.z = L6_2
  L6_2 = SetPosition
  L7_2 = L3_2
  L8_2 = L5_2
  L6_2(L7_2, L8_2)
end
BBKnockback = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = GetParam
  L3_2 = "Left"
  L4_2 = A0_2
  L5_2 = A1_2
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetParam
  L4_2 = "Right"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.DestVarTable
  L7_2 = true
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.DestVar
  L6_2 = tostring
  L7_2 = L2_2
  L6_2 = L6_2(L7_2)
  L7_2 = tostring
  L8_2 = L3_2
  L7_2 = L7_2(L8_2)
  L6_2 = L6_2 .. L7_2
  L4_2[L5_2] = L6_2
end
BBAppendString = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = 0
  L3_2 = A1_2.Delta
  if L3_2 ~= nil then
    L3_2 = A1_2.Delta
    L2_2 = L2_2 + L3_2
  end
  L3_2 = A1_2.DeltaByLevel
  if L3_2 ~= nil then
    L3_2 = A0_2.Level
    if L3_2 ~= nil then
      L3_2 = A1_2.DeltaByLevel
      L4_2 = A0_2.Level
      L3_2 = L3_2[L4_2]
      L2_2 = L2_2 + L3_2
    end
  end
  L3_2 = A1_2.DeltaVar
  if L3_2 ~= nil then
    L3_2 = GetTable
    L4_2 = A0_2
    L5_2 = A1_2.DeltaVarTable
    L6_2 = true
    L3_2 = L3_2(L4_2, L5_2, L6_2)
    L4_2 = A1_2.DeltaVar
    L4_2 = L3_2[L4_2]
    L2_2 = L2_2 + L4_2
  end
  L3_2 = A1_2.TargetVar
  if L3_2 ~= nil then
    L4_2 = A1_2.Stat
    L5_2 = L2_2
    L6_2 = A0_2[L3_2]
    L4_2(L5_2, L6_2)
  else
    L4_2 = A1_2.Stat
    L5_2 = L2_2
    L4_2(L5_2)
  end
end
BBIncStat = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = 0
  L3_2 = A1_2.Delta
  if L3_2 ~= nil then
    L3_2 = A1_2.Delta
    L2_2 = L2_2 + L3_2
  end
  L3_2 = A1_2.DeltaByLevel
  if L3_2 ~= nil then
    L3_2 = A0_2.Level
    if L3_2 ~= nil then
      L3_2 = A1_2.DeltaByLevel
      L4_2 = A0_2.Level
      L3_2 = L3_2[L4_2]
      L2_2 = L2_2 + L3_2
    end
  end
  L3_2 = A1_2.DeltaVar
  if L3_2 ~= nil then
    L3_2 = GetTable
    L4_2 = A0_2
    L5_2 = A1_2.DeltaVarTable
    L6_2 = true
    L3_2 = L3_2(L4_2, L5_2, L6_2)
    L4_2 = A1_2.DeltaVar
    L4_2 = L3_2[L4_2]
    L2_2 = L2_2 + L4_2
  end
  L3_2 = A1_2.TargetVar
  if L3_2 ~= nil then
    L4_2 = A1_2.Stat
    L5_2 = L2_2
    L6_2 = A0_2[L3_2]
    L4_2(L5_2, L6_2)
  else
    L4_2 = A1_2.Stat
    L5_2 = L2_2
    L4_2(L5_2)
  end
end
BBIncPermanentStat = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2
  L2_2 = A1_2.TargetVar
  L3_2 = 0
  L4_2 = A1_2.LaneVar
  if L4_2 ~= nil then
    L4_2 = GetTable
    L5_2 = A0_2
    L6_2 = A1_2.LaneVarTable
    L7_2 = true
    L4_2 = L4_2(L5_2, L6_2, L7_2)
    L5_2 = A1_2.LaneVar
    L5_2 = L4_2[L5_2]
    L3_2 = L3_2 + L5_2
  end
  L4_2 = A1_2.Lane
  if L4_2 ~= nil then
    L4_2 = A1_2.Lane
    L3_2 = L3_2 + L4_2
  end
  L4_2 = SetMinionLane
  L5_2 = A0_2[L2_2]
  L6_2 = L3_2
  L4_2(L5_2, L6_2)
end
BBSetMinionLane = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TargetVar
  L4_2 = 0
  L5_2 = 0
  L6_2 = A1_2.AttackVar
  if L6_2 ~= nil then
    L6_2 = GetTable
    L7_2 = A0_2
    L8_2 = A1_2.AttackVarTable
    L9_2 = true
    L6_2 = L6_2(L7_2, L8_2, L9_2)
    L7_2 = A1_2.AttackVar
    L7_2 = L6_2[L7_2]
    L4_2 = L4_2 + L7_2
  end
  L6_2 = A1_2.Attack
  if L6_2 ~= nil then
    L6_2 = A1_2.Attack
    L4_2 = L4_2 + L6_2
  end
  L6_2 = A1_2.TotalCoefficientVar
  if L6_2 ~= nil then
    L6_2 = GetTable
    L7_2 = A0_2
    L8_2 = A1_2.TotalCoefficientVarTable
    L9_2 = true
    L6_2 = L6_2(L7_2, L8_2, L9_2)
    L7_2 = A1_2.TotalCoefficientVar
    L7_2 = L6_2[L7_2]
    L5_2 = L5_2 + L7_2
  end
  L6_2 = A1_2.TotalCoefficient
  if L6_2 ~= nil then
    L6_2 = A1_2.TotalCoefficient
    L5_2 = L5_2 + L6_2
  end
  if L3_2 ~= nil then
    L6_2 = GetFlatCritDamageMod
    L7_2 = A0_2[L3_2]
    L6_2 = L6_2(L7_2)
    L7_2 = GetPercentCritDamageMod
    L8_2 = A0_2[L3_2]
    L7_2 = L7_2(L8_2)
    L8_2 = 1 + L7_2
    L8_2 = 2 * L8_2
    L8_2 = L8_2 + L6_2
    L9_2 = A1_2.DestVar
    L10_2 = L4_2 * L8_2
    L10_2 = L10_2 * L5_2
    L2_2[L9_2] = L10_2
  else
    L6_2 = A1_2.DestVar
    L7_2 = A1_2.Attack
    L7_2 = 2 * L7_2
    L2_2[L6_2] = L7_2
  end
end
BBCalculateCriticalDamage = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.AmountVarTable
  L6_2 = true
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A0_2.NextBuffVars
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.AmountVar
  L5_2 = L3_2[L5_2]
  L6_2 = A1_2.Amount
  L5_2 = L5_2 + L6_2
  L4_2.InitializeShield_Amount = L5_2
  L5_2 = A1_2.AmountVar
  if L5_2 ~= nil then
    L5_2 = A1_2.HealShieldMod
    if L5_2 ~= nil and L5_2 == true then
      L6_2 = A1_2.AttackerVar
      L7_2 = 0
      if L6_2 ~= nil then
        L8_2 = GetPercentHealingAmountMod
        L9_2 = A0_2[L6_2]
        L8_2 = L8_2(L9_2)
        L7_2 = L8_2
      end
      L8_2 = A1_2.Amount
      L9_2 = A1_2.AmountVar
      L9_2 = L3_2[L9_2]
      L10_2 = L8_2 + L9_2
      L11_2 = 1 + L7_2
      L10_2 = L10_2 * L11_2
      L11_2 = A1_2.AmountVar
      L3_2[L11_2] = L10_2
      L4_2.InitializeShield_Amount = L10_2
    end
  end
  L5_2 = SpellBuffAddNoRenew
  L6_2 = A1_2.AttackerVar
  L6_2 = A0_2[L6_2]
  L7_2 = A1_2.UnitVar
  L7_2 = A0_2[L7_2]
  L8_2 = "InitializeShieldMarker"
  L9_2 = 0
  L10_2 = 1
  L11_2 = 25000
  L12_2 = L4_2
  L5_2(L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2)
  L5_2 = BBIncreaseShield
  L6_2 = A0_2
  L7_2 = A1_2
  L5_2(L6_2, L7_2)
end
BBInitializeShield = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetPercentCooldownMod
  L4_2 = A1_2.TargetVar
  L5_2 = 0
  L6_2 = A1_2.CDVar
  if L6_2 ~= nil then
    L6_2 = GetTable
    L7_2 = A0_2
    L8_2 = A1_2.CDVarTable
    L9_2 = true
    L6_2 = L6_2(L7_2, L8_2, L9_2)
    L7_2 = A1_2.CDVar
    L7_2 = L6_2[L7_2]
    L5_2 = L5_2 + L7_2
  end
  L6_2 = A1_2.CD
  if L6_2 ~= nil then
    L6_2 = A1_2.CD
    L5_2 = L5_2 + L6_2
  end
  if L4_2 ~= nil then
    L6_2 = GetPercentCooldownMod
    L7_2 = A0_2[L4_2]
    L6_2 = L6_2(L7_2)
    L7_2 = A1_2.DestVar
    L8_2 = 1 + L6_2
    L8_2 = L5_2 * L8_2
    L2_2[L7_2] = L8_2
  else
    L6_2 = A1_2.DestVar
    L7_2 = A1_2.CD
    L2_2[L6_2] = L7_2
  end
end
BBGetModifiedCooldown = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2
  L3_2 = A1_2.TargetVar
  if L3_2 ~= nil then
    L4_2 = A1_2.Stat
    L5_2 = A0_2[L3_2]
    L4_2 = L4_2(L5_2)
    L2_2 = L4_2
  else
    L4_2 = A1_2.Stat
    L4_2 = L4_2()
    L2_2 = L4_2
  end
  L4_2 = SetVarInTable
  L5_2 = A0_2
  L6_2 = A1_2
  L7_2 = L2_2
  L4_2(L5_2, L6_2, L7_2)
end
BBGetStat = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TargetVar
  if L3_2 ~= nil then
    L4_2 = A1_2.DestVar
    L5_2 = GetLevel
    L6_2 = A0_2[L3_2]
    L5_2 = L5_2(L6_2)
    L2_2[L4_2] = L5_2
  else
    L4_2 = A1_2.DestVar
    L5_2 = GetLevel
    L5_2 = L5_2()
    L2_2[L4_2] = L5_2
  end
end
BBGetLevel = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TargetVar
  if L3_2 ~= nil then
    L4_2 = A1_2.DestVar
    L5_2 = GetUnitSignificance
    L6_2 = A0_2[L3_2]
    L5_2 = L5_2(L6_2)
    L2_2[L4_2] = L5_2
  else
    L4_2 = A1_2.DestVar
    L5_2 = GetUnitSignificance
    L5_2 = L5_2()
    L2_2[L4_2] = L5_2
  end
end
BBGetUnitSignificance = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TargetVar
  if L3_2 ~= nil then
    L4_2 = A1_2.DestVar
    L5_2 = GetArmor
    L6_2 = A0_2[L3_2]
    L5_2 = L5_2(L6_2)
    L2_2[L4_2] = L5_2
  else
    L4_2 = A1_2.DestVar
    L5_2 = GetArmor
    L5_2 = L5_2()
    L2_2[L4_2] = L5_2
  end
end
BBGetArmor = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TargetVar
  if L3_2 ~= nil then
    L4_2 = A1_2.DestVar
    L5_2 = GetSpellBlock
    L6_2 = A0_2[L3_2]
    L5_2 = L5_2(L6_2)
    L2_2[L4_2] = L5_2
  else
    L4_2 = A1_2.DestVar
    L5_2 = GetSpellBlock
    L5_2 = L5_2()
    L2_2[L4_2] = L5_2
  end
end
BBGetSpellBlock = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2
  L3_2 = A1_2.TargetVar
  if L3_2 ~= nil then
    L4_2 = GetTeamID
    L5_2 = A0_2[L3_2]
    L4_2 = L4_2(L5_2)
    L2_2 = L4_2
  else
    L4_2 = GetTeamID
    L4_2 = L4_2()
    L2_2 = L4_2
  end
  L4_2 = SetVarInTable
  L5_2 = A0_2
  L6_2 = A1_2
  L7_2 = L2_2
  L4_2(L5_2, L6_2, L7_2)
end
BBGetTeamID = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TargetVar
  L4_2 = nil
  if L3_2 ~= nil then
    L5_2 = GetTeamID
    L6_2 = A0_2[L3_2]
    L5_2 = L5_2(L6_2)
    L4_2 = L5_2
  else
    L5_2 = GetTeamID
    L5_2 = L5_2()
    L4_2 = L5_2
  end
  L5_2 = TEAM_ORDER
  if L4_2 == L5_2 then
    L5_2 = A1_2.DestVar
    L6_2 = TEAM_CHAOS
    L2_2[L5_2] = L6_2
  else
    L5_2 = TEAM_CHAOS
    if L4_2 == L5_2 then
      L5_2 = A1_2.DestVar
      L6_2 = TEAM_ORDER
      L2_2[L5_2] = L6_2
    end
  end
end
BBGetEnemyTeamID = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.TargetVar
  if L3_2 ~= nil then
    L4_2 = A1_2.DestVar
    L5_2 = GetUnitSkinName
    L6_2 = A0_2[L3_2]
    L5_2 = L5_2(L6_2)
    L2_2[L4_2] = L5_2
  end
end
BBGetUnitSkinName = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A0_2.Owner
  L4_2 = nil
  L5_2 = A1_2.TargetVar
  if L5_2 ~= nil then
    L6_2 = GetTotalAttackDamage
    L7_2 = A0_2[L5_2]
    L6_2 = L6_2(L7_2)
    L4_2 = L6_2
  else
    L6_2 = GetTotalAttackDamage
    L6_2 = L6_2()
    L4_2 = L6_2
  end
  L6_2 = SetVarInTable
  L7_2 = A0_2
  L8_2 = A1_2
  L9_2 = L4_2
  L6_2(L7_2, L8_2, L9_2)
end
BBGetTotalAttackDamage = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.DestVar
  L4_2 = A1_2.Status
  L5_2 = A1_2.TargetVar
  L5_2 = A0_2[L5_2]
  L4_2 = L4_2(L5_2)
  L2_2[L3_2] = L4_2
end
BBGetStatus = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2
  L2_2 = A1_2.TargetVar
  L2_2 = A0_2[L2_2]
  L3_2 = ClearAttackTarget
  L4_2 = L2_2
  L3_2(L4_2)
end
BBClearAttackTarget = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.Info
  L4_2 = A1_2.TargetVar
  L4_2 = A0_2[L4_2]
  L3_2 = L3_2(L4_2)
  L4_2 = SetVarInTable
  L5_2 = A0_2
  L6_2 = A1_2
  L7_2 = L3_2
  L4_2(L5_2, L6_2, L7_2)
end
BBGetCastInfo = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.TrackTimeVarTable
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetTime
  L4_2 = L4_2()
  L5_2 = A1_2.ExecuteImmediately
  L6_2 = GetParam
  L7_2 = "TimeBetweenExecutions"
  L8_2 = A0_2
  L9_2 = A1_2
  L6_2 = L6_2(L7_2, L8_2, L9_2)
  L7_2 = A1_2.TickTimeVar
  if L7_2 ~= nil then
    L7_2 = GetTable
    L8_2 = A0_2
    L9_2 = A1_2.TickTimeVarTable
    L10_2 = false
    L7_2 = L7_2(L8_2, L9_2, L10_2)
    L8_2 = A1_2.TickTimeVar
    L8_2 = L7_2[L8_2]
    if L8_2 ~= nil then
      L6_2 = L8_2
    end
  end
  L7_2 = A1_2.TrackTimeVar
  L7_2 = L3_2[L7_2]
  if L7_2 == nil then
    L7_2 = A1_2.TrackTimeVar
    L3_2[L7_2] = L4_2
    if L5_2 == true then
      L7_2 = ExecuteBuildingBlocks
      L8_2 = A2_2
      L9_2 = A0_2
      L7_2(L8_2, L9_2)
    end
  end
  L7_2 = A1_2.TrackTimeVar
  L7_2 = L3_2[L7_2]
  L7_2 = L7_2 + L6_2
  if L4_2 >= L7_2 then
    L7_2 = A1_2.TrackTimeVar
    L8_2 = A1_2.TrackTimeVar
    L8_2 = L3_2[L8_2]
    L8_2 = L8_2 + L6_2
    L3_2[L7_2] = L8_2
    L7_2 = ExecuteBuildingBlocks
    L8_2 = A2_2
    L9_2 = A0_2
    L7_2(L8_2, L9_2)
  end
end
BBExecutePeriodically = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.TrackTimeVarTable
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = A1_2.TrackTimeVar
  L3_2[L4_2] = nil
end
BBExecutePeriodicallyReset = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = A1_2.SrcValue
  L3_2 = A1_2.SrcVar
  if L3_2 ~= nil then
    L3_2 = GetTable
    L4_2 = A0_2
    L5_2 = A1_2.SrcVarTable
    L6_2 = true
    L3_2 = L3_2(L4_2, L5_2, L6_2)
    L4_2 = A1_2.SrcVar
    L2_2 = L3_2[L4_2]
  end
  L3_2 = A1_2.Status
  L4_2 = A1_2.TargetVar
  L4_2 = A0_2[L4_2]
  L5_2 = L2_2
  L3_2(L4_2, L5_2)
end
BBSetStatus = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = A1_2.ToAlert
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.SrcVarTable
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  if L3_2 ~= nil then
    L4_2 = A1_2.SrcVar
    if L4_2 ~= nil then
      L4_2 = L2_2
      L5_2 = A1_2.SrcVar
      L5_2 = L3_2[L5_2]
      L2_2 = L4_2 .. L5_2
    end
  end
  L4_2 = _ALERT
  L5_2 = L2_2
  L4_2(L5_2)
end
BBAlert = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2, L13_2, L14_2, L15_2
  L2_2 = GetParam
  L3_2 = "Message"
  L4_2 = A0_2
  L5_2 = A1_2
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  if L2_2 == nil then
    L3_2 = ReportError
    L4_2 = "Could not resolve Message param"
    L3_2(L4_2)
    return
  end
  L3_2 = true
  L4_2 = GetParam
  L5_2 = "OnUnit"
  L6_2 = A0_2
  L7_2 = A1_2
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  if L4_2 == nil then
    L3_2 = false
    L5_2 = GetParam
    L6_2 = "OnUnitByFlag"
    L7_2 = A0_2
    L8_2 = A1_2
    L5_2 = L5_2(L6_2, L7_2, L8_2)
    L4_2 = L5_2
  end
  if L4_2 == nil then
    L5_2 = ReportError
    L6_2 = "Could not resolve OnUnit param"
    L5_2(L6_2)
    return
  end
  L5_2 = true
  L6_2 = GetParam
  L7_2 = "ChampionToSayTo"
  L8_2 = A0_2
  L9_2 = A1_2
  L6_2 = L6_2(L7_2, L8_2, L9_2)
  if L6_2 == nil then
    L5_2 = false
    L7_2 = GetParam
    L8_2 = "ChampionToSayToByFlag"
    L9_2 = A0_2
    L10_2 = A1_2
    L7_2 = L7_2(L8_2, L9_2, L10_2)
    L6_2 = L7_2
  end
  if L6_2 == nil then
    L7_2 = ReportError
    L8_2 = "Could not resolve ChampionToSayTo param"
    L7_2(L8_2)
    return
  end
  L7_2 = GetParam
  L8_2 = "ShowToSpectator"
  L9_2 = A0_2
  L10_2 = A1_2
  L7_2 = L7_2(L8_2, L9_2, L10_2)
  if L7_2 == nil then
    L8_2 = ReportError
    L9_2 = "Could not resolve ShowToSpectator param"
    L8_2(L9_2)
    return
  end
  L8_2 = SayWithFloatingTextOnUnitToChampion
  L9_2 = tostring
  L10_2 = L2_2
  L9_2 = L9_2(L10_2)
  L10_2 = A1_2.TextType
  L11_2 = L4_2
  L12_2 = L3_2
  L13_2 = L6_2
  L14_2 = L5_2
  L15_2 = L7_2
  L8_2(L9_2, L10_2, L11_2, L12_2, L13_2, L14_2, L15_2)
end
BBSayWithFloatingTextOnUnitToChampion = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = A1_2.ToSay
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.SrcVarTable
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  if L3_2 ~= nil then
    L4_2 = A1_2.SrcVar
    if L4_2 ~= nil then
      L4_2 = L2_2
      L5_2 = tostring
      L6_2 = A1_2.SrcVar
      L6_2 = L3_2[L6_2]
      L5_2 = L5_2(L6_2)
      L2_2 = L4_2 .. L5_2
    end
  end
  L4_2 = nil
  L5_2 = A1_2.OwnerVar
  if L5_2 ~= nil then
    L5_2 = A1_2.OwnerVar
    L4_2 = A0_2[L5_2]
  else
    L4_2 = A0_2.Owner
  end
  L5_2 = A1_2.TextType
  if L5_2 == nil then
    A1_2.TextType = 0
  end
  L5_2 = Say
  L6_2 = L4_2
  L7_2 = L2_2
  L8_2 = A1_2.TextType
  L5_2(L6_2, L7_2, L8_2)
end
BBSay = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = A1_2.ToSay
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.SrcVarTable
  L6_2 = false
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  if L3_2 ~= nil then
    L4_2 = A1_2.SrcVar
    if L4_2 ~= nil then
      L4_2 = L2_2
      L5_2 = tostring
      L6_2 = A1_2.SrcVar
      L6_2 = L3_2[L6_2]
      L5_2 = L5_2(L6_2)
      L2_2 = L4_2 .. L5_2
    end
  end
  L4_2 = nil
  L5_2 = A1_2.OwnerVar
  if L5_2 ~= nil then
    L5_2 = A1_2.OwnerVar
    L4_2 = A0_2[L5_2]
  else
    L4_2 = A0_2.Owner
  end
  L5_2 = A1_2.TextType
  if L5_2 == nil then
    A1_2.TextType = 0
  end
  L5_2 = Say
  L6_2 = L4_2
  L7_2 = L2_2
  L8_2 = A1_2.TextType
  L5_2(L6_2, L7_2, L8_2)
end
BBDebugSay = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.DestVar
  L4_2 = BBLuaGetGold
  L5_2 = A0_2
  L6_2 = A1_2
  L4_2 = L4_2(L5_2, L6_2)
  L2_2[L3_2] = L4_2
end
BBGetGold = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.DestVar
  L4_2 = BBLuaGetTotalGold
  L5_2 = A0_2
  L6_2 = A1_2
  L4_2 = L4_2(L5_2, L6_2)
  L2_2[L3_2] = L4_2
end
BBGetTotalGold = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = SpellBuffAdd
  L3_2 = A1_2.OwnerVar
  L3_2 = A0_2[L3_2]
  L4_2 = A1_2.OwnerVar
  L4_2 = A0_2[L4_2]
  L5_2 = "TeleportMarker"
  L6_2 = 0
  L7_2 = 1
  L8_2 = 25000
  L9_2 = A0_2.NextBuffVars
  L2_2(L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2)
  L2_2 = BBTeleportToPositionHelper
  L3_2 = A0_2
  L4_2 = A1_2
  L2_2(L3_2, L4_2)
end
BBTeleportToPosition = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.XVarTable
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.XVar
  if L3_2 ~= nil and L2_2 ~= nil then
    L3_2 = A1_2.XVar
    L3_2 = L2_2[L3_2]
    Xloc = L3_2
  else
    L3_2 = A1_2.X
    Xloc = L3_2
  end
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.YVarTable
  L6_2 = true
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = A1_2.YVar
  if L4_2 ~= nil and L3_2 ~= nil then
    L4_2 = A1_2.YVar
    L4_2 = L3_2[L4_2]
    Yloc = L4_2
  else
    L4_2 = A1_2.Y
    Yloc = L4_2
  end
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.ZVarTable
  L7_2 = true
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.ZVar
  if L5_2 ~= nil and L4_2 ~= nil then
    L5_2 = A1_2.ZVar
    L5_2 = L4_2[L5_2]
    Zloc = L5_2
  else
    L5_2 = A1_2.Z
    Zloc = L5_2
  end
  L5_2 = A1_2.OwnerVar
  L6_2 = Make3DPoint
  L7_2 = Xloc
  L8_2 = Yloc
  L9_2 = Zloc
  L6_2 = L6_2(L7_2, L8_2, L9_2)
  A0_2.position = L6_2
  A1_2.OwnerVar = L5_2
  A1_2.CastPositionName = "position"
  L7_2 = BBTeleportToPosition
  L8_2 = A0_2
  L9_2 = A1_2
  L7_2(L8_2, L9_2)
end
BBTeleportToPoint = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.CenterTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = DefUpdateAura
  L4_2 = A1_2.CenterVar
  L4_2 = L2_2[L4_2]
  L5_2 = A1_2.Range
  L6_2 = A1_2.UnitScan
  L7_2 = A1_2.BuffName
  L3_2(L4_2, L5_2, L6_2, L7_2)
end
BBDefUpdateAura = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.TargetTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = ReincarnateNonDeadHero
  L4_2 = A1_2.TargetVar
  L4_2 = L2_2[L4_2]
  L3_2(L4_2)
end
BBReincarnateHero = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.DestVar
  L4_2 = A1_2.Function
  L5_2 = A1_2.OwnerVar
  L5_2 = A0_2[L5_2]
  L6_2 = A1_2.PARType
  L4_2 = L4_2(L5_2, L6_2)
  L2_2[L3_2] = L4_2
end
BBGetPAROrHealth = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2
  L2_2 = A1_2.WhomToOrderVar
  L2_2 = A0_2[L2_2]
  L3_2 = A1_2.TargetOfOrderVar
  L3_2 = A0_2[L3_2]
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.SrcVarTable
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = nil
  L6_2 = A1_2.SrcVar
  if L6_2 ~= nil and L4_2 ~= nil then
    L6_2 = A1_2.SrcVar
    L5_2 = L4_2[L6_2]
  else
    L6_2 = GetPosition
    L7_2 = L3_2
    L6_2 = L6_2(L7_2)
    L5_2 = L6_2
  end
  if L3_2 == nil then
    L3_2 = L2_2
  end
  L6_2 = IssueOrder
  L7_2 = L2_2
  L8_2 = A1_2.Order
  L9_2 = L5_2
  L10_2 = L3_2
  L6_2(L7_2, L8_2, L9_2, L10_2)
end
BBIssueOrder = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetParam
  L3_2 = "NewRange"
  L4_2 = A0_2
  L5_2 = A1_2
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = SetSpellCastRange
  L4_2 = L2_2
  L3_2(L4_2)
end
BBSetSpellCastRange = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.DestVarTable
  L5_2 = true
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.DestVar
  L4_2 = GetTime
  L4_2 = L4_2()
  L2_2[L3_2] = L4_2
end
BBGetTime = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = A1_2.ObjectVar1
  L2_2 = A0_2[L2_2]
  L3_2 = A1_2.ObjectVar2
  L3_2 = A0_2[L3_2]
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.DestVarTable
  L7_2 = true
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.DestVar
  L6_2 = DistanceBetweenObjectBounds
  L7_2 = L2_2
  L8_2 = L3_2
  L6_2 = L6_2(L7_2, L8_2)
  L4_2[L5_2] = L6_2
end
BBDistanceBetweenObjects = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = A1_2.ObjectVar
  L2_2 = A0_2[L2_2]
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.PointVarTable
  L6_2 = true
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = A1_2.PointVar
  L4_2 = L3_2[L4_2]
  L5_2 = GetTable
  L6_2 = A0_2
  L7_2 = A1_2.DestVarTable
  L8_2 = true
  L5_2 = L5_2(L6_2, L7_2, L8_2)
  L6_2 = A1_2.DestVar
  L7_2 = DistanceBetweenObjectCenterAndPoint
  L8_2 = L2_2
  L9_2 = L4_2
  L7_2 = L7_2(L8_2, L9_2)
  L5_2[L6_2] = L7_2
end
BBDistanceBetweenObjectAndPoint = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = GetParam
  L3_2 = "Point1"
  L4_2 = A0_2
  L5_2 = A1_2
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = GetParam
  L4_2 = "Point2"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.DestVarTable
  L7_2 = true
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.DestVar
  L6_2 = DistanceBetweenPoints
  L7_2 = L2_2
  L8_2 = L3_2
  L6_2 = L6_2(L7_2, L8_2)
  L4_2[L5_2] = L6_2
end
BBDistanceBetweenPoints = L0_1
L0_1 = 1
OBJECT_CENTER = L0_1
L0_1 = 2
OBJECT_BOUNDARY = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2, L10_2, L11_2, L12_2, L13_2
  L5_2 = 0
  L6_2 = nil
  L7_2 = A1_2.ObjectDistanceType
  L8_2 = A1_2.ObjectVar1
  L3_2 = A0_2[L8_2]
  L8_2 = A1_2.ObjectVar2
  L4_2 = A0_2[L8_2]
  if nil == L4_2 then
    L8_2 = A1_2.Point2Var
    if nil ~= L8_2 then
      L8_2 = GetTable
      L9_2 = A0_2
      L10_2 = A1_2.Point2VarTable
      L11_2 = true
      L8_2 = L8_2(L9_2, L10_2, L11_2)
      L9_2 = A1_2.Point2Var
      L4_2 = L8_2[L9_2]
      L5_2 = L5_2 + 1
    end
  end
  if nil == L3_2 then
    L8_2 = A1_2.Point1Var
    if nil ~= L8_2 then
      L8_2 = GetTable
      L9_2 = A0_2
      L10_2 = A1_2.Point1VarTable
      L11_2 = true
      L8_2 = L8_2(L9_2, L10_2, L11_2)
      L9_2 = A1_2.Point1Var
      L3_2 = L8_2[L9_2]
      L5_2 = L5_2 + 1
      if 1 == L5_2 then
        L9_2 = L4_2
        L4_2 = L3_2
        L3_2 = L9_2
      end
    end
  end
  if nil ~= L7_2 then
    if 0 == L5_2 then
      L8_2 = OBJECT_CENTER
      if L8_2 == L7_2 then
        L6_2 = IfDistanceBetweenObjectCentersLessThan
      else
        L8_2 = OBJECT_BOUNDARY
        if L8_2 == L7_2 then
          L6_2 = IfDistanceBetweenObjectBoundsLessThan
        else
          L8_2 = A1_2.OwnerVar
          L8_2 = A0_2[L8_2]
          L9_2 = Say
          L10_2 = L8_2
          L11_2 = "invalid object distance type"
          L12_2 = 0
          L9_2(L10_2, L11_2, L12_2)
        end
      end
    elseif 1 == L5_2 then
      L8_2 = OBJECT_CENTER
      if L8_2 == L7_2 then
        L6_2 = IfDistanceBetweenObjectCenterAndPointLessThan
      else
        L8_2 = OBJECT_BOUNDARY
        if L8_2 == L7_2 then
          L6_2 = IfDistanceBetweenObjectBoundAndPointLessThan
        else
          L8_2 = A1_2.OwnerVar
          L8_2 = A0_2[L8_2]
          L9_2 = Say
          L10_2 = L8_2
          L11_2 = "invalid object distance type"
          L12_2 = 0
          L9_2(L10_2, L11_2, L12_2)
        end
      end
    else
      L6_2 = IfDistanceBetweenPointsLessThan
    end
  end
  L8_2 = A1_2.Distance
  L9_2 = GetTable
  L10_2 = A0_2
  L11_2 = A1_2.DistanceVarTable
  L12_2 = true
  L9_2 = L9_2(L10_2, L11_2, L12_2)
  L10_2 = A1_2.DistanceVar
  if L10_2 ~= nil and L9_2 ~= nil then
    L10_2 = A1_2.DistanceVar
    L10_2 = L9_2[L10_2]
    L8_2 = L8_2 + L10_2
  end
  if L3_2 ~= nil and L4_2 ~= nil and L6_2 ~= nil and L8_2 ~= nil then
    L10_2 = L6_2
    L11_2 = L3_2
    L12_2 = L4_2
    L13_2 = L8_2
    L10_2 = L10_2(L11_2, L12_2, L13_2)
    L11_2 = CO_GREATER_THAN_OR_EQUAL
    L12_2 = A1_2.CompareOp
    if L11_2 == L12_2 then
      L10_2 = not L10_2
    end
    if L10_2 then
      L11_2 = ExecuteBuildingBlocks
      L12_2 = A2_2
      L13_2 = A0_2
      L11_2(L12_2, L13_2)
      A0_2.LastIfSucceeded = true
    else
      A0_2.LastIfSucceeded = false
    end
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfDistanceBetween = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = A1_2.TargetVar
  L2_2 = A0_2[L2_2]
  L3_2 = A1_2.CasterVar
  L3_2 = A0_2[L3_2]
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.DestVarTable
  L7_2 = true
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.DestVar
  L6_2 = SpellBuffCount
  L7_2 = L2_2
  L8_2 = A1_2.BuffName
  L9_2 = L3_2
  L6_2 = L6_2(L7_2, L8_2, L9_2)
  L4_2[L5_2] = L6_2
end
BBGetBuffCountFromCaster = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = A1_2.TargetVar
  L2_2 = A0_2[L2_2]
  L3_2 = GetTable
  L4_2 = A0_2
  L5_2 = A1_2.DestVarTable
  L6_2 = true
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = A1_2.DestVar
  L5_2 = SpellBuffCount
  L6_2 = L2_2
  L7_2 = A1_2.BuffName
  L8_2 = caster
  L5_2 = L5_2(L6_2, L7_2, L8_2)
  L3_2[L4_2] = L5_2
end
BBGetBuffCountFromAll = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.ScaleVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = nil
  L4_2 = A1_2.OwnerVar
  if L4_2 ~= nil then
    L4_2 = A1_2.OwnerVar
    L3_2 = A0_2[L4_2]
  else
    L3_2 = A0_2.Owner
  end
  L4_2 = A1_2.ScaleVar
  if L4_2 ~= nil then
    L4_2 = A1_2.ScaleVarTable
    if L4_2 ~= nil then
      L4_2 = SetScaleSkinCoef
      L5_2 = A1_2.ScaleVar
      L5_2 = L2_2[L5_2]
      L6_2 = L3_2
      L4_2(L5_2, L6_2)
  end
  else
    L4_2 = SetScaleSkinCoef
    L5_2 = A1_2.Scale
    L6_2 = L3_2
    L4_2(L5_2, L6_2)
  end
end
BBSetScaleSkinCoef = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = SpellBuffAdd
  L3_2 = A1_2.TargetVar
  L3_2 = A0_2[L3_2]
  L4_2 = A1_2.TargetVar
  L4_2 = A0_2[L4_2]
  L5_2 = "SpellShieldMarker"
  L6_2 = 0
  L7_2 = 1
  L8_2 = 37037
  L9_2 = A0_2.NextBuffVars
  L2_2(L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2)
end
BBBreakSpellShields = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L3_2 = A1_2.TargetVar
  L3_2 = A0_2[L3_2]
  L4_2 = A1_2.NumStacks
  L5_2 = GetParam
  L6_2 = "NumStacks"
  L7_2 = A0_2
  L8_2 = A1_2
  L5_2 = L5_2(L6_2, L7_2, L8_2)
  if L5_2 == 0 then
    L6_2 = SpellBuffCount
    L7_2 = L3_2
    L8_2 = A1_2.BuffName
    L9_2 = caster
    L6_2 = L6_2(L7_2, L8_2, L9_2)
    L4_2 = L6_2
  else
    L4_2 = L5_2
  end
  while 0 < L4_2 do
    L6_2 = SpellBuffRemove
    L7_2 = L3_2
    L8_2 = A1_2.BuffName
    L9_2 = A1_2.AttackerVar
    L9_2 = A0_2[L9_2]
    L6_2(L7_2, L8_2, L9_2)
    L4_2 = L4_2 - 1
  end
end
BBSpellBuffRemoveStacks = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = GetParam
  L3_2 = "Unit"
  L4_2 = A0_2
  L5_2 = A1_2
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  unit = L2_2
  L2_2 = unit
  if L2_2 == nil then
    L2_2 = ReportError
    L3_2 = "Could not resolve Unit param"
    L2_2(L3_2)
    return
  end
  L2_2 = true
  L3_2 = GetParam
  L4_2 = "ChampionToShowTo"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  if L3_2 == nil then
    L2_2 = false
    L4_2 = GetParam
    L5_2 = "ChampionToShowToByFlag"
    L6_2 = A0_2
    L7_2 = A1_2
    L4_2 = L4_2(L5_2, L6_2, L7_2)
    L3_2 = L4_2
  end
  if L3_2 == nil then
    L4_2 = ReportError
    L5_2 = "Could not resolve ChampionToShowTo param"
    L4_2(L5_2)
    return
  end
  L4_2 = SetShowHealthBarToChampion
  L5_2 = unit
  L6_2 = A1_2.Show
  L7_2 = L3_2
  L8_2 = L2_2
  L9_2 = A1_2.ApplyToSpectator
  L4_2(L5_2, L6_2, L7_2, L8_2, L9_2)
end
BBSetShowHealthBarToChampion = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2
  L3_2 = A0_2.EmoteId
  L4_2 = A1_2.EmoteId
  if L3_2 == L4_2 then
    L3_2 = ExecuteBuildingBlocks
    L4_2 = A2_2
    L5_2 = A0_2
    L3_2(L4_2, L5_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfEmoteIs = L0_1
function L0_1(A0_2, A1_2, A2_2)
  local L3_2, L4_2, L5_2
  L3_2 = A0_2.EmoteId
  L4_2 = A1_2.EmoteId
  if L3_2 ~= L4_2 then
    L3_2 = ExecuteBuildingBlocks
    L4_2 = A2_2
    L5_2 = A0_2
    L3_2(L4_2, L5_2)
    A0_2.LastIfSucceeded = true
  else
    A0_2.LastIfSucceeded = false
  end
end
BBIfEmoteIsNot = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2, L9_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.String1VarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.String1Var
  L3_2 = L2_2[L3_2]
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.String2VarTable
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.String2Var
  L5_2 = L4_2[L5_2]
  L6_2 = GetTable
  L7_2 = A0_2
  L8_2 = A1_2.ResultVarTable
  L9_2 = false
  L6_2 = L6_2(L7_2, L8_2, L9_2)
  L7_2 = A1_2.ResultVar
  L8_2 = L3_2
  L9_2 = L5_2
  L8_2 = L8_2 .. L9_2
  L6_2[L7_2] = L8_2
end
BBConcatenateStrings = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2, L7_2, L8_2
  L2_2 = GetTable
  L3_2 = A0_2
  L4_2 = A1_2.VariableVarTable
  L5_2 = false
  L2_2 = L2_2(L3_2, L4_2, L5_2)
  L3_2 = A1_2.VariableVar
  L3_2 = L2_2[L3_2]
  L4_2 = GetTable
  L5_2 = A0_2
  L6_2 = A1_2.ResultVarTable
  L7_2 = false
  L4_2 = L4_2(L5_2, L6_2, L7_2)
  L5_2 = A1_2.ResultVar
  L6_2 = "("
  L7_2 = L3_2
  L8_2 = ")"
  L6_2 = L6_2 .. L7_2 .. L8_2
  L4_2[L5_2] = L6_2
end
BBEncaseInParantheses = L0_1
function L0_1(A0_2, A1_2)
  local L2_2, L3_2, L4_2, L5_2, L6_2
  L2_2 = BBGetMinionKills
  L3_2 = A0_2
  L4_2 = A1_2
  L2_2(L3_2, L4_2)
  L2_2 = A0_2.MinionsKilled
  L3_2 = GetParam
  L4_2 = "MinionKills"
  L5_2 = A0_2
  L6_2 = A1_2
  L3_2 = L3_2(L4_2, L5_2, L6_2)
  L4_2 = GetParam
  L5_2 = "MinionKillTarget"
  L4_2 = L4_2(L5_2)
  A0_2.MinionKillSource = L4_2
  L4_2 = L2_2 + L3_2
  A0_2.MinionKills = L4_2
  L4_2 = BBSetMinionKills
  L5_2 = A0_2
  L6_2 = A1_2
  L4_2(L5_2, L6_2)
end
BBIncreaseMinionKills = L0_1
