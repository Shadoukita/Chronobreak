namespace Buffs
{
    public class OdinCenterRelicDebuff : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatMovementSpeedMod(owner, -80);
        }
    }
}