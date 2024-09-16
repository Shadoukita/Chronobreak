namespace Buffs
{
    public class BushwhackDamage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "global_Watched.troy", },
            BuffName = "BushwhackDamage",
            BuffTextureName = "NIdalee_Bushwhack.dds",
        };
        float dOTCounter;
        float damagePerTick;
        int dotCounter; // UNUSED
        Region bubbleID;
        Region bubbleID2;
        float lastTimeExecuted;
        public BushwhackDamage(float dOTCounter = default, float damagePerTick = default)
        {
            this.dOTCounter = dOTCounter;
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
            dotCounter = 4;
            TeamId team = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(team, 400, owner, 20, default, default, false);
            bubbleID2 = AddUnitPerceptionBubble(team, 50, owner, 20, default, default, true);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                if (dOTCounter < 4)
                {
                    dOTCounter++;
                    ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.1f, 1, false, false, attacker);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
        }
    }
}