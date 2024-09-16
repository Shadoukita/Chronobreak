namespace Spells
{
    public class RumbleCarpetBomb : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            Vector3 targetPosStart = GetSpellTargetPos(spell);
            Vector3 targetPosEnd = GetSpellDragEndPos(spell);
            Minion other1 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPosStart, teamOfOwner, false, true, false, true, true, true, 0, default, true, (Champion)owner);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            FaceDirection(other1, targetPosEnd);
            targetPosEnd = GetPointByUnitFacingOffset(other1, 1200, 0);
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            SpellCast(owner, default, targetPosEnd, targetPosEnd, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, targetPosStart);
            AddBuff(owner, owner, new Buffs.RumbleHeatDelay(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            IncPAR(owner, 0, PrimaryAbilityResourceType.Other);
            AddBuff(owner, owner, new Buffs.RumbleCarpetBomb(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(owner, owner, owner.Position3D, default, 2, SpellSlotType.ExtraSlots, level, true, false, false, true, false, true, targetPosStart);
        }
    }
}
namespace Buffs
{
    public class RumbleCarpetBomb : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "GalioRighteousGust",
            BuffTextureName = "",
        };
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.RumbleCarpetBombEffect));
        }
    }
}