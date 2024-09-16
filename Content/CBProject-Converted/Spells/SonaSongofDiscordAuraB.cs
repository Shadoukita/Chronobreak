namespace Buffs
{
    public class SonaSongofDiscordAuraB : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "SonaSongofDiscordAuraB",
            BuffTextureName = "Sona_SongofDiscord.dds",
        };
        float mSBoost;
        public SonaSongofDiscordAuraB(float mSBoost = default)
        {
            this.mSBoost = mSBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.mSBoost);
        }
        public override void OnUpdateStats()
        {
            IncFlatMovementSpeedMod(owner, mSBoost);
        }
    }
}