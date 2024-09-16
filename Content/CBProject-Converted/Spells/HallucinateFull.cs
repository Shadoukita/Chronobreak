namespace Spells
{
    public class HallucinateFull : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 300, 450, 600 };
        float[] effect1 = { 0.75f, 0.75f, 0.75f };
        float[] effect2 = { 1.5f, 1.5f, 1.5f };
        float[] effect3 = { 0.85f, 0.85f, 0.85f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            DestroyMissileForTarget(owner);
            int nextBuffVars_DamageAmount = effect0[level - 1];
            float nextBuffVars_DamageDealt = effect1[level - 1];
            float nextBuffVars_DamageTaken = effect2[level - 1];
            float nextBuffVars_shacoDamageTaken = effect3[level - 1];
            AddBuff(owner, owner, new Buffs.HallucinateApplicator(nextBuffVars_DamageAmount, nextBuffVars_DamageDealt, nextBuffVars_DamageTaken, nextBuffVars_shacoDamageTaken), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class HallucinateFull : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hallucinate",
            BuffTextureName = "Jester_HallucinogenBomb.dds",
        };
        float damageAmount;
        float damageDealt;
        float damageTaken;
        EffectEmitter particle;
        public HallucinateFull(float damageAmount = default, float damageDealt = default, float damageTaken = default)
        {
            this.damageAmount = damageAmount;
            this.damageDealt = damageDealt;
            this.damageTaken = damageTaken;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out _, "Jester_Copy.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, teamID, default, default, true, owner, "root", default, target, "root", default, false, false, false, false, false);
            //RequireVar(this.damageAmount);
            //RequireVar(this.damageDealt);
            //RequireVar(this.damageTaken);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "Hallucinate_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, owner, default, default, true, false, false, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
            }
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
            SpellEffectRemove(particle);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            Champion caster = GetChampionBySkinName("Shaco", teamID);
            float totalDamage = damageAmount * damageDealt;
            if (target is BaseTurret)
            {
                totalDamage *= 0.5f;
            }
            damageAmount = 0;
            ApplyDamage(caster, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 1, 0, false, false, caster);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= damageTaken;
        }
    }
}