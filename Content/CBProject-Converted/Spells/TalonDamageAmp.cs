namespace Buffs
{
    public class TalonDamageAmp : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "talon_E_tar_dmg.troy", },
            BuffName = "TalonDamageAmp",
            BuffTextureName = "TalonCutthroat.dds",
        };
        float ampValue;
        public TalonDamageAmp(float ampValue = default)
        {
            this.ampValue = ampValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.ampValue);
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (attacker == caster)
            {
                damageAmount *= ampValue;
            }
        }
    }
}