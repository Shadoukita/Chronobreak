namespace Buffs
{
    public class UdyrMA1 : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, 0.15f);
            IncFlatDodgeMod(owner, 0.06f);
        }
    }
}