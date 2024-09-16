namespace Spells
{
    public class AhriSeduceMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 90, 120, 150, 180 };
        float[] effect1 = { 1, 1.25f, 1.5f, 1.75f, 2 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_SlowPercent;
            float nextBuffVars_DrainPercent;
            bool nextBuffVars_DrainedBool;
            TeamId teamID = GetTeamID_CS(owner);
            bool isStealthed = GetStealthed(target);
            float damageAmount = effect0[level - 1];
            float tauntLength = effect1[level - 1];
            if (IsInFront(target, attacker))
            {
                nextBuffVars_SlowPercent = -0.5f;
            }
            else
            {
                nextBuffVars_SlowPercent = -0.8f;
            }
            if (!isStealthed)
            {
                BreakSpellShields(target);
                if (charVars.SeduceIsActive == 1)
                {
                    SpellEffectCreate(out _, out _, "Ahri_PassiveHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, false, false, false, false);
                    SpellEffectCreate(out _, out _, "Ahri_passive_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                    nextBuffVars_DrainPercent = 0.35f;
                    nextBuffVars_DrainedBool = false;
                    AddBuff(attacker, attacker, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.35f, 1, false, false, attacker);
                    charVars.SeduceIsActive = 0;
                    SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.AhriSoulCrusher), 1);
                }
                else
                {
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) == 0)
                    {
                        AddBuff(attacker, attacker, new Buffs.AhriSoulCrusherCounter(), 9, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                    }
                    ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.35f, 1, false, false, attacker);
                }
                SpellEffectCreate(out _, out _, "Ahri_Charm_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                AddBuff(attacker, target, new Buffs.AhriSeduce(nextBuffVars_SlowPercent), 1, 1, tauntLength, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                ApplyTaunt(attacker, target, tauntLength);
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if (target is Champion)
                {
                    BreakSpellShields(target);
                    if (charVars.SeduceIsActive == 1)
                    {
                        SpellEffectCreate(out _, out _, "Ahri_PassiveHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, false, false, false, false);
                        SpellEffectCreate(out _, out _, "Ahri_passive_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                        nextBuffVars_DrainPercent = 0.35f;
                        nextBuffVars_DrainedBool = false;
                        AddBuff(attacker, attacker, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.35f, 1, false, false, attacker);
                        charVars.SeduceIsActive = 0;
                        SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.AhriSoulCrusher), 1);
                    }
                    else
                    {
                        if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) == 0)
                        {
                            AddBuff(attacker, attacker, new Buffs.AhriSoulCrusherCounter(), 9, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                        }
                        ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.35f, 1, false, false, attacker);
                    }
                    SpellEffectCreate(out _, out _, "Ahri_Charm_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                    AddBuff(attacker, target, new Buffs.AhriSeduce(nextBuffVars_SlowPercent), 1, 1, tauntLength, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    ApplyTaunt(attacker, target, tauntLength);
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        BreakSpellShields(target);
                        if (charVars.SeduceIsActive == 1)
                        {
                            SpellEffectCreate(out _, out _, "Ahri_PassiveHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, false, false, false, false);
                            SpellEffectCreate(out _, out _, "Ahri_passive_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                            nextBuffVars_DrainPercent = 0.35f;
                            nextBuffVars_DrainedBool = false;
                            AddBuff(attacker, attacker, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.35f, 1, false, false, attacker);
                            charVars.SeduceIsActive = 0;
                            SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.AhriSoulCrusher), 1);
                        }
                        else
                        {
                            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) == 0)
                            {
                                AddBuff(attacker, attacker, new Buffs.AhriSoulCrusherCounter(), 9, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                            }
                            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.35f, 1, false, false, attacker);
                        }
                        SpellEffectCreate(out _, out _, "Ahri_Charm_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                        AddBuff(attacker, target, new Buffs.AhriSeduce(nextBuffVars_SlowPercent), 1, 1, tauntLength, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                        ApplyTaunt(attacker, target, tauntLength);
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
            GetPointByUnitFacingOffset(owner, 0, 0);
        }
    }
}
namespace Buffs
{
    public class AhriSeduceMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "",
            BuffTextureName = "",
        };
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
        }
    }
}