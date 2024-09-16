namespace Buffs
{
    public class MordekaiserCOTGPetBuff2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserCOTGPet",
            BuffTextureName = "Mordekaiser_COTG.dds",
            IsPetDurationBuff = true,
        };
        EffectEmitter particle;
        EffectEmitter particle2;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out _, "mordekeiser_cotg_skin.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle2, out _, "mordekaiser_cotg_ring.troy", default, teamID, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellBuffRemoveType(owner, BuffType.COMBAT_ENCHANCER);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.HEAL);
            SpellBuffRemoveType(owner, BuffType.HASTE);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.SHRED);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            SetGhosted(owner, true);
            float petDamage = GetTotalAttackDamage(owner);
            petDamage *= 0.2f;
            float petAP = GetFlatMagicDamageMod(owner);
            petAP *= 0.2f;
            float nextBuffVars_PetDamage = petDamage;
            float nextBuffVars_PetAP = petAP;
            AddBuff(attacker, attacker, new Buffs.MordekaiserCOTGSelf(nextBuffVars_PetDamage, nextBuffVars_PetAP), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, owner, new Buffs.MordekaiserCOTGPetBuff(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeblancPassive)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.LeblancPassive), (ObjAIBase)owner, 0);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)owner);
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MordekaiserCOTGSelf)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.MordekaiserCOTGSelf), attacker, 0);
            }
        }
        public override void OnUpdateStats()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            float casMovespeedMod = GetFlatMovementSpeedMod(caster);
            float ownMovespeedMod = GetFlatMovementSpeedMod(owner);
            float movespeedDiff = casMovespeedMod - ownMovespeedMod;
            IncFlatMovementSpeedMod(owner, movespeedDiff);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurprise)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KogMawIcathianSurprise), (ObjAIBase)owner, 0);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurpriseReady)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KogMawIcathianSurpriseReady), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                if (IsDead(attacker))
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            ObjAIBase caster;
            float nextBuffVars_DamageToDeal = 0;
            if (target is ObjAIBase)
            {
                caster = GetBuffCasterUnit();
                nextBuffVars_DamageToDeal = damageAmount;
                damageAmount -= damageAmount;
                AddBuff((ObjAIBase)target, caster, new Buffs.MordekaiserCOTGPetDmg(nextBuffVars_DamageToDeal), 1, 1, 0.001f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                caster = GetBuffCasterUnit();
                AddBuff(caster, caster, new Buffs.MordekaiserCOTGPetDmg(nextBuffVars_DamageToDeal), 1, 1, 0.001f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage(caster, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, true, true, caster);
                damageAmount = 0;
            }
        }
    }
}