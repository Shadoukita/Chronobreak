namespace Spells
{
    public class XenZhaoBattleCryActivated : SpellScript
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
    public class XenZhaoBattleCryActivated : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon", },
            AutoBuffActivateEffect = new[] { "WujustyleSC_buf.troy", },
            BuffName = "Wuju Style",
            BuffTextureName = "XenZhao_BattleCry.dds",
        };
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = 20;
            float levelDamage = 10 * level;
            float totalDamage = levelDamage + baseDamage;
            IncFlatPhysicalDamageMod(owner, totalDamage);
        }
    }
}