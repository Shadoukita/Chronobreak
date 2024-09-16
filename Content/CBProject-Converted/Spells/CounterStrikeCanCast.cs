namespace Buffs
{
    public class CounterStrikeCanCast : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Counter Strike Can Cast",
            BuffTextureName = "Armsmaster_Disarm.dds",
            NonDispellable = true,
        };
        EffectEmitter removeMe2;
        bool cooledDown;
        EffectEmitter removeMe;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (cooldown <= 0)
            {
                SpellEffectCreate(out removeMe2, out _, "CounterStrike_ready.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                cooledDown = true;
            }
            else
            {
                SpellEffectCreate(out removeMe, out _, "CounterStrike_dodged.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                cooledDown = false;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            if (!cooledDown)
            {
                SpellEffectRemove(removeMe);
            }
            else
            {
                SpellEffectRemove(removeMe2);
            }
        }
        public override void OnUpdateStats()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            if (!cooledDown)
            {
                float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (cooldown <= 0)
                {
                    cooledDown = true;
                    SpellEffectRemove(removeMe);
                    SpellEffectCreate(out removeMe2, out _, "CounterStrike_ready.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                }
            }
        }
    }
}