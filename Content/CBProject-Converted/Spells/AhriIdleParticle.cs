namespace Buffs
{
    public class AhriIdleParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            PersistsThroughDeath = true,
        };
        EffectEmitter particle1;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle1, out _, "Ahri_Orb.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
    }
}