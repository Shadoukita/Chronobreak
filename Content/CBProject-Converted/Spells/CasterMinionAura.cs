namespace Buffs
{
    public class CasterMinionAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Caster Minion Aura",
            BuffTextureName = "3022_Frozen_Heart.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentPhysicalDamageMod(owner, 1);
            IncPercentMagicDamageMod(owner, -1);
        }
    }
}