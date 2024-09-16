namespace Buffs
{
    public class GlacialShroud : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.15f);
        }
    }
}