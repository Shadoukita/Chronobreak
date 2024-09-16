namespace Spells
{
    public class UdyrTigerPunchBleed : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class UdyrTigerPunchBleed : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrTigerPunchBleed",
            BuffTextureName = "Udyr_TigerStance.dds",
            IsDeathRecapSource = true,
        };
        float dotDamage;
        float lastTimeExecuted;
        public UdyrTigerPunchBleed(float dotDamage = default)
        {
            this.dotDamage = dotDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dotDamage);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, dotDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 0, false, false, attacker);
            }
        }
    }
}