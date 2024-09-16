namespace Spells
{
    public class InfectedCleaverMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
            SpellFXOverrideSkins = new[] { "MundoMundo", },
        };
        int[] effect0 = { 80, 130, 180, 230, 280 };
        float[] effect1 = { -0.4f, -0.4f, -0.4f, -0.4f, -0.4f };
        float[] effect2 = { 0.15f, 0.18f, 0.21f, 0.23f, 0.25f };
        int[] effect3 = { 300, 400, 500, 600, 700 };
        int[] effect4 = { 25, 30, 35, 40, 45 };
        int[] effect10 = { 80, 130, 180, 230, 280 };
        float[] effect11 = { -0.4f, -0.4f, -0.4f, -0.4f, -0.4f };
        float[] effect12 = { 0.15f, 0.18f, 0.21f, 0.23f, 0.25f };
        int[] effect13 = { 300, 400, 500, 600, 700 };
        int[] effect14 = { 25, 30, 35, 40, 45 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            TeamId teamID = GetTeamID_CS(owner);
            int mundoID = GetSkinID(owner);
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                float minDamage = effect0[level - 1];
                float nextBuffVars_MoveMod = effect1[level - 1];
                float damageMod = effect2[level - 1];
                float maxDamage = effect3[level - 1];
                float health = GetHealth(target, PrimaryAbilityResourceType.MANA);
                float percentDamage = health * damageMod;
                float tempDamage = Math.Max(minDamage, percentDamage);
                float damageDealt = Math.Min(tempDamage, maxDamage);
                ApplyDamage(attacker, target, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
                AddBuff(attacker, target, new Buffs.InfectedCleaverMissile(nextBuffVars_MoveMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                DestroyMissile(missileNetworkID);
                level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float healthReturn = effect4[level - 1];
                IncHealth(owner, healthReturn, owner);
                if (mundoID == 4)
                {
                    SpellEffectCreate(out _, out _, "dr_mundo_as_mundo_infected_cleaver_tar", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "dr_mundo_infected_cleaver_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                }
            }
            else
            {
                if (target is Champion)
                {
                    float minDamage = effect0[level - 1];
                    float nextBuffVars_MoveMod = effect1[level - 1];
                    float damageMod = effect2[level - 1];
                    float maxDamage = effect3[level - 1];
                    float health = GetHealth(target, PrimaryAbilityResourceType.MANA);
                    float percentDamage = health * damageMod;
                    float tempDamage = Math.Max(minDamage, percentDamage);
                    float damageDealt = Math.Min(tempDamage, maxDamage);
                    ApplyDamage(attacker, target, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
                    AddBuff(attacker, target, new Buffs.InfectedCleaverMissile(nextBuffVars_MoveMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                    DestroyMissile(missileNetworkID);
                    if (mundoID == 4)
                    {
                        SpellEffectCreate(out _, out _, "dr_mundo_as_mundo_infected_cleaver_tar", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    }
                    else
                    {
                        SpellEffectCreate(out _, out _, "dr_mundo_infected_cleaver_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    }
                    level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float healthReturn = effect4[level - 1];
                    IncHealth(owner, healthReturn, owner);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        float minDamage = effect10[level];
                        float nextBuffVars_MoveMod = effect11[level];
                        float damageMod = effect12[level];
                        float maxDamage = effect13[level];
                        float health = GetHealth(target, PrimaryAbilityResourceType.MANA);
                        float percentDamage = health * damageMod;
                        float tempDamage = Math.Max(minDamage, percentDamage);
                        float damageDealt = Math.Min(tempDamage, maxDamage);
                        ApplyDamage(attacker, target, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, attacker);
                        AddBuff(attacker, target, new Buffs.InfectedCleaverMissile(nextBuffVars_MoveMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                        DestroyMissile(missileNetworkID);
                        if (mundoID == 4)
                        {
                            SpellEffectCreate(out _, out _, "dr_mundo_as_mundo_infected_cleaver_tar", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                        }
                        else
                        {
                            SpellEffectCreate(out _, out _, "dr_mundo_infected_cleaver_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                        }
                        level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        float healthReturn = effect14[level];
                        IncHealth(owner, healthReturn, owner);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class InfectedCleaverMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "InfectedCleaverDebuff",
            BuffTextureName = "DrMundo_InfectedCleaver.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float moveMod;
        EffectEmitter slow;
        public InfectedCleaverMissile(float moveMod = default)
        {
            this.moveMod = moveMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveMod);
            SpellEffectCreate(out slow, out _, "Global_Slow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(slow);
        }
        public override void OnUpdateStats()
        {
            float moveMod = this.moveMod;
            IncPercentMultiplicativeMovementSpeedMod(owner, moveMod);
        }
    }
}