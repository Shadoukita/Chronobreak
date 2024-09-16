namespace Buffs
{
    public class FrozenHeartAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "FrozenHeartAura",
            BuffTextureName = "122_Frozen_Heart.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, -0.2f);
        }
    }
}