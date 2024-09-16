namespace Buffs
{
    public class VoidStaff : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentMagicPenetrationMod(owner, 0.4f);
        }
    }
}