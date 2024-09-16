namespace Spells
{
    public class SummonerHeal : SpellScript
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
            float bonusHeal = ownerLevel * 25;
            float totalHeal = bonusHeal + 140;
            if (avatarVars.DefensiveMastery == 1)
            {
                totalHeal *= 1.1f;
            }
            float secondaryHeal = totalHeal * 0.5f;
            SetSpellToolTipVar(totalHeal, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            SetSpellToolTipVar(secondaryHeal, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            float baseCooldown = 270;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 3, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
        }
        public override float AdjustCooldown()
        {
            float baseCooldown = 270;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            return baseCooldown;
        }
        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int ownerLevel = GetLevel(owner);
            float bonusHeal = ownerLevel * 25;
            float totalHeal = bonusHeal + 140;
            if (avatarVars.DefensiveMastery == 1)
            {
                totalHeal *= 1.1f;
            }
            float secondaryHeal = totalHeal * 0.5f;
            if (GetBuffCountFromCaster(target, target, nameof(Buffs.SummonerHealCheck)) > 0)
            {
                if (target == owner)
                {
                    IncHealth(target, totalHeal, owner);
                }
                else
                {
                    secondaryHeal *= 0.5f;
                    IncHealth(target, secondaryHeal, owner);
                    ApplyAssistMarker(attacker, target, 10);
                }
            }
            else
            {
                if (target == owner)
                {
                    IncHealth(target, totalHeal, owner);
                }
                else
                {
                    IncHealth(target, secondaryHeal, owner);
                    ApplyAssistMarker(attacker, target, 10);
                }
            }
            if (GetBuffCountFromCaster(target, target, nameof(Buffs.SummonerHealCheck)) == 0)
            {
                AddBuff((ObjAIBase)target, target, new Buffs.SummonerHealCheck(), 1, 1, 25, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}