namespace Spells
{
    public class HateSpikeMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 2,
            },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = { 12.5f, 20, 27.5f, 35, 42.5f };
        int[] effect1 = { 25, 40, 55, 70, 85 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int hSCounter = GetSpellTargetsHitPlusOne(spell);
            if (target is Champion)
            {
                AddBuff(attacker, target, new Buffs.Malice_marker(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            if (hSCounter == 2)
            {
                ApplyDamage(owner, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.14f);
            }
            else
            {
                ApplyDamage(owner, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.28f);
            }
        }
    }
}