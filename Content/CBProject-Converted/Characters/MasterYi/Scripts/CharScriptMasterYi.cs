namespace CharScripts
{
    public class CharScriptMasterYi : CharScript
    {
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                float cooldown = GetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (cooldown <= 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.WujuStyle)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.WujuStyle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                    }
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DoubleStrikeIcon)) == 0)
            {
                AddBuff(attacker, attacker, new Buffs.DoubleStrike(), 7, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
                int dSCount = GetBuffCountFromCaster(owner, owner, nameof(Buffs.DoubleStrike));
                if (dSCount >= 7)
                {
                    AddBuff(attacker, attacker, new Buffs.DoubleStrikeIcon(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                    SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.DoubleStrike), 7);
                    OverrideAutoAttack(0, SpellSlotType.ExtraSlots, attacker, 1, true);
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.AlphaStrike))
            {
                AddBuff(owner, owner, new Buffs.AlphaStrike(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, attacker, new Buffs.DoubleStrike(), 7, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
            AddBuff(owner, owner, new Buffs.MasterYiWujuDeactivated(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}