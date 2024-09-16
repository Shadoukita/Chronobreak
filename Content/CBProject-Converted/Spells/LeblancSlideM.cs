namespace Spells
{
    public class LeblancSlideM : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        float[] effect0 = { 1.5f, 1.75f, 2, 2.25f, 2.5f };
        float[] effect1 = { 93.5f, 137.5f, 181.5f, 225.5f, 269.5f };
        float[] effect2 = { 106.25f, 156.25f, 206.25f, 256.25f, 306.25f };
        int[] effect3 = { 119, 175, 231, 287, 343 };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
            if (!canMove)
            {
                returnValue = false;
            }
            else if (!canCast)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            int level = base.level;
            float nextBuffVars_AEDamage;
            SealSpellSlot(3, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            Vector3 ownerPos = GetUnitPosition(owner);
            Vector3 castPosition = GetSpellTargetPos(spell);
            TeamId casterID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "leBlanc_displacement_cas_ult.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false);
            float distance = DistanceBetweenPoints(ownerPos, castPosition);
            if (distance > 600)
            {
                FaceDirection(owner, castPosition);
                castPosition = GetPointByUnitFacingOffset(owner, 600, 0);
            }
            float nextBuffVars_SilenceDuration = effect0[level - 1]; // UNUSED
            Vector3 nextBuffVars_OwnerPos = ownerPos;
            Vector3 nextBuffVars_CastPosition = castPosition;
            level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level == 1)
            {
                level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_AEDamage = effect1[level - 1];
            }
            else if (level == 2)
            {
                level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_AEDamage = effect2[level - 1];
            }
            else
            {
                level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_AEDamage = effect3[level - 1];
            }
            AddBuff(owner, owner, new Buffs.LeblancSlideM(nextBuffVars_OwnerPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.LeblancSlideMoveM(nextBuffVars_OwnerPos, nextBuffVars_CastPosition, nextBuffVars_AEDamage), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.LeblancSlideWallFixM(), 1, 1, 3.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LeblancSlideM : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancDisplacementM",
            BuffTextureName = "LeblancDisplacementReturnM.dds",
            SpellToggleSlot = 4,
        };
        Vector3 ownerPos;
        bool doNotTeleport;
        EffectEmitter yellowIndicator;
        public LeblancSlideM(Vector3 ownerPos = default)
        {
            this.ownerPos = ownerPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.ownerPos);
            doNotTeleport = false;
            Vector3 ownerPos = this.ownerPos;
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSlideReturnM));
            TeamId casterID = GetTeamID_CS(owner);
            SpellEffectCreate(out yellowIndicator, out _, "Leblanc_displacement_blink_indicator_ult.troy", default, casterID, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(yellowIndicator);
            if (!doNotTeleport && !expired)
            {
                Vector3 ownerPos = this.ownerPos;
                Vector3 currentPosition = GetUnitPosition(owner);
                TeamId casterID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "leBlanc_displacement_cas.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, currentPosition, target, default, default, true, default, default, false);
                TeleportToPosition(owner, ownerPos);
                SpellEffectCreate(out _, out _, "leBlanc_displacement_cas.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false);
                SpellEffectCreate(out _, out _, "Leblanc_displacement_blink_return_trigger.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, owner, default, ownerPos, true, default, default, false);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.LeblancSlideReturnM))
            {
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.LeblancSlide)) > 0)
                {
                    float cooldownTime = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cooldownTime <= 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.25f);
                    }
                }
                SpellBuffRemove(owner, nameof(Buffs.LeblancSlideWallFixM), (ObjAIBase)owner);
                SpellBuffRemoveCurrent(owner);
            }
            if (spellName == nameof(Spells.LeblancSlide))
            {
                doNotTeleport = true;
                SpellBuffRemove(owner, nameof(Buffs.LeblancSlideWallFixM), (ObjAIBase)owner);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}