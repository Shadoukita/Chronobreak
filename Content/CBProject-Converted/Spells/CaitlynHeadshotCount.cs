namespace Buffs
{
    public class CaitlynHeadshotCount : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CaitlynHeadshotCount",
            BuffTextureName = "Caitlyn_Headshot.dds",
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.CaitlynHeadshotCount));
            if (count >= charVars.TooltipAmount)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CaitlynHeadshot(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.CaitlynHeadshotCount), 0);
            }
        }
    }
}