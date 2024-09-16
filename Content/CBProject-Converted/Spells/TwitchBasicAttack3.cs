﻿namespace Spells
{
    public class TwitchBasicAttack3 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "GangsterTwitch", "PunkTwitch", },
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}