namespace Spells
{
    public class KennenLRCancel : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            SetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.5f);
            SpellBuffRemove(owner, nameof(Buffs.KennenLightningRush), owner);
            SpellBuffRemove(owner, nameof(Buffs.KennenLightningRushDamage), owner);
        }
    }
}
namespace Buffs
{
    public class KennenLRCancel : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            SpellToggleSlot = 3,
        };
    }
}