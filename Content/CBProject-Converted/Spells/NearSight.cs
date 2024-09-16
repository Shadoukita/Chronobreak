namespace Buffs
{
    public class NearSight : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NearSight",
            BuffTextureName = "Nocturne_Paranoia.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SetNearSight(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNearSight(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetNearSight(owner, true);
        }
    }
}