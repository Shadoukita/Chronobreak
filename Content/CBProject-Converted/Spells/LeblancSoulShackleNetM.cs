namespace Spells
{
    public class LeblancSoulShackleNetM : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class LeblancSoulShackleNetM : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "leBlanc_shackle_tar_ult.troy", "leBlanc_shackle_tar_blood.troy", },
            BuffName = "LeblancShackleM",
            BuffTextureName = "LeblancConjureChainsM.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
        public override void OnActivate()
        {
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}