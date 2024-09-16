namespace Buffs
{
    public class RebirthMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rebirth Marker",
            BuffTextureName = "Cryophoenix_Rebirth.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int[] effect0 = { -40, -40, -40, -40, -25, -25, -25, -10, -10, -10, -10, 5, 5, 5, 20, 20, 20, 20 };
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, -40);
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            float rebirthArmorMod = effect0[level - 1];
            SetBuffToolTipVar(1, rebirthArmorMod);
        }
    }
}