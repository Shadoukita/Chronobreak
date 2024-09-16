namespace Buffs
{
    public class SonaPowerChordCount : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaPowerChordCount",
            BuffTextureName = "Sona_PowerChord.dds",
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChordCount));
            if (count >= 3)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SonaPowerChord(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.SonaPowerChordCount), 0);
            }
        }
    }
}