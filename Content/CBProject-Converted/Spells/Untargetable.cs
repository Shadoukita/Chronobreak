namespace Buffs
{
    public class Untargetable : BuffScript
    {
        public override void OnActivate()
        {
            SetTargetable(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetTargetable(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
        }
    }
}