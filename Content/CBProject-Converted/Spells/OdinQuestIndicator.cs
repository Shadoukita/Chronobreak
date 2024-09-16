namespace Buffs
{
    public class OdinQuestIndicator : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCenterShrineBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        EffectEmitter particle;
        EffectEmitter particle2;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            SpellEffectCreate(out particle, out _, "odin_point_active.troy", default, TeamId.TEAM_ORDER, 10, 0, TeamId.TEAM_ORDER, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle2, out _, "odin_point_active.troy", default, TeamId.TEAM_CHAOS, 10, 0, TeamId.TEAM_CHAOS, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
    }
}