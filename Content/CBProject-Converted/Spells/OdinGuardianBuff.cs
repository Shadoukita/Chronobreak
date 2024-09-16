namespace Spells
{
    public class OdinGuardianBuff : SpellScript
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
    public class OdinGuardianBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "pelvis", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "Ferocious Howl",
            BuffTextureName = "Minotaur_FerociousHowl.dds",
        };
        TeamId myTeam;
        TeamId orderTeam;
        TeamId chaosTeam;
        Region bubbleID;
        Region bubbleID2;
        EffectEmitter particle;
        EffectEmitter particle2;
        EffectEmitter platformParticle;
        EffectEmitter platformParticle2;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SetGhostProof(owner, true);
            TeamId teamID = GetTeamID_CS(owner);
            myTeam = GetTeamID_CS(owner);
            orderTeam = TeamId.TEAM_ORDER;
            chaosTeam = TeamId.TEAM_CHAOS;
            bubbleID = AddUnitPerceptionBubble(orderTeam, 800, owner, 25000, default, default, true);
            bubbleID2 = AddUnitPerceptionBubble(chaosTeam, 800, owner, 25000, default, default, true);
            if (teamID == TeamId.TEAM_NEUTRAL)
            {
                float health = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
                float damage = health * -0.5f;
                IncPAR(owner, damage, PrimaryAbilityResourceType.MANA);
            }
            else if (teamID == TeamId.TEAM_ORDER || teamID == TeamId.TEAM_CHAOS)
            {
                SpellEffectCreate(out particle, out particle2, "OdinNeutralGuardian_Green.troy", "OdinNeutralGuardian_Red.troy", TeamId.TEAM_UNKNOWN, 0, 0, teamID, default, owner, false, owner, "crystal", default, owner, default, default, false, true, false, false, false);
                SpellEffectCreate(out platformParticle, out _, "blank.troy", default, teamID, 0, 0, teamID, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out platformParticle2, out _, "blank.troy", default, GetEnemyTeam(teamID), 0, 0, GetEnemyTeam(teamID), default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out particle, out _, "OdinNeutralGuardian_Stone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_ORDER, default, owner, false, owner, "crystal", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out particle2, out _, "OdinNeutralGuardian_Stone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_CHAOS, default, owner, false, owner, "crystal", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out platformParticle, out _, "blank.troy", default, TeamId.TEAM_ORDER, 0, 0, TeamId.TEAM_ORDER, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out platformParticle2, out _, "blank.troy", default, TeamId.TEAM_CHAOS, 0, 0, TeamId.TEAM_CHAOS, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(owner);
            RemovePerceptionBubble(bubbleID);
            if (teamID == TeamId.TEAM_NEUTRAL)
            {
                RemovePerceptionBubble(bubbleID2);
            }
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            SpellEffectRemove(platformParticle);
            SpellEffectRemove(platformParticle2);
        }
        public override void OnUpdateStats()
        {
            SetInvulnerable(owner, true);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectNotPet | SpellDataFlags.NotAffectSelf, default, true))
            {
                int nextBuffVars_MagicResistBuff;
                int nextBuffVars_ArmorBuff;
                if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.OdinSuperMinion)) > 0)
                {
                    nextBuffVars_MagicResistBuff = 0;
                    nextBuffVars_ArmorBuff = 0;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.OdinMinionTaunt(nextBuffVars_MagicResistBuff, nextBuffVars_ArmorBuff), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
                if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.OdinMinion)) > 0)
                {
                    nextBuffVars_MagicResistBuff = 0;
                    nextBuffVars_ArmorBuff = 0;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.OdinMinionTaunt(nextBuffVars_MagicResistBuff, nextBuffVars_ArmorBuff), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            TeamId currentTeam = GetTeamID_CS(owner);
            if (currentTeam != myTeam)
            {
                TeamId teamID;
                teamID = TeamId.TEAM_UNKNOWN; //TODO: Verify
                SpellEffectCreate(out _, out _, "GoldAquisition_glb.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
                RemovePerceptionBubble(bubbleID);
                if (myTeam == TeamId.TEAM_NEUTRAL)
                {
                    RemovePerceptionBubble(bubbleID2);
                }
                teamID = GetTeamID_CS(owner);
                myTeam = currentTeam;
                SpellEffectRemove(particle);
                SpellEffectRemove(particle2);
                SpellEffectRemove(platformParticle);
                SpellEffectRemove(platformParticle2);
                if (teamID == TeamId.TEAM_ORDER || teamID == TeamId.TEAM_CHAOS)
                {
                    PlayAnimation("Activate", 0, owner, false, true, false);
                    OverrideAnimation("Idle1", "Floating", owner);
                    SpellEffectCreate(out particle, out particle2, "OdinNeutralGuardian_Green.troy", "OdinNeutralGuardian_Red.troy", myTeam, 0, 0, myTeam, default, owner, false, owner, "crystal", default, owner, default, default, false, true, false, false, false);
                    SpellEffectCreate(out platformParticle, out _, "blank.troy", default, myTeam, 0, 0, myTeam, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out platformParticle2, out _, "blank.troy", default, GetEnemyTeam(myTeam), 0, 0, GetEnemyTeam(myTeam), default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    bubbleID = AddUnitPerceptionBubble(teamID, 800, owner, 25000, default, default, true);
                }
                else
                {
                    PlayAnimation("Deactivate", 0, owner, false, false, false);
                    ClearOverrideAnimation("Idle1", owner);
                    SpellEffectCreate(out particle, out _, "OdinNeutralGuardian_Stone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_ORDER, default, owner, false, owner, "crystal", default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out particle2, out _, "OdinNeutralGuardian_Stone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_CHAOS, default, owner, false, owner, "crystal", default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out platformParticle, out _, "blank.troy", default, TeamId.TEAM_ORDER, 0, 0, TeamId.TEAM_ORDER, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out platformParticle2, out _, "blank.troy", default, TeamId.TEAM_CHAOS, 0, 0, TeamId.TEAM_CHAOS, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    bubbleID = AddUnitPerceptionBubble(orderTeam, 800, owner, 25000, default, default, true);
                    bubbleID2 = AddUnitPerceptionBubble(chaosTeam, 800, owner, 25000, default, default, true);
                }
            }
            IncPercentArmorPenetrationMod(owner, 0.65f);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                int count1 = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppression));
                int count2 = GetBuffCountFromAll(owner, nameof(Buffs.OdinMinionSpellAttack));
                float count = count1 + count2;
                if (count == 0)
                {
                    TeamId teamID = GetTeamID_CS(owner);
                    float healAmount = 300;
                    if (teamID == TeamId.TEAM_NEUTRAL)
                    {
                        float maxHealth = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
                        float halfHealth = maxHealth * 0.5f;
                        float curHealth = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                        healAmount = halfHealth - curHealth;
                        healAmount = Math.Min(healAmount, 150);
                        healAmount = Math.Max(healAmount, -150);
                    }
                    IncPAR(owner, healAmount, PrimaryAbilityResourceType.MANA);
                }
            }
        }
    }
}