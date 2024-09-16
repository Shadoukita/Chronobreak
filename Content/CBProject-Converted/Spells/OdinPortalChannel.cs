namespace Buffs
{
    public class OdinPortalChannel : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Vector3 startPosition;
        EffectEmitter particleID;
        int isCancelled;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            startPosition = GetUnitPosition(owner);
            SpellEffectCreate(out particleID, out _, "TeleportHome.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            isCancelled = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleID);
            if (isCancelled == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinPortalTeleport(), 1, 1, 0.35f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            float _0_25; // UNITIALIZED
            _0_25 = 0.25f; //TODO: Verify
            if (ExecutePeriodically(0, ref lastTimeExecuted, false, _0_25))
            {
                float distance = DistanceBetweenObjectAndPoint(owner, startPosition);
                if (distance > 5)
                {
                    isCancelled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}