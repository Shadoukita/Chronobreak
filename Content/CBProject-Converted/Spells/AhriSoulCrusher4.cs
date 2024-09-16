namespace Buffs
{
    public class AhriSoulCrusher4 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "PotionofElusiveness_itm.troy", "PotionofBrilliance_itm.troy", "PotionofGiantStrength_itm.troy", },
            BuffName = "AhriSoulCrusher",
            BuffTextureName = "3017_Egitai_Twinsoul.dds",
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            charVars.FoxFireIsActive = 0;
        }
    }
}