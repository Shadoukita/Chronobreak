namespace Buffs
{
    public class ShadowStepDodge : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShadowStepDodge",
            BuffTextureName = "Katarina_Shunpo.dds",
        };
        float damageReduction;
        Fade iD; // UNUSED
        public ShadowStepDodge(float damageReduction = default)
        {
            this.damageReduction = damageReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageReduction);
            iD = PushCharacterFade(owner, 0.5f, 0.1f);
        }
        public override void OnDeactivate(bool expired)
        {
            iD = PushCharacterFade(owner, 1, 0.5f);
        }
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, damageReduction);
            IncPercentMagicReduction(owner, damageReduction);
        }
    }
}