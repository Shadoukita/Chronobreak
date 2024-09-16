namespace Buffs
{
    public class Sting : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.1f);
        }
    }
}