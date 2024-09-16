namespace Buffs
{
    public class Ardor : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float percentMod;
        float aP;
        float aS;
        public Ardor(float percentMod = default)
        {
            this.percentMod = percentMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.percentMod);
            aP = 0;
            aS = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, aP);
            IncPercentAttackSpeedMod(owner, aS);
        }
        public override void OnUpdateActions()
        {
            float abilityPowerStart = GetFlatMagicDamageMod(owner);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ZhonyasRing)) > 0)
            {
                abilityPowerStart /= 1.3f;
            }
            abilityPowerStart -= aP;
            aP = abilityPowerStart * percentMod;
            float attackSpeedStart = GetPercentAttackSpeedMod(owner);
            attackSpeedStart -= aS;
            aS = attackSpeedStart * percentMod;
        }
    }
}