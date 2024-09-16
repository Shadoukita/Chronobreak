namespace Spells
{
    public class H28GEvolutionTurretSpell3 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class H28GEvolutionTurretSpell3 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "H28GEvolutionTurret",
            BuffTextureName = "Jester_DeathWard.dds",
        };
        float lastAttackTime;
        float retaunts;
        public override void OnActivate()
        {
            ApplyTaunt(attacker, owner, 25000);
            ApplyDamage((ObjAIBase)owner, attacker, 0, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, (ObjAIBase)owner);
            lastAttackTime = GetGameTime();
            retaunts = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.Taunt));
        }
        public override void OnUpdateActions()
        {
            float distance = DistanceBetweenObjects(attacker, owner);
            bool targetable = GetTargetable(attacker);
            if (distance > 625)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if (IsDead(attacker))
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if (!targetable)
            {
                SpellBuffRemoveCurrent(owner);
            }
            float curTime = GetGameTime();
            float timeElapsed = curTime - lastAttackTime;
            if (timeElapsed >= 0.75f)
            {
                if (retaunts == 0)
                {
                    ApplyTaunt(attacker, owner, 250);
                    retaunts++;
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            lastAttackTime = GetGameTime();
        }
    }
}