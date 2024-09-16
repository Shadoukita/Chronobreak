namespace Buffs
{
    public class MalphiteShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { null, null, null, "", },
            BuffName = "MalphiteShield",
            BuffTextureName = "Malphite_GraniteShield.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            float maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float shieldHP = maxHP * 0.1f;
            float shieldHealth = MathF.Floor(shieldHP);
            SetBuffToolTipVar(1, shieldHealth);
        }
    }
}