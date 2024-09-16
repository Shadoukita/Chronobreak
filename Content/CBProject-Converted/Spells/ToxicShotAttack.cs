namespace Spells
{
    public class ToxicShotAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "AstronautTeemo", },
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff(owner, owner, new Buffs.Toxicshotapplicator(), 1, 1, 0.1f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            float attackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class ToxicShotAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Toxic Shot",
            BuffTextureName = "Teemo_PoisonedDart.dds",
        };
    }
}