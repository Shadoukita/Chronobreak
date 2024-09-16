namespace Spells
{
    public class MaokaiTrunkLineMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
        float[] effect1 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.MaokaiTrunkLine)) == 0)
            {
                int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float baseDamage = effect0[level - 1];
                float nextBuffVars_MoveSpeedMod = effect1[level - 1];
                TeamId casterID = GetTeamID_CS(attacker);
                bool isStealthed = GetStealthed(target);
                if (!isStealthed)
                {
                    AddBuff(owner, target, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(target);
                    SpellEffectCreate(out _, out _, "maoki_trunkSmash_unit_tar_02.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                    SpellEffectCreate(out _, out _, "maoki_trunkSmash_unit_tar.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                    ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                    AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                }
                else
                {
                    if (target is Champion)
                    {
                        AddBuff(owner, target, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(target);
                        SpellEffectCreate(out _, out _, "maoki_trunkSmash_unit_tar_02.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                        SpellEffectCreate(out _, out _, "maoki_trunkSmash_unit_tar.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                        ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                        AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                    }
                    else
                    {
                        bool canSee = CanSeeTarget(owner, target);
                        if (canSee)
                        {
                            AddBuff(owner, target, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(target);
                            SpellEffectCreate(out _, out _, "maoki_trunkSmash_unit_tar_02.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                            SpellEffectCreate(out _, out _, "maoki_trunkSmash_unit_tar.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class MaokaiTrunkLineMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiTrunkLineMissile",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
        };
    }
}