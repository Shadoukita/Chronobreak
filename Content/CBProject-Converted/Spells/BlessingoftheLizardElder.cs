namespace Buffs
{
    public class BlessingoftheLizardElder : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "BlessingoftheLizardElder",
            BuffTextureName = "48thSlave_WaveOfLoathing.dds",
            NonDispellable = true,
        };
        EffectEmitter buffParticle;
        int[] effect0 = { 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44 };
        int[] effect1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        float[] effect2 = { -0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.05f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.1f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f, -0.15f };
        float[] effect4 = { -0.08f, -0.08f, -0.08f, -0.08f, -0.08f, -0.08f, -0.16f, -0.16f, -0.16f, -0.16f, -0.16f, -0.16f, -0.24f, -0.24f, -0.24f, -0.24f, -0.24f, -0.24f };
        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            float newDuration;
            int count = GetBuffCountFromAll(attacker, nameof(Buffs.APBonusDamageToTowers));
            if (attacker is Champion)
            {
                if (!IsDead(attacker))
                {
                    newDuration = 150;
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.2f;
                    }
                    AddBuff(attacker, attacker, new Buffs.BlessingoftheLizardElder(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
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
                        newDuration *= 1.2f;
                    }
                    AddBuff(caster, caster, new Buffs.BlessingoftheLizardElder(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            //TODO: Double Check DamageSource, original script was DAMAGESOURCE_SPELL
            if (owner is Champion && target is ObjAIBase && target is not BaseTurret && damageSource == DamageSource.DAMAGE_SOURCE_ATTACK)
            {
                float nextBuffVars_MoveSpeedMod;
                int level = GetLevel(owner);
                float nextBuffVars_TickDamage = effect0[level - 1];
                int nextBuffVars_attackSpeedMod = effect1[level - 1];
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
                AddBuff(attacker, target, new Buffs.Burning(nextBuffVars_TickDamage, nextBuffVars_attackSpeedMod), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 1, true, false, false);
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}