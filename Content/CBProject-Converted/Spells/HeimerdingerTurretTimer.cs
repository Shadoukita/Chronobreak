namespace Buffs
{
    public class HeimerdingerTurretTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = false,
        };
        public override void OnDeactivate(bool expired)
        {
            if (!IsDead(owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.HeimerdingerTurretReady(), 2, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, false, false, false);
            }
        }
    }
}