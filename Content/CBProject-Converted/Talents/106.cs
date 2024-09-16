namespace Talents
{
    public class Talent_106 : TalentScript
    {
        float[] effect0 = { 0.2f, 0.4f, 0.6f };
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