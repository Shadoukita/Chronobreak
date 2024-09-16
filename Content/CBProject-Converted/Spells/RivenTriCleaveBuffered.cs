namespace Spells
{
    public class RivenTriCleaveBuffered : SpellScript
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
    public class RivenTriCleaveBuffered : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        bool championLock;
        Vector3 targetPos;
        int level;
        public RivenTriCleaveBuffered(bool championLock = default, Vector3 targetPos = default, int level = default)
        {
            this.championLock = championLock;
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
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                targetPos = GetUnitPosition(unit);
            }
            if (championLock)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.RivenTriCleaveBufferLock), true))
                {
                    targetPos = GetUnitPosition(unit);
                }
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 4, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}