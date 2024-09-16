namespace Buffs
{
    public class PoppyDITargetDmg : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyDiplomaticImmunityDmg",
            BuffTextureName = "Poppy_DiplomaticImmunity.dds",
        };
        float[] effect0 = { 1.2f, 1.3f, 1.4f };
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.PoppyDITarget)) > 0)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float levelMultiplier = effect0[level - 1];
                damageAmount *= levelMultiplier;
            }
        }
    }
}