namespace Buffs
{
    public class FiendishCodex : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.1f);
        }
    }
}