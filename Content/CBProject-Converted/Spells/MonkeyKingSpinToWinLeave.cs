namespace Spells
{
    public class MonkeyKingSpinToWinLeave : SpellScript
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
            SpellBuffRemove(owner, nameof(Buffs.MonkeyKingSpinToWin), owner, 0);
        }
    }
}
namespace Buffs
{
    public class MonkeyKingSpinToWinLeave : BuffScript
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