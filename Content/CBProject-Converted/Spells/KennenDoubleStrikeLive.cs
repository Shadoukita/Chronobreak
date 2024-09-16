namespace Buffs
{
    public class KennenDoubleStrikeLive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "KennenDoubleStrikeLive",
            BuffTextureName = "Kennen_ElectricalSurge.dds",
        };
        EffectEmitter asdf1;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, level, true);
            SpellEffectCreate(out asdf1, out _, "kennen_ds_proc.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_hand", default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(asdf1);
        }
    }
}