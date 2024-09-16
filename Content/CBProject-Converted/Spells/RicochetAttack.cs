namespace Spells
{
    public class SivirWAttack: RicochetAttack {}
    public class RicochetAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitEnemies = true,
                CanHitFriends = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 6,
            },
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 20, 35, 50, 65, 80 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int targetNum = GetSpellTargetsHitPlusOne(spell);
            float baseAttackDamage = GetBaseAttackDamage(owner);
            float bonusAD = GetFlatPhysicalDamageMod(owner);
            float totalAD = baseAttackDamage + bonusAD;
            float multipliedAD = totalAD * 1;
            float baseDamage = effect0[level - 1];
            float damageToDeal = baseDamage + multipliedAD;
            SpellBuffRemove(attacker, nameof(Buffs.Ricochet), attacker, 0);
            if (targetNum == 1)
            {
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            }
            else
            {
                float counter = 1;
                float damagePercent = 1;
                while (counter < targetNum)
                {
                    damagePercent *= 0.8f;
                    counter++;
                }
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, damagePercent, 0, 0, false, false, attacker);
            }
        }
    }
}