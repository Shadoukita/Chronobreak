namespace Spells
{
    public class GarenBladestormLeave : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.GarenBladestorm), owner);
        }
    }
}
namespace Buffs
{
    public class GarenBladestormLeave : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "",
            BuffTextureName = "Sivir_Sprint.dds",
            SpellToggleSlot = 3,
        };
    }
}