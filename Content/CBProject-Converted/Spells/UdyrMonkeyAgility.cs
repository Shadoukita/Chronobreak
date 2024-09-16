namespace Spells
{
    public class UdyrMonkeyAgility : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class UdyrMonkeyAgility : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrPassiveBuff",
            BuffTextureName = "BlindMonk_FistsOfFury.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrMonkeyAgilityBuff(), 3, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
    }
}