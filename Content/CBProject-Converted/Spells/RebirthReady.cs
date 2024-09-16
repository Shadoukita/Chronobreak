namespace Buffs
{
    public class RebirthReady : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RebirthReady",
            BuffTextureName = "Cryophoenix_Rebirth.dds",
            NonDispellable = true,
            OnPreDamagePriority = 8,
            PersistsThroughDeath = true,
        };
        bool willRemove;
        int[] effect0 = { -40, -40, -40, -40, -25, -25, -25, -10, -10, -10, -10, 5, 5, 5, 20, 20, 20, 20 };
        public override void OnActivate()
        {
            int level = GetLevel(owner);
            willRemove = false;
            float rebirthArmorMod = effect0[level - 1];
            SetBuffToolTipVar(1, rebirthArmorMod);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (curHealth <= damageAmount && GetBuffCountFromCaster(owner, owner, nameof(Buffs.WillRevive)) == 0)
            {
                damageAmount = 0;
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Rebirth)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombie)) == 0)
                    {
                        int level = GetLevel(owner);
                        float nextBuffVars_RebirthArmorMod = effect0[level - 1];
                        AddBuff((ObjAIBase)owner, owner, new Buffs.Rebirth(nextBuffVars_RebirthArmorMod), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
                willRemove = true;
            }
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            float rebirthArmorMod = effect0[level - 1];
            SetBuffToolTipVar(1, rebirthArmorMod);
        }
    }
}