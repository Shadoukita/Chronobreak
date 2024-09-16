namespace Buffs
{
    public class EzrealFastRunAnim : BuffScript
    {
        public override void OnActivate()
        {
            OverrideAnimation("Run", "Run2", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
        }
    }
}