namespace ItemPassives
{
    public class ItemID_3020 : ItemScript
    {
        public override void OnUpdateStats()
        {
            IncFlatMagicPenetrationMod(owner, 20);
        }
    }
}