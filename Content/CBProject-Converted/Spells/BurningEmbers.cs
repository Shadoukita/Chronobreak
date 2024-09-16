namespace Buffs
{
    public class BurningEmbers : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, 5);
            IncFlatPhysicalDamageMod(owner, 5);
        }
    }
}