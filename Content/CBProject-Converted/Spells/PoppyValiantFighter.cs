namespace Buffs
{
    public class PoppyValiantFighter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyValiantFighter",
            BuffTextureName = "Poppy_ValiantFighter.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType != DamageType.DAMAGE_TYPE_TRUE && attacker is not BaseTurret)
            {
                float healthCurrent = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                float damageSoftCap = 0.1f * healthCurrent;
                float damageManipulator = damageAmount;
                if (damageManipulator > damageSoftCap)
                {
                    damageManipulator -= damageSoftCap;
                    damageManipulator *= 0.5f;
                    damageAmount = damageSoftCap + damageManipulator;
                }
            }
        }
    }
}