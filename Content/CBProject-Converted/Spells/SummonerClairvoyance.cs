namespace Spells
{
    public class SummonerClairvoyance : SpellScript
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
            float baseCooldown = 70;
            if (avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown *= cooldownMultiplier;
            }
            SetSpellToolTipVar(baseCooldown, 2, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
            float duration = 4;
            if (avatarVars.UtilityMastery == 1)
            {
                duration += 2;
            }
            SetSpellToolTipVar(duration, 1, spellSlot, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, attacker);
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
        public override void SelfExecute()
        {
            EffectEmitter particleID;
            EffectEmitter particleID2;
            Vector3 targetPos = GetSpellTargetPos(spell);
            SpellEffectCreate(out _, out _, "Summoner_Cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out particleID, out particleID2, "ClairvoyanceEyeLong_green.troy", "ClairvoyanceEyeLong_red.troy", teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            EffectEmitter nextBuffVars_ParticleID = particleID;
            EffectEmitter nextBuffVars_ParticleID2 = particleID2;
            float duration = 4;
            if (avatarVars.UtilityMastery == 1)
            {
                duration += 2;
            }
            Region nextBuffVars_Bubble = AddPosPerceptionBubble(teamID, 1400, targetPos, duration, default, false);
            AddBuff(attacker, owner, new Buffs.SummonerClairvoyance(nextBuffVars_ParticleID, nextBuffVars_ParticleID2, nextBuffVars_Bubble), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SummonerClairvoyance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GuinsoosRodofOblivion_buf.troy", },
        };
        EffectEmitter particleID;
        EffectEmitter particleID2;
        Region bubble;
        public SummonerClairvoyance(EffectEmitter particleID = default, EffectEmitter particleID2 = default, Region bubble = default)
        {
            this.particleID = particleID;
            this.particleID2 = particleID2;
            this.bubble = bubble;
        }
        public override void OnActivate()
        {
            //RequireVar(this.particleID);
            //RequireVar(this.particleID2);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubble);
            SpellEffectRemove(particleID);
            SpellEffectRemove(particleID2);
        }
    }
}