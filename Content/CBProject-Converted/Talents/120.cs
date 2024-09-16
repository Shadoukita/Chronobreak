namespace Talents
{
    public class Talent_120 : TalentScript
    {
        public override void SetVarsByLevel()
        {
            float summonerCooldownBonus = 0.15f * talentLevel;
            avatarVars.SummonerCooldownBonus = summonerCooldownBonus;
        }
    }
}