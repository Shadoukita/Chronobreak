namespace Buffs
{
    public class RitualStaff : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.2f);
        }
    }
}