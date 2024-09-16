namespace Buffs
{
    public class VladimirBloodGorged : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VladimirBloodGorged",
            BuffTextureName = "Vladimir_BloodGorged.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float hPMod;
        float aPMod;
        public override void OnActivate()
        {
            hPMod = 0;
            aPMod = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, aPMod);
            IncMaxHealth(owner, hPMod, false);
            SetBuffToolTipVar(2, hPMod);
            SetBuffToolTipVar(1, aPMod);
        }
        public override void OnUpdateActions()
        {
            float currentHP = GetFlatHPPoolMod(owner);
            float currentAP = GetFlatMagicDamageMod(owner);
            currentAP -= aPMod;
            aPMod = currentHP * 0.025f;
            hPMod = currentAP * 1.4f;
        }
    }
}