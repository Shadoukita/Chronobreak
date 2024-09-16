namespace Spells
{
    public class ScurvyStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class ScurvyStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ScurvyStrike",
            IsDeathRecapSource = true,
        };
        float dotDamage;
        float moveSpeedMod;
        float lastTimeExecuted;
        public ScurvyStrike(float dotDamage = default, float moveSpeedMod = default)
        {
            this.dotDamage = dotDamage;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dotDamage);
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                int count = GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ScurvyStrikeParticle));
                float damageToDeal = dotDamage * count;
                ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
        public override void OnUpdateStats()
        {
            int count = GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ScurvyStrikeParticle));
            float totalSlow = moveSpeedMod * count;
            IncPercentMultiplicativeMovementSpeedMod(owner, totalSlow);
        }
    }
}