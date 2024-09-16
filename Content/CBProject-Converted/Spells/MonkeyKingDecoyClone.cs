namespace Buffs
{
    public class MonkeyKingDecoyClone : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
        };
        EffectEmitter particle; // UNUSED
        public override void OnActivate()
        {
            SetNotTargetableToTeam(owner, true, false);
            ShowHealthBar(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out _, "MonkeyKing_Copy.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, teamID, default, default, true, owner, "root", default, target, "root", default, false, false, false, false, false);
            IssueOrder(owner, OrderType.Hold, default, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SpellCast((ObjAIBase)owner, owner, default, default, 1, SpellSlotType.ExtraSlots, level, false, false, false, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MonkeyKingKillCloneW(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
        }
    }
}