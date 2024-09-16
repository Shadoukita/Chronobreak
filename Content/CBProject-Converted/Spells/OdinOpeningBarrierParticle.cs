namespace Buffs
{
    public class OdinOpeningBarrierParticle : BuffScript
    {
        EffectEmitter particle;
        EffectEmitter particle2;
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out _, "Odin_Forcefield_red.troy", default, teamOfOwner, 280, 0, GetEnemyTeam(teamOfOwner), default, default, true, owner, default, default, owner, default, default, false, false, default, false, false);
            SpellEffectCreate(out particle2, out _, "Odin_Forcefield_green.troy", default, GetEnemyTeam(teamOfOwner), 280, 0, teamOfOwner, default, default, true, owner, default, default, owner, default, default, false, false, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            ApplyDamage((ObjAIBase)owner, owner, 25000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
        }
    }
}