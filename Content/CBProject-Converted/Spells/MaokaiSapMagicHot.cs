namespace Buffs
{
    public class MaokaiSapMagicHot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiSapMagicHot",
            BuffTextureName = "Maokai_SapMagic.dds",
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromCaster(owner, default, nameof(Buffs.MaokaiSapMagicHot));
            if (count >= 5)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MaokaiSapMagicMelee(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.MaokaiSapMagicHot), 0);
            }
        }
    }
}