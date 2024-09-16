namespace Buffs
{
    public class BantamTrapTargetSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", },
            BuffName = "Noxious Trap Target",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float damagePerTick;
        float moveSpeedMod;
        Region bubbleID;
        float lastTimeExecuted;
        public BantamTrapTargetSlow(float damagePerTick = default, float moveSpeedMod = default)
        {
            this.damagePerTick = damagePerTick;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.moveSpeedMod);
            ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.16f, 1, false, false, attacker);
            TeamId teamID = GetTeamID_CS(attacker);
            bubbleID = AddPosPerceptionBubble(teamID, 400, owner.Position3D, 5, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.16f, 1, false, false, attacker);
            }
        }
    }
}