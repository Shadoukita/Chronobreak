namespace Buffs
{
    public class SetNoRender : BuffScript
    {
        public override void OnActivate()
        {
            SetNoRender(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, false);
            SetIgnoreCallForHelp(owner, false);
            SetSuppressCallForHelp(owner, false);
            SetCallForHelpSuppresser(owner, false);
            SetTargetable(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetNoRender(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetTargetable(owner, false);
        }
    }
}