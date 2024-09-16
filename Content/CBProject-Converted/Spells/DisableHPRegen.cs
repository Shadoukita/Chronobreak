namespace Buffs
{
    public class DisableHPRegen : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentHPRegenMod(owner, -1);
        }
    }
}