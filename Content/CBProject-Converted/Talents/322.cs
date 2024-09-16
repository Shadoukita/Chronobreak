namespace Talents
{
    public class Talent_322 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float healthPerLevel = talentLevel * 1.5f;
            int champLevel = GetLevel(owner);
            float healthMod = champLevel * healthPerLevel;
            IncMaxHealth(owner, healthMod, false);
        }
    }
}