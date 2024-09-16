namespace Buffs
{
    public class VayneInquisitionSpeedPartNormal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter speedParticle;
        public override void OnActivate()
        {
            EffectEmitter speedParticle;
            SpellEffectCreate(out speedParticle, out _, "vayne_passive_speed_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, default, default, false);
            this.speedParticle = speedParticle;
        }
        public override void OnDeactivate(bool expired)
        {
            EffectEmitter speedParticle = this.speedParticle; // UNUSED
            SpellEffectRemove(this.speedParticle);
        }
    }
}