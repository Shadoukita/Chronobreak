namespace Spells
{
    public class HasBeenRevived : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class HasBeenRevived : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "HasBeenRevived",
            BuffTextureName = "3026_Guardian_Angel_Charging.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.WillRevive(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemove(owner, nameof(Buffs.WillRevive), (ObjAIBase)owner, 0);
        }
    }
}