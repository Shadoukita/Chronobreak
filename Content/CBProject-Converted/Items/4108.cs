namespace Buffs
{
    public class _4108 : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.01f);
        }
    }
}