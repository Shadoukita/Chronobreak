namespace Buffs
{
    public class OdinShrineBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "OdinShrineBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = true,
        };
        EffectEmitter buffParticle;
        float vampVar;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            vampVar = 0.3f;
            IncPercentLifeStealMod(owner, vampVar);
            IncPercentSpellVampMod(owner, vampVar);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, vampVar);
            IncPercentSpellVampMod(owner, vampVar);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float sSCD1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                float sSCD2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                float newSSCD1 = sSCD1 - 1;
                float newSSCD2 = sSCD2 - 1;
                SetSlotSpellCooldownTimeVer2(newSSCD1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner, false);
                SetSlotSpellCooldownTimeVer2(newSSCD2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner, false);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                float tempTable1_ThirdDA;
                SpellEffectCreate(out _, out _, "TiamatMelee_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false);
                if (IsRanged((ObjAIBase)owner))
                {
                    tempTable1_ThirdDA = 0.4f * damageAmount;
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, default, nameof(Buffs.JudicatorRighteousFury)) > 0)
                    {
                        tempTable1_ThirdDA = 0.4f * damageAmount;
                    }
                    else
                    {
                        tempTable1_ThirdDA = 0.6f * damageAmount;
                    }
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 210, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (target != unit)
                    {
                        if (damageType == DamageType.DAMAGE_TYPE_MAGICAL)
                        {
                            ApplyDamage((ObjAIBase)owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                        }
                        else if (damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
                        {
                            ApplyDamage((ObjAIBase)owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                        }
                        else
                        {
                            ApplyDamage((ObjAIBase)owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                        }
                    }
                }
            }
        }
    }
}