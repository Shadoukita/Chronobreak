namespace Buffs
{
    public class OdinSpawnProto : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinSpawnProto",
            BuffTextureName = "Summoner_spawn.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        /*
        //TODO: Uncomment and fix
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            string name1 = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            string name2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(name1 == nameof(Spells.SummonerSpawn))
            {
                SetSlotSpellCooldownTimeVer2(24, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner);
            }
            if(name2 == nameof(Spells.SummonerSpawn))
            {
                SetSlotSpellCooldownTimeVer2(24, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner);
            }
        }
        */
    }
}