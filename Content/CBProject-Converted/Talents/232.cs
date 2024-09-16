namespace Talents
{
    public class Talent_232 : TalentScript
    {
        public override void OnUpdateStats()
        {
            IncPercentArmorPenetrationMod(owner, 0.1f);
        }
    }
}