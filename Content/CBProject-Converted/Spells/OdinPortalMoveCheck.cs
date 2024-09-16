namespace Buffs
{
    public class OdinPortalMoveCheck : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Vector3 startPosition;
        int isCancelled;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            startPosition = GetUnitPosition(owner);
            isCancelled = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            if (isCancelled == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinPortalChannel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            float _0_25; // UNITIALIZED
            _0_25 = 0.25f; //TODO: Verify
            if (ExecutePeriodically(0, ref lastTimeExecuted, false, _0_25))
            {
                float distance = DistanceBetweenObjectAndPoint(owner, startPosition);
                if (distance > 10)
                {
                    isCancelled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}