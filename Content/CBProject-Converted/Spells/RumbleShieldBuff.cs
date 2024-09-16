namespace Buffs
{
    public class RumbleShieldBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "Robot_Root", "BUFFBONE_CSTM_BOOSTER", },
            AutoBuffActivateEffect = new[] { "rumble_shield_speed_buf.troy", "rumble_shield_speed_buf_booster.troy", },
            BuffName = "RumbleShieldBuff",
            BuffTextureName = "Rumble_Scrap Shield2.dds",
        };
        float speedBoost;
        public RumbleShieldBuff(float speedBoost = default)
        {
            this.speedBoost = speedBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedBoost);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, speedBoost);
        }
    }
}