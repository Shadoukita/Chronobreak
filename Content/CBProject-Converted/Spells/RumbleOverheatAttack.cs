namespace Spells
{
    public class RumbleOverheatAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "GangsterTwitch", "PunkTwitch", },
        };
        float punchdmg;
        int[] effect0 = { 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            if (target is ObjAIBase && target is not BaseTurret && GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleOverheat)) > 0)
            {
                int level = GetLevel(owner);
                punchdmg = effect0[level - 1];
                ApplyDamage(attacker, target, punchdmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.3f, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class RumbleOverheatAttack : BuffScript
    {
        int punchdmg; // UNUSED
        public override void OnActivate()
        {
            punchdmg = 0;
        }
    }
}