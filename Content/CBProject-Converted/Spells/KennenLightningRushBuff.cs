namespace Spells
{
    public class KennenLightningRushBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class KennenLightningRushBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "KennenLightningRushBuff",
            BuffTextureName = "Kennen_LightningRush.dds",
            SpellToggleSlot = 3,
        };
        float defenseBonus;
        public KennenLightningRushBuff(float defenseBonus = default)
        {
            this.defenseBonus = defenseBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.defenseBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, defenseBonus);
            IncFlatSpellBlockMod(owner, defenseBonus);
            SetBuffToolTipVar(1, defenseBonus);
        }
    }
}