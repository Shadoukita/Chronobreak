namespace Buffs
{
    public class Sleep : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Sleep2_Glb.troy", },
            BuffName = "Sleep",
            BuffTextureName = "Teemo_TranquilizingShot.dds",
        };
        public override void OnActivate()
        {
            SetSleep(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSleep(owner, false);
        }
        public override void OnUpdateActions()
        {
            SetSleep(owner, true);
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}