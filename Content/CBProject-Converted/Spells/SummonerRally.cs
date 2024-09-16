namespace Spells
{
    public class SummonerRally : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
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
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            Vector3 minionPos = GetPointByUnitFacingOffset(owner, 200, 0);
            SpellEffectCreate(out _, out _, "summoner_flash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, minionPos, target, default, default, false, default, default, false);
            TeamId ownerID = GetTeamID_CS(owner);
            float duration = 15;
            if (avatarVars.RallyDurationBonus == 5)
            {
                duration += avatarVars.RallyDurationBonus;
            }
            else if (avatarVars.RallyDurationBonus == 10)
            {
                duration += avatarVars.RallyDurationBonus;
            }
            Minion other3 = SpawnMinion("Beacon", "SummonerBeacon", "idle.lua", minionPos, ownerID, true, true, false, false, true, false, 0, true, false);
            int ownerLevel = GetLevel(owner);
            float bonusHealth = ownerLevel * 25;
            float bonusRegen = ownerLevel * 1.5f;
            float nextBuffVars_FinalHPRegen = bonusRegen + 15; // UNUSED
            float nextBuffVars_BonusHealth = bonusHealth;
            if (avatarVars.RallyAPMod == 70)
            {
                AddBuff(owner, other3, new Buffs.BeaconAuraAP(nextBuffVars_BonusHealth), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            else
            {
                AddBuff(owner, other3, new Buffs.BeaconAura(nextBuffVars_BonusHealth), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}