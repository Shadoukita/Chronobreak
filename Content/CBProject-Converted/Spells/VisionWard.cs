namespace Spells
{
    public class VisionWard : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Minion other3 = SpawnMinion("VisionWard", "VisionWard", "idle.lua", targetPos, teamID, true, true, false, false, false, false, 0, true, false, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.SharedWardBuff(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.VisionWard(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ItemPlacementMissile(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetSpell(owner, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ItemPlacementMissile));
            FaceDirection(owner, targetPos);
            if (avatarVars.Scout)
            {
                AddBuff(attacker, other3, new Buffs.MasteryScoutBuff(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SpellCast(owner, default, targetPos, targetPos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class VisionWard : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ICU.troy", },
            BuffName = "Magical Sight Ward",
            BuffTextureName = "096_Eye_of_the_Observer.dds",
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
            bubbleID = AddUnitPerceptionBubble(casterID, 1100, owner, 180, default, default, true);
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