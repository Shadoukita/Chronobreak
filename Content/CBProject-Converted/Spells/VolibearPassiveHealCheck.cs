namespace Buffs
{
    public class VolibearPassiveHealCheck : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "Blitzcrank_ManaBarrier.dds",
            NonDispellable = true,
            OnPreDamagePriority = 8,
            PersistsThroughDeath = true,
        };
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            float maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float remainingHealth = currentHealth - damageAmount;
            float percentHealthRemaining = remainingHealth / maxHealth;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VolibearPassiveCD)) == 0 && percentHealthRemaining <= 0.3f)
            {
                float duration = 6;
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VolibearPassiveHeal)) > 0)
                {
                    float remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.VolibearPassiveHeal));
                    duration += remainingDuration;
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearPassiveHeal(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearPassiveCD(), 1, 1, 120, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.VolibearPassiveHealCheck), (ObjAIBase)owner, 0);
            }
        }
    }
}