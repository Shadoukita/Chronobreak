namespace Buffs
{
    public class BrandScorchCount : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CaitlynHeadshotCount",
            BuffTextureName = "Caitlyn_Headshot.dds",
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.BrandScorchCount));
            if (count >= 7)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BrandScorch(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.BrandScorchCount), 0);
            }
        }
    }
}