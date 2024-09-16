namespace Buffs
{
    public class MasteryBlastBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float percentMod;
        float aP;
        public MasteryBlastBuff(float percentMod = default)
        {
            this.percentMod = percentMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.percentMod);
            aP = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, aP);
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
        }
    }
}