namespace Buffs
{
    public class ExplosiveShotDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Explosive Shot Debuff",
            BuffTextureName = "Tristana_ExplosiveShot.dds",
        };
        float dotdmg;
        public ExplosiveShotDebuff(float dotdmg = default)
        {
            this.dotdmg = dotdmg;
        }
        public override void OnUpdateActions()
        {
            ApplyDamage(attacker, owner, dotdmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.2f, 1, false, false, attacker);
        }
        public override void OnActivate()
        {
            //RequireVar(this.dotdmg);
        }
    }
}