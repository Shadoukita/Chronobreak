namespace Buffs
{
    public class GarenCommandOther : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GarenCommandOther",
            BuffTextureName = "38.dds",
        };
        float damageReduction;
        EffectEmitter particle;
        float totalArmorAmount;
        public GarenCommandOther(float damageReduction = default, float totalArmorAmount = default)
        {
            this.damageReduction = damageReduction;
            this.totalArmorAmount = totalArmorAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageReduction);
            IncPercentPhysicalReduction(owner, damageReduction);
            IncPercentMagicReduction(owner, damageReduction);
            SpellEffectCreate(out particle, out _, "garen_commandingPresence_unit_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SetBuffToolTipVar(1, totalArmorAmount);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
        }
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, damageReduction);
            IncPercentMagicReduction(owner, damageReduction);
        }
    }
}