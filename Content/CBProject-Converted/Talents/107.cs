namespace Talents
{
    public class Talent_107 : TalentScript
    {
        public override void OnUpdateStats()
        {
            IncPercentMagicPenetrationMod(owner, 0.15f);
        }
    }
}