namespace Buffs
{
    public class IonianBootsofLucidity : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.15f);
        }
    }
}