namespace Buffs
{
    public class RegenerationRune : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        bool bountyActive; // UNUSED
        public override void OnActivate()
        {
            bountyActive = false;
        }
        public override void OnUpdateStats()
        {
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if (healthPercent >= 0.99f && lifeTime >= 45 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonsterBankSmall)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonsterBankBig)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MonsterBankSmall(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}