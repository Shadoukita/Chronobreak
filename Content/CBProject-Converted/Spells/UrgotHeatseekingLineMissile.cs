namespace Spells
{
    public class UrgotHeatseekingLineMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 10, 40, 70, 100, 130 };
        float[] effect1 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            TeamId teamID = GetTeamID_CS(attacker);
            float baseDamage = effect0[level - 1];
            float attackDamage = GetTotalAttackDamage(owner);
            float bonusAD = 0.85f * attackDamage;
            float totalDamage = baseDamage + bonusAD;
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                BreakSpellShields(target);
                ApplyDamage(owner, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotTerrorCapacitorActive2)) > 0)
                {
                    int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_MoveSpeedMod = effect1[level - 1];
                    AddBuff(attacker, target, new Buffs.UrgotSlow(), 100, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
                DestroyMissile(missileNetworkID);
                SpellEffectCreate(out _, out _, "BloodSlash.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                SpellEffectCreate(out _, out _, "UrgotHeatSeekingMissile_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                AddBuff(owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            else
            {
                if (target is Champion)
                {
                    BreakSpellShields(target);
                    ApplyDamage(owner, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotTerrorCapacitorActive2)) > 0)
                    {
                        int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        nextBuffVars_MoveSpeedMod = effect1[level - 1];
                        AddBuff(attacker, target, new Buffs.UrgotSlow(), 100, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    }
                    DestroyMissile(missileNetworkID);
                    SpellEffectCreate(out _, out _, "BloodSlash.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                    SpellEffectCreate(out _, out _, "UrgotHeatSeekingMissile_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    AddBuff(owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        BreakSpellShields(target);
                        ApplyDamage(owner, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotTerrorCapacitorActive2)) > 0)
                        {
                            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            nextBuffVars_MoveSpeedMod = effect1[level - 1];
                            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                        }
                        DestroyMissile(missileNetworkID);
                        SpellEffectCreate(out _, out _, "BloodSlash.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                        SpellEffectCreate(out _, out _, "UrgotHeatSeekingMissile_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                        AddBuff(owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class UrgotHeatseekingLineMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", },
            BuffName = "Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
        };
    }
}