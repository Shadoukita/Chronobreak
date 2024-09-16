namespace Buffs
{
    public class OdinGuardianRegenManager : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float previousTakeDamageTime;
        int dealtDamage;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            previousTakeDamageTime = GetGameTime();
            dealtDamage = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float currentTime = GetGameTime();
                float timePassed = currentTime - previousTakeDamageTime;
                if (timePassed >= 10)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianRegen(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                    previousTakeDamageTime = currentTime;
                }
                if (dealtDamage == 0)
                {
                    if (timePassed > 0.5f)
                    {
                        dealtDamage = 1;
                        float myMaxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                        float healthToDecreaseBy = 0.6f * myMaxHealth;
                        ApplyDamage((ObjAIBase)owner, owner, healthToDecreaseBy, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
                    }
                }
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.OdinGuardianRegen), 0);
            previousTakeDamageTime = GetGameTime();
        }
    }
}