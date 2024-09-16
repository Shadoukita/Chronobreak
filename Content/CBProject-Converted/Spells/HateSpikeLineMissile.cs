namespace Spells
{
    public class HateSpikeLineMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 30, 50, 70, 90, 110 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            BreakSpellShields(target);
            if (target is Champion)
            {
                ApplyDamage(owner, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.33f, 1, false, false, attacker);
                AddBuff(owner, target, new Buffs.EvelynnSoulEater(), 4, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
            }
            else
            {
                ApplyDamage(owner, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.33f, 1, false, false, attacker);
            }
        }
    }
}