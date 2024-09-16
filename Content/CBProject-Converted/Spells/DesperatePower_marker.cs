namespace Buffs
{
    public class DesperatePower_marker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Desperate Power",
            BuffTextureName = "Ryze_DesperatePower.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = { 40, 40, 40, 40, 40, 40, 80, 80, 80, 80, 80, 80, 120, 120, 120, 120, 120, 120 };
        public override void OnActivate()
        {
            lastTooltip = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float tooltipAmount = effect0[level - 1];
                if (tooltipAmount > lastTooltip)
                {
                    lastTooltip = tooltipAmount;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DesperatePower)) == 0)
            {
                float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if (healthPercent <= 0.4f)
                {
                    object nextBuffVars_AddSpellDamage = charVars.AddSpellDamage; // UNUSED
                    AddBuff((ObjAIBase)owner, owner, new Buffs.DesperatePower(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0.75f);
                }
            }
        }
    }
}