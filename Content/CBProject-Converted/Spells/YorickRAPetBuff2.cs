namespace Buffs
{
    public class YorickRAPetBuff2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "YorickOmenRevenant",
            BuffTextureName = "YorickOmenOfDeath.dds",
            IsPetDurationBuff = true,
        };
        EffectEmitter particle5;
        EffectEmitter particle;
        EffectEmitter particle3;
        EffectEmitter particle4;
        float aDRatio;
        float[] effect0 = { 0.45f, 0.6f, 0.75f };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle5, out particle5, "yorick_ult_revive_tar.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out particle, out particle, "yorick_ult_02.troy", default, teamID, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out particle3, out particle4, "yorick_revive_skin_teamID_green.troy", "yorick_revive_skin_teamID_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
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
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            SetGhosted(owner, true);
            IncPermanentPercentHPRegenMod(owner, -1);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeblancPassive)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.LeblancPassive), (ObjAIBase)owner, 0);
            }
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetSlotSpellLevel(caster, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            aDRatio = effect0[level - 1];
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(owner);
            foreach (Champion unit in GetChampions(teamID, nameof(Buffs.YorickReviveAllySelf), true))
            {
                SpellBuffClear(unit, nameof(Buffs.YorickReviveAllySelf));
            }
            if (IsDead(owner))
            {
                SpellEffectCreate(out _, out _, "YorickRevenantDeathSound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            }
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)owner);
            SpellEffectRemove(particle);
            SpellEffectRemove(particle3);
            SpellEffectRemove(particle4);
            SpellEffectRemove(particle5);
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.YorickRASelf)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.YorickRASelf), attacker, 0);
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
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            damageAmount *= aDRatio;
            ObjAIBase caster = GetBuffCasterUnit();
            ApplyDamage(caster, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, true, true, caster);
            damageAmount = 0;
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if (health >= 0)
            {
                float effectiveHeal = health * 0;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}