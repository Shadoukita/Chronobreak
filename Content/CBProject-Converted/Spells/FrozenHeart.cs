namespace Buffs
{
    public class FrozenHeart : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.2f);
        }
    }
}