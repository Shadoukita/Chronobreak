namespace Spells
{
    public class PowerFist : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 9, 8, 7, 6, 5 };
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.PowerFist(nextBuffVars_SpellCooldown), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class PowerFist : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "R_hand", "L_hand", },
            AutoBuffActivateEffect = new[] { "Powerfist_buf.troy", "Powerfist_buf.troy", },
            BuffName = "PowerFist",
            BuffTextureName = "Blitzcrank_PowerFist.dds",
            IsDeathRecapSource = true,
        };
        float spellCooldown;
        public PowerFist(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
            float spellCooldown = this.spellCooldown;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, false);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            float totalAttackDamage = GetTotalAttackDamage(owner);
            damageAmount += totalAttackDamage;
            if (target is ObjAIBase && target is not BaseTurret)
            {
                BreakSpellShields(target);
                AddBuff((ObjAIBase)owner, target, new Buffs.PowerFistSlow(), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.STUN, 0, true, false, false);
            }
            SpellBuffRemove(owner, nameof(Buffs.PowerFist), (ObjAIBase)owner, 0);
        }
    }
}