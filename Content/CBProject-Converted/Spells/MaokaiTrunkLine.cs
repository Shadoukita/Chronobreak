namespace Spells
{
    public class MaokaiTrunkLine : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        EffectEmitter partname; // UNUSED
        int[] effect0 = { 70, 115, 160, 205, 250 };
        float[] effect1 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void SelfExecute()
        {
            int level = base.level;
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if (distance > 650)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 600, 0);
            }
            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect0[level - 1];
            float nextBuffVars_MoveSpeedMod = effect1[level - 1];
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out partname, out _, "maoki_trunkSmash_cas.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellBuffRemove(unit, nameof(Buffs.MaokaiTrunkLine), owner);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.MaokaiTrunkLine)) == 0)
                {
                    bool isStealthed = GetStealthed(unit);
                    if (!isStealthed)
                    {
                        AddBuff(owner, unit, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        SpellEffectCreate(out _, out _, "PowerballHit.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                        ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                        AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                        AddBuff(attacker, unit, new Buffs.MaokaiTrunkLineStun(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                    else
                    {
                        if (unit is Champion)
                        {
                            AddBuff(owner, unit, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(unit);
                            SpellEffectCreate(out _, out _, "PowerballHit.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                            ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                            AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                            AddBuff(attacker, unit, new Buffs.MaokaiTrunkLineStun(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                        }
                        else
                        {
                            bool canSee = CanSeeTarget(owner, unit);
                            if (canSee)
                            {
                                AddBuff(owner, unit, new Buffs.MaokaiTrunkLine(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                BreakSpellShields(unit);
                                SpellEffectCreate(out _, out _, "PowerballHit.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                                ApplyDamage(attacker, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                                AddBuff(attacker, unit, new Buffs.MaokaiTrunkLineStun(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class MaokaiTrunkLine : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "MaokaiTrunkSmash",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
    }
}