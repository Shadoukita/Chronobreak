namespace Buffs
{
    public class OdinGrievousWound : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinGrievousWound",
            BuffTextureName = "GW_Debuff.dds",
            PersistsThroughDeath = true,
        };
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if (health >= 0)
            {
                float effectiveHeal = health * 0.8f;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}