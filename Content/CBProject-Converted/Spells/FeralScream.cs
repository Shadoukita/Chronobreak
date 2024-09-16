namespace Spells
{
    public class FeralScream : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "DandyChogath", },
            SpellVOOverrideSkins = new[] { "DandyChogath", },
        };
        int[] effect0 = { 75, 125, 175, 225, 275 };
        float[] effect1 = { 2, 2.25f, 2.5f, 2.75f, 3 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
            if (target is Champion)
            {
                ApplySilence(attacker, target, effect1[level - 1]);
            }
        }
    }
}
namespace Buffs
{
    public class FeralScream : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PopupMessage = new[] { "game_floatingtext_Silenced", },
        };
    }
}