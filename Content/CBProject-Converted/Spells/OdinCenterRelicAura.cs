namespace Buffs
{
    public class OdinCenterRelicAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCenterShrineBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        EffectEmitter buffParticle;
        bool killMe;
        EffectEmitter particle; // UNUSED
        EffectEmitter particle2; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetForceRenderParticles(owner, true);
            SetNoRender(owner, true);
            SpellEffectCreate(out buffParticle, out _, "NeutralMonster_buf_blue_defense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            killMe = false;
            SpellEffectCreate(out particle, out _, "PotionofGiantStrength_itm.troy", default, TeamId.TEAM_ORDER, 10, 0, TeamId.TEAM_CHAOS, default, default, true, owner, default, default, target, default, default, false, false, default, false, false);
            SpellEffectCreate(out particle, out _, "PlaceholderShield.troy", default, TeamId.TEAM_ORDER, 10, 0, TeamId.TEAM_CHAOS, default, default, true, owner, default, default, target, default, default, false, false, default, false, false);
            SpellEffectCreate(out particle2, out _, "PotionofElusiveness_itm.troy", default, TeamId.TEAM_ORDER, 10, 0, TeamId.TEAM_ORDER, default, default, true, owner, default, default, target, default, default, false, false, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 250000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
            SetNoRender(owner, true);
        }
        public override void OnUpdateStats()
        {
            if (killMe)
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinCenterRelicAura), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 175, SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    if (!killMe)
                    {
                        TeamId teamID = GetTeamID_CS(unit);
                        if (teamID == TeamId.TEAM_ORDER)
                        {
                            float newDuration = 60;
                            if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonsterBuffs)) > 0)
                            {
                                newDuration *= 1.15f;
                            }
                            else
                            {
                                if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.Monsterbuffs2)) > 0)
                                {
                                    newDuration *= 1.3f;
                                }
                            }
                            AddBuff((ObjAIBase)unit, unit, new Buffs.OdinCenterRelicBuff(), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                            killMe = true;
                            AddBuff((ObjAIBase)unit, unit, new Buffs.OdinScoreBigRelic(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}