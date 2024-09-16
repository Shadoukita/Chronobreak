namespace Talents
{
    public class Talent_114 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float dodgeMod = 0.005f * talentLevel;
            IncFlatDodgeMod(owner, dodgeMod);
        }
    }
}