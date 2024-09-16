namespace Buffs
{
    public class MissFortuneStrut : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MissFortuneStrut",
            BuffTextureName = "MissFortune_Strut.dds",
            PersistsThroughDeath = true,
        };
        float moveSpeedMod;
        bool willRemove;
        EffectEmitter running;
        float lastTimeExecuted;
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            moveSpeedMod = 0;
            willRemove = false;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MissFortuneWaves)) == 0)
            {
                moveSpeedMod = 25;
                OverrideAnimation("Run", "Run2", owner);
                willRemove = true;
                SpellEffectCreate(out running, out _, "missFortune_passive_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "root", default, owner, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
            if (willRemove)
            {
                SpellEffectRemove(running);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatMovementSpeedMod(owner, moveSpeedMod);
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MissFortuneWaves)) == 0)
                {
                    moveSpeedMod += 3.93f;
                    moveSpeedMod = Math.Min(moveSpeedMod, 70);
                    if (!willRemove)
                    {
                        OverrideAnimation("Run", "Run2", owner);
                        willRemove = true;
                        SpellEffectCreate(out running, out _, "missFortune_passive_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "root", default, owner, default, default, false);
                    }
                }
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MissFortuneStrutDebuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, false, false, false);
        }
    }
}