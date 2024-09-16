namespace Buffs
{
    public class OlafAxeThrowDamage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        int[] effect0 = { 50, 90, 130, 170, 210 };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "olaf_axeThrow_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "olaf_axeThrow_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "olaf_axeThrow_tar_03.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float bonusDamage = effect0[level - 1];
            float totalDamage = GetTotalAttackDamage(attacker);
            bool isStealthed = GetStealthed(owner);
            totalDamage *= 0.5f;
            float damageToDeal = bonusDamage + totalDamage;
            if (!isStealthed)
            {
                ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else if (owner is Champion)
            {
                ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else
            {
                bool canSee = CanSeeTarget(attacker, owner);
                if (canSee)
                {
                    ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
            }
        }
    }
}