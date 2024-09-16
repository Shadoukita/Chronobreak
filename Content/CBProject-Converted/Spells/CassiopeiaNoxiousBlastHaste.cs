namespace Spells
{
    public class CassiopeiaNoxiousBlastHaste : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
    }
}
namespace Buffs
{
    public class CassiopeiaNoxiousBlastHaste : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Interventionspeed_buf.troy", },
            BuffName = "CassiopeiaNoxiousBlastHaste",
            BuffTextureName = "Cassiopeia_NoxiousBlast.dds",
        };
        float moveSpeedMod;
        public CassiopeiaNoxiousBlastHaste(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}