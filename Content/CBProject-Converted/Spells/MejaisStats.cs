namespace Buffs
{
    public class MejaisStats : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MejaisCap",
            BuffTextureName = "3041_Mejais_Soulstealer.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, 8);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MejaisCheck)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            int count = GetBuffCountFromAll(owner, nameof(Buffs.MejaisStats));
            float aPDisplay = 8 * count;
            SetBuffToolTipVar(1, aPDisplay);
        }
    }
}