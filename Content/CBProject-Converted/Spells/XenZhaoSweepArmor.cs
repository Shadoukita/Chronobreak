namespace Buffs
{
    public class XenZhaoSweepArmor : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoSweepArmor",
            BuffTextureName = "XenZhao_CrescentSweepNew.dds",
        };
        float totalArmor;
        EffectEmitter hi;
        public XenZhaoSweepArmor(float totalArmor = default)
        {
            this.totalArmor = totalArmor;
        }
        public override void OnActivate()
        {
            //RequireVar(this.totalArmor);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            int xZSkinID = GetSkinID(owner);
            if (xZSkinID == 3)
            {
                SpellEffectCreate(out hi, out _, "xenZiou_SelfShield_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, default, default, false);
            }
            else
            {
                SpellEffectCreate(out hi, out _, "xenZiou_SelfShield_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CENTER_LOC", default, owner, default, default, false, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(hi);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, totalArmor);
        }
    }
}