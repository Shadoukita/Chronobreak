namespace Spells
{
    public class XerathArcaneBarrageWrapperExt : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            SpellCast(owner, default, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.XerathArcaneBarrageBarrage)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.XerathArcaneBarrageBarrage), owner, 0);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.XerathArcaneBarrageBarrage)) > 0)
                {
                    SetSlotSpellCooldownTimeVer2(0.6f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                }
            }
            else
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.XerathArcaneBarrageWrapper)) == 0)
                {
                    SetSlotSpellCooldownTimeVer2(0.6f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                    AddBuff(owner, owner, new Buffs.XerathArcaneBarrageBarrage(), 3, 2, 12, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    AddBuff(owner, owner, new Buffs.XerathArcaneBarrageWrapper(), 1, 1, 15, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}