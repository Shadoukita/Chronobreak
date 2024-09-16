namespace Buffs
{
    public class TurretDecay : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Turret Decay",
            BuffTextureName = "1020_Glowing_Orb.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(60, ref lastTimeExecuted, false))
            {
                IncPermanentFlatArmorMod(owner, -2);
                IncPermanentFlatSpellBlockMod(owner, -2);
            }
        }
    }
}