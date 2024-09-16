namespace Buffs
{
    public class ColossalStrength : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Colossal Strength",
            BuffTextureName = "Minotaur_ColossalStrength.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int lastF1; // UNUSED
        float lastTimeExecuted;
        int[] effect0 = { 10, 11, 12, 12, 13, 14, 15, 15, 16, 17, 18, 18, 19, 20, 21, 21, 22, 23 };
        public override void OnActivate()
        {
            lastF1 = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(5, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float damageByRank = effect0[level - 1];
                float totalAttackDamage = GetTotalAttackDamage(owner);
                float baseAttackDamage = GetBaseAttackDamage(owner);
                float abilityPower = GetFlatMagicDamageMod(owner);
                float bonusAttackDamage = totalAttackDamage - baseAttackDamage;
                float attackDamageToAdd = bonusAttackDamage * 0;
                float abilityPowerToAdd = abilityPower * 0.1f;
                float damageToDeal = damageByRank + abilityPowerToAdd;
                float currentDamage = damageToDeal + attackDamageToAdd;
                SetBuffToolTipVar(1, currentDamage);
                SetBuffToolTipVar(2, damageByRank);
                SetBuffToolTipVar(3, abilityPowerToAdd);
            }
        }
    }
}