namespace Spells
{
    public class YorickActiveRavenous : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
    }
}
namespace Buffs
{
    public class YorickActiveRavenous : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_buffbone_glb_hand_loc", "r_buffbone_glb_hand_loc", "c_buffbone_glb_center_loc", },
            AutoBuffActivateEffect = new[] { "yorick_ravenousGhoul_self_buf.troy", "yorick_ravenousGhoul_self_buf.troy", "yorick_ravenousGhoul_self_buf_spirits.troy", },
            BuffTextureName = "YorickRavenousPH.dds",
        };
        float lifestealPercent;
        public YorickActiveRavenous(float lifestealPercent = default)
        {
            this.lifestealPercent = lifestealPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifestealPercent);
            IncPercentLifeStealMod(owner, lifestealPercent);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, lifestealPercent);
        }
    }
}