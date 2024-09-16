namespace Buffs
{
    public class CardMasterStackHolder : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "CardMasterStack",
            BuffTextureName = "Cardmaster_RapidToss_Charging.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.CardMasterStackHolder));
            if (count >= 3)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CardmasterStackParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.CardMasterStackHolder), 0);
            }
        }
    }
}