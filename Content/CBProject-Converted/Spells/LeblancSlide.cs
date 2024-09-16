namespace Spells
{
    public class LeblancSlide : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        int[] effect0 = { 85, 125, 165, 205, 245 };
        float[] effect1 = { 1.5f, 1.75f, 2, 2.25f, 2.5f };
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
            Vector3 ownerPos = GetUnitPosition(owner);
            Vector3 castPosition = GetSpellTargetPos(spell);
            TeamId casterID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "leBlanc_displacement_cas.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false);
            float distance = DistanceBetweenPoints(ownerPos, castPosition);
            if (distance > 600)
            {
                FaceDirection(owner, castPosition);
                castPosition = GetPointByUnitFacingOffset(owner, 600, 0);
            }
            TeamId teamOfOwner = GetTeamID_CS(owner); // UNUSED
            float nextBuffVars_AEDamage = effect0[level - 1];
            float nextBuffVars_SilenceDuration = effect1[level - 1]; // UNUSED
            Vector3 nextBuffVars_OwnerPos = ownerPos;
            Vector3 nextBuffVars_CastPosition = castPosition;
            AddBuff(owner, owner, new Buffs.LeblancSlide(nextBuffVars_OwnerPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.LeblancSlideMove(nextBuffVars_AEDamage, nextBuffVars_OwnerPos, nextBuffVars_CastPosition), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.LeblancSlideWallFix(), 1, 1, 3.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LeblancSlide : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancDisplacement",
            BuffTextureName = "LeblancDisplacementReturn.dds",
            SpellToggleSlot = 2,
        };
        Vector3 ownerPos;
        bool doNotTeleport;
        EffectEmitter yellowIndicator;
        public LeblancSlide(Vector3 ownerPos = default)
        {
            this.ownerPos = ownerPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.ownerPos);
            doNotTeleport = false;
            Vector3 ownerPos = this.ownerPos;
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSlideReturn));
            TeamId casterID = GetTeamID_CS(owner);
            SpellEffectCreate(out yellowIndicator, out _, "Leblanc_displacement_blink_indicator.troy", default, casterID, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, owner, default, default, false, default, default, false);
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
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeblancSlideWallFix)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.LeblancSlideWallFix), (ObjAIBase)owner);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.LeblancSlideReturn))
            {
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.LeblancSlideM)) > 0)
                {
                    float cooldownTime = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cooldownTime <= 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.25f);
                    }
                }
                SpellBuffRemoveCurrent(owner);
            }
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.LeblancSlideM))
            {
                doNotTeleport = true;
                SpellBuffRemoveCurrent(owner);
                SpellBuffRemove(owner, nameof(Buffs.LeblancSlideMove), (ObjAIBase)owner);
            }
        }
    }
}