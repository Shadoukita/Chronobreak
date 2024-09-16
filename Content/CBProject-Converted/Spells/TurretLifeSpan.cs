namespace Buffs
{
    public class TurretLifeSpan : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 1500, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1);
            SpellEffectCreate(out _, out _, "jackintheboxpoof.troy", default, default, default, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target);
        }
    }
}