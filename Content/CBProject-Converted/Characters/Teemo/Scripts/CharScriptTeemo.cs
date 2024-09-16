namespace CharScripts
{
    public class CharScriptTeemo : CharScript
    {
        float lastTime2Executed;
        int[] effect0 = { 35, 31, 27 };
        int[] effect1 = { 6, 6, 6, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14, 14, 14, 16, 16, 16 };
        public override void OnUpdateActions()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ToxicShot)) == 0)
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    AddBuff(owner, owner, new Buffs.ToxicShot(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            if (ExecutePeriodically(1, ref lastTime2Executed, true))
            {
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 0)
                {
                    level = 1;
                }
                float mushroomCooldown = effect0[level - 1];
                float cooldownMod = GetPercentCooldownMod(owner);
                cooldownMod++;
                charVars.MushroomCooldown = mushroomCooldown * cooldownMod;
                SetSpellToolTipVar(charVars.MushroomCooldown, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                float mushroomCooldownNL = mushroomCooldown - 4;
                mushroomCooldownNL *= cooldownMod;
                SetSpellToolTipVar(mushroomCooldownNL, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.TrailDuration = effect1[level - 1];
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.Camouflage(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0.1f, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    AddBuff(owner, owner, new Buffs.TeemoMushrooms(), 4, 2, charVars.MushroomCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                }
            }
            if (slot == 1)
            {
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    AddBuff(owner, owner, new Buffs.TeemoMoveQuickPassive(), 1, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptTeemo : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Teemo_EagleEye.dds",
        };
    }
}