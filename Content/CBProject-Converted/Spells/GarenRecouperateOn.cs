namespace Spells
{
    public class GarenRecouperateOn : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class GarenRecouperateOn : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "GarenRecouperateOn",
            BuffTextureName = "Garen_Perseverance.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA); // UNUSED
                float curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA); // UNUSED
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.GarenRecoupDebuff)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GarenRecouperate1(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, false, false, false);
                }
            }
        }
    }
}