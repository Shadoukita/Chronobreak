namespace Talents
{
    public class Talent_109 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float armorMod = 2 * talentLevel;
            IncFlatArmorMod(owner, armorMod);
        }
    }
}