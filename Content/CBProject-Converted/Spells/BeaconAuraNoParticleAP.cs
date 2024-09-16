namespace Buffs
{
    public class BeaconAuraNoParticleAP : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rally AP",
            BuffTextureName = "Summoner_rally.dds",
        };
        float damageMod;
        float apMod;
        public override void OnActivate()
        {
            //RequireVar(this.finalHPRegen);
            int ownerLevel = GetLevel(attacker);
            damageMod = 1.47059f * ownerLevel;
            damageMod += 8.5294f;
            apMod = damageMod * 2;
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageMod);
            IncFlatMagicDamageMod(owner, apMod);
        }
    }
}