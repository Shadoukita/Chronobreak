namespace Spells
{
    public class Obduracy : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        public override void SelfExecute()
        {
            float nextBuffVars_PercMod = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.ObduracyBuff(nextBuffVars_PercMod), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class Obduracy : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
        };
    }
}