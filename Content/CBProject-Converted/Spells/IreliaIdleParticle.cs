namespace Buffs
{
    public class IreliaIdleParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", "", },
            BuffName = "IreliaIdleParticle",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        EffectEmitter particle4;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle4, out _, "irelia_ult_energy_ready.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_BACK_2", default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle4);
        }
    }
}