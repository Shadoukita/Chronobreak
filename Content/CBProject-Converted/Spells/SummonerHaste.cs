namespace Spells
{
    public class SummonerHaste : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void UpdateTooltip(int spellSlot)
        {
            float moveSpeedMod;
            if (avatarVars.OffensiveMastery == 1)
            {
                moveSpeedMod = 0.35f;
            }
            else
            {
                moveSpeedMod = 0.27f;
            }
            moveSpeedMod *= 100;
            SetSpellToolTipVar(moveSpeedMod, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            float baseCooldown = 210;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
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
            SpellEffectCreate(out _, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            float nextBuffVars_MoveSpeedMod = 0.27f;
            if (avatarVars.OffensiveMastery == 1)
            {
                nextBuffVars_MoveSpeedMod = 0.35f;
            }
            AddBuff(owner, target, new Buffs.SummonerHaste(nextBuffVars_MoveSpeedMod), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SummonerHaste : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Haste.troy", },
            BuffName = "Haste",
            BuffTextureName = "Summoner_haste.dds",
        };
        float moveSpeedMod;
        public SummonerHaste(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
            SetGhosted(owner, true);
        }
    }
}