namespace Talents
{
    public class Talent_314 : TalentScript
    {
        float smallDamageAmount;
        int[] effect0 = { 1, 2, 3 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            smallDamageAmount = effect0[level - 1];
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (attacker is ObjAIBase && attacker is not BaseTurret && attacker is not Champion)
            {
                damageAmount -= smallDamageAmount;
            }
            if (damageAmount < 0)
            {
                damageAmount = 0;
            }
        }
    }
}