namespace Buffs
{
    public class MonsterBankSmall : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Monster Bank Small",
            BuffTextureName = "23.dds",
        };
        float lastTimeExecuted;
        float numberUpgrades;
        public override void OnUpdateActions()
        {
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if (healthPercent >= 0.99f && ExecutePeriodically(5, ref lastTimeExecuted, false))
            {
                if (numberUpgrades > 0)
                {
                    IncPermanentExpReward(owner, 1.786f);
                    IncPermanentGoldReward(owner, 0.2667f);
                    numberUpgrades--;
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MonsterBankBig(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.MonsterBankSmall));
                }
            }
        }
        public override void OnActivate()
        {
            numberUpgrades = 14;
            IncPermanentExpReward(owner, 12.5f);
            IncPermanentGoldReward(owner, 2);
        }
    }
}