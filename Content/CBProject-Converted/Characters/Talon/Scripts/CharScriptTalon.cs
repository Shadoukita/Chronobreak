namespace CharScripts
{
    public class CharScriptTalon : CharScript
    {
        //int[] effect0 = { 1, 1, 1, 1, 1 };
        public override void OnUpdateActions()
        {
            //int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = GetBaseAttackDamage(owner);
            float totalAD = GetTotalAttackDamage(owner);
            float bonusAD = totalAD - baseDamage;
            float wBonusAD = bonusAD * 0.6f;
            wBonusAD = MathF.Ceiling(wBonusAD);
            SetSpellToolTipVar(wBonusAD, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)target);
            float w2BonusAD = bonusAD * 0.3f;
            w2BonusAD = MathF.Ceiling(w2BonusAD);
            SetSpellToolTipVar(w2BonusAD, 2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)target);
            float eBonusAD = bonusAD * 1.2f;
            eBonusAD = MathF.Ceiling(eBonusAD);
            SetSpellToolTipVar(eBonusAD, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)target);
            //level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //float qDamagePercentVal = effect0[level - 1]; // UNUSED
            float qTotalBonus = bonusAD * 0.3f;
            qTotalBonus = MathF.Ceiling(qTotalBonus);
            SetSpellToolTipVar(qTotalBonus, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)target);
            float rBonusAD = bonusAD * 0.9f;
            rBonusAD = MathF.Ceiling(rBonusAD);
            SetSpellToolTipVar(rBonusAD, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)target);
            float baseAP = GetFlatMagicDamageMod(owner);
            float qMagicBonus = baseAP * 0.1f; // UNUSED
            float qBonusAD2 = bonusAD * 1.2f;
            qBonusAD2 = MathF.Ceiling(qBonusAD2);
            SetSpellToolTipVar(qBonusAD2, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)target);
        }
        /*
        // There is no spell with that name, and the variable is only used by Lux
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            string slotName = GetSpellName(spell);
            if(slotName == nameof(Spells.BladeRogue_ShackleShot))
            {
                charVars.FirstTargetHit = false;
            }
        }
        */
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.LastHit = 0;
            charVars.AttackCounter = 1;
            charVars.MissileNumber = 0;
            AddBuff(attacker, target, new Buffs.TalonMercy(), 1, 1, 250000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptTalon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Wolfman_InnerHunger.dds",
        };
        public override void OnResurrect()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}