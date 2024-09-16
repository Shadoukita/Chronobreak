namespace Buffs
{
    public class MonkeyKingDeathParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        EffectEmitter particle1;
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            SpellEffectCreate(out particle1, out _, "CassiopeiaDeath.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "MonkeyKingPHRemoveRocks.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
        }
    }
}