namespace Buffs
{
    public class MaokaiDrain3Tally : BuffScript
    {
        float drainAmount;
        public MaokaiDrain3Tally(float drainAmount = default)
        {
            this.drainAmount = drainAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.drainAmount);
            charVars.Tally += drainAmount;
        }
    }
}