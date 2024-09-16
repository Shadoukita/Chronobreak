namespace Spells
{
    public class Crowstorm : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 150f, 120f, 100f, },
            ChannelDuration = 1.5f,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "SurprisePartyFiddlesticks", },
        };
        EffectEmitter confetti;
        float[] effect0 = { 62.5f, 112.5f, 162.5f };
        public override void ChannelingStart()
        {
            Vector3 castPos = GetSpellTargetPos(spell);
            FaceDirection(owner, castPos);
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            int fiddlesticksSkinID = GetSkinID(attacker);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectCreate(out confetti, out _, "Party_HornConfetti.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "BUFFBONE_CSTM_HORN", default, attacker, default, default, false, false, false, false, false);
            }
        }
        public override void ChannelingSuccessStop()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 castPos = GetSpellTargetPos(spell);
            SpellEffectCreate(out _, out _, "summoner_flashback.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, castPos, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "summoner_cast.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "summoner_flash.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, castPos, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(owner, unit, new Buffs.ParanoiaMissChance(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            TeleportToPosition(owner, castPos);
            float nextBuffVars_DamageAmount = effect0[level - 1];
            AddBuff(owner, owner, new Buffs.Crowstorm(nextBuffVars_DamageAmount), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            teamID = GetTeamID_CS(attacker);
            int fiddlesticksSkinID = GetSkinID(attacker);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(confetti);
            }
        }
        public override void ChannelingCancelStop()
        {
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            int fiddlesticksSkinID = GetSkinID(attacker);
            if (fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(confetti);
            }
        }
    }
}
namespace Buffs
{
    public class Crowstorm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Crowstorm",
            BuffTextureName = "Fiddlesticks_Crowstorm.dds",
        };
        float damageAmount;
        EffectEmitter particle;
        EffectEmitter particle2;
        float lastTimeExecuted;
        public Crowstorm(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageAmount);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out particle2, "Crowstorm_green_cas.troy", "Crowstorm_red_cas.troy", teamOfOwner, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 600, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage((ObjAIBase)owner, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.225f, 1, false, false, attacker);
                }
            }
        }
    }
}