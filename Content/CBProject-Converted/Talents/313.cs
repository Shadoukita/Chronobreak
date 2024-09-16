namespace Talents
{
    public class Talent_313 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float armorMod = 2 * talentLevel;
            IncFlatArmorMod(owner, armorMod);
        }
    }
}