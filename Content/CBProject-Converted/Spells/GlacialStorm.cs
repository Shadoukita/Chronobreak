namespace Spells
{
    public class GlacialStorm : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 40, 60, 80 };
        int[] effect1 = { 20, 25, 30 };
        public override float AdjustCooldown()
        {
            float returnValue = float.NaN;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.GlacialStorm)) == 0)
            {
                returnValue = 0;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.GlacialStorm)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.GlacialStorm), owner, 0);
            }
            else
            {
                Vector3 targetPos = GetSpellTargetPos(spell);
                float nextBuffVars_DamagePerLevel = effect0[level - 1];
                Vector3 nextBuffVars_TargetPos = targetPos;
                float nextBuffVars_ManaCost = effect1[level - 1];
                AddBuff(owner, owner, new Buffs.GlacialStorm(nextBuffVars_DamagePerLevel, nextBuffVars_ManaCost, nextBuffVars_TargetPos), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class GlacialStorm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GlacialStorm",
            BuffTextureName = "Cryophoenix_GlacialStorm.dds",
            SpellToggleSlot = 4,
        };
        float damagePerLevel;
        float manaCost;
        Vector3 targetPos;
        EffectEmitter particle;
        EffectEmitter particle2;
        float damageManaTimer;
        float slowTimer;
        int[] effect0 = { 6, 6, 6 };
        public GlacialStorm(float damagePerLevel = default, float manaCost = default, Vector3 targetPos = default)
        {
            this.damagePerLevel = damagePerLevel;
            this.manaCost = manaCost;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 1);
            //RequireVar(this.damagePerLevel);
            //RequireVar(this.manaCost);
            //RequireVar(this.targetPos);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
            Vector3 targetPos = this.targetPos;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out particle2, "cryo_storm_green_team.troy", "cryo_storm_red_team.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, damagePerLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.125f, 1, false, false, attacker);
                float nextBuffVars_AttackSpeedMod = -0.2f;
                float nextBuffVars_MovementSpeedMod = -0.2f;
                AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat = GetPercentCooldownMod(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseCooldown = effect0[level - 1];
            float multiplier = 1 + cooldownStat;
            float newCooldown = baseCooldown * multiplier;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Area, owner);
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos;
            float nextBuffVars_AttackSpeedMod;
            float nextBuffVars_MovementSpeedMod;
            if (ExecutePeriodically(0.5f, ref damageManaTimer, false))
            {
                float curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                targetPos = this.targetPos;
                if (manaCost > curMana)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    float negMana = manaCost * -1;
                    IncPAR(owner, negMana, PrimaryAbilityResourceType.MANA);
                }
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, damagePerLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.125f, 1, false, false, attacker);
                    nextBuffVars_AttackSpeedMod = -0.2f;
                    nextBuffVars_MovementSpeedMod = -0.2f;
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            if (ExecutePeriodically(0.25f, ref slowTimer, false))
            {
                bool canCast = GetCanCast(owner);
                if (!canCast)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                targetPos = this.targetPos;
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
                Vector3 ownerPos = GetUnitPosition(owner);
                float distance = DistanceBetweenPoints(ownerPos, targetPos);
                if (distance >= 1200)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    nextBuffVars_AttackSpeedMod = -0.2f;
                    nextBuffVars_MovementSpeedMod = -0.2f;
                    AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}