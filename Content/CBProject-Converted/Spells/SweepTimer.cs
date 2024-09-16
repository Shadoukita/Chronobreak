namespace Buffs
{
    public class SweepTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            bool unitFound = false;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 200, SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 1))
            {
                unitFound = true;
                SpellCast((ObjAIBase)owner, owner, owner.Position3D, owner.Position3D, 0, SpellSlotType.SpellSlots, 1, false, false, false);
            }
            if (!unitFound)
            {
                SpellCast((ObjAIBase)owner, owner, owner.Position3D, owner.Position3D, 2, SpellSlotType.SpellSlots, 1, false, false, false);
            }
        }
    }
}