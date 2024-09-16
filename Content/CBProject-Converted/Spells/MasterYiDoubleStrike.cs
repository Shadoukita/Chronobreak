namespace Spells
{
    public class MasterYiDoubleStrike : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseDamage = GetBaseAttackDamage(owner);
            if (target is ObjAIBase)
            {
                AddBuff(attacker, target, new Buffs.MasterYiDoubleStrike(), 1, 1, 0.15f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            else
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false);
            }
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false);
            RemoveOverrideAutoAttack(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.DoubleStrikeIcon), owner);
        }
    }
}
namespace Buffs
{
    public class MasterYiDoubleStrike : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            if (!IsDead(owner))
            {
                float totalAttackDamage = GetBaseAttackDamage(attacker);
                ApplyDamage(attacker, owner, totalAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false);
                if (target is ObjAIBase)
                {
                    SpellEffectCreate(out _, out _, "globalhit_yellow_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                }
            }
        }
    }
}