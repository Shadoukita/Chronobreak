namespace Spells
{
    public class RyzeDesperatePowerAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = GetBaseAttackDamage(owner);
            if (level == 1)
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0.3f, default, false, false);
            }
            else if (level == 2)
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0.4f, default, false, false);
            }
            else
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0.5f, default, false, false);
            }
        }
    }
}