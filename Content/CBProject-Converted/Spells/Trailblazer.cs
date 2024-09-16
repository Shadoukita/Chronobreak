namespace Buffs
{
    public class Trailblazer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Eagle Eye",
            BuffTextureName = "Teemo_EagleEye.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = { 6, 6, 6, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14, 14, 14, 16, 16, 16 };
        public override void OnActivate()
        {
            lastTooltip = 0;
        }
        public override void OnUpdateActions()
        {
            Vector3 curPos = GetPointByUnitFacingOffset(owner, 30, 180);
            TeamId teamID = GetTeamID_CS(attacker);
            Minion other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, default, default, (Champion)attacker);
            float moveSpeed = GetMovementSpeed(attacker);
            float moveSpeedMod = moveSpeed / 2500;
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            AddBuff((ObjAIBase)owner, other3, new Buffs.TrailblazerApplicator(nextBuffVars_MoveSpeedMod), 1, 1, charVars.TrailDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float tooltipAmount = effect0[level - 1];
                if (tooltipAmount > lastTooltip)
                {
                    lastTooltip = tooltipAmount;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }
    }
}