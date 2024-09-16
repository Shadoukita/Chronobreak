namespace Talents
{
    public class Talent_100 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float attackDamageBonus = 1 * talentLevel;
            IncFlatPhysicalDamageMod(owner, attackDamageBonus);
        }
    }
}