namespace Spells
{
    public class OdinCaptureChannelBomb : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 4.5f,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        EffectEmitter particleID;
        EffectEmitter particleID2;
        int chargeTimePassed;
        public override void ChannelingStart()
        {
            SpellEffectCreate(out particleID, out _, "OdinCaptureBeam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out particleID2, out _, "OdinCaptureBeam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "spine", default, false, false, false, false, false);
            int count = GetBuffCountFromAll(target, nameof(Buffs.OdinBombSuppression)); // UNUSED
            AddBuff(owner, owner, new Buffs.OdinCaptureChannelBomb(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, true);
            TeamId teamOfOwner = GetTeamID_CS(owner); // UNUSED
            chargeTimePassed = 0;
            AddBuff(attacker, attacker, new Buffs.OdinChannelVision(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        /*
        //TODO: Uncomment and fix
        public override void ChannelingUpdateStats()
        {
            if(this.chargeTimePassed == 0)
            {
                float accumTime; // UNITIALIZED
                if(accumTime > 1.5f)
                {
                    this.chargeTimePassed = 1;
                    SpellEffectRemove(this.particleID);
                    SpellEffectRemove(this.particleID2);
                    SpellEffectCreate(out this.particleID, out _, "OdinCaptureBeamEngaged.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "spine", default, false, false, false, false, false);
                    SpellEffectCreate(out this.particleID2, out _, "OdinCaptureBeamEngaged.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_CHANNEL_LOC", default, target, "spine", default, false, false, false, false, false);
                    UnlockAnimation(owner, true);
                    PlayAnimation("Channel", 0, owner, true, true, false);
                }
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCaptureChannelBomb)) == 0)
            {
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Move);
            }
            if(this.chargeTimePassed == 1 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCaptureChannelBomb)) > 0)
            {
                int count = GetBuffCountFromAll(target, nameof(Buffs.OdinBombSuppression));
                if(count == 0)
                {
                    TeamId teamOfOwner = GetTeamID(owner);
                    if(teamOfOwner == TeamId.TEAM_BLUE)
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinBombSuppressionOrder(), 10, 1, 30, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinBombSuppressionChaos(), 10, 1, 30, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    }
                    AddBuff((ObjAIBase)owner, target, new Buffs.OdinBombSuppression(), 1, 1, 10, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        */
        public override void ChannelingSuccessStop()
        {
            int cooldownToSet; // UNUSED
            SpellEffectRemove(particleID);
            SpellBuffRemove(target, nameof(Buffs.OdinGuardianSuppressionBomb), attacker, 0);
            SpellEffectRemove(particleID2);
            SpellBuffRemove(attacker, nameof(Buffs.OdinChannelVision), attacker, 0);
            if (IsDead(target))
            {
                cooldownToSet = 0;
                SetUseSlotSpellCooldownTime(0, owner, false);
            }
            else
            {
                cooldownToSet = 4;
                SetUseSlotSpellCooldownTime(4, owner, false);
            }
        }
        public override void ChannelingCancelStop()
        {
            int cooldownToSet; // UNUSED
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinBombSuccessParticle)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinCaptureChannelBomb), owner, 0);
                SpellEffectRemove(particleID);
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppressionChaos), 1);
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppressionOrder), 1);
                SpellEffectRemove(particleID2);
                SpellBuffRemove(attacker, nameof(Buffs.OdinChannelVision), attacker, 0);
                if (IsDead(target))
                {
                    cooldownToSet = 0;
                    SetUseSlotSpellCooldownTime(0, owner, false);
                }
                else
                {
                    cooldownToSet = 4;
                    SetUseSlotSpellCooldownTime(4, owner, false);
                }
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppression), 1);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinCaptureChannelBomb), owner, 0);
                SpellEffectCreate(out _, out _, "OdinCaptureCancel.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, "spine", default, false, false, false, false, false);
                SpellEffectRemove(particleID);
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppressionChaos), 1);
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppressionOrder), 1);
                SpellEffectRemove(particleID2);
                SpellBuffRemove(attacker, nameof(Buffs.OdinChannelVision), attacker, 0);
                if (IsDead(target))
                {
                    cooldownToSet = 0;
                    SetUseSlotSpellCooldownTime(0, owner, false);
                }
                else
                {
                    cooldownToSet = 4;
                    SetUseSlotSpellCooldownTime(4, owner, false);
                }
                SpellBuffRemoveStacks(target, owner, nameof(Buffs.OdinBombSuppression), 1);
            }
        }
    }
}
namespace Buffs
{
    public class OdinCaptureChannelBomb : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
        float channelStartTime;
        public override void OnActivate()
        {
            channelStartTime = GetBuffStartTime(owner, nameof(Buffs.OdinCaptureChannelBomb));
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (attacker is Champion && damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC)
            {
                string buffName = GetDamagingBuffName();
                float damageStartTime = GetBuffStartTime(owner, buffName);
                bool cancelChannel = false;
                if (damageStartTime == 0)
                {
                    cancelChannel = true;
                }
                if (damageStartTime >= channelStartTime)
                {
                    cancelChannel = true;
                }
                if (cancelChannel)
                {
                    SpellEffectCreate(out _, out _, "Ezreal_essenceflux_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
                    IssueOrder(owner, OrderType.OrderNone, default, owner);
                    SpellBuffRemove(attacker, nameof(Buffs.OdinChannelVision), attacker, 0);
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}