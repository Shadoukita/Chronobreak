namespace Buffs
{
    public class RumbleOverheat : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "spine", "", "", },
            AutoBuffActivateEffect = new[] { "Silence_glb.troy", "rumble_overheat.troy", "rumble_overheat_lite.troy", "rumble_overheat_lite_02.troy", },
            BuffName = "RumbleOverheat",
            BuffTextureName = "Rumble_Junkyard Titan3.dds",
        };
        float punchdmg;
        float lastTimeExecuted;
        int[] effect0 = { 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110 };
        public override void OnActivate()
        {
            SetPARColorOverride(owner, 255, 0, 0, 255, 175, 0, 0, 255);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleGrenadeCounter)) > 0)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            IncPAR(owner, 100, PrimaryAbilityResourceType.Other);
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.RumbleOverheat));
            AddBuff(attacker, owner, new Buffs.RumbleOverheatSound(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, true);
            AddBuff(attacker, owner, new Buffs.RumbleHeatDelay(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            punchdmg = 0;
            OverrideAutoAttack(5, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellBuffRemove(owner, nameof(Buffs.Infuse), (ObjAIBase)owner);
            IncPAR(owner, -20, PrimaryAbilityResourceType.Other);
            RemoveOverrideAutoAttack(owner, true);
            CancelAutoAttack(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                IncPAR(owner, -10, PrimaryAbilityResourceType.Other);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleGrenadeCounter)) == 0)
                {
                    SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                }
                else
                {
                    SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                }
                AddBuff(attacker, target, new Buffs.RumbleHeatingUp(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                int level = GetLevel(owner);
                punchdmg = effect0[level - 1];
                SetBuffToolTipVar(1, punchdmg);
            }
        }
    }
}