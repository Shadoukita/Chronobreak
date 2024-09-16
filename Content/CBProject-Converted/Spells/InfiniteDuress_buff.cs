namespace Spells
{
    public class InfiniteDuress_buff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChannelDuration = 3f,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class InfiniteDuress_buff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_hand", "R_hand", },
            AutoBuffActivateEffect = new[] { "Enrage_buf.troy", "Enrage_buf.troy", },
        };
    }
}