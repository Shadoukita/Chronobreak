namespace Buffs
{
    public class JarvanIVMartialCadence : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVMartialCadence",
            BuffTextureName = "JarvanIV_MartialCadence.dds",
            PersistsThroughDeath = true,
        };
        int[] effect0 = { 6, 6, 6, 6, 6, 6, 8, 8, 8, 8, 8, 8, 10, 10, 10, 10, 10, 10 };
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 6);
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            float healthPerc = effect0[level - 1];
            SetBuffToolTipVar(1, healthPerc);
        }
    }
}