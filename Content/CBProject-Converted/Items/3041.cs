namespace ItemPassives
{
    public class ItemID_3041 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.MejaisCheck(), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                int count = GetBuffCountFromAll(owner, nameof(Buffs.MejaisStats));
                if (count == 20)
                {
                    AddBuff(owner, owner, new Buffs.MejaisCap(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            float count = GetBuffCountFromAll(owner, nameof(Buffs.MejaisStats));
            if (count > 0)
            {
                count *= 0.33f;
                if (count < 1.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MejaisStats), 1);
                }
                else if (count < 2.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MejaisStats), 2);
                }
                else if (count < 3.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MejaisStats), 3);
                }
                else if (count < 4.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MejaisStats), 4);
                }
                else if (count < 5.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MejaisStats), 5);
                }
                else if (count < 7)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MejaisStats), 6);
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3041 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Abyssalscepter_itm.troy", },
        };
    }
}