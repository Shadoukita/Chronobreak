namespace Spells
{
    public class JarvanIVDragonStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastTime = 0.4f,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
        float[] effect1 = { -0.1f, -0.14f, -0.18f, -0.22f, -0.26f };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(owner, targetPos);
            Vector3 damagePoint = GetPointByUnitFacingOffset(owner, 425, 0);
            TeamId teamID = GetTeamID_CS(attacker);
            float physPreMod = GetFlatPhysicalDamageMod(owner);
            float bonusDamage = effect0[level - 1];
            physPreMod *= 1.2f;
            float dtD = bonusDamage + physPreMod;
            float nextBuffVars_ArmorDebuff = effect1[level - 1];
            foreach (AttackableUnit unit in GetUnitsInRectangle(owner, damagePoint, 68, 360, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                bool isStealthed = GetStealthed(unit);
                if (!isStealthed)
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, dtD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                    AddBuff(attacker, unit, new Buffs.JarvanIVDragonStrikeDebuff(nextBuffVars_ArmorDebuff), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SHRED, 0, true, false, false);
                }
                else
                {
                    if (unit is Champion)
                    {
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, dtD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                        AddBuff(attacker, unit, new Buffs.JarvanIVDragonStrikeDebuff(nextBuffVars_ArmorDebuff), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SHRED, 0, true, false, false);
                    }
                    else
                    {
                        bool canSee = CanSeeTarget(owner, unit);
                        if (canSee)
                        {
                            BreakSpellShields(unit);
                            ApplyDamage(attacker, unit, dtD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                            AddBuff(attacker, unit, new Buffs.JarvanIVDragonStrikeDebuff(nextBuffVars_ArmorDebuff), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SHRED, 0, true, false, false);
                        }
                    }
                }
            }
            foreach (AttackableUnit unit in GetUnitsInRectangle(owner, damagePoint, 150, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf, default, true))
            {
                if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.JarvanIVDemacianStandard)) > 0)
                {
                    AddBuff((ObjAIBase)unit, owner, new Buffs.JarvanIVDragonStrikePH(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                    AddBuff((ObjAIBase)unit, unit, new Buffs.JarvanIVDragonStrikeSound(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                    SpellEffectCreate(out _, out _, "caitlyn_peaceMaker_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "spine", default, unit, default, default, true, false, false, false, false);
                }
            }
            AddBuff(owner, owner, new Buffs.JarvanIVDragonStrike(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JarvanIVDragonStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "JarvanIV_DragonStrike.dds",
        };
    }
}