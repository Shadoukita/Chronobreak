namespace Spells
{
    public class SwainShadowGrasp : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        int[] effect0 = { 2, 2, 2, 2, 2 };
        int[] effect1 = { 80, 120, 160, 200, 240 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            float nextBuffVars_RootDuration = effect0[level - 1];
            float nextBuffVars_GraspDamage = effect1[level - 1];
            AddBuff(attacker, other3, new Buffs.SwainShadowGrasp(nextBuffVars_GraspDamage, nextBuffVars_RootDuration), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SwainShadowGrasp : BuffScript
    {
        float graspDamage;
        float rootDuration;
        EffectEmitter groundParticleEffect;
        EffectEmitter groundParticleEffect2;
        EffectEmitter a;
        public SwainShadowGrasp(float graspDamage = default, float rootDuration = default)
        {
            this.graspDamage = graspDamage;
            this.rootDuration = rootDuration;
        }
        public override void OnActivate()
        {
            //RequireVar(this.graspDamage);
            //RequireVar(this.rootDuration);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out groundParticleEffect, out groundParticleEffect2, "Swain_shadowGrasp_warning_green.troy", "Swain_shadowGrasp_warning_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out a, out _, "swain_shadowGrasp_transform.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(groundParticleEffect);
            SpellEffectRemove(groundParticleEffect2);
            SpellEffectRemove(a);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, graspDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.SwainShadowGraspRoot(), 1, 1, rootDuration, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, true, false);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}