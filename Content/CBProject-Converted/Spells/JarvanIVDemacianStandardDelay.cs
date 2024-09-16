namespace Spells
{
    public class JarvanIVDemacianStandardDelay : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class JarvanIVDemacianStandardDelay : BuffScript
    {
        int level;
        float damageToDeal;
        EffectEmitter particle;
        EffectEmitter particle1;
        int[] effect0 = { 10, 13, 16, 19, 22 };
        float[] effect1 = { 0.1f, 0.13f, 0.16f, 0.19f, 0.22f };
        public JarvanIVDemacianStandardDelay(int level = default, float damageToDeal = default)
        {
            this.level = level;
            this.damageToDeal = damageToDeal;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageToDeal);
            //RequireVar(this.level);
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out particle1, "JarvanDemacianStandard_tar_green.troy", "JarvanDemacianStandard_tar_red.troy", teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnDeactivate(bool expired)
        {
            int level = this.level;
            SpellEffectRemove(particle);
            SpellEffectRemove(particle1);
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "JarvanDemacianStandard_hit.troy", default, teamID, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, true, true, attacker);
            }
            Vector3 ownerPos = GetUnitPosition(owner);
            Minion other3 = SpawnMinion("Beacon", "JarvanIVStandard", "idle.lua", ownerPos, teamID, true, true, false, false, true, true, 0, false, false, (Champion)attacker);
            int nextBuffVars_ArmorMod = effect0[level - 1];
            float nextBuffVars_AttackSpeedMod = effect1[level - 1];
            AddBuff(attacker, other3, new Buffs.JarvanIVDemacianStandard(nextBuffVars_ArmorMod, nextBuffVars_AttackSpeedMod), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}