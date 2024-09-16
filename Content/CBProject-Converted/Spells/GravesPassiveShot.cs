namespace Spells
{
    public class GravesPassiveShot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class GravesPassiveShot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "head", },
            AutoBuffActivateEffect = new[] { "", "RighteousFuryHalo_buf.troy", },
            BuffName = "JudicatorRighteousFury",
            BuffTextureName = "Judicator_RighteousFury.dds",
        };
        public override void OnActivate()
        {
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
        }
    }
}