namespace Spells
{
    public class JaxEmpowerTwo : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 9, 7.5f, 6, 4.5f, 3 };
        int[] effect1 = { 20, 50, 80, 110, 140 };
        public override void SelfExecute()
        {
            float nextBuffVars_SpellCooldown = effect0[level - 1];
            int nextBuffVars_BonusDamage = effect1[level - 1]; // UNUSED
            AddBuff(owner, owner, new Buffs.JaxEmpowerTwo(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class JaxEmpowerTwo : BuffScript
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
        int[] effect0 = { 40, 85, 130, 175, 220 };
        public JaxEmpowerTwo(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
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
            if (target is not BaseTurret)
            {
                TeamId teamID = GetTeamID_CS(owner);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellEffectRemove(particle);
                SpellEffectCreate(out particle, out _, "EmpowerTwoHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
                SpellEffectCreate(out particle, out _, "EmpowerTwoHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.JaxEmpowerTwo), (ObjAIBase)owner, 0);
                SetDodgePiercing(owner, false);
            }
        }
    }
}