namespace Buffs
{
    public class GragasDrunkenRageSelf : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "GragasDrunkenRageSelf",
            BuffTextureName = "GragasDrunkenRage.dds",
        };
        float damageIncrease;
        float damageReduction;
        EffectEmitter arr;
        public GragasDrunkenRageSelf(float damageIncrease = default, float damageReduction = default)
        {
            this.damageIncrease = damageIncrease;
            this.damageReduction = damageReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageIncrease);
            //RequireVar(this.damageReduction);
            SpellEffectCreate(out arr, out _, "gragas_buff_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(arr);
        }
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, damageReduction);
            IncPercentMagicReduction(owner, damageReduction);
            IncFlatPhysicalDamageMod(owner, damageIncrease);
            float damageReductionMod = 100 * damageReduction;
            SetBuffToolTipVar(1, damageIncrease);
            SetBuffToolTipVar(2, damageReductionMod);
        }
    }
}