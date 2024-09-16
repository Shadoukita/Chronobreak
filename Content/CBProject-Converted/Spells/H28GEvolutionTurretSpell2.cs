namespace Spells
{
    public class H28GEvolutionTurretSpell2 : SpellScript
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
    public class H28GEvolutionTurretSpell2 : BuffScript
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
            lastAttackTime = GetGameTime();
            ApplyTaunt(attacker, owner, 250);
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
            if (distance > 475)
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