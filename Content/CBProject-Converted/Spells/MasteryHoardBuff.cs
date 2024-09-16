namespace Spells
{
    public class MasteryHoardBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class MasteryHoardBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "FortifyBuff",
            BuffTextureName = "Summoner_fortify.dds",
            PersistsThroughDeath = true,
        };
        float goldAmount;
        public MasteryHoardBuff(float goldAmount = default)
        {
            this.goldAmount = goldAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.goldAmount);
            IncGold(owner, goldAmount);
        }
    }
}