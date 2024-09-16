namespace Buffs
{
    public class RenektonPredator : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "RenektonPredator",
            BuffTextureName = "Renekton_Predator.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int[] effect0 = { 5, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 10, 15, 15, 15, 15, 15, 15, 15 };
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            return returnValue;
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonInCombat)) == 0)
                {
                    IncPAR(owner, -2, PrimaryAbilityResourceType.Other);
                }
                int level = GetLevel(owner);
                float offensiveGain = effect0[level - 1];
                SetBuffToolTipVar(1, offensiveGain);
            }
            float rageCount = GetPAR(owner, PrimaryAbilityResourceType.Other);
            if (rageCount >= 50)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonRageReady(), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                SetPARColorOverride(owner, 255, 0, 0, 255, 175, 0, 0, 255);
            }
            else
            {
                SetPARColorOverride(owner, 255, 85, 85, 85, 175, 55, 55, 55);
                ClearPARColorOverride(owner);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is not BaseTurret && target is ObjAIBase)
            {
                IncPAR(owner, 5, PrimaryAbilityResourceType.Other);
                float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if (healthPercent <= charVars.RageThreshold)
                {
                    IncPAR(owner, 2.5f, PrimaryAbilityResourceType.Other);
                }
            }
        }
    }
}