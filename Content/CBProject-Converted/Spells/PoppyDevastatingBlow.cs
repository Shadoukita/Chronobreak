namespace Spells
{
    public class PoppyDevastatingBlow : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 4,
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 8, 7, 6, 5, 4 };
        int[] effect1 = { 20, 40, 60, 80, 100 };
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            float nextBuffVars_BonusDamage = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.PoppyDevastatingBlow(nextBuffVars_SpellCooldown, nextBuffVars_BonusDamage), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class PoppyDevastatingBlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "hammer_b", "", },
            AutoBuffActivateEffect = new[] { "Poppy_DevastatingBlow_buf.troy", "", },
            BuffName = "PoppyDevastatingBlow",
            BuffTextureName = "PoppyDevastatingBlow.dds",
        };
        float spellCooldown;
        float bonusDamage;
        int[] effect0 = { 75, 150, 225, 300, 375 };
        public PoppyDevastatingBlow(float spellCooldown = default, float bonusDamage = default)
        {
            this.spellCooldown = spellCooldown;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown = this.spellCooldown;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, false);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float maxDamage = effect0[level - 1];
            if (target is ObjAIBase && target is not BaseTurret)
            {
                TeamId teamID = GetTeamID_CS(owner);
                float tarMaxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                tarMaxHealth *= 0.08f;
                float damageToDeal = tarMaxHealth + bonusDamage;
                damageToDeal = Math.Min(damageToDeal, maxDamage);
                SpellEffectCreate(out _, out _, "Poppy_DevastatingBlow_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                damageToDeal += damageAmount;
                BreakSpellShields(target);
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
                SpellBuffRemove(owner, nameof(Buffs.PoppyDevastatingBlow), (ObjAIBase)owner);
                damageAmount -= damageAmount;
            }
        }
    }
}