namespace Buffs
{
    public class OriannaBallTracker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShadowWalk",
            BuffTextureName = "Blitzcrank_StaticField.dds",
        };
        Vector3 myPosition;
        public OriannaBallTracker(Vector3 myPosition = default)
        {
            this.myPosition = myPosition;
        }
        public override void OnActivate()
        {
            //RequireVar(this.myPosition);
            charVars.BallPosition = myPosition;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(attacker, nameof(Buffs.OrianaGhost));
        }
    }
}