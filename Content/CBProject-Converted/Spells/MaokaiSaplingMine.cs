namespace Spells
{
    public class MaokaiSaplingMine : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class MaokaiSaplingMine : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
        };
        float mineDamageAmount;
        bool sprung;
        TeamId teamID; // UNUSED
        bool active;
        AttackableUnit homingBeacon;
        bool detonated;
        float sprungCount;
        EffectEmitter particle;
        EffectEmitter particle2;
        EffectEmitter particle3;
        EffectEmitter particle4;
        float lastTimeExecuted;
        Region perceptionBubble; // UNUSED
        public MaokaiSaplingMine(float mineDamageAmount = default, bool sprung = default)
        {
            this.mineDamageAmount = mineDamageAmount;
            this.sprung = sprung;
        }
        public override void OnActivate()
        {
            //RequireVar(this.sprung);
            if (!sprung)
            {
                //RequireVar(this.mineDamageAmount);
                SetCanMove(owner, false);
                SetGhosted(owner, true);
                SetInvulnerable(owner, true);
                SetTargetable(owner, false);
                SetCanAttack(owner, false);
                teamID = GetTeamID_CS(owner);
                active = false;
                homingBeacon = null;
                detonated = false;
                sprungCount = 0;
            }
            Vector3 pos = GetPointByUnitFacingOffset(owner, 10, 180);
            FaceDirection(owner, pos);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            if (!detonated)
            {
                SpellEffectCreate(out particle, out _, "maoki_sapling_detonate.troy", default, teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, default, default, false, false);
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, mineDamageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                }
            }
            SetInvulnerable(owner, false);
            SetNoRender(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle3);
            SpellEffectRemove(particle4);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                TeamId teamID = GetTeamID_CS(attacker);
                if (active)
                {
                    bool canSee;
                    if (sprung)
                    {
                        SetCanMove(owner, true);
                        AttackableUnit other1 = homingBeacon;
                        sprungCount++;
                        if (sprungCount >= 11)
                        {
                            if (!detonated)
                            {
                                detonated = true;
                                SpellEffectCreate(out particle, out _, "maoki_sapling_detonate.troy", default, teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, default, default, false, false);
                                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                                {
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, mineDamageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                                }
                            }
                            SpellBuffRemoveCurrent(owner);
                            return; //BreakExecution();
                        }
                        else
                        {
                            if (sprungCount >= 2)
                            {
                                IssueOrder(owner, OrderType.MoveTo, default, other1);
                                if (sprungCount == 2)
                                {
                                    PlayAnimation("Run", 0, owner, false, false, false);
                                }
                            }
                            foreach (AttackableUnit unit1 in GetClosestUnitsInArea(owner, owner.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 10, default, true))
                            {
                                canSee = CanSeeTarget(owner, unit1);
                                if (canSee)
                                {
                                    if (!detonated)
                                    {
                                        SpellBuffRemove(owner, nameof(Buffs.MaokaiSapling2), (ObjAIBase)owner, 0);
                                        detonated = true;
                                        SpellEffectCreate(out particle, out _, "maoki_sapling_detonate.troy", default, teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, default, default, false, false);
                                        foreach (AttackableUnit unit2 in GetUnitsInArea(attacker, owner.Position3D, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                                        {
                                            BreakSpellShields(unit2);
                                            ApplyDamage(attacker, unit2, mineDamageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                                        }
                                    }
                                }
                                SpellBuffRemoveCurrent(owner);
                            }
                        }
                    }
                    else
                    {
                        foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 8, default, true))
                        {
                            canSee = CanSeeTarget(owner, unit);
                            if (canSee)
                            {
                                if (!sprung)
                                {
                                    TeamId unitTeam = GetTeamID_CS(unit);
                                    perceptionBubble = AddUnitPerceptionBubble(unitTeam, 10, owner, 2.5f, default, owner, false);
                                    sprung = true;
                                    sprungCount = 0;
                                    homingBeacon = unit;
                                    FaceDirection(owner, unit.Position3D);
                                    OverrideAnimation("Idle1", "Pop", owner);
                                    AddBuff((ObjAIBase)owner, owner, new Buffs.MaokaiSapling2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                }
                            }
                        }
                    }
                }
                else
                {
                    sprungCount++;
                    if (sprungCount >= 2)
                    {
                        sprungCount = 0;
                        active = true;
                        SpellEffectCreate(out particle, out particle2, "maokai_sapling_rdy_indicator_green.troy", "maokai_sapling_rdy_indicator_red.troy", teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "BUFFBONE_CSTM_STEM_3", default, target, default, default, false, default, default, false, false);
                        SpellEffectCreate(out particle3, out particle4, "maokai_sapling_team_id_green.troy", "maokai_sapling_team_id_red.troy", teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
                    }
                }
            }
        }
    }
}