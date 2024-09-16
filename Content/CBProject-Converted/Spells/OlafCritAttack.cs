﻿namespace Spells
{
    public class OlafCritAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellVOOverrideSkins = new[] { "BroOlaf", },
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            hitResult = HitResult.HIT_Critical;
            float baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}