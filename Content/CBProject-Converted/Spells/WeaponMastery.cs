namespace Buffs
{
    public class WeaponMastery : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WeaponMastery",
            BuffTextureName = "Armsmaster_MasterOfArms.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float damageAdded;
        float weaponDamage;
        public override void OnActivate()
        {
            //RequireVar(this.damageAdded);
            weaponDamage = GetFlatPhysicalDamageMod(owner);
        }
        public override void OnUpdateStats()
        {
            damageAdded = weaponDamage * 0.131f;
            IncFlatPhysicalDamageMod(owner, damageAdded);
        }
        public override void OnUpdateActions()
        {
            weaponDamage = GetFlatPhysicalDamageMod(owner);
        }
    }
}