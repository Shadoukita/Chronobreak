namespace Spells
{
    public class ToxicShot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "AstronautTeemo", },
        };
    }
}
namespace Buffs
{
    public class ToxicShot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Toxic Attack",
            BuffTextureName = "Teemo_PoisonedDart.dds",
            SpellToggleSlot = 3,
        };
        public override void OnActivate()
        {
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
        }
    }
}