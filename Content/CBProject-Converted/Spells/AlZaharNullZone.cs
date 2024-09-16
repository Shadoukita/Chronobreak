namespace Spells
{
    public class AlZaharNullZone : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.04f, 0.05f, 0.06f, 0.07f, 0.08f };
        int[] effect1 = { 20, 30, 40, 50, 60 };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
            {
                AddBuff(attacker, attacker, new Buffs.AlZaharVoidlingCount(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            float healthPercent = effect0[level - 1];
            int nextBuffVars_HealthFlat = effect1[level - 1]; // UNUSED
            float abilityPowerRatio = GetFlatMagicDamageMod(owner);
            float abilityPower = abilityPowerRatio * 0.0001f;
            float healthPercentPerTick = healthPercent + abilityPower;
            float nextBuffVars_HealthPercentPerTick = healthPercentPerTick;
            AddBuff(attacker, owner, new Buffs.AlZaharNullZone(nextBuffVars_HealthPercentPerTick, nextBuffVars_TargetPos), 5, 1, 5, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class AlZaharNullZone : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "AlZaharNullZone",
        };
        float healthPercentPerTick;
        Vector3 targetPos;
        EffectEmitter particle1;
        EffectEmitter particle2;
        float lastTimeExecuted;
        public AlZaharNullZone(float healthPercentPerTick = default, Vector3 targetPos = default)
        {
            this.healthPercentPerTick = healthPercentPerTick;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.healthFlat);
            //RequireVar(this.healthPercentPerTick);
            //RequireVar(this.targetPos);
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            Vector3 targetPos = this.targetPos;
            SpellEffectCreate(out _, out _, "AlzaharNullZoneFlash.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, owner, default, default, true, default, default, false, false);
            SpellEffectCreate(out particle1, out particle2, "AlzaharVoidPortal_flat_green.troy", "AlzaharVoidPortal_flat_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle1);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float health;
                float damagePerTick;
                TeamId teamOfOwner = GetTeamID_CS(attacker); // UNUSED
                Vector3 targetPos = this.targetPos;
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 280, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    health = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    damagePerTick = health * healthPercentPerTick;
                    ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 280, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, default, true))
                {
                    health = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    damagePerTick = health * healthPercentPerTick;
                    damagePerTick = Math.Min(120, damagePerTick);
                    ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
            }
        }
    }
}