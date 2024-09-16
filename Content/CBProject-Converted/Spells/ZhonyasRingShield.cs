namespace Buffs
{
    public class ZhonyasRingShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "head", "head", },
            AutoBuffActivateEffect = new[] { "", "zhonyas_cylinder.troy", "zhonya_ring_self_skin.troy", "zhonyas_ring_activate.troy", },
            BuffName = "Zhonyas Ring",
            BuffTextureName = "3157_Zhonyas_Hourglass.dds",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            PauseAnimation(owner, true);
            DestroyMissileForTarget(owner);
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
            SetStunned(owner, true);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            SetTargetable(owner, true);
            SetStunned(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            PauseAnimation(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetInvulnerable(owner, true);
            SetStunned(owner, true);
            SetTargetable(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
        }
    }
}