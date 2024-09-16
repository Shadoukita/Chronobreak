namespace Buffs
{
    public class ShatterSelfBonus : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShatterSelfBonus",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
        EffectEmitter taric;
        float armorBonus;
        int[] effect0 = { 10, 15, 20, 25, 30 };
        public override void OnActivate()
        {
            SpellEffectCreate(out taric, out _, "ShatterReady_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(taric);
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            armorBonus = effect0[level - 1];
            IncFlatArmorMod(owner, armorBonus);
        }
    }
}