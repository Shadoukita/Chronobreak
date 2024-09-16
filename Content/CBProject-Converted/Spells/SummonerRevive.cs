namespace Spells
{
    public class SummonerRevive : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 220, 240, 260, 280, 300, 320, 340, 360, 380, 400, 420, 440, 460, 480, 500, 520, 540, 560 };
        public override bool CanCast()
        {
            return IsDead(owner);
        }
        public override void UpdateTooltip(int spellSlot)
        {
            int level = GetLevel(owner);
            float healthMod = effect0[level - 1];
            SetSpellToolTipVar(healthMod, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            float baseCooldown = 540;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
        }
        public override float AdjustCooldown()
        {
            float baseCooldown = 540;
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
            float nextBuffVars_MoveSpeedMod;
            SpellEffectCreate(out _, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, target, default, default, false, false, false, false, false);
            ReincarnateHero(owner);
            if (avatarVars.DefensiveMastery == 1)
            {
                nextBuffVars_MoveSpeedMod = 1.25f;
                AddBuff(owner, target, new Buffs.SummonerReviveSpeedBoost(nextBuffVars_MoveSpeedMod), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
            }
            int level = GetLevel(owner);
            float nextBuffVars_HealthMod = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.ReviveMarker(nextBuffVars_HealthMod), 1, 1, 120, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}