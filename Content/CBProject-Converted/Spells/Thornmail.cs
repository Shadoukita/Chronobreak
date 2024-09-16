namespace Buffs
{
    public class Thornmail : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Thornmail",
            BuffTextureName = "3075_Thornmail.dds",
        };
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (attacker is not BaseTurret && hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                SpellEffectCreate(out _, out _, "Thornmail_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false);
                float percentDamageTaken = damageAmount * 0.3f;
                ApplyDamage((ObjAIBase)owner, attacker, percentDamageTaken, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_REACTIVE, 1, 0, 1, false, false);
            }
        }
    }
}