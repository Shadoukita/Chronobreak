namespace Talents
{
    public class Talent_213 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float abilityPowerBonus = 1 * talentLevel;
            IncFlatMagicDamageMod(owner, abilityPowerBonus);
        }
    }
}