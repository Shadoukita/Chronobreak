namespace Spells
{
    public class CassiopeiaSecondSkin : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class CassiopeiaSecondSkin : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", },
            AutoBuffActivateEffect = new[] { "Judicator_buf.troy", },
            BuffName = "CassiopeiaSecondSkin",
            BuffTextureName = "Cassiopeia_DeadlyCadence.dds",
            NonDispellable = true,
        };
        float testAmount;
        public override void OnActivate()
        {
            testAmount = charVars.SecondSkin;
            SetBuffToolTipVar(1, charVars.SecondSkin);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, charVars.SecondSkin);
            IncFlatSpellBlockMod(owner, charVars.SecondSkin);
        }
        public override void OnUpdateActions()
        {
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if (healthPercent > 0.5f)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                if (testAmount != charVars.SecondSkinMR)
                {
                    testAmount = charVars.SecondSkin;
                    SetBuffToolTipVar(1, charVars.SecondSkin);
                }
            }
        }
    }
}