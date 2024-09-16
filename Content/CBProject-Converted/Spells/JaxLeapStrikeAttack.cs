namespace Spells
{
    public class JaxLeapStrikeAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        EffectEmitter particle; // UNUSED
        int[] effect0 = { 70, 110, 150, 190, 230 };
        int[] effect1 = { 40, 85, 130, 175, 220 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            BreakSpellShields(target);
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float bonusDamage = effect0[level - 1];
            float totalAD = GetTotalAttackDamage(owner);
            float baseAD = GetBaseAttackDamage(owner);
            float bonusAD = totalAD - baseAD;
            float attackDamage = bonusAD * 1; // UNUSED
            float damageToDeal = bonusDamage + bonusAD;
            float baseAP = GetFlatMagicDamageMod(owner);
            float aPDamage = baseAP * 0.6f;
            damageToDeal += aPDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.JaxEmpowerTwo)) > 0)
            {
                TeamId teamID; // UNITIALIZED
                teamID = TeamId.TEAM_UNKNOWN; //TODO: Verify
                level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
                SpellEffectCreate(out particle, out _, "EmpowerTwoHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.JaxEmpowerTwo), owner, 0);
            }
            if (target is Champion && owner.Team != target.Team)
            {
                IssueOrder(owner, OrderType.AttackTo, default, target);
            }
        }
    }
}