namespace Buffs
{
    public class LuxDeathParticleTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        EffectEmitter particle1; // UNUSED
        public override void OnActivate()
        {
            SpellEffectCreate(out particle1, out _, "Luxdeathparticle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", owner.Position3D, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.LuxDeathParticle(), 1, 1, 250000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}