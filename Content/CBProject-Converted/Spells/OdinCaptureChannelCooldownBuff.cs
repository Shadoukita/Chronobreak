namespace Spells
{
    public class OdinCaptureChannelCooldownBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class OdinCaptureChannelCooldownBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "OdinCaptureChannelCooldownBuff",
            BuffTextureName = "Odin_Interrupted.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}