local L0_1, L1_1, L2_1, L3_1, L4_1, L5_1, L6_1, L7_1, L8_1, L9_1, L10_1, L11_1, L12_1, L13_1, L14_1, L15_1, L16_1, L17_1, L18_1, L19_1, L20_1, L21_1, L22_1, L23_1, L24_1
L0_1 = {}
L1_1 = "Champion"
L2_1 = "Champion_Clone"
L3_1 = "Minion"
L4_1 = "Minion_Lane"
L5_1 = "Minion_Lane_Siege"
L6_1 = "Minion_Lane_Super"
L7_1 = "Minion_Summon"
L8_1 = "Monster"
L9_1 = "Monster_Epic"
L10_1 = "Monster_Large"
L11_1 = "Special_EpicMonsterIgnores"
L12_1 = "Special_SyndraSphere"
L13_1 = "Special_TeleportTarget"
L14_1 = "Special_Tunnel"
L15_1 = "Structure"
L16_1 = "Structure_Inhibitor"
L17_1 = "Structure_Nexus"
L18_1 = "Structure_Turret"
L19_1 = "Structure_Turret_Outer"
L20_1 = "Structure_Turret_Inner"
L21_1 = "Structure_Turret_Inhib"
L22_1 = "Structure_Turret_Nexus"
L23_1 = "Structure_Turret_Shrine"
L24_1 = "Ward"
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
L0_1[20] = L20_1
L0_1[21] = L21_1
L0_1[22] = L22_1
L0_1[23] = L23_1
L0_1[24] = L24_1
UnitTags = L0_1
L0_1 = {}
UnitTagFlags = L0_1
function L0_1()
  local L0_2, L1_2, L2_2, L3_2, L4_2, L5_2, L6_2
  L0_2 = UnitTagFlags
  L0_2.None = 0
  L0_2 = 1
  L1_2 = ipairs
  L2_2 = UnitTags
  L1_2, L2_2, L3_2 = L1_2(L2_2)
  for L4_2, L5_2 in L1_2, L2_2, L3_2 do
    L6_2 = UnitTagFlags
    L6_2[L5_2] = L0_2
    L0_2 = L0_2 * 2
  end
end
_BuildTags = L0_1
L0_1 = _BuildTags
L0_1()
function L0_1(A0_2)
  local L1_2, L2_2, L3_2, L4_2, L5_2, L6_2, L7_2
  L1_2 = 0
  L2_2 = string
  L2_2 = L2_2.gmatch
  L3_2 = A0_2
  L4_2 = "[^| ]+"
  L2_2, L3_2, L4_2 = L2_2(L3_2, L4_2)
  for L5_2 in L2_2, L3_2, L4_2 do
    L6_2 = UnitTagFlags
    L6_2 = L6_2[L5_2]
    if L6_2 ~= nil then
      L6_2 = UnitTagFlags
      L6_2 = L6_2[L5_2]
      L1_2 = L1_2 + L6_2
    end
  end
  return L1_2
end
ParseUnitTagFlags = L0_1
