namespace Buffs
{
    public class EmpowerCleave : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "EmpowerCleave",
            BuffTextureName = "Armsmaster_Empower.dds",
        };
        public override void OnActivate()
        {
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false);
        }
    }
}