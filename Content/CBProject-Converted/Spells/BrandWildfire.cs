namespace Spells
{
    public class BrandWildfire : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
            SpellFXOverrideSkins = new[] { "FrostFireBrand", },
        };
        int[] effect0 = { 150, 250, 350, 400, 400 };
        int[] effect1 = { 0, 0, 0, 0, 0 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int brandSkinID;
            SpellBuffClear(owner, nameof(Buffs.BrandWildfire));
            AddBuff((ObjAIBase)target, owner, new Buffs.BrandWildfire(), 5, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            bool doOnce = false;
            float damageToDeal = effect0[level - 1];
            foreach (AttackableUnit unit in GetRandomUnitsInArea(attacker, target.Position3D, 600, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 10, default, false))
            {
                if (unit != target && !doOnce)
                {
                    Vector3 attackerPos;
                    bool isStealthed = GetStealthed(unit);
                    if (!isStealthed)
                    {
                        attackerPos = GetUnitPosition(target);
                        int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.BrandAblaze)) > 0)
                        {
                            SpellCast(attacker, unit, default, default, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, attackerPos);
                        }
                        else
                        {
                            SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, attackerPos);
                        }
                        doOnce = true;
                    }
                    else
                    {
                        bool canSee = CanSeeTarget(attacker, unit);
                        if (canSee)
                        {
                            attackerPos = GetUnitPosition(target);
                            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.BrandAblaze)) > 0)
                            {
                                SpellCast(attacker, unit, default, default, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, attackerPos);
                            }
                            else
                            {
                                SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, attackerPos);
                            }
                            doOnce = true;
                        }
                    }
                }
            }
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.BrandAblaze)) > 0)
            {
                brandSkinID = GetSkinID(attacker);
                if (brandSkinID == 3)
                {
                    SpellEffectCreate(out _, out _, "BrandConflagration_tar_frost.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "BrandConflagration_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, target, damageToDeal + effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
            }
            else
            {
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, target, damageToDeal + effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
                brandSkinID = GetSkinID(attacker);
                if (brandSkinID == 3)
                {
                    SpellEffectCreate(out _, out _, "BrandConflagration_tar_frost.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "BrandConflagration_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class BrandWildfire : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spell Flux",
            BuffTextureName = "Ryze_LightningFlux.dds",
        };
    }
}