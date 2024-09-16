namespace Buffs
{
    public class FallenOneTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true);
        }
    }
}