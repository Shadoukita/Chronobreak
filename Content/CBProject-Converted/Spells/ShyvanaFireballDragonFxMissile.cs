namespace Spells
{
    public class ShyvanaFireballDragonFxMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
    }
}
namespace Buffs
{
    public class ShyvanaFireballDragonFxMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
    }
}