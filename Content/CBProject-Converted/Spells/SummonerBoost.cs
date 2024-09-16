namespace Spells
{
    public class SummonerBoost : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void UpdateTooltip(int spellSlot)
        {
            float baseCooldown = 210;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            if (avatarVars.DefensiveMastery == 1)
            {
                SetSpellToolTipVar(4, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            }
            else
            {
                SetSpellToolTipVar(3, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            }
        }
        public override float AdjustCooldown()
        {
            float baseCooldown = 210;
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
            SpellEffectCreate(out _, out _, "Summoner_Boost.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffClear(target, nameof(Buffs.SummonerExhaust));
            SpellBuffClear(target, nameof(Buffs.ExhaustSlow));
            SpellBuffClear(target, nameof(Buffs.ExhaustDebuff));
            SpellBuffClear(target, nameof(Buffs.SummonerDot));
            if (avatarVars.DefensiveMastery == 1)
            {
                AddBuff(attacker, target, new Buffs.SummonerBoost(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.SummonerBoost(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class SummonerBoost : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Cleanse",
            BuffTextureName = "Summoner_boost.dds",
        };
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.SNARE)
                {
                    duration *= 0.35f;
                }
                if (type == BuffType.SLOW)
                {
                    duration *= 0.35f;
                }
                if (type == BuffType.FEAR)
                {
                    duration *= 0.35f;
                }
                if (type == BuffType.CHARM)
                {
                    duration *= 0.35f;
                }
                if (type == BuffType.SLEEP)
                {
                    duration *= 0.35f;
                }
                if (type == BuffType.STUN)
                {
                    duration *= 0.35f;
                }
                if (type == BuffType.TAUNT)
                {
                    duration *= 0.35f;
                }
                if (type == BuffType.BLIND)
                {
                    duration *= 0.35f;
                }
                if (type == BuffType.SILENCE)
                {
                    duration *= 0.35f;
                }
                duration = Math.Max(0.3f, duration);
            }
            return returnValue;
        }
    }
}