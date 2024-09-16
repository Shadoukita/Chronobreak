namespace Spells
{
    public class WormAttack : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 0.7f, 0, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.WormAttack(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class WormAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WormAttack",
            BuffTextureName = "48thSlave_SoulDrain.dds",
        };
        float damageMod;
        public override void OnActivate()
        {
            float charDamage = GetTotalAttackDamage(owner);
            damageMod = charDamage * -0.5f;
            IncFlatPhysicalDamageMod(owner, damageMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageMod);
        }
    }
}