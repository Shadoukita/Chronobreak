namespace Buffs
{
    public class InfinityEdge : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatCritDamageMod(owner, 0.5f);
        }
    }
}