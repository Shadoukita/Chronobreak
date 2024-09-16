namespace Spells
{
    public class WriggleLanternWard : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class WriggleLanternWard : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ICU.troy", },
            BuffName = "WriggleLanternWard",
            BuffTextureName = "1020_Glowing_Orb.dds",
        };
        Region bubbleID;
        float lastTimeExecuted;
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (scriptName == nameof(Buffs.GlobalWallPush))
                {
                    returnValue = false;
                }
                else if (type == BuffType.FEAR)
                {
                    returnValue = false;
                }
                else if (type == BuffType.CHARM)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SILENCE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLEEP)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLOW)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SNARE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else if (type == BuffType.TAUNT)
                {
                    returnValue = false;
                }
                else if (type == BuffType.BLIND)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SUPPRESSION)
                {
                    returnValue = false;
                }
                else if (type == BuffType.COMBAT_DEHANCER)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId casterID = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(casterID, 1100, owner, 180, default, default, false);
            SetForceRenderParticles(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            ApplyDamage((ObjAIBase)owner, owner, 600, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            if (lifeTime >= 2)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Stealth(), 1, 1, 600, BuffAddType.RENEW_EXISTING, BuffType.INVISIBILITY, 0, true, false, true);
            }
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                IncPAR(owner, -1, PrimaryAbilityResourceType.Shield);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageAmount >= 1 && attacker is not BaseTurret)
            {
                if (damageSource != DamageSource.DAMAGE_SOURCE_ATTACK)
                {
                    damageAmount = 0;
                }
                else
                {
                    damageAmount = 1;
                }
            }
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            return returnValue;
        }
    }
}