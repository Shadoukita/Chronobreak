namespace Buffs
{
    public class OdinGuardianSuppression : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
        };
        EffectEmitter particle;
        float startTime;
        TeamId myTeamID;
        TeamId oldMyTeamID;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle, out _, "odin_suppression.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "Crystal", owner.Position3D, owner, default, default, false, false, false, false, false);
            startTime = GetGameTime();
            ApplyStun((ObjAIBase)owner, owner, 0.5f);
            myTeamID = GetTeamID_CS(owner);
            oldMyTeamID = myTeamID;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.OdinGuardianSuppressionOrder));
            SpellBuffClear(owner, nameof(Buffs.OdinGuardianSuppressionChaos));
            SpellEffectRemove(particle);
        }
        public override void OnUpdateStats()
        {
            int orderChannelCount = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppressionOrder));
            int chaosChannelBuff = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppressionChaos));
            if (orderChannelCount > 0 && chaosChannelBuff > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.OdinGuardianSuppression));
            }
            if (orderChannelCount == 0 && chaosChannelBuff == 0)
            {
                SpellBuffClear(owner, nameof(Buffs.OdinGuardianSuppression));
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                float currentTime = GetGameTime();
                float timePassed = currentTime - startTime;
                if (timePassed >= 1.5f)
                {
                    int run = 1;
                    int chaosChannelBuff = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppressionChaos));
                    int orderChannelCount = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppressionOrder));
                    if (chaosChannelBuff > 0)
                    {
                        if (orderChannelCount > 0)
                        {
                            run = 0;
                        }
                    }
                    if (orderChannelCount > 0)
                    {
                        if (chaosChannelBuff > 0)
                        {
                            run = 0;
                        }
                    }
                    float totalBuffCount = Math.Max(orderChannelCount, chaosChannelBuff);
                    if (run == 1)
                    {
                        float damageMultiplier = totalBuffCount - 1;
                        damageMultiplier *= 0.4f;
                        int prilisasBlessingCount = GetBuffCountFromAll(owner, nameof(Buffs.PrilisasBlessing));
                        if (prilisasBlessingCount > 0)
                        {
                            damageMultiplier++;
                        }
                        else
                        {
                            damageMultiplier++;
                        }
                        float totalHP = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
                        float dtD = totalHP * 0.0294f;
                        dtD *= damageMultiplier;
                        myTeamID = GetTeamID_CS(owner);
                        if (myTeamID == TeamId.TEAM_NEUTRAL)
                        {
                            if (oldMyTeamID != myTeamID)
                            {
                                SpellBuffRemove(owner, nameof(Buffs.OdinCaptureSoundEmptying), (ObjAIBase)owner, 0);
                            }
                            oldMyTeamID = myTeamID;
                            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCaptureSoundFilling(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                            if (chaosChannelBuff > 0)
                            {
                                dtD *= -0.5f;
                            }
                            else
                            {
                                dtD *= 0.5f;
                            }
                        }
                        else
                        {
                            if (oldMyTeamID != myTeamID)
                            {
                                SpellBuffRemove(owner, nameof(Buffs.OdinCaptureSoundFilling), (ObjAIBase)owner, 0);
                            }
                            oldMyTeamID = myTeamID;
                            dtD *= -1;
                            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCaptureSoundEmptying(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        }
                        IncPAR(owner, dtD, PrimaryAbilityResourceType.MANA);
                    }
                }
            }
        }
    }
}