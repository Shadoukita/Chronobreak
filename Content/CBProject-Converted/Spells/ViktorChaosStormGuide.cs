namespace Spells
{
    public class ViktorChaosStormGuide : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        public override void SelfExecute()
        {
            float hMCSStartTime = GetBuffStartTime(owner, nameof(Buffs.ViktorChaosStormGuide));
            float hMCSCurrTime = GetTime();
            float remainingBuffTime = hMCSCurrTime * hMCSStartTime;
            bool hasTarget = false;
            Vector3 centerPos = GetUnitPosition(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, centerPos, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, nameof(Buffs.ViktorChaosStormGuide), true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.ViktorChaosStormGuide)) > 0)
                {
                    if (unit is Champion)
                    {
                        SpellBuffRemove(unit, nameof(Buffs.ViktorChaosStormGuide), owner, 0);
                    }
                    else
                    {
                        SetInvulnerable(unit, false);
                        ApplyDamage((ObjAIBase)unit, unit, 25000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, (ObjAIBase)unit);
                    }
                }
            }
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                AddBuff(attacker, unit, new Buffs.ViktorChaosStormGuide(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                hasTarget = true;
            }
            if (!hasTarget)
            {
                TeamId teamID = GetTeamID_CS(owner);
                Minion other2 = SpawnMinion("GuideMarker", "TestCube", default, targetPos, teamID, false, true, false, true, false, true, 0, false, false);
                AddBuff(attacker, other2, new Buffs.ViktorExpirationTimer(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                AddBuff(attacker, other2, new Buffs.ViktorChaosStormGuide(), 1, 1, 7 + remainingBuffTime, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class ViktorChaosStormGuide : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "ViktorStormGuide",
            BuffTextureName = "ViktorChaosStorm.dds",
            IsPetDurationBuff = true,
        };
        EffectEmitter particle1;
        EffectEmitter particle2;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            SpellEffectCreate(out particle1, out _, "Viktor_ChaosStorm_indicator.troy", default, caster.Team, 0, 0, caster.Team, default, caster, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle2, out _, "Viktor_ChaosStorm_indicator_02.troy", default, caster.Team, 0, 0, caster.Team, default, caster, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                Vector3 ownerPos = GetUnitPosition(owner);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, ownerPos, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectUntargetable, nameof(Buffs.ViktorChaosStormAOE), true))
                {
                    float distance = DistanceBetweenObjects(unit, owner); // UNUSED
                    if (caster.Team == unit.Team)
                    {
                        if (owner is Champion)
                        {
                            IssueOrder(unit, OrderType.MoveTo, default, owner);
                        }
                        else
                        {
                            IssueOrder(unit, OrderType.MoveTo, default, owner);
                        }
                    }
                }
            }
        }
    }
}