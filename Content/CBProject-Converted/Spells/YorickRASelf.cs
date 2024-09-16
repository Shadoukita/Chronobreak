namespace Buffs
{
    public class YorickRASelf : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserCOTGSelf",
            BuffTextureName = "Mordekaiser_COTG.dds",
            NonDispellable = true,
        };
        float petDamage;
        float petAP;
        public override void OnActivate()
        {
            //RequireVar(this.petDamage);
            //RequireVar(this.petAP);
            IncPermanentFlatPhysicalDamageMod(attacker, petDamage);
            IncPermanentFlatMagicDamageMod(attacker, petAP);
            SetBuffToolTipVar(1, petDamage);
            SetBuffToolTipVar(2, petAP);
        }
        public override void OnDeactivate(bool expired)
        {
            petDamage *= -1;
            petAP *= -1;
            IncPermanentFlatMagicDamageMod(owner, petAP);
            IncPermanentFlatPhysicalDamageMod(owner, petDamage);
        }
    }
}