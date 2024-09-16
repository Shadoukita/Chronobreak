namespace Talents
{
    public class Talent_243 : TalentScript
    {
        float[] effect0 = { 0.25f, 0.5f, 0.75f, 1 };
        public override void OnUpdateStats()
        {
            int ownerLevel = GetLevel(owner);
            int level = talentLevel;
            float bonusDamage = effect0[level - 1];
            float totalBonusDamage = bonusDamage * ownerLevel;
            IncFlatMagicDamageMod(owner, totalBonusDamage);
        }
    }
}