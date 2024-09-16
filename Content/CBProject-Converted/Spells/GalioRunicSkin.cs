namespace Buffs
{
    public class GalioRunicSkin : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GalioRunicSkin",
            BuffTextureName = "Galio_RunicSkin.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float totalMR;
        public override void OnActivate()
        {
            totalMR = GetSpellBlock(owner);
        }
        public override void OnUpdateStats()
        {
            float aPMod = totalMR * 0.5f;
            IncFlatMagicDamageMod(owner, aPMod);
            SetBuffToolTipVar(1, aPMod);
        }
        public override void OnUpdateActions()
        {
            totalMR = GetSpellBlock(owner);
        }
    }
}