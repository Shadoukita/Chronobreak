namespace Spells
{
    public class GarenE: GarenBladestorm {}
    public class GarenBladestorm : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 25, 45, 65, 85, 105 };
        int[] effect1 = { 13, 12, 11, 10, 9 };
        float[] effect2 = { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
        public override void SelfExecute()
        {
            float nextBuffVars_baseDamage = effect0[level - 1];
            float nextBuffVars_SpellCooldown = effect1[level - 1];
            SetSpell(owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GarenBladestormLeave));
            SpellBuffRemoveType(owner, BuffType.SLOW);
            AddBuff(attacker, owner, new Buffs.GarenBladestorm(nextBuffVars_SpellCooldown, nextBuffVars_baseDamage), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            float nextBuffVars_MoveSpeedMod = effect2[level - 1]; // UNUSED
            SetSlotSpellCooldownTimeVer2(1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
        }
    }
}
namespace Buffs
{
    public class GarenBladestorm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "GarenBladestorm",
            BuffTextureName = "Garen_KeepingthePeace.dds",
        };
        EffectEmitter particle2;
        EffectEmitter particleID;
        float spellCooldown;
        float baseDamage;
        float lastTimeExecuted;
        public GarenBladestorm(float spellCooldown = default, float baseDamage = default)
        {
            this.spellCooldown = spellCooldown;
            this.baseDamage = baseDamage;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team && type == BuffType.SLOW)
            {
                float hardnessPercent = GetPercentHardnessMod(owner);
                if (hardnessPercent <= 0.5f)
                {
                    duration *= 0.5f;
                    float reversalDivisor = 1 - hardnessPercent;
                    duration /= reversalDivisor;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            OverrideAnimation("Run", "Spell3", owner);
            SpellEffectCreate(out particle2, out _, "garen_bladeStorm_cas_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out particleID, out _, "garen_weapon_glow_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_1", default, owner, "BUFFBONE_WEAPON_3", default, false, false, false, false, false);
            SetGhosted(owner, true);
            SetCanAttack(owner, false);
            //RequireVar(this.spellCooldown);
            //RequireVar(this.baseDamage);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GarenBladestorm));
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            StopCurrentOverrideAnimation("Spell3", owner, false);
            ClearOverrideAnimation("Run", owner);
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(particle2);
            SpellEffectRemove(particleID);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                float critChance = GetFlatCritChanceMod(owner);
                float critDamage = GetFlatCritDamageMod(owner);
                critDamage += 2;
                PlayAnimation("Spell3", 0, owner, true, false, true);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
                float totalDamage = GetTotalAttackDamage(owner);
                float baseDamage = GetBaseAttackDamage(owner);
                float bonusDamage = totalDamage - baseDamage;
                float ratioDamage = 0.7f * bonusDamage;
                float preBonusCrit = ratioDamage * critDamage;
                float damageToDealHero = ratioDamage + this.baseDamage;
                float critHero = preBonusCrit + this.baseDamage;
                float critMinion = critHero / 2;
                float damageToDeal = damageToDealHero / 2;
                TeamId teamID = GetTeamID_CS(owner);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (unit is Champion)
                    {
                        if (RandomChance() < critChance)
                        {
                            ApplyDamage(attacker, unit, critHero, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            SpellEffectCreate(out _, out _, "garen_bladestormCrit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                        else
                        {
                            ApplyDamage(attacker, unit, damageToDealHero, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            SpellEffectCreate(out _, out _, "garen_keeper0fPeace_tar_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                    }
                    else
                    {
                        bool canSee = CanSeeTarget(owner, unit);
                        if (canSee)
                        {
                            if (RandomChance() < critChance)
                            {
                                ApplyDamage(attacker, unit, critMinion, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                SpellEffectCreate(out _, out _, "garen_bladestormCrit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                            }
                            else
                            {
                                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                SpellEffectCreate(out _, out _, "garen_keeper0fPeace_tar_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true, false, false, false, false);
                            }
                        }
                        else
                        {
                            bool isStealthed = GetStealthed(unit);
                            if (!isStealthed)
                            {
                                if (RandomChance() < critChance)
                                {
                                    ApplyDamage(attacker, unit, critMinion, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                    SpellEffectCreate(out _, out _, "garen_bladestormCrit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                }
                                else
                                {
                                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                    SpellEffectCreate(out _, out _, "garen_keeper0fPeace_tar_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, true, false, false, false, false);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}