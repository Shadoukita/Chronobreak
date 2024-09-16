namespace Spells
{
    public class XenZhaoComboTarget : SpellScript
    {
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.XenZhaoComboTarget(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class XenZhaoComboTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoComboTarget",
            BuffTextureName = "XinZhao_ThreeTalon.dds",
            SpellToggleSlot = 1,
        };
        EffectEmitter asdf2;
        EffectEmitter asdf1;
        int[] effect0 = { 10, 10, 10, 10, 10 };
        public override void OnActivate()
        {
            CancelAutoAttack(owner, true);
            SpellEffectCreate(out asdf2, out _, "xenZiou_ChainAttack_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, default, default, false);
            SpellEffectCreate(out asdf1, out _, "xenZiou_ChainAttack_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, owner, default, default, false);
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(asdf2);
            SpellEffectRemove(asdf1);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            if (expired)
            {
                float cDMod = GetPercentCooldownMod(owner);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float cooldownByLevel = effect0[level - 1];
                float modulatedCD = 1 + cDMod;
                float trueCD = modulatedCD * cooldownByLevel;
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, trueCD);
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is not BaseTurret && target is ObjAIBase)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SkipNextAutoAttack((ObjAIBase)owner);
                SpellCast((ObjAIBase)owner, target, target.Position3D, target.Position3D, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, true, false);
            }
        }
    }
}