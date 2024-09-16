namespace Spells
{
    public class DeathsCaress : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathsCaress)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.DeathsCaress), owner);
            }
            else
            {
                SpellCast(owner, owner, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class DeathsCaress : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DeathsCaress_buf.troy", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "Death's Caress",
            BuffTextureName = "Sion_DeathsCaress.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float totalArmorAmount;
        float finalArmorAmount;
        float lastTimeExecuted;
        float ticktimer;
        float oldArmorAmount;
        int[] effect0 = { -70, -80, -90, -100, -110 };
        public DeathsCaress(float totalArmorAmount = default, float finalArmorAmount = default, float ticktimer = default)
        {
            this.totalArmorAmount = totalArmorAmount;
            this.finalArmorAmount = finalArmorAmount;
            this.ticktimer = ticktimer;
        }
        public override void OnActivate()
        {
            //RequireVar(this.totalArmorAmount);
            //RequireVar(this.finalArmorAmount);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DeathsCaress));
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 4);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float manaCostInc = effect0[level - 1];
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, manaCostInc, PrimaryAbilityResourceType.MANA);
            IncreaseShield(owner, totalArmorAmount, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if (totalArmorAmount > 0)
            {
                RemoveShield(owner, totalArmorAmount, true, true);
                SpellEffectCreate(out _, out _, "DeathsCaress_nova.prt", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 525, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, finalArmorAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
            }
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.DeathsCaressFull));
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = 8 * multiplier;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            SetBuffToolTipVar(1, totalArmorAmount);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                ticktimer--;
                if (ticktimer < 4)
                {
                    Say(owner, " ", ticktimer);
                }
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = totalArmorAmount;
            if (totalArmorAmount >= damageAmount)
            {
                totalArmorAmount -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= totalArmorAmount;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= totalArmorAmount;
                totalArmorAmount = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}