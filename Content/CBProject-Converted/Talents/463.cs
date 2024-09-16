namespace Talents
{
    public class Talent_463 : TalentScript
    {
        public override void SetVarsByLevel()
        {
            float summonerCooldownBonus = 0.15f * talentLevel;
            avatarVars.SummonerCooldownBonus = summonerCooldownBonus;
        }
    }
}