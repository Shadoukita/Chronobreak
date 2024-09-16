namespace Buffs
{
    public class LuxLightstrikeDetonator : BuffScript
    {
        float lsCooldown;
        Vector3 _attacker;
        public LuxLightstrikeDetonator(float lSCooldown = default, Vector3 attacker = default)
        {
            this.lsCooldown = lSCooldown;
            this._attacker = attacker;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lSCooldown);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LuxLightstrikeToggle));
        }
        public override void OnDeactivate(bool expired)
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, this._attacker, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage((ObjAIBase)owner, unit, 300, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
            ApplyDamage(attacker, attacker, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * this.lsCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LuxLightStrikeKugel));
        }
    }
}