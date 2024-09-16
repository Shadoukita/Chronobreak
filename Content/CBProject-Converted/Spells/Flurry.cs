namespace Spells
{
    public class Flurry : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            //object monkTotalDamage; // UNITIALIZED
            //object nextBuffVars_MonkTotalDamage; // UNUSED
            charVars.FlurryScalar = 0.7f;
            //nextBuffVars_MonkTotalDamage = monkTotalDamage;
            AddBuff((ObjAIBase)target, target, new Buffs.Flurry(), 1, 1, 0.58f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
        }
    }
}
namespace Buffs
{
    public class Flurry : BuffScript
    {
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float monkTotalDamage = GetTotalAttackDamage(owner);
            monkTotalDamage *= charVars.FlurryScalar;
            foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, 1))
            {
                if (unit is ObjAIBase)
                {
                    SpellEffectCreate(out _, out _, "GlobalHit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false);
                }
                ApplyDamage(attacker, unit, monkTotalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
        }
        public override void OnUpdateActions()
        {
            float monkTotalDamage = GetTotalAttackDamage(owner);
            monkTotalDamage *= charVars.FlurryScalar;
            if (ExecutePeriodically(0.15f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, 1))
                {
                    if (unit is ObjAIBase)
                    {
                        SpellEffectCreate(out _, out _, "GlobalHit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false);
                    }
                    ApplyDamage(attacker, unit, monkTotalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1);
                }
            }
        }
    }
}