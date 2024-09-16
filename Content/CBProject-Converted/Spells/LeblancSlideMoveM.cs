namespace Buffs
{
    public class LeblancSlideMoveM : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        Vector3 ownerPos;
        Vector3 castPosition;
        float aEDamage;
        float distance;
        EffectEmitter b;
        EffectEmitter distortionFx;
        EffectEmitter partname; // UNUSED
        int[] effect0 = { 22, 44, 66, 88, 110 };
        int[] effect1 = { 25, 50, 75, 100, 125 };
        int[] effect2 = { 28, 56, 84, 112, 140 };
        int[] effect3 = { 20, 40, 60, 80, 100 };
        public LeblancSlideMoveM(Vector3 ownerPos = default, Vector3 castPosition = default, float aEDamage = default, float distance = default)
        {
            this.ownerPos = ownerPos;
            this.castPosition = castPosition;
            this.aEDamage = aEDamage;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            TeamId casterID; // UNITIALIZED
            casterID = TeamId.TEAM_UNKNOWN; //TODO: Verify
            //RequireVar(this.aEDamage);
            //RequireVar(this.silenceDuration);
            //RequireVar(this.ownerPos);
            //RequireVar(this.castPosition);
            float distance = DistanceBetweenObjectAndPoint(owner, castPosition);
            if (distance < 10)
            {
                Vector3 castPosition = this.castPosition; // UNUSED
                castPosition = GetPointByUnitFacingOffset(owner, 10, 0);
            }
            Move(owner, castPosition, 1600, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            PlayAnimation("Spell2", 0, owner, true, false, true);
            SpellEffectCreate(out b, out _, "Leblanc_displacement_blink_target_ult.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out distortionFx, out _, "LeBlanc_Displacement_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(b);
            UnlockAnimation(owner, true);
            SpellEffectRemove(distortionFx);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            Vector3 currentPosition = GetUnitPosition(owner); // UNUSED
            SpellEffectCreate(out partname, out _, "leBlanc_slide_impact_self_ult.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            TeamId casterID = GetTeamID_CS(owner);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                SpellEffectCreate(out _, out _, "leBlanc_slide_impact_unit_tar.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                if (level == 1)
                {
                    ApplyDamage((ObjAIBase)owner, unit, aEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.66f, 1, false, false, (ObjAIBase)owner);
                }
                else if (level == 2)
                {
                    ApplyDamage((ObjAIBase)owner, unit, aEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, (ObjAIBase)owner);
                }
                else
                {
                    ApplyDamage((ObjAIBase)owner, unit, aEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.84f, 1, false, false, (ObjAIBase)owner);
                }
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.LeblancChaosOrbM)) > 0)
                {
                    ApplySilence(attacker, unit, 2);
                    SpellBuffRemove(unit, nameof(Buffs.LeblancChaosOrbM), (ObjAIBase)owner, 0);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (level == 1)
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.33f, 1, false, false, attacker);
                    }
                    else if (level == 2)
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, unit, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 1, false, false, attacker);
                    }
                    else
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, unit, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.42f, 1, false, false, attacker);
                    }
                }
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.LeblancChaosOrb)) > 0)
                {
                    ApplySilence(attacker, unit, 2);
                    SpellBuffRemove(unit, nameof(Buffs.LeblancChaosOrb), (ObjAIBase)owner, 0);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, unit, effect3[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 1, false, false, attacker);
                }
            }
        }
    }
}