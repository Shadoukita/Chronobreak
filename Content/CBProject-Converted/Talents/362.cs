namespace Talents
{
    public class Talent_362 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float hP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float plusHealth = hP * 0.03f;
            IncMaxHealth(owner, plusHealth, false);
        }
        public override void OnUpdateActions()
        {
            int level = talentLevel;
            avatarVars.MasteryJuggernaut = true;
        }
    }
}