namespace Buffs
{
    public class StrengthOfSpirit : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float multiplier;
        float hpRegen;
        public StrengthOfSpirit(float multiplier = default)
        {
            this.multiplier = multiplier;
        }
        public override void OnActivate()
        {
            hpRegen = 0;
            //RequireVar(this.multiplier);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPRegenMod(owner, hpRegen);
        }
        public override void OnUpdateActions()
        {
            float maxMana = GetMaxPAR(target, PrimaryAbilityResourceType.MANA);
            hpRegen = multiplier * maxMana;
        }
    }
}