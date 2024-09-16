namespace Spells
{
    public class MissFortuneRicochetShot : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 25, 60, 95, 130, 165 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            bool isStealthed;
            if (hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if (hitResult == HitResult.HIT_Dodge)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if (hitResult == HitResult.HIT_Miss)
            {
                hitResult = HitResult.HIT_Normal;
            }
            TeamId teamID = GetTeamID_CS(attacker);
            float attackDamage = GetTotalAttackDamage(attacker);
            float attackBonus = 0.75f * attackDamage;
            float abilityDamage = effect0[level - 1];
            float damageToDeal = attackBonus + abilityDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            SpellEffectCreate(out _, out _, "missFortune_richochet_tar_first.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            Minion other1 = SpawnMinion("LocationFinder", "TestCube", "idle.lua", target.Position3D, teamID, true, true, true, true, true, true, 0, default, true);
            FaceDirection(other1, attacker.Position3D);
            Vector3 leftPos = GetPointByUnitFacingOffset(other1, 500, 90);
            Vector3 rightPos = GetPointByUnitFacingOffset(other1, 500, 270);
            Minion other2 = SpawnMinion("LocationFinder", "TestCube", "idle.lua", leftPos, teamID, true, true, true, true, true, true, 0, default, true);
            Minion other3 = SpawnMinion("LocationFinder", "TestCube", "idle.lua", rightPos, teamID, true, true, true, true, true, true, 0, default, true);
            FaceDirection(other2, attacker.Position3D);
            FaceDirection(other3, attacker.Position3D);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            Vector3 targetPos = GetUnitPosition(other1);
            int eatHydra = 0;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, other1.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (IsBehind(other1, unit))
                {
                    isStealthed = GetStealthed(unit);
                    if (!isStealthed)
                    {
                        AddBuff(attacker, unit, new Buffs.MissFortuneRShotHolder(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    }
                }
            }
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, other2.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.MissFortuneRShotHolder), true))
            {
                isStealthed = GetStealthed(unit);
                if (!isStealthed)
                {
                    SpellBuffRemove(unit, nameof(Buffs.MissFortuneRShotHolder), attacker);
                    AddBuff(attacker, unit, new Buffs.MissFortuneRicochetShot(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, other3.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.MissFortuneRShotHolder), true))
            {
                isStealthed = GetStealthed(unit);
                if (!isStealthed)
                {
                    SpellBuffRemove(unit, nameof(Buffs.MissFortuneRShotHolder), attacker);
                    AddBuff(attacker, unit, new Buffs.MissFortuneRicochetShot(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            foreach (AttackableUnit unit in GetRandomUnitsInArea(attacker, other1.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.MissFortuneRShotHolder), true))
            {
                SpellCast(attacker, unit, unit.Position3D, unit.Position3D, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, targetPos);
                eatHydra = 1;
            }
            if (eatHydra < 1)
            {
                foreach (AttackableUnit unit in GetRandomUnitsInArea(attacker, other1.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.MissFortuneRicochetShot), true))
                {
                    SpellCast(attacker, unit, unit.Position3D, unit.Position3D, 0, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, targetPos);
                }
            }
        }
    }
}
namespace Buffs
{
    public class MissFortuneRicochetShot : BuffScript
    {
    }
}