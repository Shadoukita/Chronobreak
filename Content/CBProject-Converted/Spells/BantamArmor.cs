namespace Buffs
{
    public class BantamArmor : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, 150);
        }
    }
}