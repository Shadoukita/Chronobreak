namespace Buffs
{
    public class DragonVisionBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Dragon Vision Buff",
            BuffTextureName = "Summoner_clairvoyance.dds",
        };
        Vector3 particlePosition;
        EffectEmitter castParticle;
        Region bubble;
        public DragonVisionBuff(Vector3 particlePosition = default)
        {
            this.particlePosition = particlePosition;
        }
        public override void OnActivate()
        {
            if (owner is Champion)
            {
                Vector3 particlePosition = this.particlePosition;
                TeamId teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out castParticle, out _, "TwistedTreelineClairvoyance.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, particlePosition, target, default, default, false);
                bubble = AddPosPerceptionBubble(teamID, 1150, particlePosition, 90, default, false);
            }
            else
            {
                particlePosition = GetUnitPosition(owner);
            }
            //RequireVar(this.particlePosition);
        }
        public override void OnDeactivate(bool expired)
        {
            if (owner is Champion)
            {
                SpellEffectRemove(castParticle);
                RemovePerceptionBubble(bubble);
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (owner is not Champion)
            {
                Vector3 nextBuffVars_ParticlePosition = particlePosition;
                AddBuff(attacker, attacker, new Buffs.DragonVisionBuff(nextBuffVars_ParticlePosition), 1, 1, 90, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}