namespace Buffs
{
    public class SonaSongofDiscordCheck : BuffScript
    {
        EffectEmitter particleID;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out particleID, out _, "SonaPowerChordReady_violet.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            SpellBuffRemove(owner, nameof(Buffs.SonaHymnofValorCheck), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.SonaAriaofPerseveranceCheck), (ObjAIBase)owner);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.SonaSongofDiscordAttackUpgrade));
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleID);
        }
    }
}