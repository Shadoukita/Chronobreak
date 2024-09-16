namespace Buffs
{
    public class XenZhaoComboAutoFinish : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoComboAutoFinish",
            BuffTextureName = "XinZhao_ThreeTalon.dds",
        };
        EffectEmitter asdf2;
        EffectEmitter asdf1;
        int[] effect0 = { 10, 10, 10, 10, 10 };
        public override void OnActivate()
        {
            SpellEffectCreate(out asdf2, out _, "xenZiou_ChainAttack_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, default, default, false, default, default, false);
            SpellEffectCreate(out asdf1, out _, "xenZiou_ChainAttack_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, owner, default, default, false, default, default, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(asdf1);
            SpellEffectRemove(asdf2);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            float cDMod = GetPercentCooldownMod(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldownByLevel = effect0[level - 1];
            float modulatedCD = 1 + cDMod;
            float trueCD = modulatedCD * cooldownByLevel;
            SetSlotSpellCooldownTimeVer2(trueCD, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is not BaseTurret && target is ObjAIBase)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SkipNextAutoAttack((ObjAIBase)owner);
                SpellCast((ObjAIBase)owner, target, target.Position3D, target.Position3D, 2, SpellSlotType.ExtraSlots, level, false, false, false, false, true, false);
            }
        }
        public override void OnUpdateStats()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}