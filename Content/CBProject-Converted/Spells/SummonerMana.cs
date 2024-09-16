namespace Spells
{
    public class SummonerMana : SpellScript
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
            float bonusMana = ownerLevel * 30;
            float totalMana = bonusMana + 160;
            if (avatarVars.UtilityMastery == 1)
            {
                totalMana *= 1.2f;
            }
            float secondaryMana = totalMana * 0.5f;
            SetSpellToolTipVar(totalMana, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            SetSpellToolTipVar(secondaryMana, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            float baseCooldown = 180;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 3, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
        }
        public override float AdjustCooldown()
        {
            float baseCooldown = 180;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            return baseCooldown;
        }
        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            int ownerLevel = GetLevel(owner);
            float bonusMana = ownerLevel * 30;
            float totalMana = bonusMana + 160;
            if (avatarVars.UtilityMastery == 1)
            {
                totalMana *= 1.2f;
            }
            float secondaryMana = totalMana * 0.5f;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 600, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out _, out _, "Summoner_Mana.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, false, false, false, false);
                if (unit == owner)
                {
                    IncPAR(unit, totalMana, PrimaryAbilityResourceType.MANA);
                }
                else
                {
                    IncPAR(unit, secondaryMana, PrimaryAbilityResourceType.MANA);
                }
            }
        }
    }
}