namespace Spells
{
    public class SonaHymnofValorAttackUpgrade : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int levelDamage = GetLevel(owner);
            float bonusDamage = levelDamage * 9;
            float totalDamage = bonusDamage + 14;
            totalDamage *= 2;
            float nextBuffVars_TotalDamage = totalDamage;
            float attackDamage = GetTotalAttackDamage(owner);
            ApplyDamage(owner, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            AddBuff(attacker, target, new Buffs.SonaHymnPCDeathRecapFix(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.IfHasBuffCheck(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemove(owner, nameof(Buffs.SonaPowerChord), owner, 0);
        }
    }
}
namespace Buffs
{
    public class SonaHymnofValorAttackUpgrade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
    }
}