namespace Spells
{
    public class LuxMaliceCannonPartFix2 : SpellScript
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
    public class LuxMaliceCannonPartFix2 : BuffScript
    {
        public override void OnActivate()
        {
            Vector3 ownerPos = GetUnitPosition(owner); // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 beam1 = GetPointByUnitFacingOffset(owner, 1884, 0);
            Vector3 beam2 = GetPointByUnitFacingOffset(owner, 2826, 0);
            Vector3 beam3 = GetPointByUnitFacingOffset(owner, 2475, 0);
            Minion other1 = SpawnMinion("hiu", "TestCube", "idle.lua", beam1, teamID, false, true, false, false, false, true, 300, default, false, (Champion)owner);
            Minion other2 = SpawnMinion("hiu", "TestCube", "idle.lua", beam2, teamID, false, true, false, false, false, true, 300, default, false, (Champion)owner);
            Minion other3 = SpawnMinion("hiu", "TestCube", "idle.lua", beam3, teamID, false, true, false, false, false, true, 300, default, false, (Champion)owner);
            LinkVisibility(other1, attacker);
            LinkVisibility(other2, attacker);
            LinkVisibility(other3, attacker);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(other2, other2, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(other3, other3, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}