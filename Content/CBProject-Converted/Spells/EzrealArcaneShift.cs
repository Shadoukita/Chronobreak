namespace Spells
{
    public class EzrealArcaneShift : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "CyberEzreal", },
        };
        Region bubbleID; // UNUSED
        public override void SelfExecute()
        {
            int ownerSkinID = GetSkinID(owner);
            Vector3 castPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, castPos);
            FaceDirection(owner, castPos);
            if (distance >= 475)
            {
                castPos = GetPointByUnitFacingOffset(owner, 475, 0);
            }
            TeleportToPosition(owner, castPos);
            TeamId teamID = GetTeamID_CS(owner);
            if (ownerSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "Ezreal_arcaneshift_cas_pulsefire.troy", default, teamID, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                SpellEffectCreate(out _, out _, "Ezreal_arcaneshift_flash_pulsefire.troy", default, teamID, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "Ezreal_arcaneshift_cas.troy", default, teamID, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                SpellEffectCreate(out _, out _, "Ezreal_arcaneshift_flash.troy", default, teamID, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            }
            TeamId casterID = GetTeamID_CS(owner);
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bool fired = false;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 5, default, true))
            {
                if (!fired)
                {
                    bool isStealthed = GetStealthed(unit);
                    bool canSee = CanSeeTarget(owner, unit);
                    if (!isStealthed)
                    {
                        bubbleID = AddUnitPerceptionBubble(casterID, 100, unit, 1, default, default, false);
                        FaceDirection(attacker, unit.Position3D);
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                        fired = true;
                    }
                    else if (unit is Champion)
                    {
                        bubbleID = AddUnitPerceptionBubble(casterID, 100, unit, 1, default, default, false);
                        FaceDirection(attacker, unit.Position3D);
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                        fired = true;
                    }
                    else if (canSee)
                    {
                        bubbleID = AddUnitPerceptionBubble(casterID, 100, unit, 1, default, default, false);
                        FaceDirection(attacker, unit.Position3D);
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                        fired = true;
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class EzrealArcaneShift : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "",
            BuffName = "RiftWalk",
            BuffTextureName = "Voidwalker_Riftwalk.dds",
        };
    }
}