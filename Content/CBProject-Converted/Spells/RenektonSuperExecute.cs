namespace Spells
{
    public class RenektonSuperExecute : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            IsDamagingSpell = true,
        };
        int[] effect0 = { 5, 15, 25, 35, 45 };
        float[] effect1 = { 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f };
        int[] effect2 = { 1, 1, 1, 1, 1 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float currentFury = 0;
            if (hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if (hitResult == HitResult.HIT_Miss)
            {
                hitResult = HitResult.HIT_Normal;
            }
            float ragePercent = GetPARPercent(owner, PrimaryAbilityResourceType.Other);
            bool furyBonus = false;
            float bonusDamage = effect0[level - 1];
            if (ragePercent >= 0.5f)
            {
                furyBonus = true;
                IncPAR(owner, -50, PrimaryAbilityResourceType.Other);
                currentFury = GetPAR(owner, PrimaryAbilityResourceType.Other);
            }
            BreakSpellShields(target);
            if (furyBonus)
            {
                AddBuff(owner, owner, new Buffs.RenektonUnlockAnimation(), 1, 1, 0.51f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.RenektonUnlockAnimation(), 1, 1, 0.3f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusPercent = effect1[level - 1];
            baseDamage *= bonusPercent;
            baseDamage += bonusDamage;
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, effect2[level - 1], 0, bonusPercent, false, true, attacker);
            if (target is ObjAIBase)
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, effect2[level - 1], 0, bonusPercent, false, true, attacker);
                if (!furyBonus)
                {
                    ApplyStun(attacker, target, 0.75f);
                }
            }
            if (furyBonus)
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, effect2[level - 1], 0, bonusPercent, false, true, attacker);
                SpellBuffClear(owner, nameof(Buffs.RenektonRageReady));
            }
            if (furyBonus)
            {
                float postFury = GetPAR(owner, PrimaryAbilityResourceType.Other);
                float furyCost = currentFury - postFury;
                IncPAR(owner, furyCost, PrimaryAbilityResourceType.Other);
                ApplyStun(attacker, target, 1.5f);
            }
            AddBuff(owner, owner, new Buffs.RenektonWeaponGlowFade(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetDodgePiercing(owner, false);
        }
    }
}