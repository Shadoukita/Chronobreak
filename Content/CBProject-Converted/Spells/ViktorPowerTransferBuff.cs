namespace Buffs
{
    public class ViktorPowerTransferBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "spine", },
            AutoBuffActivateEffect = new[] { "HexMageTEMPQDEBUFF.troy", },
            BuffName = "Haste",
            BuffTextureName = "Averdrian_ConsumeSpirit.dds",
        };
        float[] effect0 = { -0.03f, -0.06f, -0.09f, -0.12f, -0.15f };
        public override void OnUpdateStats()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetSlotSpellLevel(caster, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float percMaxHealthMod = effect0[level - 1];
            IncPercentHPPoolMod(owner, percMaxHealthMod);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            ObjAIBase other3 = GetBuffCasterUnit();
            Vector3 targetPos = GetUnitPosition(other3); // UNUSED
            Vector3 ownerPos = GetUnitPosition(owner);
            SpellCast(other3, other3, default, default, 2, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, ownerPos);
        }
    }
}