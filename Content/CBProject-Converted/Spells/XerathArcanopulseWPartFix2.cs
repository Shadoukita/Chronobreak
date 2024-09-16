namespace Spells
{
    public class XerathArcanopulseWPartFix2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class XerathArcanopulseWPartFix2 : BuffScript
    {
        public override void OnActivate()
        {
            Vector3 ownerPos = GetUnitPosition(owner); // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 beam1 = GetPointByUnitFacingOffset(owner, 850, 0);
            Vector3 beam2 = GetPointByUnitFacingOffset(owner, 1150, 0);
            Vector3 beam3 = GetPointByUnitFacingOffset(owner, 1400, 0);
            Minion other1 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam1, teamID, false, true, false, false, false, true, 300, false, false);
            Minion other2 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam2, teamID, false, true, false, false, false, true, 300, false, false);
            Minion other3 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam3, teamID, false, true, false, false, false, true, 300, false, false);
            LinkVisibility(other1, owner);
            LinkVisibility(other1, other2);
            LinkVisibility(other2, other3);
            LinkVisibility(other3, attacker);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other2, other2, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other3, other3, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}