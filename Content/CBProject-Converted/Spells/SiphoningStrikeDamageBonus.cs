namespace Buffs
{
    public class SiphoningStrikeDamageBonus : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CannibalismMaxHPGained",
            BuffTextureName = "Sion_SpiritFeast.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float damageBonus;
        public SiphoningStrikeDamageBonus(float damageBonus = default)
        {
            this.damageBonus = damageBonus;
        }
        public override void OnActivate()
        {
            charVars.DamageBonus += damageBonus;
        }
    }
}