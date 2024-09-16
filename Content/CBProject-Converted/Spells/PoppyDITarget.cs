namespace Buffs
{
    public class PoppyDITarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DiplomaticImmunity_tar.troy", },
            BuffName = "PoppyDITarget",
            BuffTextureName = "Poppy_DiplomaticImmunity.dds",
            NonDispellable = true,
        };
        int[] effect0 = { 6, 7, 8 };
        public override void OnActivate()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetSlotSpellLevel(caster, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            AddBuff((ObjAIBase)owner, caster, new Buffs.PoppyDiplomaticImmunity(), 1, 1, effect0[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (GetBuffCountFromCaster(caster, owner, nameof(Buffs.PoppyDiplomaticImmunity)) > 0)
            {
                SpellBuffRemove(caster, nameof(Buffs.PoppyDiplomaticImmunity), (ObjAIBase)owner);
            }
        }
    }
}