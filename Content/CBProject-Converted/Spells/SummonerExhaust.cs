namespace Spells
{
    public class SummonerExhaust : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
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
            SetSpellToolTipVar(baseCooldown, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
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
            float nextBuffVars_MoveSpeedMod = -0.4f;
            AddBuff(attacker, target, new Buffs.ExhaustSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.SummonerExhaust(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            if (avatarVars.OffensiveMastery == 1)
            {
                float nextBuffVars_ArmorMod = -10;
                AddBuff(attacker, target, new Buffs.ExhaustDebuff(nextBuffVars_ArmorMod), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
            }
        }
    }
}
namespace Buffs
{
    public class SummonerExhaust : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, null, "", },
            AutoBuffActivateEffect = new[] { "summoner_banish.troy", "", "", },
            BuffName = "ExhaustDebuff",
            BuffTextureName = "Summoner_Exhaust.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageSource == DamageSource.DAMAGE_SOURCE_ATTACK)
            {
                damageAmount *= 0.3f;
            }
            else if (damageSource != DamageSource.DAMAGE_SOURCE_INTERNALRAW)
            {
                if (damageType != DamageType.DAMAGE_TYPE_TRUE)
                {
                    damageAmount *= 0.65f;
                }
            }
        }
    }
}