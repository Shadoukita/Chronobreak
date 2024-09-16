namespace Spells
{
    public class RivenTriCleaveDamage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
    }
}
namespace Buffs
{
    public class RivenTriCleaveDamage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RivenTriCleave",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        int count;
        float bonusDamage;
        float totalDamage;
        EffectEmitter swordStreak; // UNUSED
        int[] effect0 = { 30, 55, 80, 105, 130 };
        public RivenTriCleaveDamage(int count = default)
        {
            this.count = count;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.count);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            CancelAutoAttack(owner, false);
            float attackDamage = GetTotalAttackDamage(owner);
            float baseAD = GetBaseAttackDamage(owner);
            attackDamage -= baseAD;
            attackDamage *= 0.7f;
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusDamage = effect0[level - 1];
            float totalDamage = attackDamage + bonusDamage;
            this.totalDamage = totalDamage;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellBuffClear(unit, nameof(Buffs.RivenTriCleaveDamageDebuff));
            }
            if (count != 2)
            {
                Vector3 focalPoint = GetPointByUnitFacingOffset(owner, 125, 0);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, focalPoint, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RivenTriCleaveDamageDebuff), false))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RivenTriCleaveDamageDebuff(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, this.totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, (ObjAIBase)owner);
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RivenTriCleaveDamageDebuff), false))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RivenTriCleaveDamageDebuff(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, this.totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, (ObjAIBase)owner);
                }
            }
            if (count == 0)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenFengShuiEngine)) > 0)
                {
                    SpellEffectCreate(out _, out swordStreak, "exile_Q_01_trail_02_ult.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out swordStreak, "exile_Q_03_trail.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_hand", default, owner, default, default, true, false, false, false, false);
                    SpellEffectCreate(out _, out swordStreak, "exile_Q_01_trail_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, true, false, false, false, false);
                }
            }
            if (count == 1)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenFengShuiEngine)) > 0)
                {
                    SpellEffectCreate(out _, out swordStreak, "exile_Q_02_trail_02_ult.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out swordStreak, "exile_Q_03_trail.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_hand", default, owner, default, default, true, false, false, false, false);
                    SpellEffectCreate(out _, out swordStreak, "exile_Q_02_trail_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, true, false, false, false, false);
                }
            }
            if (count == 2)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenFengShuiEngine)) > 0)
                {
                    SpellEffectCreate(out _, out swordStreak, "exile_Q_03_trail_02_ult.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out swordStreak, "exile_Q_03_trail.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_hand", default, owner, default, default, true, false, false, false, false);
                    SpellEffectCreate(out _, out swordStreak, "exile_Q_03_trail_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, true, false, false, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveUnlock(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RivenTriCleaveGhost(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateActions()
        {
            if (count != 2)
            {
                Vector3 focalPoint = GetPointByUnitFacingOffset(owner, 125, 0);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, focalPoint, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RivenTriCleaveDamageDebuff), false))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RivenTriCleaveDamageDebuff(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, (ObjAIBase)owner);
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 75, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RivenTriCleaveDamageDebuff), false))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RivenTriCleaveDamageDebuff(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, (ObjAIBase)owner);
                }
            }
        }
        public override void OnMoveEnd()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellBuffClear(owner, nameof(Buffs.RivenSword));
            SpellBuffClear(owner, nameof(Buffs.RivenTriCleaveDamage));
            SpellBuffClear(owner, nameof(Buffs.RivenTriCleaveDamage));
            CancelAutoAttack(owner, true);
        }
        public override void OnMoveSuccess()
        {
            float range;
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 focalPoint = GetPointByUnitFacingOffset(owner, 100, 0);
            if (count != 2)
            {
                range = 200;
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenFengShuiEngine)) > 0)
                {
                    range *= 1.25f;
                    if (count == 1)
                    {
                        SpellEffectCreate(out _, out _, "exile_Q_02_detonate_ult.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, focalPoint, default, default, focalPoint, true, false, false, false, true, owner);
                    }
                    else
                    {
                        SpellEffectCreate(out _, out _, "exile_Q_02_detonate_ult.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, focalPoint, default, default, focalPoint, true, false, false, false, true, owner);
                    }
                }
                else
                {
                    if (count == 1)
                    {
                        SpellEffectCreate(out _, out _, "exile_Q_02_detonate.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, focalPoint, default, default, focalPoint, true, false, false, false, true, owner);
                    }
                    else
                    {
                        SpellEffectCreate(out _, out _, "exile_Q_01_detonate.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, focalPoint, default, default, focalPoint, true, false, false, false, true, owner);
                    }
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, focalPoint, range, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RivenTriCleaveDamageDebuff), false))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RivenTriCleaveDamageDebuff(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, (ObjAIBase)owner);
                }
            }
            if (count == 2)
            {
                range = 265;
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenFengShuiEngine)) > 0)
                {
                    SpellEffectCreate(out _, out _, "exile_Q_03_detonate_ult.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, focalPoint, default, default, focalPoint, true, false, false, false, false);
                    range *= 1.25f;
                }
                else
                {
                    SpellEffectCreate(out _, out _, "exile_Q_03_detonate.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, focalPoint, default, default, focalPoint, true, false, false, false, false);
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, focalPoint, range, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RivenTriCleaveDamageDebuff), false))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RivenTriCleaveDamageDebuff2(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    float dist = DistanceBetweenObjects(unit, owner); // UNUSED
                    ApplyDamage((ObjAIBase)owner, unit, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, (ObjAIBase)owner);
                    BreakSpellShields(unit);
                    if (!IsDead(unit))
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.RivenKnockback(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                }
            }
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                IssueOrder(owner, OrderType.AttackTo, default, unit);
            }
        }
        public override void OnMoveFailure()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            Vector3 focalPoint = GetPointByUnitFacingOffset(owner, 100, 0);
            TeamId teamID = GetTeamID_CS(owner);
            if (count == 2)
            {
                float range = 265;
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenFengShuiEngine)) > 0)
                {
                    SpellEffectCreate(out _, out _, "exile_Q_03_detonate_ult.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, focalPoint, default, default, focalPoint, true, false, false, false, false);
                    range *= 1.25f;
                }
                else
                {
                    SpellEffectCreate(out _, out _, "exile_Q_03_detonate.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, focalPoint, default, default, focalPoint, true, false, false, false, false);
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, focalPoint, range, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RivenTriCleaveDamageDebuff), false))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RivenTriCleaveDamageDebuff2(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, (ObjAIBase)owner);
                    BreakSpellShields(unit);
                    if (!IsDead(unit))
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.RivenKnockback(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                }
            }
        }
    }
}