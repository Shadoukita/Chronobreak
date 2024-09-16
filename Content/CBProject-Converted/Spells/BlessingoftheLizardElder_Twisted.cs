namespace Buffs
{
    public class BlessingoftheLizardElder_Twisted : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "BlessingoftheLizardElder",
            BuffTextureName = "48thSlave_WaveOfLoathing.dds",
            NonDispellable = true,
        };
        Vector3 particlePosition;
        EffectEmitter buffParticle;
        EffectEmitter castParticle;
        Region bubble;
        int[] effect0 = { 15, 15, 20, 20, 25, 25, 30, 30, 35, 35, 40, 40, 45, 45, 50, 50, 55, 55 };
        int[] effect1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        float[] effect2 = { -0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f };
        float[] effect4 = { -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.2f, -0.2f, -0.2f, -0.2f, -0.2f, -0.2f, -0.3f, -0.3f, -0.3f, -0.3f, -0.3f, -0.3f };
        public BlessingoftheLizardElder_Twisted(Vector3 particlePosition = default)
        {
            this.particlePosition = particlePosition;
        }
        public override void OnActivate()
        {
            //RequireVar(this.particlePosition);
            SpellEffectCreate(out buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            Vector3 particlePosition = this.particlePosition;
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out castParticle, out _, "ClairvoyanceEyeLong.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, particlePosition, target, default, default, false, default, default, false);
            bubble = AddPosPerceptionBubble(teamID, 2200, particlePosition, 180, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
            SpellEffectRemove(castParticle);
            RemovePerceptionBubble(bubble);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            int count = GetBuffCountFromAll(attacker, nameof(Buffs.APBonusDamageToTowers));
            float newDuration = 90;
            Vector3 nextBuffVars_ParticlePosition = particlePosition;
            if (!IsDead(attacker))
            {
                if (attacker is Champion)
                {
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.15f;
                    }
                    else
                    {
                        if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.Monsterbuffs2)) > 0)
                        {
                            newDuration *= 1.3f;
                        }
                    }
                    AddBuff(attacker, attacker, new Buffs.BlessingoftheLizardElder_Twisted(nextBuffVars_ParticlePosition), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else if (count != 0)
            {
                ObjAIBase caster = GetPetOwner((Pet)attacker);
                if (caster is Champion && !IsDead(caster))
                {
                    newDuration = 150;
                    if (GetBuffCountFromCaster(caster, caster, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.15f;
                    }
                    else
                    {
                        if (GetBuffCountFromCaster(caster, caster, nameof(Buffs.Monsterbuffs2)) > 0)
                        {
                            newDuration *= 1.3f;
                        }
                    }
                    AddBuff(caster, caster, new Buffs.BlessingoftheLizardElder_Twisted(nextBuffVars_ParticlePosition), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && owner is Champion && target is ObjAIBase && target is not BaseTurret)
            {
                float nextBuffVars_MoveSpeedMod;
                int level = GetLevel(owner);
                float nextBuffVars_TickDamage = effect0[level - 1];
                float nextBuffVars_attackSpeedMod = effect1[level - 1];
                AddBuff(attacker, target, new Buffs.Burning(nextBuffVars_TickDamage, nextBuffVars_attackSpeedMod), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 1, true, false, false);
                if (IsRanged((ObjAIBase)owner))
                {
                    nextBuffVars_MoveSpeedMod = effect2[level - 1];
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, default, nameof(Buffs.JudicatorRighteousFury)) > 0)
                    {
                        nextBuffVars_MoveSpeedMod = effect2[level - 1];
                    }
                    else
                    {
                        nextBuffVars_MoveSpeedMod = effect4[level - 1];
                    }
                }
                float nextBuffVars_AttackSpeedMod = effect1[level - 1];
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}