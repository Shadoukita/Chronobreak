namespace Talents
{
    public class Talent_113 : TalentScript
    {
        float damageBlock;
        float[] effect0 = { 1, 1.5f, 2 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            damageBlock = effect0[level - 1];
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
            {
                damageAmount -= damageBlock;
            }
            if (damageAmount < 0)
            {
                damageAmount = 0;
            }
        }
    }
}