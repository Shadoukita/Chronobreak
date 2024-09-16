namespace Buffs
{
    public class XerathAscended : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XerathAscended",
            BuffTextureName = "Xerath_AscendedForm.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float aPMod;
        public override void OnActivate()
        {
            aPMod = GetFlatMagicDamageMod(owner);
            SetBuffToolTipVar(2, 15);
        }
        public override void OnUpdateStats()
        {
            float armorBonus = aPMod * 0.15f;
            IncFlatArmorMod(owner, armorBonus);
            SetBuffToolTipVar(1, armorBonus);
        }
        public override void OnUpdateActions()
        {
            aPMod = GetFlatMagicDamageMod(owner);
        }
    }
}