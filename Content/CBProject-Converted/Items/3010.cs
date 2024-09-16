namespace ItemPassives
{
    public class ItemID_3010 : ItemScript
    {
        int ownerLevel;
        public override void OnUpdateActions()
        {
            int tempLevel = GetLevel(owner);
            if (tempLevel > ownerLevel)
            {
                AddBuff(attacker, target, new Buffs.CatalystHeal(), 1, 1, 8.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                ownerLevel = tempLevel;
            }
        }
        public override void OnActivate()
        {
            ownerLevel = GetLevel(owner);
        }
    }
}