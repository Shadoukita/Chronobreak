namespace Buffs
{
    public class OdinCombatActive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCombatActive",
            BuffTextureName = "Averdrian_ConsumeSpirit.dds",
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.Internal_50MS), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.OdinGrievousWound), (ObjAIBase)owner, 0);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Internal_50MS(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGrievousWound(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}