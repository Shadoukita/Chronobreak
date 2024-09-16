namespace Buffs
{
    public class MonkeyKingDoubleClone : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
        };
        public override void OnActivate()
        {
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            SetNoRender(owner, true);
            SetCanMove(owner, false);
        }
    }
}