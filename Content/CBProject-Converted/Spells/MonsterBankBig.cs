namespace Buffs
{
    public class MonsterBankBig : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "treasure_chest_gold_sparkle.troy", },
            BuffName = "Monster Bank Big",
            BuffTextureName = "Treasure_Chest.dds",
        };
    }
}