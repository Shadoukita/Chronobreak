namespace Buffs
{
    public class OdinBombSuppression : BuffScript
    {
        float startTime;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            startTime = GetGameTime();
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.OdinBombSuppressionOrder));
            SpellBuffClear(owner, nameof(Buffs.OdinBombSuppressionChaos));
        }
        public override void OnUpdateStats()
        {
            int orderChannelCount = GetBuffCountFromAll(owner, nameof(Buffs.OdinBombSuppressionOrder));
            int chaosChannelBuff = GetBuffCountFromAll(owner, nameof(Buffs.OdinBombSuppressionChaos));
            if (orderChannelCount > 0 && chaosChannelBuff > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.OdinBombSuppression));
            }
            if (orderChannelCount == 0 && chaosChannelBuff == 0)
            {
                SpellBuffClear(owner, nameof(Buffs.OdinBombSuppression));
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                float currentTime = GetGameTime();
                float timePassed = currentTime - startTime;
                if (timePassed >= 0)
                {
                    int chaosChannelBuff = GetBuffCountFromAll(owner, nameof(Buffs.OdinBombSuppressionChaos));
                    int orderChannelCount = GetBuffCountFromAll(owner, nameof(Buffs.OdinBombSuppressionOrder));
                    float totalBuffCount = Math.Max(chaosChannelBuff, orderChannelCount);
                    if (totalBuffCount > 0)
                    {
                        float modifier = totalBuffCount - 1;
                        modifier *= -700;
                        float pAR_to_modify = modifier + -7000;
                        IncPAR(owner, pAR_to_modify, PrimaryAbilityResourceType.MANA);
                        float maxPAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
                        float currentPAR = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                        float pAR_Percent = currentPAR / maxPAR;
                        if (pAR_Percent <= 0.05f)
                        {
                            float newDuration = 50;
                            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                            {
                                newDuration *= 1.2f;
                            }
                            AddBuff(attacker, attacker, new Buffs.OdinCenterRelicBuff(), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                            AddBuff(attacker, attacker, new Buffs.OdinScoreBigRelic(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            AddBuff(attacker, attacker, new Buffs.OdinCenterRelicBuffDamage(), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                            AddBuff(attacker, attacker, new Buffs.OdinBombSuccessParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            ApplyDamage((ObjAIBase)owner, owner, 500, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
                        }
                    }
                }
            }
        }
    }
}