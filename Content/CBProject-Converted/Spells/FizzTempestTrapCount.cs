namespace Buffs
{
    public class FizzTempestTrapCount : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.FizzTempestTrap)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.FizzTempestTrap), (ObjAIBase)owner, 0);
            }
        }
    }
}