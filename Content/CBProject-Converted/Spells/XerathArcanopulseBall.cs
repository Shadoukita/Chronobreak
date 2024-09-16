namespace Buffs
{
    public class XerathArcanopulseBall : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter particle;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out _, "Xerath_beam_cas.troy", default, teamID, 550, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "top", default, target, default, default, true, false, false, false, false);
            SetNoRender(owner, true);
            SetInvulnerable(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)owner);
        }
    }
}