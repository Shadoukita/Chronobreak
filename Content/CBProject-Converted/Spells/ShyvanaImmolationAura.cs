namespace Spells
{
    public class ShyvanaImmolationAura : SpellScript
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
            float nextBuffVars_DamagePerTick = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.ShyvanaImmolationAura(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ShyvanaImmolationAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", },
            AutoBuffActivateEffect = new[] { "shyvana_scorchedEarth_01.troy", "shyvana_scorchedEarth_speed.troy", },
            BuffName = "ShyvanaScorchedEarth",
            BuffTextureName = "ShyvanaScorchedEarth.dds",
        };
        float damagePerTick;
        float movementSpeed;
        float lastTimeExecuted;
        float[] effect0 = { 0.3f, 0.35f, 0.4f, 0.45f, 0.5f };
        int[] effect1 = { 25, 40, 55, 70, 85 };
        public ShyvanaImmolationAura(float damagePerTick = default, float movementSpeed = default)
        {
            this.damagePerTick = damagePerTick;
            this.movementSpeed = movementSpeed;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.movementSpeed);
            float bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD *= 0.2f;
            damagePerTick += bonusAD;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                TeamId teamID = GetTeamID_CS(attacker);
                SpellEffectCreate(out _, out _, "shyvana_scorchedEarth_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
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
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    TeamId teamID = GetTeamID_CS(attacker);
                    SpellEffectCreate(out _, out _, "shyvana_scorchedEarth_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                movementSpeed *= 0.85f;
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (charVars.HitCount < 5)
            {
                float remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.ShyvanaImmolationAura));
                float newDuration = remainingDuration + 1;
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_MovementSpeed = effect0[level - 1];
                float nextBuffVars_DamagePerTick = effect1[level - 1];
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaImmolationAura(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                charVars.HitCount++;
            }
        }
    }
}