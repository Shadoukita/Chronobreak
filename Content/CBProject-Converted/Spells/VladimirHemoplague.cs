namespace Spells
{
    public class VladimirHemoplague : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "BloodKingVladimir", },
            SpellVOOverrideSkins = new[] { "BloodkingVladimir", },
        };
        public override void SelfExecute()
        {
            float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA); // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(targetPos, ownerPos);
            FaceDirection(owner, targetPos);
            if (distance > 701)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1050, 0);
            }
            Minion other2 = SpawnMinion("k", "TestCubeRender", "idle.lua", targetPos, teamID, true, true, false, true, true, true, 0, false, true, (Champion)attacker);
            SpellCast(owner, other2, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class VladimirHemoplague : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "Vladimir_HemoplagueImmune.dds",
        };
    }
}