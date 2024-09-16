namespace Buffs
{
    public class Pyromania : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pyromania",
            BuffTextureName = "Annie_Brilliance_Charging.dds",
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pyromania));
            if (count >= 5)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Pyromania_particle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.Pyromania), 0);
            }
        }
    }
}