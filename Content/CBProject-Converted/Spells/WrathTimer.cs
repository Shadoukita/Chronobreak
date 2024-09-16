namespace Buffs
{
    public class WrathTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position3D, owner.Position3D, 2, SpellSlotType.SpellSlots, 1, false, false, false);
        }
    }
}