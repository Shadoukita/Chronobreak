namespace Spells
{
    public class CounterStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 80, 110, 140, 170, 200 };
        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "Counterstrike_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SpellBuffRemove(owner, nameof(Buffs.CounterStrikeCanCast), owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
            ApplyStun(attacker, target, 1);
        }
    }
}
namespace Buffs
{
    public class CounterStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CounterStrike",
            BuffTextureName = "Armsmaster_Disarm.dds",
        };
    }
}