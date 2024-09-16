namespace Spells
{
    public class GragasExplosiveCask : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
        };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(targetPos, ownerPos);
            FaceDirection(owner, targetPos);
            if (distance > 1050)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1050, 0);
            }
            Minion other2 = SpawnMinion("k", "TestCubeRender", "idle.lua", targetPos, teamID, true, true, false, true, true, true, 0, default, true, (Champion)attacker);
            SpellCast(owner, other2, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}