namespace Buffs
{
    public class MaokaiSapMagicPass : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiSapMagicPass",
            BuffTextureName = "MaokaiSapMagic.dds",
            PersistsThroughDeath = true,
        };
        float healAmount;
        public override void OnActivate()
        {
            healAmount = 0;
        }
        public override void OnUpdateActions()
        {
            float maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            healAmount = maxHP * 0.07f;
            healAmount = MathF.Floor(healAmount);
            SetBuffToolTipVar(1, healAmount);
        }
    }
}