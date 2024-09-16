namespace Spells
{
    public class PuncturingTaunt : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { -10, -15, -20, -25, -30 };
        float[] effect1 = { 1, 1.5f, 2, 2.5f, 3 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int nextBuffVars_ArmorDebuff = effect0[level - 1];
            float tauntDuration = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.PuncturingTauntArmorDebuff(nextBuffVars_ArmorDebuff), 1, 1, tauntDuration, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
            ApplyTaunt(attacker, target, tauntDuration);
        }
    }
}
namespace Buffs
{
    public class PuncturingTaunt : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Armordillo_ScaledPlating.dds",
        };
    }
}