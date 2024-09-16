namespace Buffs
{
    public class ManamuneAttackTrack : BuffScript
    {
        float cooldownResevoir;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            cooldownResevoir = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(3, ref lastTimeExecuted, true))
            {
                if (cooldownResevoir < 2)
                {
                    cooldownResevoir++;
                }
            }
        }
        /*
        //TODO: Uncomment and fix
        public override void OnHitUnit(AttackableUnit target, float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(!spellVars.DoesntTriggerSpellCasts && this.cooldownResevoir > 0)
            {
                SpellEffectCreate(out killMe_, out _, "TearoftheGoddess_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                charVars.TearBonusMana++;
                charVars.TearBonusMana = Math.Min(charVars.TearBonusMana, 1000);
                this.cooldownResevoir -= 1;
            }
        }
        */
    }
}