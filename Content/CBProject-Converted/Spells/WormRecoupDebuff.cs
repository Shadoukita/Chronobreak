namespace Buffs
{
    public class WormRecoupDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WormRecoupDebuff",
            BuffTextureName = "1031_Chain_Vest.dds",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.WormRecouperate1), (ObjAIBase)owner);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecoupDebuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}