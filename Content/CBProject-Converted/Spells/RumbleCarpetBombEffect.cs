namespace Spells
{
    public class RumbleCarpetBombEffect : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
    }
}
namespace Buffs
{
    public class RumbleCarpetBombEffect : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", "", "", },
            AutoBuffActivateEffect = new[] { "pirate_attack_buf_01.troy", "", "", },
            BuffName = "Danger Zone",
            BuffTextureName = "034_Steel_Shield.dds",
            PersistsThroughDeath = true,
        };
        int counter; // UNUSED
        Vector3 missilePosition;
        EffectEmitter particle;
        EffectEmitter particle2;
        EffectEmitter particle3;
        float lastTimeExecuted;
        public RumbleCarpetBombEffect(Vector3 missilePosition = default)
        {
            this.missilePosition = missilePosition;
        }
        public override void OnActivate()
        {
            counter = 0;
            //RequireVar(this.missilePosition);
            Vector3 missilePosition = this.missilePosition;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            int rumbleSkinID = GetSkinID(attacker);
            if (teamOfOwner == TeamId.TEAM_ORDER)
            {
                SpellEffectCreate(out particle, out _, "rumble_ult_impact.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, true, default, default, missilePosition, target, default, default, true, default, default, false, false);
                if (rumbleSkinID == 2)
                {
                    SpellEffectCreate(out particle2, out _, "rumble_ult_impact_burn_cannon_ball_team_ID_green.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_ORDER, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out particle3, out _, "rumble_ult_impact_burn_cannon_ball_team_ID_red.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_CHAOS, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
                else if (rumbleSkinID == 1)
                {
                    SpellEffectCreate(out particle2, out _, "rumble_ult_impact_burn_pineapple_team_ID_green.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_ORDER, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out particle3, out _, "rumble_ult_impact_burn_pineapple_team_ID_red.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_CHAOS, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out particle2, out _, "rumble_ult_impact_burn_teamID_green.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_ORDER, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out particle3, out _, "rumble_ult_impact_burn_teamID_red.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_CHAOS, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
            }
            else
            {
                SpellEffectCreate(out particle, out _, "rumble_ult_impact.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, true, default, default, missilePosition, target, default, default, true, default, default, false, false);
                if (rumbleSkinID == 2)
                {
                    SpellEffectCreate(out particle2, out _, "rumble_ult_impact_burn_cannon_ball_team_ID_red.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_ORDER, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out particle3, out _, "rumble_ult_impact_burn_cannon_ball_team_ID_green.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_CHAOS, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
                else if (rumbleSkinID == 1)
                {
                    SpellEffectCreate(out particle2, out _, "rumble_ult_impact_burn_pineapple_team_ID_red.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_ORDER, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out particle3, out _, "rumble_ult_impact_burn_pineapple_team_ID_green.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_CHAOS, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out particle2, out _, "rumble_ult_impact_burn_teamID_red.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_ORDER, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out particle3, out _, "rumble_ult_impact_burn_teamID_green.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_CHAOS, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle3);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                Vector3 missilePosition = this.missilePosition;
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, missilePosition, 205, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.RumbleCarpetBombSlow(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    TeamId teamOfOwner = GetTeamID_CS(attacker);
                    if (teamOfOwner == TeamId.TEAM_ORDER)
                    {
                        AddBuff(attacker, unit, new Buffs.RumbleCarpetBombBurnOrder(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, false, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, unit, new Buffs.RumbleCarpetBombBurnDest(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, false, false, false);
                    }
                }
            }
        }
    }
}