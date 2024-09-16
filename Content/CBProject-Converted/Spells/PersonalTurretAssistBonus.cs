namespace Buffs
{
    public class PersonalTurretAssistBonus : BuffScript
    {
        //Change not documented on wiki that happened between 1.0.0.131 and 4.20: Turrets' AttackSpeed Reduced (Turrets no longer gain extra AttackSpeed from any source)

        public override void OnActivate()
        {
            //IncPermanentPercentAttackSpeedMod(attacker, 0.3f);
            IncPermanentFlatArmorMod(attacker, 25);
            IncPermanentFlatSpellBlockMod(attacker, 25);
        }
        public override void OnDeactivate(bool expired)
        {
            //IncPermanentPercentAttackSpeedMod(attacker, -0.2f);
            IncPermanentFlatArmorMod(attacker, -15);
            IncPermanentFlatSpellBlockMod(attacker, -15);
        }
    }
}