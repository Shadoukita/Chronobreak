namespace Buffs
{
    public class SmiteMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Widowmaker",
            BuffTextureName = "3084_Widowmaker.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                ApplyDamage(attacker, target, 30, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 1);
            }
        }
    }
}