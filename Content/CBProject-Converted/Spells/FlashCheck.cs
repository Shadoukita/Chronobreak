namespace Buffs
{
    public class FlashCheck : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int flashSlot;
        public FlashCheck(int flashSlot = default)
        {
            this.flashSlot = flashSlot;
        }
        public override void OnActivate()
        {
            //RequireVar(this.flashSlot);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float currentCooldown;
            if (attacker is BaseTurret)
            {
                if (flashSlot == 0)
                {
                    currentCooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                    if (currentCooldown <= 3)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 3);
                    }
                }
                else
                {
                    currentCooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                    if (currentCooldown <= 3)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 3);
                    }
                }
            }
            if (attacker is Champion)
            {
                if (flashSlot == 0)
                {
                    currentCooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                    if (currentCooldown <= 3)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 3);
                    }
                }
                else
                {
                    currentCooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                    if (currentCooldown <= 3)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 3);
                    }
                }
            }
        }
    }
}