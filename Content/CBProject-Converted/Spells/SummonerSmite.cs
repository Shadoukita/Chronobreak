namespace Spells
{
    public class SummonerSmite : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void UpdateTooltip(int spellSlot)
        {
            int ownerLevel = GetLevel(owner);
            float bonusDamage = ownerLevel * 25;
            float totalDamage = bonusDamage + 420;
            SetSpellToolTipVar(totalDamage, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            float baseCooldown = 70;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
        }
        public override float AdjustCooldown()
        {
            float baseCooldown = 70;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            return baseCooldown;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            int ownerLevel = GetLevel(owner);
            float bonusDamage = ownerLevel * 25;
            float totalDamage = bonusDamage + 420;
            ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            if (avatarVars.DefensiveMastery == 1)
            {
                IncGold(owner, 10);
            }
        }
    }
}