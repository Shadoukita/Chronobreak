namespace Buffs
{
    public class OdinScoreManager : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float totalDamageOT;
        float lastTimeExecuted;
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team == attacker.Team && attacker is Champion && attacker != owner)
            {
                if (type == BuffType.COMBAT_ENCHANCER)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.OdinScoreAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinScoreLowHP)) > 0)
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinScoreArchAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                if (type == BuffType.HASTE)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.OdinScoreAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinScoreLowHP)) > 0)
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinScoreArchAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                if (type == BuffType.INVULNERABILITY)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.OdinScoreAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinScoreLowHP)) > 0)
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinScoreArchAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                if (type == BuffType.HEAL)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.OdinScoreAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinScoreLowHP)) > 0)
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinScoreArchAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                if (type == BuffType.PHYSICAL_IMMUNITY)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.OdinScoreAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinScoreLowHP)) > 0)
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.OdinScoreArchAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
            return returnValue;
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            totalDamageOT += damageAmount;
            float hP_Percent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float damagePercent = totalDamageOT / maxHealth;
            if (hP_Percent <= 0.4f)
            {
                AddBuff((ObjAIBase)owner, attacker, new Buffs.OdinScoreLowHPAttacker(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (damagePercent > 0.2f)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OdinScoreLowHP(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinScoreLowHP)) > 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OdinScoreLowHP(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinScoreAvengerTarget(), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, target, new Buffs.OdinScoreKiller(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if (owner.Team == target.Team && target is Champion && target != owner)
            {
                AddBuff((ObjAIBase)owner, target, new Buffs.OdinScoreAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinScoreLowHP)) > 0)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.OdinScoreArchAngel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            totalDamageOT = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                totalDamageOT *= 0.9f;
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            totalDamageOT = 0;
        }
    }
}