namespace Spells
{
    public class MaokaiSapling2 : SpellScript
    {
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Region asdf = AddPosPerceptionBubble(teamID, 250, targetPos, 1, default, false); // UNUSED
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(targetPos, ownerPos); // UNUSED
            FaceDirection(owner, targetPos);
            Minion other2 = SpawnMinion("k", "TestCubeRender10Vision", "idle.lua", targetPos, teamID, true, true, false, true, true, true, 1, default, true, (Champion)attacker);
            AddBuff(attacker, other2, new Buffs.MaokaiSapling2(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(owner, other2, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class MaokaiSapling2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "C_BUFFBONE_GLB_CHEST_LOC", },
            AutoBuffActivateEffect = new[] { "maokai_sapling_activated_indicator.troy", },
        };
        public override void OnActivate()
        {
            IncFlatBubbleRadiusMod(owner, 690);
        }
        public override void OnUpdateStats()
        {
            IncFlatBubbleRadiusMod(owner, 690);
        }
    }
}