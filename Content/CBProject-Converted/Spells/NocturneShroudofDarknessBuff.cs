namespace Spells
{
    public class NocturneShroudofDarknessBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class NocturneShroudofDarknessBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "R_hand", "L_hand", "", },
            AutoBuffActivateEffect = new[] { "nocturne_shroud_AttackSpeed_buff.troy", "nocturne_shroud_AttackSpeed_buff.troy", "", },
            BuffName = "NocturneShroudofDarkness",
            BuffTextureName = "Nocturne_ShroudofDarkness.dds",
        };
        float attackSpeedBoost;
        float[] effect0 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            attackSpeedBoost = effect0[level - 1];
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedBoost);
        }
    }
}