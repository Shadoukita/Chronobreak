namespace Buffs
{
    public class BootsOfMobilityDebuff : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatMovementSpeedMod(owner, -60);
        }
    }
}