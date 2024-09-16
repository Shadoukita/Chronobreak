namespace Buffs
{
    public class OdinGuardianUI : BuffScript
    {
        float towerHP;
        public override void OnActivate()
        {
            float towerHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            this.towerHP = towerHP;
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            float percentDamage = damageAmount / towerHP;
            if (percentDamage >= 0.01f)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianUIDamage(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianUIDamageChaos(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            float percentHeal = health / towerHP;
            if (percentHeal > 0.01f)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinGuardianUIDamageOrder(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            return returnValue;
        }
    }
}