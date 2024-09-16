namespace Spells
{
    public class LeonaSolarFlare : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 150, 250, 350 };
        public override void SelfExecute()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(targetPos, ownerPos);
            float nextBuffVars_Distance = distance; // UNUSED
            SpellEffectCreate(out _, out _, "Leona_SolarFlare_cas.troy", default, TeamId.TEAM_ORDER, 100, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "root", default, attacker, default, default, true, default, default, false, false);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            float nextBuffVars_DamageAmount = effect0[level - 1];
            int nextBuffVars_Level = level; // UNUSED
            AddBuff(attacker, other3, new Buffs.LeonaSolarFlare(nextBuffVars_DamageAmount), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Region nextBuffVars_Bubble = AddPosPerceptionBubble(teamOfOwner, 800, targetPos, 4, default, false);
            AddBuff(owner, owner, new Buffs.LeonaSolarFlareVision(nextBuffVars_Bubble), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LeonaSolarFlare : BuffScript
    {
        float damageAmount;
        EffectEmitter particle1;
        EffectEmitter particle;
        EffectEmitter a; // UNUSED
        public LeonaSolarFlare(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
            //RequireVar(this.damageAmount);
            //RequireVar(this.distance);
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle1, out particle, "Leona_SolarFlare_cas_green.troy", "Leona_SolarFlare_cas_red.troy", teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
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
            SpellEffectRemove(particle);
            SpellEffectRemove(particle1);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out a, out _, "Leona_SolarFlare_tar.troy", default, TeamId.TEAM_NEUTRAL, 100, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
            int level = GetLevel(attacker);
            int nextBuffVars_Level = level; // UNUSED
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "Leona_SolarBarrier_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                AddBuff(attacker, unit, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.LeonaSolarFlareSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyStun(attacker, unit, 1.5f);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}