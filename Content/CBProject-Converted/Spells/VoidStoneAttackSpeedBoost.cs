namespace Buffs
{
    public class VoidStoneAttackSpeedBoost : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VoidStoneEmpowered",
            BuffTextureName = "Voidwalker_Netherburn.dds",
        };
        float attackSpeedBoost;
        public override void OnActivate()
        {
            attackSpeedBoost = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            attackSpeedBoost = 0;
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedBoost);
            float tooltipAttackSpeed = attackSpeedBoost * 100;
            SetBuffToolTipVar(1, tooltipAttackSpeed);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                float attackSpeedIncrement = damageAmount * 0.0015f;
                attackSpeedBoost += attackSpeedIncrement;
            }
        }
    }
}