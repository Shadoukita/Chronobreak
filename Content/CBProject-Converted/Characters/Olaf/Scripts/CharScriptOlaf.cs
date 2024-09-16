namespace CharScripts
{
    public class CharScriptOlaf : CharScript
    {
        float lastTimeExecuted;
        float bonusDamage;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float healthDamage = maxHealth * 0.01f;
                SetSpellToolTipVar(healthDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (attacker == owner)
            {
                float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if (currentHealth <= damageAmount)
                {
                    damageAmount = currentHealth - 1;
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.OlafAxeThrow))
            {
                Vector3 targetPos = GetSpellTargetPos(spell);
                float distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                distance += 50;
                Vector3 facingPos = GetPointByUnitFacingOffset(owner, distance, 0);
                Vector3 nextBuffVars_FacingPos = facingPos;
                Vector3 nextBuffVars_TargetPos = targetPos;
                AddBuff(owner, owner, new Buffs.OlafAxeThrow(nextBuffVars_FacingPos, nextBuffVars_TargetPos), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.OlafBerzerkerRage(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float healthDamage = maxHealth * 0.004f;
            bonusDamage = 12 + healthDamage;
            SetSpellToolTipVar(bonusDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                IncPermanentFlatArmorPenetrationMod(owner, 10);
            }
        }
    }
}