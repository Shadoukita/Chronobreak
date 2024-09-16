namespace Buffs
{
    public class PhysicalImmunity : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Invulnerability.troy", },
            BuffName = "PhysicalImmunity",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        public override void OnActivate()
        {
            SetPhysicalImmune(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetPhysicalImmune(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetPhysicalImmune(owner, true);
        }
    }
}