namespace Buffs
{
    public class CounterStrikeDodgeUp : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CounterStrikeDodgeUp",
            BuffTextureName = "Armsmaster_Disarm.dds",
        };
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float lvlDodgeMod = level * 0.02f;
            float dodgeMod = lvlDodgeMod + 0.08f;
            IncFlatDodgeMod(owner, dodgeMod);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CounterStrikeCanCast)) == 0)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
    }
}