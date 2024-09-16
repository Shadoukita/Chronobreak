namespace Buffs
{
    public class JaxRelentlessAssaultSpeed : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", },
            AutoBuffActivateEffect = new[] { "JaxRelentlessAssault_buf.troy", "", },
            BuffName = "RelentlessBarrier",
            BuffTextureName = "Armsmaster_CoupDeGrace.dds",
        };
        float bonusAP;
        float bonusAD;
        int[] effect0 = { 25, 45, 65 };
        float[] effect1 = { 0.2f, 0.2f, 0.2f };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float bonusADAP = effect0[level - 1];
            float totalAD = GetTotalAttackDamage(owner);
            float baseAD = GetBaseAttackDamage(owner);
            float bonusAD = totalAD - baseAD;
            float bonusAP = GetFlatMagicDamageMod(owner);
            float multiplier = effect1[level - 1];
            bonusAD *= multiplier;
            bonusAP *= multiplier;
            this.bonusAP = bonusAP + bonusADAP;
            this.bonusAD = bonusAD + bonusADAP;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, bonusAP);
            IncFlatPhysicalDamageMod(owner, bonusAD);
        }
    }
}