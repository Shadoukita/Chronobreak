namespace Buffs
{
    public class DeathCraze : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "DeathCraze",
            BuffTextureName = "DrMundo_Nethershade.dds",
        };
        float baseDamage;
        float damageAdded;
        public override void OnActivate()
        {
            //RequireVar(charVars.MundoPercent);
            //RequireVar(this.baseDamage);
            //RequireVar(this.damageAdded);
        }
        public override void OnUpdateStats()
        {
            damageAdded = baseDamage * charVars.MundoPercent;
            IncFlatPhysicalDamageMod(owner, damageAdded);
        }
        public override void OnUpdateActions()
        {
            baseDamage = GetBaseAttackDamage(owner);
        }
    }
}