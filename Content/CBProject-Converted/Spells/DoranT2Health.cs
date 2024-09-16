namespace Buffs
{
    public class DoranT2Health : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float healthVar;
        public DoranT2Health(float healthVar = default)
        {
            this.healthVar = healthVar;
        }
        public override void OnActivate()
        {
            //RequireVar(this.healthVar);
            IncPermanentFlatHPPoolMod(owner, healthVar);
        }
        public override void OnDeactivate(bool expired)
        {
            float removeHealth = healthVar * -1;
            IncPermanentFlatHPPoolMod(owner, removeHealth);
        }
    }
}