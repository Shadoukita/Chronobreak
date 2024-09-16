namespace Buffs
{
    public class BeaconAuraNoParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rally",
            BuffTextureName = "Summoner_rally.dds",
        };
        float damageMod;
        public override void OnActivate()
        {
            //RequireVar(this.finalHPRegen);
            int ownerLevel = GetLevel(attacker);
            damageMod = 1.47059f * ownerLevel;
            damageMod += 8.5294f;
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageMod);
        }
    }
}