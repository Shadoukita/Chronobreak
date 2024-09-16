namespace Spells
{
    public class OdinBombBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 120f, 100f, 80f, 10f, 10f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class OdinBombBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "OdinShrineBombBuff",
            BuffTextureName = "DrMundo_BurningAgony.dds",
        };
        Region bubbleID;
        Region bubbleID2;
        EffectEmitter buffParticle;
        EffectEmitter buffParticle2;
        EffectEmitter crystalParticle;
        EffectEmitter crystalParticle2;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SetCanMove(owner, false);
            SetForceRenderParticles(owner, true);
            SetTargetable(owner, false);
            TeamId orderTeam = TeamId.TEAM_ORDER;
            TeamId chaosTeam = TeamId.TEAM_CHAOS;
            bubbleID = AddUnitPerceptionBubble(orderTeam, 350, owner, 25000, default, default, true);
            bubbleID2 = AddUnitPerceptionBubble(chaosTeam, 350, owner, 25000, default, default, true);
            if (teamID == TeamId.TEAM_ORDER)
            {
                SpellEffectCreate(out buffParticle, out buffParticle2, "odin_relic_buf_red.troy", "odin_relic_buf_green.troy", TeamId.TEAM_ORDER, 0, 0, TeamId.TEAM_ORDER, default, owner, false, owner, default, default, owner, default, default, false, true, false, false, false);
                SpellEffectCreate(out crystalParticle, out crystalParticle2, "Odin_Prism_Red.troy", "Odin_Prism_Green.troy", TeamId.TEAM_ORDER, 0, 0, TeamId.TEAM_ORDER, default, owner, false, owner, default, default, owner, default, default, false, true, true, false, false);
            }
            else
            {
                SpellEffectCreate(out buffParticle, out buffParticle2, "odin_relic_buf_green.troy", "odin_relic_buf_red.troy", TeamId.TEAM_ORDER, 0, 0, TeamId.TEAM_ORDER, default, owner, false, owner, default, default, owner, default, default, false, true, false, false, false);
                SpellEffectCreate(out crystalParticle, out crystalParticle2, "Odin_Prism_Green.troy", "Odin_Prism_Red.troy", TeamId.TEAM_ORDER, 0, 0, TeamId.TEAM_ORDER, default, owner, false, owner, default, default, owner, default, default, false, true, true, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
            SpellEffectRemove(buffParticle);
            SpellEffectRemove(buffParticle2);
            SpellEffectRemove(crystalParticle);
            SpellEffectRemove(crystalParticle2);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            float healthPercent = GetPARPercent(target, PrimaryAbilityResourceType.MANA);
            float size = 350 * healthPercent; // UNUSED
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.OdinBombSuppression));
                if (count == 0)
                {
                    TeamId teamID = GetTeamID_CS(owner); // UNUSED
                    float healAmount = 20000;
                    IncPAR(owner, healAmount, PrimaryAbilityResourceType.MANA);
                }
            }
        }
    }
}