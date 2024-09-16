namespace Spells
{
    public class ArmsmasterLeapStrikeAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 95, 130, 165, 200 };
        int[] effect1 = { 20, 45, 70, 95, 120 };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.LeapStrikeSpeed(), 1, 1, 0.35f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level;
            float bonusDamage;
            BreakSpellShields(target);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.EmpowerTwo)) > 0)
            {
                float bonusAttackDamage = GetFlatPhysicalDamageMod(owner);
                level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                bonusDamage = effect0[level - 1];
                float physicalBonus = bonusAttackDamage * 0.4f;
                float aOEDmg = physicalBonus + bonusDamage;
                ApplyDamage(attacker, target, aOEDmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0.7f, false, false, attacker);
                SpellBuffRemove(owner, nameof(Buffs.EmpowerTwo), owner, 0);
            }
            float attackDamage = GetTotalAttackDamage(owner);
            level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusDamage = effect1[level - 1];
            float damageToDeal = attackDamage + bonusDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 0, false, false, attacker);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RelentlessAssaultMarker)) > 0)
            {
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.RelentlessAssaultMarker), 0);
            }
            if (target is Champion && owner.Team != target.Team)
            {
                IssueOrder(owner, OrderType.AttackTo, default, target);
            }
        }
    }
}