namespace Buffs
{
    public class EquipmentMastery : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EquipmentMastery",
            BuffTextureName = "Armsmaster_MasterOfArms.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float aPHealthAdded;
        float attackHealthAdded;
        float attackTotal;
        float aPTotal;
        public override void OnActivate()
        {
            //RequireVar(this.aPHealthAdded);
            //RequireVar(this.attackHealthAdded);
            attackTotal = GetFlatPhysicalDamageMod(owner);
            aPTotal = GetFlatMagicDamageMod(owner);
        }
        public override void OnUpdateStats()
        {
            aPHealthAdded = aPTotal * 2;
            attackHealthAdded = attackTotal * 3;
            IncMaxHealth(owner, aPHealthAdded, false);
            IncMaxHealth(owner, attackHealthAdded, false);
            SetBuffToolTipVar(1, attackHealthAdded);
            SetBuffToolTipVar(2, aPHealthAdded);
        }
        public override void OnUpdateActions()
        {
            aPTotal = GetFlatMagicDamageMod(owner);
            attackTotal = GetFlatPhysicalDamageMod(owner);
        }
    }
}