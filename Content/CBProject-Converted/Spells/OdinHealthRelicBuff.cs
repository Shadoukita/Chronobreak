namespace Spells
{
    public class OdinHealthRelicBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class OdinHealthRelicBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Regenerationpotion_itm.troy", },
            BuffName = "OdinHealthRelic",
            BuffTextureName = "2003_Regeneration_Potion.dds",
        };
        float healPerTick;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float baseHeal = 80;
            int level = GetLevel(owner);
            float perLevelHeal = 25 * level;
            float totalHeal = perLevelHeal + baseHeal;
            healPerTick = totalHeal / 10;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                IncHealth(owner, healPerTick, owner);
            }
        }
    }
}