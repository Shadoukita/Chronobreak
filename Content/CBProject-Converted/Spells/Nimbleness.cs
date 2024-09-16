namespace Spells
{
    public class Nimbleness : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class Nimbleness : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Nimbleness",
            BuffTextureName = "3009_Boots_of_Teleportation.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.1f);
        }
    }
}