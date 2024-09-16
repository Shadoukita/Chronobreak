namespace Buffs
{
    public class HauntingGuise : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatMagicPenetrationMod(owner, 20);
        }
    }
}