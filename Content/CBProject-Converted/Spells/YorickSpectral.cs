namespace Spells
{
    public class YorickSpectral : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        int[] effect0 = { 9, 8, 7, 6, 5 };
        int[] effect1 = { 30, 60, 90, 120, 150 };
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            float nextBuffVars_BonusDamage = effect1[level - 1];
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickSpectralUnlock)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.YorickSpectralUnlock), owner, 0);
            }
            AddBuff(owner, owner, new Buffs.YorickSpectral(nextBuffVars_BonusDamage, nextBuffVars_SpellCooldown), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.YorickSpectralUnlock(), 1, 1, 11, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class YorickSpectral : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[] { "yorick_spectralGhoul_attack_buf_self.troy", "yorick_spectralGhoul_attack_buf_self.troy", },
            BuffName = "YorickSpectralPreHit",
            BuffTextureName = "YorickOmenOfWarPreHit.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusDamage;
        float spellCooldown;
        public YorickSpectral(float bonusDamage = default, float spellCooldown = default)
        {
            this.bonusDamage = bonusDamage;
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusDamage);
            //RequireVar(this.spellCooldown);
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
        public override void OnUpdateStats()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonSpectral)) > 0)
                {
                    SpellBuffClear(owner, nameof(Buffs.YorickSummonSpectral));
                }
                if (hitResult == HitResult.HIT_Critical)
                {
                    hitResult = HitResult.HIT_Normal;
                }
                TeamId teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "yorick_spectralGhoul_attack_buf_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                damageAmount *= 1.2f;
                float totalDamage = damageAmount + bonusDamage;
                AddBuff(attacker, target, new Buffs.YorickSpectralPrimaryTarget(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, true, true, attacker);
                Vector3 targetPos = GetPointByUnitFacingOffset(owner, 25, 0);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, true, true, false, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.YorickSpectral), (ObjAIBase)owner, 0);
                damageAmount -= damageAmount;
            }
        }
    }
}