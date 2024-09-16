namespace Buffs
{
    public class DangerZoneTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "corki_fire_buf.troy", },
            BuffName = "Poisoned",
            BuffTextureName = "Jester_DeathWard.dds",
        };
        float damagePerTick;
        public DangerZoneTarget(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, default, false, false);
        }
    }
}