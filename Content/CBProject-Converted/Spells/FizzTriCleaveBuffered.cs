namespace Spells
{
    public class FizzTriCleaveBuffered : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
    }
}
namespace Buffs
{
    public class FizzTriCleaveBuffered : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        Vector3 targetPos;
        int level;
        public FizzTriCleaveBuffered(Vector3 targetPos = default, int level = default)
        {
            this.targetPos = targetPos;
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.level);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}