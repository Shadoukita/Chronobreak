namespace Spells
{
    public class WujuStyleSuperCharged : SpellScript
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
    public class WujuStyleSuperCharged : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weaponstreak", },
            AutoBuffActivateEffect = new[] { "WujustyleSC_buf.troy", },
            BuffName = "Wuju Style",
            BuffTextureName = "MasterYi_SunderingStrikes.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MasterYiWujuDeactivated(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
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