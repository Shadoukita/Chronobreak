namespace Spells
{
    public class GarenCommandKill : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class GarenCommandKill : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "GarenCommandKill",
            BuffTextureName = "Garen_CommandingPresence.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusArmor;
        float bonusMR;
        int[] effect0 = { 25, 25, 25, 25, 25 };
        public GarenCommandKill(float bonusArmor = default, float bonusMR = default)
        {
            this.bonusArmor = bonusArmor;
            this.bonusMR = bonusMR;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusArmor);
            //RequireVar(this.bonusMR);
            //RequireVar(this.maxBonus);
        }
        public override void OnKill(AttackableUnit target)
        {
            if (charVars.TotalBonus < charVars.MaxBonus)
            {
                float bonusAdd = 0.5f + charVars.TotalBonus;
                charVars.TotalBonus = bonusAdd;
                IncPermanentFlatSpellBlockMod(owner, 0.5f);
                IncPermanentFlatArmorMod(owner, 0.5f);
                AddBuff((ObjAIBase)owner, owner, new Buffs.GarenKillBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 1)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_BonusArmor = bonusArmor;
                float nextBuffVars_BonusMR = bonusMR;
                charVars.MaxBonus = effect0[level - 1];
                float nextBuffVars_MaxBonus = charVars.MaxBonus; // UNUSED
                AddBuff((ObjAIBase)owner, owner, new Buffs.GarenCommandKill(nextBuffVars_BonusArmor, nextBuffVars_BonusMR), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}