namespace Buffs
{
    public class ManaBarrierIcon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ManaBarrierIcon",
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
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ManaBarrier)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ManaBarrierCooldown)) == 0 && percentHealthRemaining <= 0.2f)
            {
                float nextBuffVars_ManaShield;
                float nextBuffVars_amountToSubtract;
                float twentyPercentHealth = 0.2f * maxHealth;
                float damageToLetThrough = currentHealth - twentyPercentHealth;
                float damageToBlock = damageAmount - damageToLetThrough;
                float curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                float manaShield = curMana * 0.5f;
                if (manaShield >= damageToBlock)
                {
                    nextBuffVars_ManaShield = manaShield;
                    nextBuffVars_amountToSubtract = damageToBlock;
                    damageAmount -= damageToBlock;
                    SpellEffectCreate(out _, out _, "SteamGolemShield_hit.troy", default, TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.ManaBarrier(nextBuffVars_ManaShield, nextBuffVars_amountToSubtract), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    damageAmount -= manaShield;
                    SpellEffectCreate(out _, out _, "SteamGolemShield_hit.troy", default, TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.ManaBarrierCooldown(), 1, 1, 60, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}