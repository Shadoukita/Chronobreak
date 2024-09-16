namespace Buffs
{
    public class OdinShrineHealBuff : BuffScript
    {
        bool willRemove;
        float tickWorth;
        float tickWorthMana;
        float tickNumber;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float maxMP = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            float tickWorth = maxHP / 21;
            float tickWorthMana = maxMP / 6;
            willRemove = false;
            this.tickWorth = tickWorth;
            this.tickWorthMana = tickWorthMana;
            tickNumber = 1;
            SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC)
            {
                willRemove = true;
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2, ref lastTimeExecuted, false))
            {
                if (!willRemove)
                {
                    float healAmount = tickWorth * tickNumber;
                    IncPAR(owner, tickWorthMana, PrimaryAbilityResourceType.MANA);
                    IncHealth(owner, healAmount, owner);
                    SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    tickNumber++;
                }
            }
        }
    }
}