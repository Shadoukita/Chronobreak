namespace Spells
{
    public class VolibearRChargetracker : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class VolibearRChargetracker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", null, "", },
            AutoBuffActivateEffect = new[] { "", "", "", "", },
            BuffName = "VolibearRApplicator",
            BuffTextureName = "Minotaur_Pulverize.dds",
        };
    }
}