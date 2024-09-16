namespace Spells
{
    public class RumbleGrenade : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float cooldown;
        int[] effect0 = { 20, 20, 20, 20, 20 };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        int[] effect2 = { 10, 10, 10, 10, 10 };
        public override void SelfExecute()
        {
            float nextBuffVars_BaseCDR;
            float nextBuffVars_CDRMod;
            Vector3 targetPos = GetSpellTargetPos(spell);
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            float firstCost = effect0[level - 1];
            float secondCost = effect1[level - 1];
            if (true)
            {
                level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float cDRMod = GetPercentCooldownMod(attacker);
                nextBuffVars_BaseCDR = effect2[level - 1];
                nextBuffVars_CDRMod = -1 * cDRMod;
                nextBuffVars_CDRMod = 1 - nextBuffVars_CDRMod;
                nextBuffVars_BaseCDR *= nextBuffVars_CDRMod;
            }
            if (distance > 800)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 750, 0);
            }
            cooldown = nextBuffVars_BaseCDR;
            SpellCast(owner, default, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleGrenadeCounter)) == 0)
            {
                float par = GetPAR(target, PrimaryAbilityResourceType.Other);
                if (par >= 80)
                {
                    AddBuff(attacker, attacker, new Buffs.RumbleOverheat(), 1, 1, 5.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    SetPARColorOverride(owner, 255, 0, 0, 255, 175, 0, 0, 255);
                }
                AddBuff(attacker, attacker, new Buffs.RumbleGrenadeCounter(), 1, 1, 3.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, attacker, new Buffs.RumbleGrenade(), 1, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                AddBuff(attacker, attacker, new Buffs.RumbleGrenadeCD(), 1, 1, cooldown, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                IncPAR(owner, firstCost, PrimaryAbilityResourceType.Other);
                AddBuff(attacker, attacker, new Buffs.RumbleHeatDelay(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleDangerZone)) > 0)
                {
                    AddBuff(attacker, target, new Buffs.RumbleGrenadeDZ(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.RumbleGrenadeCounter), owner);
                SpellBuffRemove(owner, default, owner);
                IncPAR(owner, secondCost, PrimaryAbilityResourceType.Other);
                SpellBuffClear(owner, nameof(Buffs.RumbleGrenadeCounter));
            }
        }
    }
}
namespace Buffs
{
    public class RumbleGrenade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "RumbleGrenade",
            BuffTextureName = "Rumble_Electro Harpoon.dds",
        };
        public override void OnActivate()
        {
            SetSlotSpellCooldownTimeVer2(0.5f, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.RumbleGrenadeCD));
            SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, duration);
        }
    }
}