namespace Spells
{
    public class XerathArcanopulse : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos); // UNUSED
            FaceDirection(owner, targetPos);
            Vector3 beam1 = GetPointByUnitFacingOffset(owner, 145, 0);
            Vector3 beam3 = GetPointByUnitFacingOffset(owner, 1100, 0);
            Minion other1 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam1, teamID, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            Minion other3 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam3, teamID, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            FaceDirection(other1, other3.Position3D);
            LinkVisibility(other1, other3);
            AddBuff(attacker, other1, new Buffs.XerathArcanopulseDeath(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.XerathArcanopulseDeath(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other3, other1, new Buffs.XerathArcanopulsePartFix(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other3, other1, new Buffs.XerathArcanopulsePartFix2(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other1, other3, new Buffs.XerathArcanopulseBeam(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, other1, new Buffs.XerathArcanopulseBall(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}