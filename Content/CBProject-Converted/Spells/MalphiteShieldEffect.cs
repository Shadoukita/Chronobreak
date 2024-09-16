namespace Buffs
{
    public class MalphiteShieldEffect : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "MalphiteObduracyEffect",
            BuffTextureName = "Malphite_GraniteShield.dds",
            NonDispellable = true,
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldHealth;
        float lastTimeExecuted;
        float oldArmorAmount;
        public override void OnActivate()
        {
            float hPPool = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            shieldHealth = 0.1f * hPPool;
            SpellBuffRemove(owner, nameof(Buffs.MalphiteShieldRemoval), (ObjAIBase)owner);
            IncreaseShield(owner, shieldHealth, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MalphiteShieldRemoval(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (shieldHealth > 0)
            {
                RemoveShield(owner, shieldHealth, true, true);
            }
        }
        public override void OnUpdateActions()
        {
            if (shieldHealth <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MalphiteShieldBeenHit)) == 0)
                {
                    oldArmorAmount = shieldHealth;
                    float hPPool = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                    shieldHealth = 0.1f * hPPool;
                    oldArmorAmount = shieldHealth - oldArmorAmount;
                    ModifyShield(owner, oldArmorAmount, true, true, true);
                }
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = shieldHealth;
            if (shieldHealth >= damageAmount)
            {
                shieldHealth -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= shieldHealth;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= shieldHealth;
                shieldHealth = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.MalphiteShieldBeenHit(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}