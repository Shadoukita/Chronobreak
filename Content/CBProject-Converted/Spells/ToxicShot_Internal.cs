namespace Spells
{
    public class ToxicShot_Internal : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
    }
}
namespace Buffs
{
    public class ToxicShot_Internal : BuffScript
    {
        float damagePerTick;
        float stackingDamagePerTick;
        float lastTimeExecuted;
        public ToxicShot_Internal(float damagePerTick = default, float stackingDamagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
            this.stackingDamagePerTick = stackingDamagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.stackingDamagePerTick);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ToxicShotAttack)) > 0)
                {
                    int count = GetBuffCountFromAll(owner, nameof(Buffs.ToxicShotAttack));
                    float stackDamage = stackingDamagePerTick * count;
                    float damageAmount = damagePerTick + stackDamage;
                    ApplyDamage(attacker, owner, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0.14f);
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}