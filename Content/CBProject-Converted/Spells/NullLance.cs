namespace Spells
{
    public class NullLance : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 130, 180, 230, 280 };
        float[] effect1 = { 1, 1.4f, 1.8f, 2.2f, 2.6f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 1, false, false, attacker);
            ApplySilence(attacker, target, effect1[level - 1]);
        }
    }
}
namespace Buffs
{
    public class NullLance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "Null_Lance_buf.troy", },
            BuffName = "NullLance",
            BuffTextureName = "Averdrian_AstralBeam.dds",
        };
    }
}