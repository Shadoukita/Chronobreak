namespace CharScripts
{
    public class CharScriptSivir : CharScript
    {
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTime2Executed, true))
            {
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float totalDamage = GetTotalAttackDamage(owner);
                float baseDamage = GetBaseAttackDamage(owner);
                float bonusDamage = totalDamage - baseDamage;
                float spell3Display = bonusDamage * 1.1f;
                SetSpellToolTipVar(spell3Display, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.SpiralBlade))
            {
                charVars.PercentOfAttack = 1;
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.SivirPassive(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}