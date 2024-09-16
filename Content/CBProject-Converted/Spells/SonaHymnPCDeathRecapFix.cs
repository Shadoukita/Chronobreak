namespace Buffs
{
    public class SonaHymnPCDeathRecapFix : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        float totalDamage;
        public SonaHymnPCDeathRecapFix(float totalDamage = default)
        {
            this.totalDamage = totalDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.totalDamage);
            ApplyDamage(attacker, owner, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
        }
    }
}