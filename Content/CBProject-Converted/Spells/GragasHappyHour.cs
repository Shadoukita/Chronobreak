namespace Buffs
{
    public class GragasHappyHour : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GragasHappyHour",
            BuffTextureName = "GragasPassiveHeal.dds",
        };
        float healAmount;
        public override void OnUpdateStats()
        {
            float maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float healHP = maxHP * 0.02f;
            healAmount = MathF.Floor(healHP);
            SetBuffToolTipVar(1, healAmount);
        }
    }
}