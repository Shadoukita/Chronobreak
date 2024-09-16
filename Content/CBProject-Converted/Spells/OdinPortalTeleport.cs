namespace Buffs
{
    public class OdinPortalTeleport : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Vector3 startPosition;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            startPosition = GetUnitPosition(owner);
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 currentPos = GetUnitPosition(owner);
            SetCameraPosition((Champion)owner, currentPos);
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
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}