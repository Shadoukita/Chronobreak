namespace Spells
{
    public class VolibearW : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 80, 125, 170, 215, 260 };
        public override bool CanCast()
        {
            bool returnValue = true;
            returnValue = false;
            int count = GetBuffCountFromAll(owner, nameof(Buffs.VolibearWStats));
            if (count == 3)
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            bool debuffFound = false; // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "VolibearW_tar.troy", "VolibearW_tar.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, teamID, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            BreakSpellShields(target);
            float damage = effect0[level - 1];
            float hPPoolMod = GetFlatHPPoolMod(attacker);
            hPPoolMod *= 0.15f;
            damage += hPPoolMod;
            float maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float currentHP = GetHealth(target, PrimaryAbilityResourceType.MANA);
            float missingHP = maxHP - currentHP;
            float missingHPPerc = missingHP / maxHP;
            missingHPPerc++;
            damage *= missingHPPerc;
            ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class VolibearW : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "VolibearWBuff",
            BuffTextureName = "DrMundo_Masochism.dds",
            PersistsThroughDeath = true,
        };
        float[] effect0 = { 0.08f, 0.11f, 0.14f, 0.17f, 0.2f };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                TeamId teamID = GetTeamID_CS(owner); // UNUSED
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float nextBuffVars_VolibearWAS = effect0[level - 1];
                AddBuff(attacker, attacker, new Buffs.VolibearWStats(nextBuffVars_VolibearWAS), 3, 1, 4.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                int count = GetBuffCountFromAll(attacker, nameof(Buffs.VolibearWStats));
                if (count >= 3)
                {
                    AddBuff(attacker, attacker, new Buffs.VolibearWParticle(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                }
                UpdateCanCast(attacker);
            }
        }
    }
}