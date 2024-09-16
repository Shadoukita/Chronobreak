namespace Buffs
{
    public class TurretBackdoorBonus : BuffScript
    {
        public override void OnActivate()
        {
            IncPermanentFlatArmorMod(owner, -150);
            IncPermanentFlatSpellBlockMod(owner, -150);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentFlatArmorMod(owner, 150);
            IncPermanentFlatSpellBlockMod(owner, 150);
        }
    }
}