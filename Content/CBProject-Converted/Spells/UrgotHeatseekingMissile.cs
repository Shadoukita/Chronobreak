namespace Spells
{
    public class UrgotHeatseekingMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(owner, targetPos);
            Vector3 ownerPos = GetUnitPosition(owner);
            int homed = 0;
            float distance = DistanceBetweenPoints(targetPos, ownerPos);
            TeamId teamID = GetTeamID_CS(owner);
            if (distance <= 3000)
            {
                float distanceObjs;
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, 999, nameof(Buffs.UrgotCorrosiveDebuff), true))
                {
                    if (homed == 0)
                    {
                        distanceObjs = DistanceBetweenObjects(owner, unit);
                        if (distanceObjs <= 1200)
                        {
                            SpellEffectCreate(out _, out _, "UrgotHeatseekingIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, default, default, targetPos, default, default, targetPos, true);
                            homed = 1;
                            SpellCast(owner, unit, owner.Position3D, owner.Position3D, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
                            SpellEffectCreate(out _, out _, "UrgotTargetIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                        }
                    }
                }
                if (homed == 0)
                {
                    foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 999, nameof(Buffs.UrgotCorrosiveDebuff), true))
                    {
                        if (homed == 0)
                        {
                            distanceObjs = DistanceBetweenObjects(owner, unit);
                            if (distanceObjs <= 1200)
                            {
                                SpellEffectCreate(out _, out _, "UrgotHeatseekingIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, default, default, targetPos, default, default, targetPos, true);
                                homed = 1;
                                SpellCast(owner, unit, owner.Position3D, owner.Position3D, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
                                SpellEffectCreate(out _, out _, "UrgotTargetIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                            }
                        }
                    }
                }
            }
            if (homed == 0)
            {
                SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false, owner.Position3D);
            }
        }
    }
}
namespace Buffs
{
    public class UrgotHeatseekingMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", },
            BuffName = "Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
        };
    }
}