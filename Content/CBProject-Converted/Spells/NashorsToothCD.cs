namespace Buffs
{
    public class NashorsToothCD : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.25f);
        }
    }
}