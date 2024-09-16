namespace Spells
{
    public class ShyvanaDoubleAttackDragon : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
        };
        int[] effect0 = { 10, 9, 8, 7, 6 };
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.ShyvanaDoubleAttackDragon(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class ShyvanaDoubleAttackDragon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "R_Hand", "L_Hand", },
            AutoBuffActivateEffect = new[] { "shyvana_doubleAttack_buf.troy", "shyvana_doubleAttack_buf.troy", },
            BuffName = "ShyvanaDoubleAttackDragon",
            BuffTextureName = "ShyvanaTwinBite.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float spellCooldown;
        float[] effect0 = { 0.8f, 0.85f, 0.9f, 0.95f, 1 };
        int[] effect1 = { 15, 25, 35, 45, 55 };
        public ShyvanaDoubleAttackDragon(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            OverrideAutoAttack(3, SpellSlotType.ExtraSlots, owner, 1, true);
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
            TeamId teamID = GetTeamID_CS(owner);
            float baseAttackDamage = GetBaseAttackDamage(owner);
            SpellBuffRemove(owner, nameof(Buffs.ShyvanaDoubleAttackDragon), (ObjAIBase)owner, 0);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position3D, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                float distance = DistanceBetweenObjects(attacker, unit);
                if (target != unit && distance < 250 && IsInFront(owner, unit))
                {
                    float procDamage;
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                    ApplyDamage(attacker, unit, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, effect0[level - 1], 0, 1, false, false, attacker);
                    if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.ShyvanaFireballMissile)) > 0)
                    {
                        level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        procDamage = effect1[level - 1];
                        ApplyDamage(attacker, unit, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.1f, 0, false, false, attacker);
                        teamID = GetTeamID_CS(attacker);
                        SpellEffectCreate(out _, out _, "shyvana_flameBreath_reignite.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                    }
                    if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.ShyvanaFireballMissileMinion)) > 0)
                    {
                        level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        procDamage = effect1[level - 1];
                        ApplyDamage(attacker, unit, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.1f, 0, false, false, attacker);
                        teamID = GetTeamID_CS(attacker);
                        SpellEffectCreate(out _, out _, "shyvana_flameBreath_reignite.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                    }
                    level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (level == 1)
                    {
                        IncPAR(owner, 2, PrimaryAbilityResourceType.Other);
                    }
                    else if (level == 2)
                    {
                        IncPAR(owner, 3, PrimaryAbilityResourceType.Other);
                    }
                    else if (level == 3)
                    {
                        IncPAR(owner, 4, PrimaryAbilityResourceType.Other);
                    }
                    if (target is ObjAIBase)
                    {
                        SpellEffectCreate(out _, out _, "shyvana_doubleAttack_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    }
                }
            }
        }
    }
}