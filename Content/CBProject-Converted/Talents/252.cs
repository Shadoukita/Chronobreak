namespace Talents
{
    public class Talent_252 : TalentScript
    {
        int[] effect0 = { 2, 4, 6 };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            IncFlatArmorPenetrationMod(owner, effect0[level - 1]);
        }
    }
}