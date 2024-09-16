namespace Spells
{
    public class ShenKiAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (target is ObjAIBase)
            {
                SpellEffectCreate(out _, out _, "Globalhit_red.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, owner.Position3D, owner, default, default, true, default, default, false);
            }
            float baseDmg = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            float maxHP = GetFlatHPPoolMod(owner);
            float bonusDmgFromHP = maxHP * 0.08f;
            int level = GetLevel(owner);
            float shurikenDamage = effect0[level - 1];
            float damageToDeal = bonusDmgFromHP + shurikenDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            SpellBuffRemove(attacker, nameof(Buffs.ShenWayOfTheNinjaAura), attacker, 0);
        }
    }
}
namespace Buffs
{
    public class ShenKiAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
        };
    }
}