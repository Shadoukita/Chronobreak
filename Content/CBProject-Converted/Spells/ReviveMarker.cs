namespace Buffs
{
    public class ReviveMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Preservation",
            BuffTextureName = "Summoner_revive.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float healthMod;
        public ReviveMarker(float healthMod = default)
        {
            this.healthMod = healthMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.healthMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, healthMod);
        }
    }
}