namespace Buffs
{
    public class VolibearQSpeedPart : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter speedParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out speedParticle, out _, "volibear_q_speed_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(speedParticle);
        }
    }
}