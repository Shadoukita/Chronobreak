namespace Spells
{
    public class RenektonSliceAndDice : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 18, 17, 16, 15, 14 };
        int[] effect1 = { 30, 60, 90, 120, 150 };
        float[] effect2 = { -0.15f, -0.175f, -0.2f, -0.225f, -0.25f };
        public override void SelfExecute()
        {
            bool nextBuffVars_DiceVersion;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RenektonTargetSliced), true))
            {
                SpellBuffClear(unit, nameof(Buffs.RenektonTargetSliced));
            }
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float moveSpeed = GetMovementSpeed(owner);
            float dashSpeed = moveSpeed + 750;
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance >= 0)
            {
                FaceDirection(owner, targetPos);
                distance = 450;
                targetPos = GetPointByUnitFacingOffset(owner, 450, 0);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonSliceAndDiceDelay)) == 0)
            {
                float cooldownMod = GetPercentCooldownMod(owner);
                float multiplier = 1 + cooldownMod;
                float cooldownTime = effect0[level - 1];
                float debuffTime = multiplier * cooldownTime;
                AddBuff(owner, owner, new Buffs.RenektonSliceAndDiceTimer(), 1, 1, debuffTime, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                nextBuffVars_DiceVersion = false;
            }
            else
            {
                nextBuffVars_DiceVersion = true;
                SpellBuffClear(owner, nameof(Buffs.RenektonSliceAndDiceDelay));
            }
            float nextBuffVars_DashSpeed = dashSpeed;
            float nextBuffVars_BonusDamage = effect1[level - 1];
            float nextBuffVars_ArmorShred = effect2[level - 1];
            float nextBuffVars_Distance = distance;
            Vector3 nextBuffVars_TargetPos = targetPos;
            AddBuff(attacker, owner, new Buffs.RenektonSliceAndDice(nextBuffVars_DiceVersion, nextBuffVars_DashSpeed, nextBuffVars_BonusDamage, nextBuffVars_ArmorShred, nextBuffVars_Distance, nextBuffVars_TargetPos), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.1f, true, false, false);
        }
    }
}
namespace Buffs
{
    public class RenektonSliceAndDice : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "lhnd", "rhnd", },
            AutoBuffActivateEffect = new[] { "Global_Haste.troy", "Global_Haste.troy", },
            BuffName = "RenekthonSliceAndDice",
            BuffTextureName = "Renekton_SliceAndDice.dds",
        };
        bool diceVersion;
        float dashSpeed;
        float bonusDamage;
        float armorShred;
        float distance;
        Vector3 targetPos;
        bool hitUnit; // UNUSED
        bool rageBonus;
        EffectEmitter shinyParticle;
        int level; // UNUSED
        public RenektonSliceAndDice(bool diceVersion = default, float dashSpeed = default, float bonusDamage = default, float armorShred = default, float distance = default, Vector3 targetPos = default)
        {
            this.diceVersion = diceVersion;
            this.dashSpeed = dashSpeed;
            this.bonusDamage = bonusDamage;
            this.armorShred = armorShred;
            this.distance = distance;
            this.targetPos = targetPos;
        }
        public override void OnCollision()
        {
            if (owner.Team != target.Team && target is ObjAIBase)
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RenektonTargetSliced), false))
                {
                    bool shouldHit = true;
                    bool visible = CanSeeTarget(owner, unit);
                    if (!visible)
                    {
                        if (unit is not Champion)
                        {
                            shouldHit = false;
                        }
                    }
                    hitUnit = true;
                    float baseAttack = GetBaseAttackDamage(owner);
                    float hitDamage = 0 * baseAttack;
                    hitDamage += bonusDamage;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonTargetSliced(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonSliceAndDiceDelay)) > 0)
                    {
                    }
                    else if (diceVersion)
                    {
                    }
                    else if (!shouldHit)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonSliceAndDiceDelay(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        SetSlotSpellCooldownTimeVer2(0, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                    }
                    if (shouldHit)
                    {
                        TeamId ownerVar = GetTeamID_CS(owner); // UNUSED
                        BreakSpellShields(unit);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonBloodSplatterTarget(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        if (rageBonus)
                        {
                            float nextBuffVars_ArmorShred = armorShred;
                            AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonSliceAndDiceDebuff(nextBuffVars_ArmorShred), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
                            ApplyDamage((ObjAIBase)owner, unit, hitDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1.5f, 0, 0.9f, false, false, (ObjAIBase)owner);
                        }
                        else
                        {
                            ApplyDamage((ObjAIBase)owner, unit, hitDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0.9f, false, false, (ObjAIBase)owner);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            PlayAnimation("Spell3", 0, owner, true, false, true);
            StartTrackingCollisions(owner, true);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.diceVersion);
            //RequireVar(this.distance);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.shinyParticle);
            //RequireVar(this.armorShred);
            Vector3 targetPos = this.targetPos;
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            Move(owner, targetPos, dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            hitUnit = false;
            rageBonus = false;
            float rageAmount = GetPAR(owner, PrimaryAbilityResourceType.Other);
            if (rageAmount >= 50)
            {
                if (diceVersion)
                {
                    rageBonus = true;
                    IncPAR(owner, -50, PrimaryAbilityResourceType.Other);
                    SpellEffectCreate(out shinyParticle, out _, "RenektonSliceDice_buf_rage.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "lhand", default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out shinyParticle, out _, "RenektonSliceDice_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "lhand", default, target, default, default, true, false, false, false, false);
                }
            }
            else
            {
                SpellEffectCreate(out shinyParticle, out _, "RenektonSliceDice_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "lhand", default, target, default, default, true, false, false, false, false);
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RenektonTargetSliced), false))
            {
                bool shouldHit = true;
                bool visible = CanSeeTarget(owner, unit);
                if (!visible && unit is not Champion)
                {
                    shouldHit = false;
                }
                hitUnit = true;
                float baseAttack = GetBaseAttackDamage(owner);
                float hitDamage = 0 * baseAttack;
                hitDamage += bonusDamage;
                AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonTargetSliced(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonSliceAndDiceDelay)) > 0)
                {
                }
                else if (diceVersion)
                {
                }
                else if (!shouldHit)
                {
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonSliceAndDiceDelay(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SetSlotSpellCooldownTimeVer2(0, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                }
                if (shouldHit)
                {
                    TeamId ownerVar = GetTeamID_CS(owner); // UNUSED
                    BreakSpellShields(unit);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonBloodSplatterTarget(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (rageBonus)
                    {
                        float nextBuffVars_ArmorShred = armorShred;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonSliceAndDiceDebuff(nextBuffVars_ArmorShred), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        ApplyDamage((ObjAIBase)owner, unit, hitDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1.5f, 0, 0.9f, false, false, (ObjAIBase)owner);
                    }
                    else
                    {
                        ApplyDamage((ObjAIBase)owner, unit, hitDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0.9f, false, false, (ObjAIBase)owner);
                    }
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            StartTrackingCollisions(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectRemove(shinyParticle);
            SetCanAttack(owner, false);
            SetCanMove(owner, true);
            float ragePercent = GetPARPercent(owner, PrimaryAbilityResourceType.Shield); // UNUSED
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RenektonTargetSliced), false))
            {
                bool shouldHit = true;
                bool visible = CanSeeTarget(owner, unit);
                if (!visible && unit is not Champion)
                {
                    shouldHit = false;
                }
                hitUnit = true;
                float baseAttack = GetBaseAttackDamage(owner);
                float hitDamage = 0 * baseAttack;
                hitDamage += bonusDamage;
                AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonTargetSliced(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonSliceAndDiceDelay)) > 0)
                {
                }
                else if (diceVersion)
                {
                }
                else if (!shouldHit)
                {
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonSliceAndDiceDelay(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SetSlotSpellCooldownTimeVer2(0, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                }
                if (shouldHit)
                {
                    TeamId ownerVar = GetTeamID_CS(owner); // UNUSED
                    BreakSpellShields(unit);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonBloodSplatterTarget(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (rageBonus)
                    {
                        float nextBuffVars_ArmorShred = armorShred;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonSliceAndDiceDebuff(nextBuffVars_ArmorShred), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        ApplyDamage((ObjAIBase)owner, unit, hitDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1.5f, 0, 0.9f, false, false, (ObjAIBase)owner);
                    }
                    else
                    {
                        ApplyDamage((ObjAIBase)owner, unit, hitDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0.9f, false, false, (ObjAIBase)owner);
                    }
                }
            }
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RenektonTargetSliced), true))
            {
                SpellBuffClear(unit, nameof(Buffs.RenektonTargetSliced));
            }
            if (diceVersion)
            {
                SpellBuffClear(owner, nameof(Buffs.RenektonSliceAndDiceDelay));
            }
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetCanAttack(owner, false);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.RenektonTargetSliced), false))
            {
                bool shouldHit = true;
                bool visible = CanSeeTarget(owner, unit);
                if (!visible && unit is not Champion)
                {
                    shouldHit = false;
                }
                hitUnit = true;
                float baseAttack = GetBaseAttackDamage(owner);
                float hitDamage = 0 * baseAttack;
                hitDamage += bonusDamage;
                AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonTargetSliced(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonSliceAndDiceDelay)) > 0)
                {
                }
                else if (diceVersion)
                {
                }
                else if (!shouldHit)
                {
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonSliceAndDiceDelay(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    SetSlotSpellCooldownTimeVer2(0, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                }
                if (shouldHit)
                {
                    TeamId ownerVar = GetTeamID_CS(owner); // UNUSED
                    BreakSpellShields(unit);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonBloodSplatterTarget(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (rageBonus)
                    {
                        float nextBuffVars_ArmorShred = armorShred;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonSliceAndDiceDebuff(nextBuffVars_ArmorShred), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        ApplyDamage((ObjAIBase)owner, unit, hitDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1.5f, 0, 0.9f, false, false, (ObjAIBase)owner);
                    }
                    else
                    {
                        ApplyDamage((ObjAIBase)owner, unit, hitDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0.9f, false, false, (ObjAIBase)owner);
                    }
                }
            }
        }
        public override void OnMoveEnd()
        {
            SpellEffectRemove(shinyParticle);
            StopMove(owner);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetCanAttack(owner, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonUnlockAnimationAttack(), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemove(owner, nameof(Buffs.RenektonSliceAndDice), (ObjAIBase)owner, 0);
        }
    }
}