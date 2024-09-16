namespace Spells
{
    public class TormentedSoil : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 25, 40, 55, 70, 85 };
        int[] effect1 = { -4, -5, -6, -7, -8 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_DamagePerTick = effect0[level - 1];
            int nextBuffVars_MRminus = effect1[level - 1];
            AddBuff(attacker, attacker, new Buffs.TormentedSoil(nextBuffVars_DamagePerTick, nextBuffVars_TargetPos, nextBuffVars_MRminus), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class TormentedSoil : BuffScript
    {
        float damagePerTick;
        Vector3 targetPos;
        float mRminus;
        EffectEmitter particle2;
        EffectEmitter particle;
        float lastTimeExecuted;
        public TormentedSoil(float damagePerTick = default, Vector3 targetPos = default, float mRminus = default)
        {
            this.damagePerTick = damagePerTick;
            this.targetPos = targetPos;
            this.mRminus = mRminus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.targetPos);
            //RequireVar(this.mRminus);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = this.targetPos;
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_MRminus = mRminus;
            SpellEffectCreate(out particle2, out particle, "TormentedSoil_green_tar.troy", "TormentedSoil_red_tar.troy", teamOfOwner, 280, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 280, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.TormentedSoilDebuff(nextBuffVars_TargetPos, nextBuffVars_MRminus), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                SpellEffectCreate(out _, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "L_foot", default, unit, default, default, false, false, false, false, false);
                SpellEffectCreate(out _, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "R_foot", default, unit, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos = this.targetPos;
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_MRminus = mRminus;
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 280, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                {
                    ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.TormentedSoilDebuff(nextBuffVars_TargetPos, nextBuffVars_MRminus), 5, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                    SpellEffectCreate(out _, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "L_foot", default, unit, default, default, false, false, false, false, false);
                    SpellEffectCreate(out _, out _, "FireFeet_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "R_foot", default, unit, default, default, false, false, false, false, false);
                }
            }
        }
    }
}