namespace Spells
{
    public class EmpowerTwo : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 9, 8, 7, 6, 5 };
        int[] effect1 = { 60, 95, 130, 165, 200 };
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            float nextBuffVars_BonusDamage = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.EmpowerTwo(nextBuffVars_SpellCooldown, nextBuffVars_BonusDamage), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class EmpowerTwo : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "EmpowerTwo",
            BuffTextureName = "Armsmaster_Empower.dds",
        };
        EffectEmitter particle;
        float spellCooldown;
        float bonusDamage;
        public EmpowerTwo(float spellCooldown = default, float bonusDamage = default)
        {
            this.spellCooldown = spellCooldown;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out _, "armsmaster_empower_self_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, "weapon", default, false, false, false, false, false);
            SpellEffectCreate(out particle, out _, "armsmaster_empower_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown = this.spellCooldown;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SpellEffectRemove(particle);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, false);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                TeamId teamID = GetTeamID_CS(owner);
                SpellEffectRemove(particle);
                float attackDamage = GetFlatPhysicalDamageMod(owner);
                float physicalBonus = attackDamage * 0.4f;
                float aOEDmg = physicalBonus + bonusDamage;
                SpellEffectCreate(out particle, out _, "EmpowerTwoHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, aOEDmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                SpellEffectCreate(out particle, out _, "EmpowerTwoHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.EmpowerTwo), (ObjAIBase)owner, 0);
                SetDodgePiercing(owner, false);
            }
        }
    }
}