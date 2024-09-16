namespace Buffs
{
    public class LightstrikerApplicator : BuffScript
    {
        float attackCounter;
        public override void OnActivate()
        {
            attackCounter = 0;
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            attackCounter++;
            if (attackCounter == 4)
            {
                ObjAIBase caster = GetBuffCasterUnit();
                if (attacker is not Champion)
                {
                    caster = GetPetOwner((Pet)attacker);
                }
                ApplyDamage(caster, target, 100, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                attackCounter = 0;
                if (target is ObjAIBase)
                {
                    SpellEffectCreate(out _, out _, "sword_of_the_divine_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
                }
            }
        }
    }
}