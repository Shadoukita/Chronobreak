namespace Buffs
{
    public class SpikedShell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spiked Shell",
            BuffTextureName = "Armordillo_ScavengeArmor.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float armorAmount;
        public override void OnActivate()
        {
            armorAmount = GetArmor(owner);
        }
        public override void OnUpdateStats()
        {
            float damageAmount = armorAmount * 0.25f;
            IncFlatPhysicalDamageMod(owner, damageAmount);
            SetBuffToolTipVar(1, damageAmount);
            SetBuffToolTipVar(2, 25);
        }
        public override void OnUpdateActions()
        {
            armorAmount = GetArmor(owner);
        }
    }
}