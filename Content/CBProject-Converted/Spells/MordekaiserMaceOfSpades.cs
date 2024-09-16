namespace Spells
{
    public class MordekaiserMaceOfSpades : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 25, 32, 39, 46, 53 };
        int[] effect1 = { 8, 7, 6, 5, 4 };
        public override void SelfExecute()
        {
            float healthCost = effect0[level - 1];
            float temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
            float nextBuffVars_SpellCooldown = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.MordekaiserMaceOfSpades(nextBuffVars_SpellCooldown), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class MordekaiserMaceOfSpades : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "BUFFBONE_CSTM_WEAPON_1", "", },
            AutoBuffActivateEffect = new[] { "", "mordakaiser_maceOfSpades_activate.troy", "", "", },
            BuffName = "MordekaiserMaceOfSpades",
            BuffTextureName = "MordekaiserMaceOfSpades.dds",
        };
        float spellCooldown;
        bool willRemove;
        int[] effect0 = { 80, 110, 140, 170, 200 };
        public MordekaiserMaceOfSpades(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            willRemove = false;
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is not ObjAIBase)
            {
            }
            else if (target is BaseTurret)
            {
            }
            else
            {
                TeamId teamID = GetTeamID_CS(owner);
                willRemove = true;
                AddBuff((ObjAIBase)owner, owner, new Buffs.MordekaiserSyphonParticle(), 1, 1, 0.2f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float baseDamage = effect0[level - 1];
                baseDamage = GetBaseAttackDamage(owner);
                float totalDamage = GetTotalAttackDamage(owner);
                float damageDifference = 0;
                if (damageAmount > totalDamage)
                {
                    damageDifference = damageAmount - totalDamage;
                }
                float bonusDamage = totalDamage - baseDamage;
                baseDamage += bonusDamage;
                float abilityPower = GetFlatMagicDamageMod(owner);
                float bonusAPDamage = abilityPower * 0.4f;
                baseDamage += bonusAPDamage;
                float unitCount = 0;
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    unitCount++;
                }
                if (unitCount > 1)
                {
                    SpellEffectCreate(out _, out _, "mordakaiser_maceOfSpades_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    Vector3 targetPos = GetUnitPosition(target);
                    foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, target.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 4, default, true))
                    {
                        if (unit != target)
                        {
                            SpellCast((ObjAIBase)owner, unit, target.Position3D, target.Position3D, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, targetPos);
                        }
                    }
                }
                else
                {
                    baseDamage *= 1.65f;
                    SpellEffectCreate(out _, out _, "mordakaiser_maceOfSpades_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                if (damageDifference > 0)
                {
                    baseDamage += damageDifference;
                }
                damageAmount -= damageAmount;
                float nextBuffVars_BaseDamage = baseDamage;
                AddBuff((ObjAIBase)target, owner, new Buffs.MordekaiserMaceOfSpadesDmg(nextBuffVars_BaseDamage), 1, 1, 0.001f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}