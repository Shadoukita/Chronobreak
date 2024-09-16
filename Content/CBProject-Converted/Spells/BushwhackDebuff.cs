namespace Spells
{
    public class BushwhackDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class BushwhackDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Bushwhack",
            BuffTextureName = "NIdalee_Bushwhack.dds",
        };
        float debuff;
        public BushwhackDebuff(float debuff = default)
        {
            this.debuff = debuff;
        }
        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
            IncPercentArmorMod(owner, debuff);
            IncPercentSpellBlockMod(owner, debuff);
            float tooltipDebuff = debuff * -100;
            SetBuffToolTipVar(1, tooltipDebuff);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, debuff);
            IncPercentSpellBlockMod(owner, debuff);
        }
    }
}