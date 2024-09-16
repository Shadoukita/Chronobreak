namespace Spells
{
    public class JaxRelentlessAssaultAS : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class JaxRelentlessAssaultAS : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "JaxRelentlessAssaultAS",
            BuffTextureName = "Armsmaster_MasterOfArms.dds",
        };
        int[] effect0 = { 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 8, 8, 9, 9, 9, 10 };
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.JaxRelentlessAttack), (ObjAIBase)owner, 0);
            SpellBuffClear(owner, nameof(Buffs.JaxRelentlessAssaultDebuff));
        }
        public override void OnUpdateActions()
        {
            int level = GetLevel(owner);
            float aS = effect0[level - 1];
            int count = GetBuffCountFromAll(owner, nameof(Buffs.JaxRelentlessAssaultAS));
            aS *= count;
            SetBuffToolTipVar(1, aS);
        }
    }
}