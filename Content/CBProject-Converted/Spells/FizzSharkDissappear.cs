namespace Buffs
{
    public class FizzSharkDissappear : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SetNoRender(owner, true);
        }
        public override void OnResurrect()
        {
            SetNoRender(owner, false);
            SpellBuffClear(owner, nameof(Buffs.FizzSharkDissappear));
        }
    }
}