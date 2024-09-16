namespace Buffs
{
    public class AkaliTwinDmg : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_hand", "", "", },
            AutoBuffActivateEffect = new[] { "akali_twinDisciplines_DMG_buf.troy", "", "", },
            BuffName = "AkaliTwinDmg",
            BuffTextureName = "AkaliTwinDisciplines.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float akaliDmg;
        float baseVampPercent;
        float additionalVampPercent;
        float vampPercentTooltip;
        float lastTimeExecuted;
        public AkaliTwinDmg(float akaliDmg = default)
        {
            this.akaliDmg = akaliDmg;
        }
        public override void OnActivate()
        {
            //RequireVar(this.akaliDmg);
            baseVampPercent = 0.08f;
            akaliDmg -= 10;
            additionalVampPercent = akaliDmg / 600;
            charVars.VampPercent = baseVampPercent + additionalVampPercent;
            vampPercentTooltip = 100 * charVars.VampPercent;
            SetBuffToolTipVar(1, vampPercentTooltip);
        }
        public override void OnUpdateStats()
        {
            IncPercentSpellVampMod(owner, charVars.VampPercent);
        }
        public override void OnUpdateActions()
        {
            akaliDmg = GetFlatPhysicalDamageMod(owner);
            akaliDmg -= 10;
            additionalVampPercent = akaliDmg / 600;
            charVars.VampPercent = baseVampPercent + additionalVampPercent;
            if (ExecutePeriodically(2, ref lastTimeExecuted, false))
            {
                vampPercentTooltip = 100 * charVars.VampPercent;
                SetBuffToolTipVar(1, vampPercentTooltip);
            }
        }
    }
}