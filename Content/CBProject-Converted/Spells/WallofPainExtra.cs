namespace Spells
{
    public class KarthusWallOfPainExtra: WallofPainExtra {}
    public class WallofPainExtra : SpellScript
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
    public class WallofPainExtra : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Wall of Pain",
            BuffTextureName = "Lich_WallOfPain.dds",
        };
        float armorMod;
        public WallofPainExtra(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorMod);
            IncFlatSpellBlockMod(owner, armorMod);
        }
    }
}