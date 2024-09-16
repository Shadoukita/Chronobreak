namespace Spells
{
    public class MonkeyKingDoubleAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
        };
        EffectEmitter battleCries; // UNUSED
        int[] effect0 = { 9, 8, 7, 6, 5 };
        public override void SelfExecute()
        {
            TeamId teamID; // UNITIALIZED
            teamID = TeamId.TEAM_UNKNOWN; //TODO: Verify
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.MonkeyKingDoubleAttack(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
            SpellEffectCreate(out battleCries, out _, "xenZiou_battle_cry_weapon_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weapon_a_bend3", default, owner, "weapon_b_bend3", default, false, default, default, false, false);
        }
    }
}
namespace Buffs
{
    public class MonkeyKingDoubleAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon_a_bend3", "weapon_b_bend3", },
            AutoBuffActivateEffect = new[] { "monkey_king_crushingBlow_buf_self.troy", "monkey_king_crushingBlow_buf_self.troy", },
            BuffName = "MonkeyKingDoubleAttack",
            BuffTextureName = "MonkeyKingCrushingBlow.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float spellCooldown;
        int[] effect0 = { 30, 60, 90, 120, 150 };
        float[] effect1 = { -0.3f, -0.3f, -0.3f, -0.3f, -0.3f };
        public MonkeyKingDoubleAttack(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, true);
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
            RemoveOverrideAutoAttack(owner, true);
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateStats()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            float damageToDeal; // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float bonusDamage = effect0[level - 1];
            float totalAD = GetTotalAttackDamage(owner);
            float bonusADRatio = totalAD * 0.1f;
            bonusDamage += bonusADRatio;
            if (hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if (target is ObjAIBase)
            {
                if (target is BaseTurret)
                {
                    damageToDeal = bonusDamage + damageAmount;
                    SpellEffectCreate(out _, out _, "monkey_king_crushingBlow_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.MonkeyKingDoubleAttack), (ObjAIBase)owner, 0);
                }
                else
                {
                    damageToDeal = bonusDamage + damageAmount;
                    float nextBuffVars_ArmorDebuff = effect1[level - 1];
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.MonkeyKingDoubleAttackDebuff(nextBuffVars_ArmorDebuff), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    SpellEffectCreate(out _, out _, "monkey_king_crushingBlow_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.MonkeyKingDoubleAttack), (ObjAIBase)owner, 0);
                }
            }
            else
            {
                damageToDeal = bonusDamage + damageAmount;
                SpellBuffRemove(owner, nameof(Buffs.MonkeyKingDoubleAttack), (ObjAIBase)owner, 0);
            }
            damageAmount += bonusDamage;
        }
    }
}