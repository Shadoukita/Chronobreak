namespace Buffs
{
    public class Blind : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Global_miss.troy", },
            BuffName = "Blind",
            BuffTextureName = "BlindMonk_SightUnseeing.dds",
        };

        public override BuffScriptMetaData BuffMetaData { get; } = new BuffScriptMetaData
        {
            BuffAddType = BuffAddType.STACKS_AND_OVERLAPS,
            BuffType = BuffType.BLIND
        };

        float missChance;
        public Blind(float missChance = default)
        {
            this.missChance = missChance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.missChance);
            CancelAutoAttack(owner, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatMissChanceMod(owner, missChance);
        }
    }
}