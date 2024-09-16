namespace Buffs
{
    public class Brutalizer : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.1f);
            IncFlatArmorPenetrationMod(owner, 15);
        }
    }
}