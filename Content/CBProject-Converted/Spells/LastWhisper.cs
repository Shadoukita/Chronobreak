namespace Buffs
{
    public class LastWhisper : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentArmorPenetrationMod(owner, 0.4f);
        }
    }
}