namespace Spells
{
    public class EzrealTrueshotBarrage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "CyberEzreal", },
            SpellVOOverrideSkins = new[] { "CyberEzreal", },
        };
        int[] effect0 = { 0, 0, 0, 0, 0 };
        int[] effect1 = { 350, 500, 650 };
        public override void UpdateTooltip(int spellSlot)
        {
            float totalDamage = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusDamage = totalDamage - baseDamage;
            float spell3Display = bonusDamage * 1;
            SetSpellToolTipVar(spell3Display, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void SelfExecute()
        {
            int ownerSkinID = GetSkinID(owner);
            TeamId ownerTeamID = GetTeamID_CS(owner);
            if (ownerSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "Ezreal_PulseFire_Ult_Thrusters.troy", default, ownerTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_Vent_Low_R", default, owner, default, default, true, false, false, false, false);
                SpellEffectCreate(out _, out _, "Ezreal_PulseFire_Ult_Thrusters.troy", default, ownerTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_Vent_Low_L", default, owner, default, default, true, false, false, false, false);
            }
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            float percentOfAttack = charVars.PercentOfAttack;
            float totalDamage = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusDamage = totalDamage - baseDamage;
            bonusDamage *= 1;
            SpellEffectCreate(out _, out _, "Ezreal_TrueShot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, false, false, false, false);
            AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + effect0[level - 1], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            float physPreMod = GetFlatPhysicalDamageMod(owner);
            float physPostMod = 1 * physPreMod; // UNUSED
            float aPPreMod = GetFlatMagicDamageMod(owner);
            float aPPostMod = 0.9f * aPPreMod; // UNUSED
            ApplyDamage(owner, target, bonusDamage + effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0.9f, 1, false, false, attacker);
            charVars.PercentOfAttack *= 0.92f;
            charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.3f);
        }
    }
}