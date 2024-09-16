namespace Buffs
{
    public class LightningRodApplicator : BuffScript
    {
        float attackCounter;
        EffectEmitter particleID; // UNUSED
        public override void OnActivate()
        {
            attackCounter = 0;
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            if (target is ObjAIBase && target is not BaseTurret && hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                if (attackCounter == 3)
                {
                    ObjAIBase caster = GetBuffCasterUnit(); // UNUSED
                    if (attacker is not Champion)
                    {
                        caster = GetPetOwner((Pet)attacker);
                    }
                    SpellEffectCreate(out particleID, out _, "kennen_btl_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, attacker, "head", default, target, "root", default, true, false, false, false, false);
                    int nextBuffVars_BounceCounter = 1;
                    AddBuff(attacker, target, new Buffs.LightningRodChain(nextBuffVars_BounceCounter), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    attackCounter = 0;
                }
                else
                {
                    attackCounter++;
                }
            }
        }
    }
}