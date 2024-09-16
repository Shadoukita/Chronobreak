namespace ItemPassives
{
    public class ItemID_3141 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                AddBuff(attacker, target, new Buffs.MuramasaCheck(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
                int count = GetBuffCountFromAll(owner, nameof(Buffs.MuramasaStats));
                if (count == 20)
                {
                    AddBuff(owner, owner, new Buffs.MuramasaCap(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            float count = GetBuffCountFromAll(owner, nameof(Buffs.MuramasaStats));
            if (count > 0)
            {
                count *= 0.33f;
                if (count < 1.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MuramasaStats), 1);
                }
                else if (count < 2.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MuramasaStats), 2);
                }
                else if (count < 3.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MuramasaStats), 3);
                }
                else if (count < 4.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MuramasaStats), 4);
                }
                else if (count < 5.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MuramasaStats), 5);
                }
                else if (count < 7)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MuramasaStats), 6);
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3141 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Abyssalscepter_itm.troy", },
        };
    }
}