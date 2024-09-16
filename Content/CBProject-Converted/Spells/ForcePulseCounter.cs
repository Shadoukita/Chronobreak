namespace Buffs
{
    public class ForcePulseCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForcePulse",
            BuffTextureName = "Kassadin_ForcePulse_Charging.dds",
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.ForcePulseCounter));
            if (count >= 6)
            {
                AddBuff(attacker, attacker, new Buffs.ForcePulseCanCast(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.ForcePulseCounter), 0);
            }
        }
    }
}