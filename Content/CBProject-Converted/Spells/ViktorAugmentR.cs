namespace Buffs
{
    public class ViktorAugmentR : BuffScript
    {
        public override void OnActivate()
        {
            IncPermanentPercentCooldownMod(owner, -0.1f);
        }
    }
}