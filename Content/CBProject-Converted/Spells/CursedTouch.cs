namespace Buffs
{
    public class CursedTouch : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CursedTouch",
            BuffTextureName = "SadMummy_CorpseExplosion.dds",
        };
        float magicResistReduction;
        public CursedTouch(float magicResistReduction = default)
        {
            this.magicResistReduction = magicResistReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.magicResistReduction);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, magicResistReduction);
        }
    }
}