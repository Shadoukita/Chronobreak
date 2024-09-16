namespace Buffs
{
    public class CasterMinionAuraNoParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Caster Minion Aura",
            BuffTextureName = "3022_Frozen_Heart.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentPhysicalDamageMod(owner, 0);
            IncPercentMagicDamageMod(owner, -1);
        }
    }
}