namespace Spells
{
    public class KogMawBioArcaneBarrageAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "NewYearDragonKogMaw", },
            SpellVOOverrideSkins = new[] { "NewYearDragonKogMaw", },
        };
        float[] effect0 = { 0.02f, 0.03f, 0.04f, 0.05f, 0.06f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = GetBaseAttackDamage(owner);
            int kMSkinID = GetSkinID(attacker);
            if (target is ObjAIBase)
            {
                if (kMSkinID == 5)
                {
                    SpellEffectCreate(out _, out _, "KogMawChineseBasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "KogMawSpatter.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
            }
            ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            if (target is ObjAIBase && target is not BaseTurret && hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                float abilityPower = GetFlatMagicDamageMod(owner);
                float maxHealthDamage = effect0[level - 1];
                float bonusMaxHealthDamage = 0.0001f * abilityPower;
                float totalMaxHealthDamage = bonusMaxHealthDamage + maxHealthDamage;
                float maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                float damageToApply = maxHealth * totalMaxHealthDamage;
                TeamId teamId = GetTeamID_CS(target);
                if (teamId == TeamId.TEAM_NEUTRAL)
                {
                    damageToApply = Math.Min(100, damageToApply);
                }
                ApplyDamage(attacker, target, damageToApply, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
    }
}