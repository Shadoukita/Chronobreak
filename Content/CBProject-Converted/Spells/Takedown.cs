namespace Spells
{
    public class Takedown : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 5, 5, 5, 5, 5 };
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.Takedown(nextBuffVars_SpellCooldown), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class Takedown : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "R_hand", "L_hand", "neck", },
            AutoBuffActivateEffect = new[] { "", "", "nidalee_takedown_cas_02.troy", "", },
            BuffName = "Takedown",
            BuffTextureName = "Nidalee_TakeDown.dds",
        };
        float spellCooldown;
        public Takedown(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, true);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown = this.spellCooldown;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SetDodgePiercing(owner, false);
            RemoveOverrideAutoAttack(owner, true);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}