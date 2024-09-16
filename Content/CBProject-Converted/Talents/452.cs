namespace Talents
{
    public class Talent_452 : TalentScript
    {
        float[] effect0 = { 0.004f, 0.007f, 0.01f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float regenPercent = effect0[level - 1];
            float maxMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            float regen = regenPercent * maxMana;
            regen /= 5;
            IncFlatHPRegenMod(owner, regen);
        }
    }
}