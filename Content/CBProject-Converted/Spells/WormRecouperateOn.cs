namespace Spells
{
    public class WormRecouperateOn : SpellScript
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
    public class WormRecouperateOn : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "WormRecouperateOn",
            BuffTextureName = "3011_Dawnseeker.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.WormRecoupDebuff)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecouperate1(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
    }
}