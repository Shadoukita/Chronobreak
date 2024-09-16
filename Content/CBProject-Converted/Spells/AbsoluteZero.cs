namespace Spells
{
    public class AbsoluteZero : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 3f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "NunuBot", },
            SpellVOOverrideSkins = new[] { "", },
        };
        float[] effect0 = { -0.5f, -0.5f, -0.5f };
        float[] effect1 = { -0.25f, -0.25f, -0.25f };
        int[] effect2 = { 625, 875, 1125 };
        public override void ChannelingStart()
        {
            float nextBuffVars_MovementSpeedMod = effect0[level - 1];
            float nextBuffVars_AttackSpeedMod = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.AbsoluteZero(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 10, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellBuffRemove(owner, nameof(Buffs.AbsoluteZero), owner, 0);
            SpellEffectCreate(out _, out _, "AbsoluteZero_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out _, out _, "AbsoluteZero_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 2.5f, 1, false, false, attacker);
            }
        }
        public override void ChannelingCancelStop()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            float secondDamage = effect2[level - 1];
            float totalTime = 0.25f * charVars.LifeTime;
            SpellEffectCreate(out _, out _, "AbsoluteZero_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 650, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out _, out _, "AbsoluteZero_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                BreakSpellShields(unit);
                ApplyDamage(owner, unit, secondDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, totalTime, 2.5f, 1, false, false, attacker);
            }
            SpellBuffRemove(owner, nameof(Buffs.AbsoluteZero), owner, 0);
        }
    }
}
namespace Buffs
{
    public class AbsoluteZero : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "AbsoluteZero3_cas.troy", },
            BuffName = "Absolute Zero",
            BuffTextureName = "Yeti_Shatter.dds",
        };
        EffectEmitter particle;
        EffectEmitter particle2;
        float movementSpeedMod;
        float attackSpeedMod;
        float lastTimeExecuted;
        public AbsoluteZero(float movementSpeedMod = default, float attackSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out particle2, "AbsoluteZero2_green_cas.troy", "AbsoluteZero2_red_cas.troy", teamOfOwner, 10, 0, TeamId.TEAM_CHAOS, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            //RequireVar(this.movementSpeedMod);
            //RequireVar(this.attackSpeedMod);
            float nextBuffVars_MovementSpeedMod = movementSpeedMod;
            float nextBuffVars_AttackSpeedMod = attackSpeedMod;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff((ObjAIBase)owner, unit, new Buffs.AbsoluteZeroSlow(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
            charVars.LifeTime = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_MovementSpeedMod = movementSpeedMod;
                    float nextBuffVars_AttackSpeedMod = attackSpeedMod;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.AbsoluteZeroSlow(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            charVars.LifeTime = lifeTime;
        }
    }
}