namespace Spells
{
    public class LuxLightBindingMis : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 110, 160, 210, 260 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            bool canSee;
            float distance = DistanceBetweenObjects(owner, target); // UNUSED
            bool isStealthed = GetStealthed(target);
            float damageAmount = effect0[level - 1];
            float stunLength = 2;
            float halfDamage = damageAmount * 0.5f;
            float halfSnare = stunLength * 0.5f;
            if (!charVars.FirstTargetHit)
            {
                if (!isStealthed)
                {
                    ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                    AddBuff(attacker, target, new Buffs.LuxLightBindingMis(), 1, 1, stunLength, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                    charVars.FirstTargetHit = true;
                    if (target is not BaseTurret)
                    {
                        AddBuff(owner, target, new Buffs.LuxIlluminatingFraulein(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
                    }
                }
                else
                {
                    if (target is Champion)
                    {
                        ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                        AddBuff(attacker, target, new Buffs.LuxLightBindingMis(), 1, 1, stunLength, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                        charVars.FirstTargetHit = true;
                        if (target is not BaseTurret)
                        {
                            AddBuff(owner, target, new Buffs.LuxIlluminatingFraulein(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
                        }
                    }
                    else
                    {
                        canSee = CanSeeTarget(owner, target);
                        if (canSee)
                        {
                            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                            AddBuff(attacker, target, new Buffs.LuxLightBindingMis(), 1, 1, stunLength, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                            charVars.FirstTargetHit = true;
                            if (target is not BaseTurret)
                            {
                                AddBuff(owner, target, new Buffs.LuxIlluminatingFraulein(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
                            }
                        }
                    }
                }
            }
            else
            {
                if (!isStealthed)
                {
                    ApplyDamage(attacker, target, halfDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false, attacker);
                    AddBuff(attacker, target, new Buffs.LuxLightBinding(), 1, 1, halfSnare, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                    if (target is not BaseTurret)
                    {
                        AddBuff(owner, target, new Buffs.LuxIlluminatingFraulein(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
                    }
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    if (target is Champion)
                    {
                        ApplyDamage(attacker, target, halfDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false, attacker);
                        AddBuff(attacker, target, new Buffs.LuxLightBinding(), 1, 1, halfSnare, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                        if (target is not BaseTurret)
                        {
                            AddBuff(owner, target, new Buffs.LuxIlluminatingFraulein(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
                        }
                        DestroyMissile(missileNetworkID);
                    }
                    else
                    {
                        canSee = CanSeeTarget(owner, target);
                        if (canSee)
                        {
                            ApplyDamage(attacker, target, halfDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false, attacker);
                            AddBuff(attacker, target, new Buffs.LuxLightBinding(), 1, 1, halfSnare, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false);
                            if (target is not BaseTurret)
                            {
                                AddBuff(owner, target, new Buffs.LuxIlluminatingFraulein(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
                            }
                            DestroyMissile(missileNetworkID);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class LuxLightBindingMis : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "LuxLightBinding_tar.troy", },
            BuffName = "LuxLightBindingMis",
            BuffTextureName = "LuxCrashingBlitz2.dds",
        };
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}