namespace Buffs
{
    public class OdinShrineTimeBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineTimeBuff",
            BuffTextureName = "Chronokeeper_Haste.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        EffectEmitter buffParticle;
        public override void OnActivate()
        {
            float ultCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float newUltCD = ultCD / 2;
            SetSlotSpellCooldownTimeVer2(newUltCD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            float sS0CD = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            float newSS0CD = sS0CD / 2;
            SetSlotSpellCooldownTimeVer2(newSS0CD, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner);
            float sS1CD = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            float newSS1CD = sS1CD / 2;
            SetSlotSpellCooldownTimeVer2(newSS1CD, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner);
            SpellEffectCreate(out buffParticle, out _, "NeutralMonster_buf_blue_defense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }
    }
}