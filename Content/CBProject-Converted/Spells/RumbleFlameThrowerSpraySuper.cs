namespace Spells
{
    public class RumbleFlameThrowerSpraySuper : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 15, 30, 45, 60, 75 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int nextBuffVars_Level = level; // UNUSED
            float damage = effect0[level - 1];
            float aP = GetFlatMagicDamageMod(owner);
            aP *= 0.225f;
            damage += aP;
            damage *= 1.3f;
            if (target is not Champion)
            {
                damage *= 0.5f;
            }
            ApplyDamage(owner, target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
    }
}