namespace Buffs
{
    public class GarenFastMove : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "garen_descisiveStrike_speed.troy", "garen_descisiveStrike_indicator.troy", },
            BuffName = "GarenFastMove",
            BuffTextureName = "Garen_DecisiveStrike.dds",
        };
        float speedMod;
        public GarenFastMove(float speedMod = default)
        {
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, speedMod);
        }
    }
}