namespace Buffs
{
    public class GragasBarrelRollRender : BuffScript
    {
        public override void OnActivate()
        {
            SetCallForHelpSuppresser(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
        }
    }
}