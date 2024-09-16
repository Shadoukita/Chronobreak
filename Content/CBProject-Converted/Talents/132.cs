namespace Talents
{
    public class Talent_132 : TalentScript
    {
        float[] effect0 = { 0.02f, 0.03f, 0.04f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float regenPercent = effect0[level - 1];
            IncPercentPARRegenMod(owner, regenPercent, PrimaryAbilityResourceType.MANA);
            IncPercentHPRegenMod(owner, regenPercent);
        }
    }
}