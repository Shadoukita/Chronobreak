namespace Spells
{
    public class InfectedCleaver : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        /*
        //TODO: Uncomment and fix
        public override void SelfExecute()
        {
            Vector3 pos; // UNITIALIZED
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, level, true, true, false);
        }
        */
    }
}
namespace Buffs
{
    public class InfectedCleaver : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "head", },
            AutoBuffActivateEffect = new[] { "SealFate_tar.troy", "GLOBAL_SILENCE.TROY", },
            BuffName = "Wild Cards",
            BuffTextureName = "Cardmaster_PowerCard.dds",
            NonDispellable = true,
        };
    }
}