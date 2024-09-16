namespace Spells
{
    public class ShyvanaImmolateDragon : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.3f, 0.35f, 0.4f, 0.45f, 0.5f };
        int[] effect1 = { 25, 40, 55, 70, 85 };
        public override void SelfExecute()
        {
            float nextBuffVars_MovementSpeed = effect0[level - 1];
            int nextBuffVars_DamagePerTick = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.ShyvanaImmolateDragon(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ShyvanaImmolateDragon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", },
            AutoBuffActivateEffect = new[] { "shyvana_scorchedEarth_dragon_01.troy", "shyvana_scorchedEarth_speed.troy", },
            BuffName = "ShyvanaScorchedEarthDragon",
            BuffTextureName = "ShyvanaScorchedEarth.dds",
        };
        float damagePerTick;
        float movementSpeed;
        Vector3 lastPosition;
        float lastTimeExecuted;
        float[] effect0 = { 0.3f, 0.35f, 0.4f, 0.45f, 0.5f };
        int[] effect1 = { 25, 40, 55, 70, 85 };
        public ShyvanaImmolateDragon(float damagePerTick = default, float movementSpeed = default)
        {
            this.damagePerTick = damagePerTick;
            this.movementSpeed = movementSpeed;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.movementSpeed);
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 curPos = GetPointByUnitFacingOffset(owner, 25, 180);
            float nextBuffVars_DamagePerTick = damagePerTick;
            Minion other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)owner);
            AddBuff((ObjAIBase)owner, other3, new Buffs.ShyvanaIDApplicator(nextBuffVars_DamagePerTick), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            lastPosition = curPos;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.ShyvanaIDDamage)) == 0)
                {
                    nextBuffVars_DamagePerTick = damagePerTick;
                    AddBuff(attacker, unit, new Buffs.ShyvanaIDDamage(nextBuffVars_DamagePerTick), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            charVars.HitCount = 0;
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, movementSpeed);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_DamagePerTick;
            Vector3 curPos = GetPointByUnitFacingOffset(owner, 25, 180);
            float distance = DistanceBetweenPoints(curPos, lastPosition);
            if (distance >= 150)
            {
                TeamId teamID = GetTeamID_CS(attacker);
                nextBuffVars_DamagePerTick = damagePerTick;
                Minion other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)attacker);
                AddBuff(attacker, other3, new Buffs.ShyvanaIDApplicator(nextBuffVars_DamagePerTick), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                lastPosition = curPos;
            }
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.ShyvanaIDDamage)) == 0)
                    {
                        nextBuffVars_DamagePerTick = damagePerTick;
                        AddBuff(attacker, unit, new Buffs.ShyvanaIDDamage(nextBuffVars_DamagePerTick), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                movementSpeed *= 0.85f;
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (charVars.HitCount < 4)
            {
                float remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.ShyvanaImmolateDragon));
                float newDuration = remainingDuration + 1;
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_MovementSpeed = effect0[level - 1];
                int nextBuffVars_DamagePerTick = effect1[level - 1];
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaImmolateDragon(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                charVars.HitCount++;
            }
        }
    }
}