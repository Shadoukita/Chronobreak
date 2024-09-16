namespace Buffs
{
    public class LuxDeathParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        EffectEmitter particle2; // UNUSED
        public override void OnActivate()
        {
            SpellEffectCreate(out particle2, out _, "Lux_death.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, "C_BUFFBONE_GLB_CENTER_LOC", default, false);
        }
    }
}