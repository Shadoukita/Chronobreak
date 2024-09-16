namespace Spells
{
    public class GravesPassiveShotAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 20, 20, 20, 35, 35, 35, 35, 50, 50, 50, 50, 65, 65, 65, 65, 80, 80, 80 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float damageToDeal;
            int level = GetLevel(attacker);
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            if (target is not BaseTurret)
            {
                damageToDeal = GetTotalAttackDamage(attacker);
                float bonusDamage = effect0[level - 1];
                damageToDeal += bonusDamage;
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                damageToDeal *= 0.5f;
                AddBuff(attacker, target, new Buffs.GravesPassiveShotAttack(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, target.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, nameof(Buffs.GravesPassiveShotAttack), false))
                {
                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
                SpellBuffRemove(attacker, nameof(Buffs.GravesPassiveShot), attacker, 0);
                AddBuff(attacker, attacker, new Buffs.GravesPassiveStack(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
            }
            else
            {
                damageToDeal = GetTotalAttackDamage(attacker);
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class GravesPassiveShotAttack : BuffScript
    {
    }
}