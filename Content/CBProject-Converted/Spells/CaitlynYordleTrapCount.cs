namespace Buffs
{
    public class CaitlynYordleTrapCount : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.CaitlynYordleTrap)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.CaitlynYordleTrap), (ObjAIBase)owner);
            }
        }
    }
}